// Copyright (c) 2014-2015 Wolfgang Borgsmüller
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
// 1. Redistributions of source code must retain the above copyright 
//    notice, this list of conditions and the following disclaimer.
// 
// 2. Redistributions in binary form must reproduce the above copyright 
//    notice, this list of conditions and the following disclaimer in the 
//    documentation and/or other materials provided with the distribution.
// 
// 3. Neither the name of the copyright holder nor the names of its 
//    contributors may be used to endorse or promote products derived 
//    from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS 
// OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND 
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR 
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.


using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public enum SignatureType {
    LibraryCall,
    ClientCallback
}

public class Signature {

    public static Signature Create(SignatureType type, ISignatureOwner owner, Parser.SignatureData sd, ApiTypeBuilder api) {
        var s = CustomSignatures.ForFunction(type, owner, sd, api);
        if(s == null) {
            return new Signature(type, owner, sd, api);
        } else {
            return s;
        }
    }

    protected class ArgList {
        private List<string> args = new List<string>();

        public void Add(string arg) {
            if(!string.IsNullOrWhiteSpace(arg)) {
                args.Add(arg);
                if(arg == "cef_trace_client* client")
                    System.Diagnostics.Debugger.Break();
            }
        }

        public string Join() {
            var retval = string.Join(", ", args);
            args.Clear();
            return retval;
        }
    }

    public SignatureType Type { get; private set; }

    public readonly ISignatureOwner Owner;
    public readonly Argument[] Arguments;
    public readonly ApiType ReturnType;

    public readonly bool ReturnValueIsConst;

    protected ArgList args = new ArgList();

    protected Signature(SignatureType type, ISignatureOwner owner, Parser.SignatureData sd, ApiTypeBuilder api) {
        Type = type;
        Owner = owner;
        var args = new List<Argument>();
        var index = 0;

        foreach(var arg in sd.Arguments) {
            args.Add(new Argument(arg, api, index));
            index += 1;
        }

        this.Arguments = args.ToArray();

        this.ReturnType = api.GetApiType(sd.ReturnType, false);
        this.ReturnValueIsConst = sd.ReturnValueIsConst;
        var comments = owner.Comments;

        DebugPrintUnhandledArrayArguments();
    }

    public virtual Argument[] RemoteArguments {
        get { return ManagedArguments; }
    }

    public virtual ApiType RemoteReturnType {
        get { return PublicReturnType; }
    }

    private Argument[] _managedArgs;

    public virtual Argument[] ManagedArguments {
        get {
            if(_managedArgs == null) {
                var list = new List<Argument>();
                foreach(var arg in Arguments) {
                    if(arg.ArgumentType.PInvokeSymbol != null) {
                        list.Add(arg);
                    }
                }
                _managedArgs = list.ToArray();
            }
            return _managedArgs;
        }
    }

    public virtual ApiType PublicReturnType {
        get { return ReturnType; }
    }

    public string NativeArgumentList {
        get {
            if(Type == SignatureType.ClientCallback) {
                args.Add(string.Format("(({0}*)self)->gc_handle", Arguments[0].ArgumentType.AsCefStructPtrType.Struct.CfxNativeSymbol));
                if(!ReturnType.IsVoid) {
                    args.Add("&__retval");
                }
                for(var i = 1; i <= Arguments.Length - 1; i++) {
                    args.Add(Arguments[i].NativeCallbackArgument);
                }
            } else {
                for(var i = 0; i <= Arguments.Length - 1; i++) {
                    args.Add(Arguments[i].NativeCallArgument);
                }
            }
            return args.Join();
        }
    }

    public string PublicEventConstructorArgumentList {
        get {
            for(var i = 1; i <= ManagedArguments.Count() - 1; i++) {
                if(ManagedArguments[i].ArgumentType.IsIn) {
                    args.Add(ManagedArguments[i].PublicEventConstructorParameter);
                }
            }
            return args.Join();
        }
    }

    public string PublicEventConstructorParameterList {
        get {
            for(var i = 1; i <= ManagedArguments.Length - 1; i++) {
                if(ManagedArguments[i].ArgumentType.IsIn) {
                    args.Add(ManagedArguments[i].PublicEventConstructorArgument);
                }
            }
            return args.Join();
        }
    }

