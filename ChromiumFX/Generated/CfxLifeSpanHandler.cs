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

namespace Chromium {
    using Event;

    /// <summary>
    /// Implement this structure to handle events related to browser life span. The
    /// functions of this structure will be called on the UI thread unless otherwise
    /// indicated.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
    /// </remarks>
    public class CfxLifeSpanHandler : CfxClientBase {

        internal static CfxLifeSpanHandler Wrap(IntPtr nativePtr) {
            if(nativePtr == IntPtr.Zero) return null;
            var handlePtr = CfxApi.LifeSpanHandler.cfx_life_span_handler_get_gc_handle(nativePtr);
            return (CfxLifeSpanHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(handlePtr).Target;
        }


        private static object eventLock = new object();

        // on_before_popup
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall, SetLastError = false)]
        private delegate void cfx_life_span_handler_on_before_popup_delegate(IntPtr gcHandlePtr, out int __retval, IntPtr browser, IntPtr frame, IntPtr target_url_str, int target_url_length, IntPtr target_frame_name_str, int target_frame_name_length, int target_disposition, int user_gesture, IntPtr popupFeatures, IntPtr windowInfo, out IntPtr client, IntPtr settings, out int no_javascript_access);
        private static cfx_life_span_handler_on_before_popup_delegate cfx_life_span_handler_on_before_popup;
        private static IntPtr cfx_life_span_handler_on_before_popup_ptr;

        internal static void on_before_popup(IntPtr gcHandlePtr, out int __retval, IntPtr browser, IntPtr frame, IntPtr target_url_str, int target_url_length, IntPtr target_frame_name_str, int target_frame_name_length, int target_disposition, int user_gesture, IntPtr popupFeatures, IntPtr windowInfo, out IntPtr client, IntPtr settings, out int no_javascript_access) {
            var self = (CfxLifeSpanHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null || self.CallbacksDisabled) {
                __retval = default(int);
                client = default(IntPtr);
                no_javascript_access = default(int);
                return;
            }
            var e = new CfxOnBeforePopupEventArgs(browser, frame, target_url_str, target_url_length, target_frame_name_str, target_frame_name_length, target_disposition, user_gesture, popupFeatures, windowInfo, settings);
            var eventHandler = self.m_OnBeforePopup;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
            if(e.m_frame_wrapped == null) CfxApi.cfx_release(e.m_frame);
            client = CfxClient.Unwrap(e.m_client_wrapped);
            no_javascript_access = e.m_no_javascript_access;
            __retval = e.m_returnValue ? 1 : 0;
        }

