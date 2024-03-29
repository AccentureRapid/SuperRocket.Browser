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

// Generated file. Do not edit.


using System;

namespace Chromium.Remote {

    internal class CfxV8ContextGetCurrentContextRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetCurrentContextRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetCurrentContextRenderProcessCall) {}

        internal IntPtr __retval;

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_current_context();
        }
    }

    internal class CfxV8ContextGetEnteredContextRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetEnteredContextRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetEnteredContextRenderProcessCall) {}

        internal IntPtr __retval;

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_entered_context();
        }
    }

    internal class CfxV8ContextInContextRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextInContextRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextInContextRenderProcessCall) {}

        internal bool __retval;

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = 0 != CfxApi.V8Context.cfx_v8context_in_context();
        }
    }

    internal class CfxV8ContextGetTaskRunnerRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetTaskRunnerRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetTaskRunnerRenderProcessCall) {}

        internal IntPtr @this;
        internal IntPtr __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_task_runner(@this);
        }
    }

    internal class CfxV8ContextIsValidRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextIsValidRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextIsValidRenderProcessCall) {}

        internal IntPtr @this;
        internal bool __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = 0 != CfxApi.V8Context.cfx_v8context_is_valid(@this);
        }
    }

    internal class CfxV8ContextGetBrowserRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetBrowserRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetBrowserRenderProcessCall) {}

        internal IntPtr @this;
        internal IntPtr __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_browser(@this);
        }
    }

    internal class CfxV8ContextGetFrameRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetFrameRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetFrameRenderProcessCall) {}

        internal IntPtr @this;
        internal IntPtr __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_frame(@this);
        }
    }

    internal class CfxV8ContextGetGlobalRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextGetGlobalRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextGetGlobalRenderProcessCall) {}

        internal IntPtr @this;
        internal IntPtr __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = CfxApi.V8Context.cfx_v8context_get_global(@this);
        }
    }

    internal class CfxV8ContextEnterRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextEnterRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextEnterRenderProcessCall) {}

        internal IntPtr @this;
        internal bool __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = 0 != CfxApi.V8Context.cfx_v8context_enter(@this);
        }
    }

    internal class CfxV8ContextExitRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextExitRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextExitRenderProcessCall) {}

        internal IntPtr @this;
        internal bool __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = 0 != CfxApi.V8Context.cfx_v8context_exit(@this);
        }
    }

    internal class CfxV8ContextIsSameRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextIsSameRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextIsSameRenderProcessCall) {}

        internal IntPtr @this;
        internal IntPtr that;
        internal bool __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
            h.Write(that);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
            h.Read(out that);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            __retval = 0 != CfxApi.V8Context.cfx_v8context_is_same(@this, that);
        }
    }

    internal class CfxV8ContextEvalRenderProcessCall : RenderProcessCall {

        internal CfxV8ContextEvalRenderProcessCall()
            : base(RemoteCallId.CfxV8ContextEvalRenderProcessCall) {}

        internal IntPtr @this;
        internal string code;
        internal IntPtr retval;
        internal IntPtr exception;
        internal bool __retval;

        protected override void WriteArgs(StreamHandler h) {
            h.Write(@this);
            h.Write(code);
        }

        protected override void ReadArgs(StreamHandler h) {
            h.Read(out @this);
            h.Read(out code);
        }

        protected override void WriteReturn(StreamHandler h) {
            h.Write(retval);
            h.Write(exception);
            h.Write(__retval);
        }

        protected override void ReadReturn(StreamHandler h) {
            h.Read(out retval);
            h.Read(out exception);
            h.Read(out __retval);
        }

        protected override void ExecuteInTargetProcess(RemoteConnection connection) {
            var code_pinned = new PinnedString(code);
            __retval = 0 != CfxApi.V8Context.cfx_v8context_eval(@this, code_pinned.Obj.PinnedPtr, code_pinned.Length, out retval, out exception);
            code_pinned.Obj.Free();
        }
    }

}