    public string OriginalParameterList {
        get {
            foreach(var arg in Arguments) {
                args.Add(arg.OriginalParameter);
            }
            return args.Join();
        }
    }

    public string OriginalParameterListUnnamed {
        get {
            foreach(var arg in Arguments) {
                if(arg.IsConst) {
                    args.Add("const " + arg.ArgumentType.OriginalSymbol);
                } else {
                    args.Add(arg.ArgumentType.OriginalSymbol);
                }
            }
            return args.Join();
        }
    }

    public virtual string NativeParameterList {
        get {
            if(Type == SignatureType.ClientCallback) {
                args.Add("gc_handle_t self");
                if(!ReturnType.IsVoid) {
                    args.Add(ReturnType.NativeOutSignature("__retval"));
                }
                for(var i = 1; i <= Arguments.Length - 1; i++) {
                    args.Add(Arguments[i].NativeCallbackParameter);
                }
            } else {
                for(var i = 0; i <= Arguments.Length - 1; i++) {
                    args.Add(Arguments[i].NativeCallParameter);
                }
            }
            return args.Join();
        }
    }

    public virtual string NativeFunctionHeader(string functionName) {

        var retType = ReturnType.NativeSymbol;
        if(ReturnValueIsConst) {
            retType = "const " + retType;
        }

        return string.Format("static {0} {1}({2})", retType, functionName, NativeParameterList);
    }

    public virtual string PInvokeFunctionHeader(string functionName) {
        for(var i = 0; i <= Arguments.Length - 1; i++) {
            args.Add(Arguments[i].PInvokeCallParameter);
        }
        return string.Format("{0} {1}({2})", ReturnType.PInvokeSymbol, functionName, args.Join());
    }

    public string PInvokeParameterList {
        get {
            args.Add("IntPtr gcHandlePtr");
            if(!ReturnType.IsVoid) {
                args.Add(ReturnType.PInvokeOutSignature("__retval"));
            }
            for(var i = 1; i <= Arguments.Count() - 1; i++) {
                args.Add(Arguments[i].PInvokeCallbackParameter);
            }
            return args.Join();
        }
    }

    public string PublicParameterList {
        get {
            Debug.Assert(Type == SignatureType.LibraryCall);
            foreach(var arg in ManagedArguments) {
                if(arg.PublicCallParameter != null) {
                    args.Add(arg.PublicCallParameter);
                }
            }
            return args.Join();
        }
    }

    public string PublicArgumentList {
        get {
            Debug.Assert(Type == SignatureType.LibraryCall);
            foreach(var arg in ManagedArguments) {
                args.Add(arg.PublicCallArgument);
            }
            return args.Join();
        }
    }

    public string ProxyArgumentList {
        get {
            Debug.Assert(Type == SignatureType.LibraryCall);
            foreach(var arg in ManagedArguments) {
                args.Add(arg.ProxyCallArgument);
            }
            return args.Join();
        }
    }

    public virtual string PublicFunctionHeader(string functionName) {
        return string.Format("{0} {1}({2})", PublicReturnType.PublicSymbol, functionName, PublicParameterList);
    }

    public string RemoteParameterList {
        get {
            foreach(var arg in RemoteArguments) {
                if(arg.RemoteCallParameter != null) {
                    args.Add(arg.RemoteCallParameter);
                }
            }
            return args.Join();
        }
    }

    public virtual void EmitPublicCall(CodeBuilder b) {

        var apiCall = string.Format("CfxApi.{2}.{0}({1})", Owner.CfxApiFunctionName, PublicArgumentList, Owner.PublicClassName.Substring(3));

        for(var i = 0; i <= ManagedArguments.Length - 1; i++) {
            ManagedArguments[i].EmitPrePublicCallStatements(b);
        }

        var b1 = new CodeBuilder(b.CurrentIndent);
        for(var i = 0; i <= ManagedArguments.Length - 1; i++) {
            ManagedArguments[i].EmitPostPublicStatements(b1);
        }

        if(PublicReturnType.IsVoid) {
            b.AppendLine(apiCall + ";");
            b.AppendBuilder(b1);
        } else {
            if(b1.IsNotEmpty) {
                b.AppendLine("var __retval = {0};", apiCall);
                b.AppendBuilder(b1);
                b.AppendLine("return {0};", PublicReturnType.PublicReturnExpression("__retval"));
            } else {
                b.AppendLine("return {0};", PublicReturnType.PublicReturnExpression(apiCall));
            }
        }
    }

