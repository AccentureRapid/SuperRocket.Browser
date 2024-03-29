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

public class CefV8HandlerExecuteSignature : SignatureWithStructPtrArray {
    private Argument[] m_publicArguments;

    public CefV8HandlerExecuteSignature(ISignatureOwner owner, Parser.SignatureData sd, ApiTypeBuilder api)
        : base(SignatureType.ClientCallback, owner, sd, api, 4, 3) {
        Arguments[6] = new Argument(Arguments[6], new CefStringOutType());
        base.ManagedArguments[5] = new Argument(base.ManagedArguments[5], new CefStringOutType());
        var list = new List<Argument>();
        foreach(var arg in base.ManagedArguments) {
            if(arg.VarName != "retval") {
                list.Add(arg);
            }
        }
        m_publicArguments = list.ToArray();
    }

    public override ApiType PublicReturnType {
        get { return new CefStructPtrType(new CefStructType("cef_v8value", null), "*"); }
    }

    public override Argument[] ManagedArguments {
        get { return m_publicArguments; }
    }

    protected override void EmitPostPublicEventHandlerReturnValueStatements(CodeBuilder b) {
        b.AppendLine("retval = CfxV8Value.Unwrap(e.m_returnValue);");
        b.AppendLine("__retval = e.m_returnValue != null || e.m_exception_wrapped != null ? 1 : 0;");
    }

}