        // on_after_created
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall, SetLastError = false)]
        private delegate void cfx_life_span_handler_on_after_created_delegate(IntPtr gcHandlePtr, IntPtr browser);
        private static cfx_life_span_handler_on_after_created_delegate cfx_life_span_handler_on_after_created;
        private static IntPtr cfx_life_span_handler_on_after_created_ptr;

        internal static void on_after_created(IntPtr gcHandlePtr, IntPtr browser) {
            var self = (CfxLifeSpanHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null || self.CallbacksDisabled) {
                return;
            }
            var e = new CfxOnAfterCreatedEventArgs(browser);
            var eventHandler = self.m_OnAfterCreated;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
        }

        // do_close
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall, SetLastError = false)]
        private delegate void cfx_life_span_handler_do_close_delegate(IntPtr gcHandlePtr, out int __retval, IntPtr browser);
        private static cfx_life_span_handler_do_close_delegate cfx_life_span_handler_do_close;
        private static IntPtr cfx_life_span_handler_do_close_ptr;

        internal static void do_close(IntPtr gcHandlePtr, out int __retval, IntPtr browser) {
            var self = (CfxLifeSpanHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null || self.CallbacksDisabled) {
                __retval = default(int);
                return;
            }
            var e = new CfxDoCloseEventArgs(browser);
            var eventHandler = self.m_DoClose;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
            __retval = e.m_returnValue ? 1 : 0;
        }

        // on_before_close
        [System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall, SetLastError = false)]
        private delegate void cfx_life_span_handler_on_before_close_delegate(IntPtr gcHandlePtr, IntPtr browser);
        private static cfx_life_span_handler_on_before_close_delegate cfx_life_span_handler_on_before_close;
        private static IntPtr cfx_life_span_handler_on_before_close_ptr;

        internal static void on_before_close(IntPtr gcHandlePtr, IntPtr browser) {
            var self = (CfxLifeSpanHandler)System.Runtime.InteropServices.GCHandle.FromIntPtr(gcHandlePtr).Target;
            if(self == null || self.CallbacksDisabled) {
                return;
            }
            var e = new CfxOnBeforeCloseEventArgs(browser);
            var eventHandler = self.m_OnBeforeClose;
            if(eventHandler != null) eventHandler(self, e);
            e.m_isInvalid = true;
            if(e.m_browser_wrapped == null) CfxApi.cfx_release(e.m_browser);
        }

        internal CfxLifeSpanHandler(IntPtr nativePtr) : base(nativePtr) {}
        public CfxLifeSpanHandler() : base(CfxApi.LifeSpanHandler.cfx_life_span_handler_ctor) {}

        /// <summary>
        /// Called on the IO thread before a new popup browser is created. The
        /// |Browser| and |Frame| values represent the source of the popup request. The
        /// |TargetUrl| and |TargetFrameName| values indicate where the popup
        /// browser should navigate and may be NULL if not specified with the request.
        /// The |TargetDisposition| value indicates where the user intended to open
        /// the popup (e.g. current tab, new tab, etc). The |UserGesture| value will
        /// be true (1) if the popup was opened via explicit user gesture (e.g.
        /// clicking a link) or false (0) if the popup opened automatically (e.g. via
        /// the DomContentLoaded event). The |PopupFeatures| structure contains
        /// additional information about the requested popup window. To allow creation
        /// of the popup browser optionally modify |WindowInfo|, |Client|, |Settings|
        /// and |NoJavascriptAccess| and return false (0). To cancel creation of the
        /// popup browser return true (1). The |Client| and |Settings| values will
        /// default to the source browser's values. If the |NoJavascriptAccess| value
        /// is set to false (0) the new browser will not be scriptable and may not be
        /// hosted in the same renderer process as the source browser. Any
        /// modifications to |WindowInfo| will be ignored if the parent browser is
        /// wrapped in a CfxBrowserView.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public event CfxOnBeforePopupEventHandler OnBeforePopup {
            add {
                lock(eventLock) {
                    if(m_OnBeforePopup == null) {
                        if(cfx_life_span_handler_on_before_popup == null) {
                            cfx_life_span_handler_on_before_popup = on_before_popup;
                            cfx_life_span_handler_on_before_popup_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(cfx_life_span_handler_on_before_popup);
                        }
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 0, cfx_life_span_handler_on_before_popup_ptr);
                    }
                    m_OnBeforePopup += value;
                }
            }
            remove {
                lock(eventLock) {
                    m_OnBeforePopup -= value;
                    if(m_OnBeforePopup == null) {
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 0, IntPtr.Zero);
                    }
                }
            }
        }

        private CfxOnBeforePopupEventHandler m_OnBeforePopup;

        /// <summary>
        /// Called after a new browser is created. This callback will be the first
        /// notification that references |Browser|.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public event CfxOnAfterCreatedEventHandler OnAfterCreated {
            add {
                lock(eventLock) {
                    if(m_OnAfterCreated == null) {
                        if(cfx_life_span_handler_on_after_created == null) {
                            cfx_life_span_handler_on_after_created = on_after_created;
                            cfx_life_span_handler_on_after_created_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(cfx_life_span_handler_on_after_created);
                        }
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 1, cfx_life_span_handler_on_after_created_ptr);
                    }
                    m_OnAfterCreated += value;
                }
            }
            remove {
                lock(eventLock) {
                    m_OnAfterCreated -= value;
                    if(m_OnAfterCreated == null) {
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 1, IntPtr.Zero);
                    }
                }
            }
        }

        private CfxOnAfterCreatedEventHandler m_OnAfterCreated;

        /// <summary>
        /// Called when a browser has recieved a request to close. This may result
        /// directly from a call to CfxBrowserHost::*close_browser() or indirectly
        /// if the browser is parented to a top-level window created by CEF and the
        /// user attempts to close that window (by clicking the 'X', for example). The
        /// do_close() function will be called after the JavaScript 'onunload' event
        /// has been fired.
        /// An application should handle top-level owner window close notifications by
        /// calling CfxBrowserHost.TrycloseBrowser() or
        /// CfxBrowserHost.CloseBrowser(false (0)) instead of allowing the window
        /// to close immediately (see the examples below). This gives CEF an
        /// opportunity to process the 'onbeforeunload' event and optionally cancel the
        /// close before do_close() is called.
        /// When windowed rendering is enabled CEF will internally create a window or
        /// view to host the browser. In that case returning false (0) from do_close()
        /// will send the standard close notification to the browser's top-level owner
        /// window (e.g. WM_CLOSE on Windows, performClose: on OS X, "delete_event" on
        /// Linux or CfxWindowDelegate.CanClose() callback from Views). If the
        /// browser's host window/view has already been destroyed (via view hierarchy
        /// tear-down, for example) then do_close() will not be called for that browser
        /// since is no longer possible to cancel the close.
        /// When windowed rendering is disabled returning false (0) from do_close()
        /// will cause the browser object to be destroyed immediately.
        /// If the browser's top-level owner window requires a non-standard close
        /// notification then send that notification from do_close() and return true
        /// (1).
        /// The CfxLifeSpanHandler.OnBeforeClose() function will be called
        /// after do_close() (if do_close() is called) and immediately before the
        /// browser object is destroyed. The application should only exit after
        /// on_before_close() has been called for all existing browsers.
        /// The below examples describe what should happen during window close when the
        /// browser is parented to an application-provided top-level window.
        /// Example 1: Using CfxBrowserHost.TrycloseBrowser(). This is
        /// recommended for clients using standard close handling and windows created
        /// on the browser process UI thread. 1.  User clicks the window close button
        /// which sends a close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser() (which internally calls CloseBrowser(false)).
        /// TryCloseBrowser() returns false so the client cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// CEF sends a close notification to the application's top-level window
        /// (because DoClose() returned false by default).
        /// 7.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser(). TryCloseBrowser() returns true so the client
        /// allows the window close.
        /// 8.  Application's top-level window is destroyed. 9.  Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 10. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// Example 2: Using CfxBrowserHost.CloseBrowser(false (0)) and
        /// implementing the do_close() callback. This is recommended for clients using
        /// non-standard close handling or windows that were not created on the browser
        /// process UI thread. 1.  User clicks the window close button which sends a
        /// close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and:
        /// A. Calls CfxBrowserHost.CloseBrowser(false).
        /// B. Cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// Application's do_close() handler is called. Application will:
        /// A. Set a flag to indicate that the next close attempt will be allowed.
        /// B. Return false.
        /// 7.  CEF sends an close notification to the application's top-level window.
        /// 8.  Application's top-level window receives the close notification and
        /// allows the window to close based on the flag from #6B.
        /// 9.  Application's top-level window is destroyed. 10. Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 11. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public event CfxDoCloseEventHandler DoClose {
            add {
                lock(eventLock) {
                    if(m_DoClose == null) {
                        if(cfx_life_span_handler_do_close == null) {
                            cfx_life_span_handler_do_close = do_close;
                            cfx_life_span_handler_do_close_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(cfx_life_span_handler_do_close);
                        }
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 2, cfx_life_span_handler_do_close_ptr);
                    }
                    m_DoClose += value;
                }
            }
            remove {
                lock(eventLock) {
                    m_DoClose -= value;
                    if(m_DoClose == null) {
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 2, IntPtr.Zero);
                    }
                }
            }
        }

        private CfxDoCloseEventHandler m_DoClose;

        /// <summary>
        /// Called just before a browser is destroyed. Release all references to the
        /// browser object and do not attempt to execute any functions on the browser
        /// object after this callback returns. This callback will be the last
        /// notification that references |Browser|. See do_close() documentation for
        /// additional usage information.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public event CfxOnBeforeCloseEventHandler OnBeforeClose {
            add {
                lock(eventLock) {
                    if(m_OnBeforeClose == null) {
                        if(cfx_life_span_handler_on_before_close == null) {
                            cfx_life_span_handler_on_before_close = on_before_close;
                            cfx_life_span_handler_on_before_close_ptr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(cfx_life_span_handler_on_before_close);
                        }
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 3, cfx_life_span_handler_on_before_close_ptr);
                    }
                    m_OnBeforeClose += value;
                }
            }
            remove {
                lock(eventLock) {
                    m_OnBeforeClose -= value;
                    if(m_OnBeforeClose == null) {
                        CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 3, IntPtr.Zero);
                    }
                }
            }
        }

        private CfxOnBeforeCloseEventHandler m_OnBeforeClose;

        internal override void OnDispose(IntPtr nativePtr) {
            if(m_OnBeforePopup != null) {
                m_OnBeforePopup = null;
                CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 0, IntPtr.Zero);
            }
            if(m_OnAfterCreated != null) {
                m_OnAfterCreated = null;
                CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 1, IntPtr.Zero);
            }
            if(m_DoClose != null) {
                m_DoClose = null;
                CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 2, IntPtr.Zero);
            }
            if(m_OnBeforeClose != null) {
                m_OnBeforeClose = null;
                CfxApi.LifeSpanHandler.cfx_life_span_handler_set_managed_callback(NativePtr, 3, IntPtr.Zero);
            }
            base.OnDispose(nativePtr);
        }
    }


    namespace Event {

        /// <summary>
        /// Called on the IO thread before a new popup browser is created. The
        /// |Browser| and |Frame| values represent the source of the popup request. The
        /// |TargetUrl| and |TargetFrameName| values indicate where the popup
        /// browser should navigate and may be NULL if not specified with the request.
        /// The |TargetDisposition| value indicates where the user intended to open
        /// the popup (e.g. current tab, new tab, etc). The |UserGesture| value will
        /// be true (1) if the popup was opened via explicit user gesture (e.g.
        /// clicking a link) or false (0) if the popup opened automatically (e.g. via
        /// the DomContentLoaded event). The |PopupFeatures| structure contains
        /// additional information about the requested popup window. To allow creation
        /// of the popup browser optionally modify |WindowInfo|, |Client|, |Settings|
        /// and |NoJavascriptAccess| and return false (0). To cancel creation of the
        /// popup browser return true (1). The |Client| and |Settings| values will
        /// default to the source browser's values. If the |NoJavascriptAccess| value
        /// is set to false (0) the new browser will not be scriptable and may not be
        /// hosted in the same renderer process as the source browser. Any
        /// modifications to |WindowInfo| will be ignored if the parent browser is
        /// wrapped in a CfxBrowserView.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public delegate void CfxOnBeforePopupEventHandler(object sender, CfxOnBeforePopupEventArgs e);

        /// <summary>
        /// Called on the IO thread before a new popup browser is created. The
        /// |Browser| and |Frame| values represent the source of the popup request. The
        /// |TargetUrl| and |TargetFrameName| values indicate where the popup
        /// browser should navigate and may be NULL if not specified with the request.
        /// The |TargetDisposition| value indicates where the user intended to open
        /// the popup (e.g. current tab, new tab, etc). The |UserGesture| value will
        /// be true (1) if the popup was opened via explicit user gesture (e.g.
        /// clicking a link) or false (0) if the popup opened automatically (e.g. via
        /// the DomContentLoaded event). The |PopupFeatures| structure contains
        /// additional information about the requested popup window. To allow creation
        /// of the popup browser optionally modify |WindowInfo|, |Client|, |Settings|
        /// and |NoJavascriptAccess| and return false (0). To cancel creation of the
        /// popup browser return true (1). The |Client| and |Settings| values will
        /// default to the source browser's values. If the |NoJavascriptAccess| value
        /// is set to false (0) the new browser will not be scriptable and may not be
        /// hosted in the same renderer process as the source browser. Any
        /// modifications to |WindowInfo| will be ignored if the parent browser is
        /// wrapped in a CfxBrowserView.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public class CfxOnBeforePopupEventArgs : CfxEventArgs {

            internal IntPtr m_browser;
            internal CfxBrowser m_browser_wrapped;
            internal IntPtr m_frame;
            internal CfxFrame m_frame_wrapped;
            internal IntPtr m_target_url_str;
            internal int m_target_url_length;
            internal string m_target_url;
            internal IntPtr m_target_frame_name_str;
            internal int m_target_frame_name_length;
            internal string m_target_frame_name;
            internal int m_target_disposition;
            internal int m_user_gesture;
            internal IntPtr m_popupFeatures;
            internal CfxPopupFeatures m_popupFeatures_wrapped;
            internal IntPtr m_windowInfo;
            internal CfxClient m_client_wrapped;
            internal IntPtr m_settings;
            internal CfxBrowserSettings m_settings_wrapped;
            internal int m_no_javascript_access;

            internal bool m_returnValue;
            private bool returnValueSet;

            internal CfxOnBeforePopupEventArgs(IntPtr browser, IntPtr frame, IntPtr target_url_str, int target_url_length, IntPtr target_frame_name_str, int target_frame_name_length, int target_disposition, int user_gesture, IntPtr popupFeatures, IntPtr windowInfo, IntPtr settings) {
                m_browser = browser;
                m_frame = frame;
                m_target_url_str = target_url_str;
                m_target_url_length = target_url_length;
                m_target_frame_name_str = target_frame_name_str;
                m_target_frame_name_length = target_frame_name_length;
                m_target_disposition = target_disposition;
                m_user_gesture = user_gesture;
                m_popupFeatures = popupFeatures;
                m_windowInfo = windowInfo;
                m_settings = settings;
            }

            /// <summary>
            /// Get the Browser parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxBrowser Browser {
                get {
                    CheckAccess();
                    if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                    return m_browser_wrapped;
                }
            }
            /// <summary>
            /// Get the Frame parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxFrame Frame {
                get {
                    CheckAccess();
                    if(m_frame_wrapped == null) m_frame_wrapped = CfxFrame.Wrap(m_frame);
                    return m_frame_wrapped;
                }
            }
            /// <summary>
            /// Get the TargetUrl parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public string TargetUrl {
                get {
                    CheckAccess();
                    m_target_url = StringFunctions.PtrToStringUni(m_target_url_str, m_target_url_length);
                    return m_target_url;
                }
            }
            /// <summary>
            /// Get the TargetFrameName parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public string TargetFrameName {
                get {
                    CheckAccess();
                    m_target_frame_name = StringFunctions.PtrToStringUni(m_target_frame_name_str, m_target_frame_name_length);
                    return m_target_frame_name;
                }
            }
            /// <summary>
            /// Get the TargetDisposition parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxWindowOpenDisposition TargetDisposition {
                get {
                    CheckAccess();
                    return (CfxWindowOpenDisposition)m_target_disposition;
                }
            }
            /// <summary>
            /// Get the UserGesture parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public bool UserGesture {
                get {
                    CheckAccess();
                    return 0 != m_user_gesture;
                }
            }
            /// <summary>
            /// Get the PopupFeatures parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxPopupFeatures PopupFeatures {
                get {
                    CheckAccess();
                    if(m_popupFeatures_wrapped == null) m_popupFeatures_wrapped = CfxPopupFeatures.Wrap(m_popupFeatures);
                    return m_popupFeatures_wrapped;
                }
            }
            /// <summary>
            /// Get the WindowInfo parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxWindowInfo WindowInfo {
                get {
                    CheckAccess();
                    return CfxWindowInfo.Wrap(m_windowInfo);
                }
            }
            /// <summary>
            /// Set the Client out parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxClient Client {
                set {
                    CheckAccess();
                    m_client_wrapped = value;
                }
            }
            /// <summary>
            /// Get the Settings parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public CfxBrowserSettings Settings {
                get {
                    CheckAccess();
                    if(m_settings_wrapped == null) m_settings_wrapped = CfxBrowserSettings.Wrap(m_settings);
                    return m_settings_wrapped;
                }
            }
            /// <summary>
            /// Set the NoJavascriptAccess out parameter for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// </summary>
            public bool NoJavascriptAccess {
                set {
                    CheckAccess();
                    m_no_javascript_access = value ? 1 : 0;
                }
            }
            /// <summary>
            /// Set the return value for the <see cref="CfxLifeSpanHandler.OnBeforePopup"/> callback.
            /// Calling SetReturnValue() more then once per callback or from different event handlers will cause an exception to be thrown.
            /// </summary>
            public void SetReturnValue(bool returnValue) {
                CheckAccess();
                if(returnValueSet) {
                    throw new CfxException("The return value has already been set");
                }
                returnValueSet = true;
                this.m_returnValue = returnValue;
            }

            public override string ToString() {
                return String.Format("Browser={{{0}}}, Frame={{{1}}}, TargetUrl={{{2}}}, TargetFrameName={{{3}}}, TargetDisposition={{{4}}}, UserGesture={{{5}}}, PopupFeatures={{{6}}}, WindowInfo={{{7}}}, Settings={{{8}}}", Browser, Frame, TargetUrl, TargetFrameName, TargetDisposition, UserGesture, PopupFeatures, WindowInfo, Settings);
            }
        }

        /// <summary>
        /// Called after a new browser is created. This callback will be the first
        /// notification that references |Browser|.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public delegate void CfxOnAfterCreatedEventHandler(object sender, CfxOnAfterCreatedEventArgs e);

        /// <summary>
        /// Called after a new browser is created. This callback will be the first
        /// notification that references |Browser|.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public class CfxOnAfterCreatedEventArgs : CfxEventArgs {

            internal IntPtr m_browser;
            internal CfxBrowser m_browser_wrapped;

            internal CfxOnAfterCreatedEventArgs(IntPtr browser) {
                m_browser = browser;
            }

            /// <summary>
            /// Get the Browser parameter for the <see cref="CfxLifeSpanHandler.OnAfterCreated"/> callback.
            /// </summary>
            public CfxBrowser Browser {
                get {
                    CheckAccess();
                    if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                    return m_browser_wrapped;
                }
            }

            public override string ToString() {
                return String.Format("Browser={{{0}}}", Browser);
            }
        }

        /// <summary>
        /// Called when a browser has recieved a request to close. This may result
        /// directly from a call to CfxBrowserHost::*close_browser() or indirectly
        /// if the browser is parented to a top-level window created by CEF and the
        /// user attempts to close that window (by clicking the 'X', for example). The
        /// do_close() function will be called after the JavaScript 'onunload' event
        /// has been fired.
        /// An application should handle top-level owner window close notifications by
        /// calling CfxBrowserHost.TrycloseBrowser() or
        /// CfxBrowserHost.CloseBrowser(false (0)) instead of allowing the window
        /// to close immediately (see the examples below). This gives CEF an
        /// opportunity to process the 'onbeforeunload' event and optionally cancel the
        /// close before do_close() is called.
        /// When windowed rendering is enabled CEF will internally create a window or
        /// view to host the browser. In that case returning false (0) from do_close()
        /// will send the standard close notification to the browser's top-level owner
        /// window (e.g. WM_CLOSE on Windows, performClose: on OS X, "delete_event" on
        /// Linux or CfxWindowDelegate.CanClose() callback from Views). If the
        /// browser's host window/view has already been destroyed (via view hierarchy
        /// tear-down, for example) then do_close() will not be called for that browser
        /// since is no longer possible to cancel the close.
        /// When windowed rendering is disabled returning false (0) from do_close()
        /// will cause the browser object to be destroyed immediately.
        /// If the browser's top-level owner window requires a non-standard close
        /// notification then send that notification from do_close() and return true
        /// (1).
        /// The CfxLifeSpanHandler.OnBeforeClose() function will be called
        /// after do_close() (if do_close() is called) and immediately before the
        /// browser object is destroyed. The application should only exit after
        /// on_before_close() has been called for all existing browsers.
        /// The below examples describe what should happen during window close when the
        /// browser is parented to an application-provided top-level window.
        /// Example 1: Using CfxBrowserHost.TrycloseBrowser(). This is
        /// recommended for clients using standard close handling and windows created
        /// on the browser process UI thread. 1.  User clicks the window close button
        /// which sends a close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser() (which internally calls CloseBrowser(false)).
        /// TryCloseBrowser() returns false so the client cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// CEF sends a close notification to the application's top-level window
        /// (because DoClose() returned false by default).
        /// 7.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser(). TryCloseBrowser() returns true so the client
        /// allows the window close.
        /// 8.  Application's top-level window is destroyed. 9.  Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 10. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// Example 2: Using CfxBrowserHost.CloseBrowser(false (0)) and
        /// implementing the do_close() callback. This is recommended for clients using
        /// non-standard close handling or windows that were not created on the browser
        /// process UI thread. 1.  User clicks the window close button which sends a
        /// close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and:
        /// A. Calls CfxBrowserHost.CloseBrowser(false).
        /// B. Cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// Application's do_close() handler is called. Application will:
        /// A. Set a flag to indicate that the next close attempt will be allowed.
        /// B. Return false.
        /// 7.  CEF sends an close notification to the application's top-level window.
        /// 8.  Application's top-level window receives the close notification and
        /// allows the window to close based on the flag from #6B.
        /// 9.  Application's top-level window is destroyed. 10. Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 11. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public delegate void CfxDoCloseEventHandler(object sender, CfxDoCloseEventArgs e);

        /// <summary>
        /// Called when a browser has recieved a request to close. This may result
        /// directly from a call to CfxBrowserHost::*close_browser() or indirectly
        /// if the browser is parented to a top-level window created by CEF and the
        /// user attempts to close that window (by clicking the 'X', for example). The
        /// do_close() function will be called after the JavaScript 'onunload' event
        /// has been fired.
        /// An application should handle top-level owner window close notifications by
        /// calling CfxBrowserHost.TrycloseBrowser() or
        /// CfxBrowserHost.CloseBrowser(false (0)) instead of allowing the window
        /// to close immediately (see the examples below). This gives CEF an
        /// opportunity to process the 'onbeforeunload' event and optionally cancel the
        /// close before do_close() is called.
        /// When windowed rendering is enabled CEF will internally create a window or
        /// view to host the browser. In that case returning false (0) from do_close()
        /// will send the standard close notification to the browser's top-level owner
        /// window (e.g. WM_CLOSE on Windows, performClose: on OS X, "delete_event" on
        /// Linux or CfxWindowDelegate.CanClose() callback from Views). If the
        /// browser's host window/view has already been destroyed (via view hierarchy
        /// tear-down, for example) then do_close() will not be called for that browser
        /// since is no longer possible to cancel the close.
        /// When windowed rendering is disabled returning false (0) from do_close()
        /// will cause the browser object to be destroyed immediately.
        /// If the browser's top-level owner window requires a non-standard close
        /// notification then send that notification from do_close() and return true
        /// (1).
        /// The CfxLifeSpanHandler.OnBeforeClose() function will be called
        /// after do_close() (if do_close() is called) and immediately before the
        /// browser object is destroyed. The application should only exit after
        /// on_before_close() has been called for all existing browsers.
        /// The below examples describe what should happen during window close when the
        /// browser is parented to an application-provided top-level window.
        /// Example 1: Using CfxBrowserHost.TrycloseBrowser(). This is
        /// recommended for clients using standard close handling and windows created
        /// on the browser process UI thread. 1.  User clicks the window close button
        /// which sends a close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser() (which internally calls CloseBrowser(false)).
        /// TryCloseBrowser() returns false so the client cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// CEF sends a close notification to the application's top-level window
        /// (because DoClose() returned false by default).
        /// 7.  Application's top-level window receives the close notification and
        /// calls TryCloseBrowser(). TryCloseBrowser() returns true so the client
        /// allows the window close.
        /// 8.  Application's top-level window is destroyed. 9.  Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 10. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// Example 2: Using CfxBrowserHost.CloseBrowser(false (0)) and
        /// implementing the do_close() callback. This is recommended for clients using
        /// non-standard close handling or windows that were not created on the browser
        /// process UI thread. 1.  User clicks the window close button which sends a
        /// close notification to
        /// the application's top-level window.
        /// 2.  Application's top-level window receives the close notification and:
        /// A. Calls CfxBrowserHost.CloseBrowser(false).
        /// B. Cancels the window close.
        /// 3.  JavaScript 'onbeforeunload' handler executes and shows the close
        /// confirmation dialog (which can be overridden via
        /// CfxJSDialogHandler.OnBeforeUnloadDialog()).
        /// 4.  User approves the close. 5.  JavaScript 'onunload' handler executes. 6.
        /// Application's do_close() handler is called. Application will:
        /// A. Set a flag to indicate that the next close attempt will be allowed.
        /// B. Return false.
        /// 7.  CEF sends an close notification to the application's top-level window.
        /// 8.  Application's top-level window receives the close notification and
        /// allows the window to close based on the flag from #6B.
        /// 9.  Application's top-level window is destroyed. 10. Application's
        /// on_before_close() handler is called and the browser object
        /// is destroyed.
        /// 11. Application exits by calling cef_quit_message_loop() if no other
        /// browsers
        /// exist.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public class CfxDoCloseEventArgs : CfxEventArgs {

            internal IntPtr m_browser;
            internal CfxBrowser m_browser_wrapped;

            internal bool m_returnValue;
            private bool returnValueSet;

            internal CfxDoCloseEventArgs(IntPtr browser) {
                m_browser = browser;
            }

            /// <summary>
            /// Get the Browser parameter for the <see cref="CfxLifeSpanHandler.DoClose"/> callback.
            /// </summary>
            public CfxBrowser Browser {
                get {
                    CheckAccess();
                    if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                    return m_browser_wrapped;
                }
            }
            /// <summary>
            /// Set the return value for the <see cref="CfxLifeSpanHandler.DoClose"/> callback.
            /// Calling SetReturnValue() more then once per callback or from different event handlers will cause an exception to be thrown.
            /// </summary>
            public void SetReturnValue(bool returnValue) {
                CheckAccess();
                if(returnValueSet) {
                    throw new CfxException("The return value has already been set");
                }
                returnValueSet = true;
                this.m_returnValue = returnValue;
            }

            public override string ToString() {
                return String.Format("Browser={{{0}}}", Browser);
            }
        }

        /// <summary>
        /// Called just before a browser is destroyed. Release all references to the
        /// browser object and do not attempt to execute any functions on the browser
        /// object after this callback returns. This callback will be the last
        /// notification that references |Browser|. See do_close() documentation for
        /// additional usage information.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public delegate void CfxOnBeforeCloseEventHandler(object sender, CfxOnBeforeCloseEventArgs e);

        /// <summary>
        /// Called just before a browser is destroyed. Release all references to the
        /// browser object and do not attempt to execute any functions on the browser
        /// object after this callback returns. This callback will be the last
        /// notification that references |Browser|. See do_close() documentation for
        /// additional usage information.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_life_span_handler_capi.h">cef/include/capi/cef_life_span_handler_capi.h</see>.
        /// </remarks>
        public class CfxOnBeforeCloseEventArgs : CfxEventArgs {

            internal IntPtr m_browser;
            internal CfxBrowser m_browser_wrapped;

            internal CfxOnBeforeCloseEventArgs(IntPtr browser) {
                m_browser = browser;
            }

            /// <summary>
            /// Get the Browser parameter for the <see cref="CfxLifeSpanHandler.OnBeforeClose"/> callback.
            /// </summary>
            public CfxBrowser Browser {
                get {
                    CheckAccess();
                    if(m_browser_wrapped == null) m_browser_wrapped = CfxBrowser.Wrap(m_browser);
                    return m_browser_wrapped;
                }
            }

            public override string ToString() {
                return String.Format("Browser={{{0}}}", Browser);
            }
        }

    }
}