    protected virtual void EmitExecuteInTargetProcess(CodeBuilder b) {

        for(var i = 0; i <= ManagedArguments.Length - 1; i++) {
            ManagedArguments[i].EmitPreProxyCallStatements(b);
        }

        var apiCall = string.Format("CfxApi.{2}.{0}({1})", Owner.CfxApiFunctionName, ProxyArgumentList, Owner.PublicClassName.Substring(3));
        if(PublicReturnType.IsVoid) {
            b.AppendLine(apiCall + ";");
        } else {
            b.AppendLine("__retval = {0};", PublicReturnType.ProxyReturnExpression(apiCall));
        }

        for(var i = 0; i <= ManagedArguments.Length - 1; i++) {
            ManagedArguments[i].EmitPostProxyCallStatements(b);
        }
    }

    public void EmitPublicEventCtorStatements(CodeBuilder b) {
        for(var i = 1; i <= ManagedArguments.Count() - 1; i++) {
            if(ManagedArguments[i].ArgumentType.IsIn) {
                ManagedArguments[i].EmitPublicEventCtorStatements(b);
            }
        }
    }

    public void EmitPostPublicEventHandlerCallStatements(CodeBuilder b) {

        for(var i = 1; i <= ManagedArguments.Count() - 1; i++) {
            ManagedArguments[i].EmitPostPublicRaiseEventStatements(b);
            if(ManagedArguments[i].TypeIsRefCounted) {
                b.AppendLine("if(e.m_{0}_wrapped == null) CfxApi.cfx_release(e.m_{0});", ManagedArguments[i].VarName);
            }
        }
        EmitPostPublicEventHandlerReturnValueStatements(b);
    }

    protected virtual void EmitPostPublicEventHandlerReturnValueStatements(CodeBuilder b) {
        if(!PublicReturnType.IsVoid) {
            b.AppendLine("__retval = {0};", PublicReturnType.PublicUnwrapExpression("e.m_returnValue"));
        }
    }

    public virtual void EmitRemoteCall(CodeBuilder b) {
        b.AppendLine("var call = new {0}();", Owner.RemoteCallId);

        foreach(var arg in ManagedArguments) {
            if(arg.ArgumentType.IsIn) {
                arg.EmitPreRemoteCallStatements(b);
            }
        }

        if(Owner is CefExportFunction)
            b.AppendLine("call.RequestExecution(CfxRemoteCallContext.CurrentContext.connection);");
        else
            b.AppendLine("call.RequestExecution(this);");

        foreach(var arg in ManagedArguments) {
            if(arg.ArgumentType.IsOut) {
                arg.EmitPostRemoteCallStatements(b);
            } else if(arg.ArgumentType.IsStringCollectionType && Owner.CefName.Contains("::get_")) {
                arg.EmitPostRemoteCallStatements(b);
            }
        }

        if(!PublicReturnType.IsVoid) {
            b.AppendLine("return {0};", ReturnType.RemoteWrapExpression("call.__retval"));
        }
    }

