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
    /// Structure used to represent a frame in the browser window. When used in the
    /// browser process the functions of this structure may be called on any thread
    /// unless otherwise indicated in the comments. When used in the render process
    /// the functions of this structure may only be called on the main thread.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
    /// </remarks>
    public class CfrFrame : CfrBase {

        internal static CfrFrame Wrap(IntPtr proxyId) {
            if(proxyId == IntPtr.Zero) return null;
            var weakCache = CfxRemoteCallContext.CurrentContext.connection.weakCache;
            lock(weakCache) {
                var cfrObj = (CfrFrame)weakCache.Get(proxyId);
                if(cfrObj == null) {
                    cfrObj = new CfrFrame(proxyId);
                    weakCache.Add(proxyId, cfrObj);
                }
                return cfrObj;
            }
        }



        private CfrFrame(IntPtr proxyId) : base(proxyId) {}

        /// <summary>
        /// True if this object is currently attached to a valid frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public bool IsValid {
            get {
                var call = new CfxFrameIsValidRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns true (1) if this is the main (top-level) frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public bool IsMain {
            get {
                var call = new CfxFrameIsMainRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns true (1) if this is the focused frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public bool IsFocused {
            get {
                var call = new CfxFrameIsFocusedRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the name for this frame. If the frame has an assigned name (for
        /// example, set via the iframe "name" attribute) then that value will be
        /// returned. Otherwise a unique name will be constructed based on the frame
        /// parent hierarchy. The main (top-level) frame will always have an NULL name
        /// value.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public string Name {
            get {
                var call = new CfxFrameGetNameRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the globally unique identifier for this frame or &lt; 0 if the
        /// underlying frame does not yet exist.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public long Identifier {
            get {
                var call = new CfxFrameGetIdentifierRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the parent of this frame or NULL if this is the main (top-level)
        /// frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public CfrFrame Parent {
            get {
                var call = new CfxFrameGetParentRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return CfrFrame.Wrap(call.__retval);
            }
        }

        /// <summary>
        /// Returns the URL currently loaded in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public string Url {
            get {
                var call = new CfxFrameGetUrlRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return call.__retval;
            }
        }

        /// <summary>
        /// Returns the browser that this frame belongs to.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public CfrBrowser Browser {
            get {
                var call = new CfxFrameGetBrowserRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return CfrBrowser.Wrap(call.__retval);
            }
        }

        /// <summary>
        /// Get the V8 context associated with the frame. This function can only be
        /// called from the render process.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public CfrV8Context V8Context {
            get {
                var call = new CfxFrameGetV8ContextRenderProcessCall();
                call.@this = proxyId;
                call.RequestExecution(this);
                return CfrV8Context.Wrap(call.__retval);
            }
        }

        /// <summary>
        /// Execute undo in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Undo() {
            var call = new CfxFrameUndoRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute redo in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Redo() {
            var call = new CfxFrameRedoRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute cut in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Cut() {
            var call = new CfxFrameCutRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute copy in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Copy() {
            var call = new CfxFrameCopyRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute paste in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Paste() {
            var call = new CfxFramePasteRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute delete in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void Delete() {
            var call = new CfxFrameDelRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute select all in this frame.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void SelectAll() {
            var call = new CfxFrameSelectAllRenderProcessCall();
            call.@this = proxyId;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Retrieve this frame's HTML source as a string sent to the specified
        /// visitor.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void GetSource(CfrStringVisitor visitor) {
            var call = new CfxFrameGetSourceRenderProcessCall();
            call.@this = proxyId;
            call.visitor = CfrObject.Unwrap(visitor);
            call.RequestExecution(this);
        }

        /// <summary>
        /// Retrieve this frame's display text as a string sent to the specified
        /// visitor.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void GetText(CfrStringVisitor visitor) {
            var call = new CfxFrameGetTextRenderProcessCall();
            call.@this = proxyId;
            call.visitor = CfrObject.Unwrap(visitor);
            call.RequestExecution(this);
        }

        /// <summary>
        /// Load the request represented by the |request| object.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void LoadRequest(CfrRequest request) {
            var call = new CfxFrameLoadRequestRenderProcessCall();
            call.@this = proxyId;
            call.request = CfrObject.Unwrap(request);
            call.RequestExecution(this);
        }

        /// <summary>
        /// Load the specified |url|.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void LoadUrl(string url) {
            var call = new CfxFrameLoadUrlRenderProcessCall();
            call.@this = proxyId;
            call.url = url;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Load the contents of |stringVal| with the specified dummy |url|. |url|
        /// should have a standard scheme (for example, http scheme) or behaviors like
        /// link clicks and web security restrictions may not behave as expected.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void LoadString(string stringVal, string url) {
            var call = new CfxFrameLoadStringRenderProcessCall();
            call.@this = proxyId;
            call.stringVal = stringVal;
            call.url = url;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Execute a string of JavaScript code in this frame. The |scriptUrl|
        /// parameter is the URL where the script in question can be found, if any. The
        /// renderer may request this URL to show the developer the source of the
        /// error.  The |startLine| parameter is the base line number to use for error
        /// reporting.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void ExecuteJavaScript(string code, string scriptUrl, int startLine) {
            var call = new CfxFrameExecuteJavaScriptRenderProcessCall();
            call.@this = proxyId;
            call.code = code;
            call.scriptUrl = scriptUrl;
            call.startLine = startLine;
            call.RequestExecution(this);
        }

        /// <summary>
        /// Visit the DOM document. This function can only be called from the render
        /// process.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_frame_capi.h">cef/include/capi/cef_frame_capi.h</see>.
        /// </remarks>
        public void VisitDom(CfrDomVisitor visitor) {
            var call = new CfxFrameVisitDomRenderProcessCall();
            call.@this = proxyId;
            call.visitor = CfrObject.Unwrap(visitor);
            call.RequestExecution(this);
        }

        internal override void OnDispose(IntPtr proxyId) {
            connection.weakCache.Remove(proxyId);
        }
    }
}
