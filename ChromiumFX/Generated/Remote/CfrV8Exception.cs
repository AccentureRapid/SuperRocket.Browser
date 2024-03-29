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

    /// <summary>
    /// Structure representing a V8 exception. The functions of this structure may be
    /// called on any render process thread.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
    /// </remarks>
    public class CfrV8Exception : CfrBase {

        internal static CfrV8Exception Wrap(IntPtr proxyId) {
            if(proxyId == IntPtr.Zero) return null;
            var weakCache = CfxRemoteCallContext.CurrentContext.connection.weakCache;
            lock(weakCache) {
                var cfrObj = (CfrV8Exception)weakCache.Get(proxyId);
                if(cfrObj == null) {
                    cfrObj = new CfrV8Exception(proxyId);
                    weakCache.Add(proxyId, cfrObj);
                }
                return cfrObj;
            }
        }



        private CfrV8Exception(IntPtr proxyId) : base(proxyId) {}

        /// <summary>
        /// Returns the exception message.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public string Message {
            get {
                var call = new CfxV8ExceptionGetMessageRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the line of source code that the exception occurred within.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public string SourceLine {
            get {
                var call = new CfxV8ExceptionGetSourceLineRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the resource name for the script from where the function causing
        /// the error originates.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public string ScriptResourceName {
            get {
                var call = new CfxV8ExceptionGetScriptResourceNameRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the 1-based number of the line where the error occurred or 0 if the
        /// line number is unknown.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int LineNumber {
            get {
                var call = new CfxV8ExceptionGetLineNumberRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the index within the script of the first character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int StartPosition {
            get {
                var call = new CfxV8ExceptionGetStartPositionRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the index within the script of the last character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int EndPosition {
            get {
                var call = new CfxV8ExceptionGetEndPositionRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the index within the line of the first character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int StartColumn {
            get {
                var call = new CfxV8ExceptionGetStartColumnRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the index within the line of the last character where the error
        /// occurred.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_v8_capi.h">cef/include/capi/cef_v8_capi.h</see>.
        /// </remarks>
        public int EndColumn {
            get {
                var call = new CfxV8ExceptionGetEndColumnRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        internal override void OnDispose(IntPtr proxyId) {
            connection.weakCache.Remove(proxyId);
        }
    }
}