    public void EmitRemoteCallClassBody(CodeBuilder b) {
        b.AppendLine();

        foreach(var arg in ManagedArguments) {
            arg.EmitRemoteCallFields(b);
        }

        if(!PublicReturnType.IsVoid) {
            PublicReturnType.EmitRemoteCallFields(b, "__retval");
        }

        b.AppendLine();
        if(ManagedArguments.Length > 0) {
            b.BeginBlock("protected override void WriteArgs(StreamHandler h)");
            foreach(var arg in ManagedArguments) {
                if(arg.ArgumentType.IsIn) {
                    arg.EmitRemoteWrite(b);
                }
            }
            b.EndBlock();
            b.AppendLine();

            b.BeginBlock("protected override void ReadArgs(StreamHandler h)");
            foreach(var arg in ManagedArguments) {
                if(arg.ArgumentType.IsIn) {
                    arg.EmitRemoteRead(b);
                }
            }
            b.EndBlock();
            b.AppendLine();
        }

        var outArgs = new List<Argument>();
        foreach(var arg in ManagedArguments) {
            if(arg.ArgumentType.IsOut) {
                outArgs.Add(arg);
            } else if(arg.ArgumentType.IsStringCollectionType && Owner.CefName.Contains("::get_")) {
                outArgs.Add(arg);
            }
        }

        if(outArgs.Count > 0 || !PublicReturnType.IsVoid) {

            b.BeginBlock("protected override void WriteReturn(StreamHandler h)");
            foreach(var arg in outArgs) {
                arg.EmitRemoteWrite(b);
            }
            if(!PublicReturnType.IsVoid) {
                b.AppendLine("h.Write(__retval);", PublicReturnType.PublicSymbol);
            }
            b.EndBlock();
            b.AppendLine();

            b.BeginBlock("protected override void ReadReturn(StreamHandler h)");
            foreach(var arg in outArgs) {
                arg.EmitRemoteRead(b);
            }
            if(!PublicReturnType.IsVoid) {
                b.AppendLine("h.Read(out __retval);", PublicReturnType.PublicSymbol);
            }
            b.EndBlock();
            b.AppendLine();
        }

        b.BeginBlock("protected override void ExecuteInTargetProcess(RemoteConnection connection)");
        EmitExecuteInTargetProcess(b);
        b.EndBlock();
    }

    public virtual void EmitNativeCall(CodeBuilder b, string functionName) {
        var b1 = new CodeBuilder(b.CurrentIndent);
        for(var i = 0; i <= Arguments.Length - 1; i++) {
            Arguments[i].EmitPreNativeCallStatements(b);
            Arguments[i].EmitPostNativeCallStatements(b1);
        }

        var functionCall = string.Format("{0}({1})", functionName, NativeArgumentList);
        ReturnType.EmitNativeReturnStatements(b, functionCall, b1);
    }

    public virtual void DebugPrintUnhandledArrayArguments() {
        if(Owner.CefName == "cef_binary_value_create")
            return;
        if(Owner.CefName == "cef_binary_value::get_data")
            return;
        if(Owner.CefName == "cef_resource_handler::get_response_headers")
            return;
        if(Owner.CefName == "cef_resource_bundle_handler::get_data_resource")
            return;
        if(Owner.CefName == "cef_resource_bundle_handler::get_data_resource_for_scale")
            return;
        if(Owner.CefName == "cef_urlrequest_client::on_download_data")
            return;
        if(Owner.CefName == "cef_zip_reader::read_file")
            return;
        if(Owner.CefName == "cef_resource_bundle::get_data_resource")
            return;
        if(Owner.CefName == "cef_resource_bundle::get_data_resource_for_scale")
            return;
        if(Owner.CefName == "cef_response_filter::filter")
            return;
        if(Owner.CefName.StartsWith("cef_image::add_"))
            return;


        for(var i = 0; i <= Arguments.Length - 1; i++) {
            var suffixLength = CountArgumentSuffixLength(Arguments[i]);
            if(suffixLength > 0) {
                var arrName = Arguments[i].VarName.Substring(0, Arguments[i].VarName.Length - suffixLength);
                if(i > 0 && Arguments[i - 1].VarName.StartsWith(arrName)) {
                    Debug.Print("UnhandledArrayArgument {0} {1} {2} {3}", Owner.CallMode, Owner.CefName, Arguments[i - 1], Arguments[i]);
                } else if(i < Arguments.Length - 1 && Arguments[i + 1].VarName.StartsWith(arrName)) {
                    Debug.Print("UnhandledArrayArgument {0} {1} {2} {3}", Owner.CallMode, Owner.CefName, Arguments[i], Arguments[i + 1]);
                } else {
                }
            }
        }
    }

    private int CountArgumentSuffixLength(Argument arg) {
        if(arg.VarName.EndsWith("_size"))
            return 5;
        if(arg.VarName.EndsWith("_count"))
            return 6;
        if(arg.VarName.EndsWith("_length"))
            return 7;
        if(arg.VarName.EndsWith("Size"))
            return 4;
        if(arg.VarName.EndsWith("Count"))
            return 5;
        if(arg.VarName.EndsWith("Length"))
            return 6;
        return 0;
    }

    public override string ToString() {
        for(var i = 0; i <= ManagedArguments.Length - 1; i++) {
            args.Add(ManagedArguments[i].ToString());
        }
        return args.Join();
    }
}