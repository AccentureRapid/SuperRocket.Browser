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



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Chromium.Event;
using Chromium.Remote;
using Chromium.Remote.Event;

using Chromium.WebBrowser.Event;

namespace Chromium.WebBrowser {


    /// <summary>
    /// Windows Forms webbrowser control based on ChromiumFX.
    /// </summary>
    [DesignerCategory("")]
    public class ChromiumWebBrowser : Control {

        private static CfxBrowserSettings defaultBrowserSettings;

        /// <summary>
        /// The CfxBrowserSettings applied for new instances of ChromiumWebBrowser.
        /// Any changes to these settings will only apply to new browsers,
        /// leaving already created browsers unaffected.
        /// </summary>
        public static CfxBrowserSettings DefaultBrowserSettings {
            get {
                if(defaultBrowserSettings == null) {
                    defaultBrowserSettings = new CfxBrowserSettings();
                }
                return defaultBrowserSettings;
            }
        }

        /// <summary>
        /// Provides an opportunity to change initialization settings
        /// and subscribe to browser process handler events.
        /// </summary>
        public static event OnBeforeCfxInitializeEventHandler OnBeforeCfxInitialize;
        internal static void RaiseOnBeforeCfxInitialize(CfxSettings settings, CfxBrowserProcessHandler processHandler) {
            var handler = OnBeforeCfxInitialize;
            if(handler != null) {
                var e = new OnBeforeCfxInitializeEventArgs(settings, processHandler);
                handler(e);
            }
        }

        /// <summary>
        /// Provides an opportunity to view and/or modify command-line arguments before
        /// processing by CEF and Chromium. The |ProcessType| value will be NULL for
        /// the browser process. Do not keep a reference to the CfxCommandLine
        /// object passed to this function. The CfxSettings.CommandLineArgsDisabled
        /// value can be used to start with an NULL command-line object. Any values
        /// specified in CfxSettings that equate to command-line arguments will be set
        /// before this function is called. Be cautious when using this function to
        /// modify command-line arguments for non-browser processes as this may result
        /// in undefined behavior including crashes.
        /// </summary>
        public static event OnBeforeCommandLineProcessingEventHandler OnBeforeCommandLineProcessing;
        internal static void RaiseOnBeforeCommandLineProcessing(CfxOnBeforeCommandLineProcessingEventArgs e) {
            var handler = OnBeforeCommandLineProcessing;
            if(handler != null) {
                handler(e);
            }
        }

        /// <summary>
        /// Provides an opportunity to register custom schemes. Do not keep a reference
        /// to the |Registrar| object. This function is called on the main thread for
        /// each process and the registered schemes should be the same across all
        /// processes.
        /// </summary>
        public static event OnRegisterCustomSchemesEventHandler OnRegisterCustomSchemes;
        internal static void RaiseOnRegisterCustomSchemes(CfxOnRegisterCustomSchemesEventArgs e) {
            var handler = OnRegisterCustomSchemes;
            if(handler != null) {
                handler(e);
            }
        }

        /// <summary>
        /// For each new render process created, provides an opportunity to subscribe
        /// to CfrRenderProcessHandler remote callback events.
        /// </summary>
        public static event RemoteProcessCreatedEventHandler RemoteProcessCreated;
        internal static void RaiseRemoteProcessCreated(CfrRenderProcessHandler renderProcessHandler) {
            var ev = RemoteProcessCreated;
            if(ev != null) {
                ev(new RemoteProcessCreatedEventArgs(renderProcessHandler));
            }
        }

        [Obsolete("OnRemoteContextCreated is deprecated, use RemoteProcessCreated instead.")]
        public static event OnRemoteContextCreatedEventHandler OnRemoteContextCreated;
        internal static void RaiseOnRemoteContextCreated() {
            var handler = OnRemoteContextCreated;
            if(handler != null) {
                handler(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initialize the ChromiumWebBrowser and ChromiumFX libraries.
        /// The application can change initialization settings by handling
        /// the OnBeforeCfxInitialize event.
        /// </summary>
        public static void Initialize() {
            BrowserProcess.Initialize();
        }

        /// <summary>
        /// This function should be called on the main application thread to shut down
        /// the CEF browser process before the application exits.
        /// </summary>
        public static void Shutdown() {
            CfxRuntime.Shutdown();
        }

        /// <summary>
        /// The CfxBrowserProcessHandler for this browser process.
        /// Do not access this property before calling ChromiumWebBrowser.Initialize()
        /// </summary>
        public static CfxBrowserProcessHandler BrowserProcessHandler {
            get {
                return BrowserProcess.processHandler;
            }
        }

        private static readonly Dictionary<int, WeakReference> browsers = new Dictionary<int, WeakReference>();

        internal static ChromiumWebBrowser GetBrowser(int id) {
            lock(browsers) {
                WeakReference r;
                if(browsers.TryGetValue(id, out r)) {
                    return (ChromiumWebBrowser)r.Target;
                }
                return null;
            }
        }

        private static void AddToBrowserCache(ChromiumWebBrowser wb) {
            lock(browsers) {
                var deadRefs = new List<int>(browsers.Count);
                foreach(var b in browsers) {
                    if(!b.Value.IsAlive) deadRefs.Add(b.Key);
                }
                foreach(var r in deadRefs) {
                    browsers.Remove(r);
                }
                browsers[wb.Browser.Identifier] = new WeakReference(wb);
            }
        }

        private BrowserClient client;

        /// <summary>
        /// Returns the CfxBrowser object for this ChromiumWebBrowser.
        /// Might be null if the browser has not yet been created.
        /// Wait for the BrowserCreated event before accessing this property.
        /// </summary>
        public CfxBrowser Browser { get; private set; }

        /// <summary>
        /// Returns the CfxBrowserHost object for this ChromiumWebBrowser.
        /// Might be null if the browser has not yet been created.
        /// Wait for the BrowserCreated event before accessing this property.
        /// </summary>
        public CfxBrowserHost BrowserHost { get; private set; }

        /// <summary>
        /// The invoke mode for this browser. See also JSInvokeMode.
        /// Changes to the invoke mode will be effective after the next
        /// time the browser creates a V8 context. If this is set to
        /// "Inherit", then "Invoke" will be assumed. The invoke mode
        /// also applies to VisitDom and EvaluateJavascript.
        /// </summary>
        public JSInvokeMode RemoteCallbackInvokeMode { get; set; }

        /// <summary>
        /// Indicates whether render process callbacks on this browser
        /// will be executed on the thread that owns the 
        /// browser's underlying window handle.
        /// Depends on the invoke mode. If the invoke mode is set to
        /// "Inherit", then "Invoke" will be assumed.
        /// </summary>
        public bool RemoteCallbacksWillInvoke {
            get {
                return RemoteCallbackInvokeMode != JSInvokeMode.DontInvoke;
            }
        }

        private readonly object browserSyncRoot = new object();
        private IntPtr browserWindowHandle;
        
        internal readonly Dictionary<string, JSObject> frameGlobalObjects = new Dictionary<string, JSObject>();
        internal readonly Dictionary<string, WebResource> webResources = new Dictionary<string, WebResource>();

        internal RenderProcess remoteProcess;
        internal CfrBrowser remoteBrowser;

        private string initialUrl;

        /// <summary>
        /// Creates a ChromiumWebBrowser object with about:blank as initial URL.
        /// The underlying CfxBrowser is created immediately with the
        /// default CfxRequestContext.
        /// </summary>
        public ChromiumWebBrowser() : this(null, true) { }

        /// <summary>
        /// Creates a ChromiumWebBrowser object with about:blank as initial URL.
        /// If createImmediately is true, then the underlying CfxBrowser is 
        /// created immediately with the default CfxRequestContext.
        /// </summary>
        /// <param name="createImmediately"></param>
        public ChromiumWebBrowser(bool createImmediately) : this(null, createImmediately) { }

        /// <summary>
        /// Creates a ChromiumWebBrowser object with the given initial URL.
        /// The underlying CfxBrowser is created immediately with the
        /// default CfxRequestContext.
        /// </summary>
        public ChromiumWebBrowser(string initialUrl) : this(initialUrl, true) { }

        /// <summary>
        /// Creates a ChromiumWebBrowser object with the given initial URL.
        /// If createImmediately is true, then the underlying CfxBrowser is 
        /// created immediately with the default CfxRequestContext.
        /// </summary>
        public ChromiumWebBrowser(string initialUrl, bool createImmediately) {

            if(BrowserProcess.initialized) {

                SetStyle(ControlStyles.ContainerControl
                    | ControlStyles.ResizeRedraw
                    | ControlStyles.FixedWidth
                    | ControlStyles.FixedHeight
                    | ControlStyles.StandardClick
                    | ControlStyles.StandardDoubleClick
                    | ControlStyles.UserMouse
                    | ControlStyles.SupportsTransparentBackColor
                    | ControlStyles.EnableNotifyMessage
                    | ControlStyles.DoubleBuffer
                    | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.UseTextForAccessibility
                    | ControlStyles.Opaque
                    , false);

                SetStyle(ControlStyles.UserPaint
                    | ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.CacheText
                    | ControlStyles.Selectable
                    , true);

                if(initialUrl == null)
                    this.initialUrl = "about:blank";
                else
                    this.initialUrl = initialUrl;

                client = new BrowserClient(this);

                GlobalObject = new JSObject();
                GlobalObject.SetBrowser("window", this);

                if(createImmediately)
                    CreateBrowser();

            } else {
                BackColor = System.Drawing.Color.White;
                Width = 200;
                Height = 160;
                var label = new Label();
                label.AutoSize = true;
                label.Text = "ChromiumWebBrowser";
                label.Parent = this;
            }
        }

        /// <summary>
        /// Creates the underlying CfxBrowser with the default CfxRequestContext.
        /// This method should only be called if this ChromiumWebBrowser
        /// was instanciated with createImmediately == false.
        /// </summary>
        public void CreateBrowser() {
            CreateBrowser((CfxRequestContext)null);
        }

        /// <summary>
        /// Creates the underlying CfxBrowser with the default CfxRequestContext
        /// and the given initial URL.
        /// This method should only be called if this ChromiumWebBrowser
        /// was instanciated with createImmediately == false.
        /// </summary>
        public void CreateBrowser(string initialUrl) {
            this.initialUrl = initialUrl;
            CreateBrowser((CfxRequestContext)null);
        }

        /// <summary>
        /// Creates the underlying CfxBrowser with the given 
        /// CfxRequestContext and initial URL.
        /// This method should only be called if this ChromiumWebBrowser
        /// was instanciated with createImmediately == false.
        /// </summary>
        public void CreateBrowser(string initialUrl, CfxRequestContext requestContext) {
            this.initialUrl = initialUrl;
            CreateBrowser(requestContext);
        }

        /// <summary>
        /// Creates the underlying CfxBrowser with the given CfxRequestContext.
        /// This method should only be called if this ChromiumWebBrowser
        /// was instanciated with createImmediately == false.
        /// </summary>
        public void CreateBrowser(CfxRequestContext requestContext) {

            // avoid illegal cross-thread calls
            if(InvokeRequired) {
                Invoke((MethodInvoker)(() => CreateBrowser(requestContext)));
                return;
            }

            var windowInfo = new CfxWindowInfo();
            // in order to avoid focus issues when creating browsers offscreen,
            // the browser must be created with a disabled child window.
            windowInfo.SetAsDisabledChild(Handle);

            if(!CfxBrowserHost.CreateBrowser(windowInfo, client, initialUrl, DefaultBrowserSettings, requestContext))
                throw new ChromiumWebBrowserException("Failed to create browser instance.");
        }


        /// <summary>
        /// Returns the context menu handler for this browser. If this is never accessed the default
        /// implementation will be used.
        /// </summary>
        public CfxContextMenuHandler ContextMenuHandler { get { return client.ContextMenuHandler; } }

        /// <summary>
        /// Returns the life span handler for this browser.
        /// </summary>
        public CfxLifeSpanHandler LifeSpanHandler { get { return client.lifeSpanHandler; } }

        /// <summary>
        /// Returns the load handler for this browser.
        /// </summary>
        public CfxLoadHandler LoadHandler { get { return client.LoadHandler; } }

        /// <summary>
        /// Returns the request handler for this browser.
        /// Do not set the return value in the GetResourceHandler event for URLs
        /// with associated WebResources (see also SetWebResource).
        /// </summary>
        public CfxRequestHandler RequestHandler { get { return client.requestHandler; } }

        /// <summary>
        /// Returns the display handler for this browser.
        /// </summary>
        public CfxDisplayHandler DisplayHandler { get { return client.DisplayHandler; } }

        /// <summary>
        /// Returns the download handler for this browser. If this is never accessed
        /// downloads will not be allowed.
        /// </summary>
        public CfxDownloadHandler DownloadHandler { get { return client.DownloadHandler; } }

        /// <summary>
        /// Returns the drag handler for this browser.
        /// </summary>
        public CfxDragHandler DragHandler { get { return client.DragHandler; } }

        /// <summary>
        /// Returns the dialog handler for this browser. If this is never accessed the default
        /// implementation will be used.
        /// </summary>
        public CfxDialogHandler DialogHandler { get { return client.DialogHandler; } }

        /// <summary>
        /// Returns the find handler for this browser.
        /// </summary>
        public CfxFindHandler FindHandler { get { return client.FindHandler; } }

        /// <summary>
        /// Returns the focus handler for this browser.
        /// </summary>
        public CfxFocusHandler FocusHandler { get { return client.FocusHandler; } }

        /// <summary>
        /// Returns the geolocation handler for this browser. If this is never accessed
        /// geolocation access will be denied by default.
        /// </summary>
        public CfxGeolocationHandler GeolocationHandler { get { return client.GeolocationHandler; } }

        /// <summary>
        /// Returns the js dialog handler for this browser. If this is never accessed the default
        /// implementation will be used.
        /// </summary>
        public CfxJsDialogHandler JsDialogHandler { get { return client.JsDialogHandler; } }

        /// <summary>
        /// Returns the keyboard handler for this browser.
        /// </summary>
        public CfxKeyboardHandler KeyboardHandler { get { return client.KeyboardHandler; } }



        /// <summary>
        /// Returns the URL currently loaded in the main frame.
        /// </summary>
        public System.Uri Url {
            get {
                if(Browser == null) return null;
                Uri retval;
                Uri.TryCreate(Browser.MainFrame.Url, UriKind.RelativeOrAbsolute, out retval);
                return retval;
            }
        }

        /// <summary>
        /// Returns true if the browser is currently loading.
        /// </summary>
        public bool IsLoading { get { return Browser == null ? false : Browser.IsLoading; } }

        /// <summary>
        /// Returns true if the browser can navigate backwards.
        /// </summary>
        public bool CanGoBack { get { return Browser == null ? false : Browser.CanGoBack; } }

        /// <summary>
        /// Returns true if the browser can navigate forwards.
        /// </summary>
        public bool CanGoForward { get { return Browser == null ? false : Browser.CanGoForward; } }

        /// <summary>
        /// Navigate backwards.
        /// </summary>
        public void GoBack() { if(Browser != null) Browser.GoBack(); }

        /// <summary>
        /// Navigate forwards.
        /// </summary>
        public void GoForward() { if(Browser != null) Browser.GoForward(); }

        /// <summary>
        /// Load the specified |url| into the main frame.
        /// </summary>
        public void LoadUrl(string url) {
            if(Browser != null)
                Browser.MainFrame.LoadUrl(url);
            else {
                lock(browserSyncRoot) {
                    if(Browser != null) {
                        Browser.MainFrame.LoadUrl(url);
                    } else {
                        m_loadUrlDeferred = url;
                    }
                }
            }
        }

        /// <summary>
        /// Load the contents of |stringVal| with the specified dummy |url|. |url|
        /// should have a standard scheme (for example, http scheme) or behaviors like
        /// link clicks and web security restrictions may not behave as expected.
        /// </summary>
        public void LoadString(string stringVal, string url) {
            if(Browser != null) {
                Browser.MainFrame.LoadString(stringVal, url);
            } else {
                lock(browserSyncRoot) {
                    if(Browser != null) {
                        Browser.MainFrame.LoadString(stringVal, url);
                    } else {
                        m_loadUrlDeferred = url;
                        m_loadStringDeferred = stringVal;
                    }
                }
            }
        }

        /// <summary>
        /// Load the contents of |stringVal| with dummy url about:blank.
        /// </summary>
        public void LoadString(string stringVal) {
            LoadString(stringVal, "about:blank");
        }


        private int findId;
        private string currentFindText;
        private bool currentMatchCase;

        /// <summary>
        /// Search for |searchText|. |forward| indicates whether to search forward or
        /// backward within the page. |matchCase| indicates whether the search should
        /// be case-sensitive.
        /// Returns the identifier for this find operation (see also CfxFindHandler),
        /// or -1 if the browser has not yet been created.
        /// </summary>
        public int Find(string searchText, bool forward, bool matchCase) {
            if(BrowserHost == null)
                return -1;
            var findNext = currentFindText == searchText && currentMatchCase == matchCase;
            if(!findNext) {
                currentFindText = searchText;
                currentMatchCase = matchCase;
                ++findId;
            }

            BrowserHost.Find(findId, searchText, forward, matchCase, findNext);
            return findId;
        }

        /// <summary>
        /// Search for |searchText|. |forward| indicates whether to search forward or
        /// backward within the page. The search will be case-insensitive.
        /// Returns the identifier for this find operation (see also CfxFindHandler),
        /// or -1 if the browser has not yet been created.
        /// </summary>
        public int Find(string searchText, bool forward) {
            return Find(searchText, forward, false);
        }

        /// <summary>
        /// Search for |searchText|. The search will be forward and case-insensitive.
        /// Returns the identifier for this find operation (see also CfxFindHandler),
        /// or -1 if the browser has not yet been created.
        /// </summary>
        public int Find(string searchText) {
            return Find(searchText, true, false);
        }


        private FindToolbar m_findToolbar;

        /// <summary>
        /// Get the find toolbar of this browser window.
        /// </summary>
        public FindToolbar FindToolbar {
            get {
                if(m_findToolbar == null)
                    m_findToolbar = new FindToolbar(this);
                return m_findToolbar;
            }
        }


        /// <summary>
        /// Execute a string of javascript code in the browser's main frame.
        /// Execution is asynchronous, this function returns immediately.
        /// Returns false if the browser has not yet been created.
        /// </summary>
        public bool ExecuteJavascript(string code) {
            if(Browser != null) {
                Browser.MainFrame.ExecuteJavaScript(code, null, 0);
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// Special Invoke for framework callbacks from the render process.
        /// Maintains the thread in the context of the calling remote thread.
        /// Use this instead of invoke when the following conditions are meat:
        /// 1) The current thread is executing in the scope of a framework
        ///    callback event from the render process (ex. CfrTask.Execute).
        /// 2) You need to Invoke on the webbrowser control and
        /// 3) The invoked code needs to call into the render process.
        /// </summary>
        public Object RenderThreadInvoke(Delegate method, params Object[] args) {

            if(!CfxRemoteCallContext.IsInContext) {
                throw new ChromiumWebBrowserException("Can't use RenderThreadInvoke without being in the scope of a render process callback.");
            }

            if(!InvokeRequired)
                return method.DynamicInvoke(args);

            object retval = null;
            var context = CfxRemoteCallContext.CurrentContext;

            // Use BeginInvoke and Wait instead of Invoke.
            // Invoke marshals exceptions back to the calling thread.
            // We want exceptions to be thrown in place.

            var waitLock = new object();
            lock(waitLock) {
                BeginInvoke((MethodInvoker)(() => {
                    context.Enter();
                    try {
                        retval = method.DynamicInvoke(args);
                    } finally {
                        context.Exit();
                        lock(waitLock) {
                            Monitor.PulseAll(waitLock);
                        }
                    }
                }));
                Monitor.Wait(waitLock);
            }
            return retval;
        }

        /// <summary>
        /// Special Invoke for framework callbacks from the render process.
        /// Maintains the thread within the context of the calling remote thread.
        /// Use this instead of invoke when the following conditions are meat:
        /// 1) The current thread is executing in the scope of a framework
        ///    callback event from the render process (ex. CfrTask.Execute).
        /// 2) You need to Invoke on the webbrowser control and
        /// 3) The invoked code needs to call into the render process.
        /// </summary>
        public void RenderThreadInvoke(MethodInvoker method) {

            if(!CfxRemoteCallContext.IsInContext) {
                throw new ChromiumWebBrowserException("Can't use RenderThreadInvoke without being in the scope of a render process callback.");
            }

            if(!InvokeRequired) {
                method.Invoke();
                return;
            }

            var context = CfxRemoteCallContext.CurrentContext;

            // Use BeginInvoke and Wait instead of Invoke.
            // Invoke marshals exceptions back to the calling thread.
            // We want exceptions to be thrown in place.

            var waitLock = new object();
            lock(waitLock) {
                BeginInvoke((MethodInvoker)(() => {
                    context.Enter();
                    try {
                        method.Invoke();
                    } finally {
                        context.Exit();
                        lock(waitLock) {
                            Monitor.PulseAll(waitLock);
                        }
                    }
                }));
                Monitor.Wait(waitLock);
            }
        }

        /// <summary>
        /// Evaluate a string of javascript code in the browser's main frame.
        /// Evaluation is done asynchronously in the render process.
        /// Returns false if the remote browser is currently unavailable.
        /// If this function returns false, then |callback| will not be called. Otherwise,
        /// |callback| will be called asynchronously in the context of the render thread and,
        /// if RemoteCallbackInvokeMode is set to Invoke, on the thread that owns the 
        /// browser's underlying window handle.
        /// 
        /// Use with care:
        /// The callback may never be called if the render process gets killed prematurely.
        /// On success the CfrV8Value argument of the callback will be set to the return value
        /// of the evaluated script, if any. On failure the CfrV8Exception argument of the callback
        /// will be set to the exception thrown by the evaluated script, if any.
        /// Do not block the callback since it blocks the render thread.
        /// 
        /// *** WARNING ***
        /// In CEF 3.2623 and higher, the return value of the evaluation 
        /// seems to be broken in some cases (see also issue #65).
        /// 
        /// </summary>
        public bool EvaluateJavascript(string code, Action<CfrV8Value, CfrV8Exception> callback) {
            return EvaluateJavascript(code, JSInvokeMode.Inherit, callback);
        }

        /// <summary>
        /// Evaluate a string of javascript code in the browser's main frame.
        /// Evaluation is done asynchronously in the render process.
        /// Returns false if the remote browser is currently unavailable.
        /// If this function returns false, then |callback| will not be called. Otherwise,
        /// |callback| will be called asynchronously in the context of the render thread.
        /// 
        /// If |invokeMode| is set to Invoke, |callback| will be called on the thread that 
        /// owns the browser's underlying window handle. If |invokeMode| is set to Inherit,
        /// |callback| will be called according to RemoteCallbackInvokeMode.
        /// 
        /// Use with care:
        /// The callback may never be called if the render process gets killed prematurely.
        /// On success the CfrV8Value argument of the callback will be set to the return value
        /// of the evaluated script, if any. On failure the CfrV8Exception argument of the callback
        /// will be set to the exception thrown by the evaluated script, if any.
        /// Do not block the callback since it blocks the render thread.
        /// 
        /// *** WARNING ***
        /// In CEF 3.2623 and higher, the return value of the evaluation 
        /// seems to be broken in some cases (see also issue #65).
        /// 
        /// </summary>
        public bool EvaluateJavascript(string code, JSInvokeMode invokeMode, Action<CfrV8Value, CfrV8Exception> callback) {
            var rb = remoteBrowser;
            if(rb == null) return false;
            try {
                var ctx = rb.CreateRemoteCallContext();
                ctx.Enter();
                try {
                    var taskRunner = CfrTaskRunner.GetForThread(CfxThreadId.Renderer);
                    var task = new EvaluateTask(this, code, invokeMode, callback);
                    taskRunner.PostTask(task);
                    return true;
                } finally {
                    ctx.Exit();
                }
            } catch(CfxRemotingException) {
                return false;
            }
        }

        private class EvaluateTask : CfrTask {

            // quick fix: cache evaluation tasks so the GC won't
            // collect them before CEF performs the callback (fixes issue #64)
            // TODO: This involves hashing and locking. Is there a faster way to do this?
            private static HashSet<EvaluateTask> tasks = new HashSet<EvaluateTask>();

            ChromiumWebBrowser wb;
            string code;
            JSInvokeMode invokeMode;
            Action<CfrV8Value, CfrV8Exception> callback;

            internal EvaluateTask(ChromiumWebBrowser wb, string code, JSInvokeMode invokeMode, Action<CfrV8Value, CfrV8Exception> callback) {
                this.wb = wb;
                this.code = code;
                this.invokeMode = invokeMode;
                this.callback = callback;
                lock(tasks) tasks.Add(this);
                Execute += (s, e) => {
                    if(invokeMode == JSInvokeMode.Invoke || (invokeMode == JSInvokeMode.Inherit && wb.RemoteCallbacksWillInvoke))
                        wb.RenderThreadInvoke(() => Task_Execute(e));
                    else
                        Task_Execute(e);
                    lock(tasks) tasks.Remove(this);
                };
            }

            void Task_Execute(CfrEventArgs e) {
                CfrV8Value retval;
                CfrV8Exception ex;
                bool result = false;
                try {
                    var context = wb.remoteBrowser.MainFrame.V8Context;
                    result = context.Eval(code, out retval, out ex);
                } catch {
                    callback(null, null);
                    return;
                }
                if(result) {
                    callback(retval, null);
                } else {
                    callback(null, ex);
                }
            }
        }

        /// <summary>
        /// Represents the main frame's global javascript object (window).
        /// Any changes to the global object's properties will be available 
        /// after the next time a V8 context is created in the render process
        /// for the main frame of this browser.
        /// </summary>
        public JSObject GlobalObject { get; private set; }

        /// <summary>
        /// Represents a named frame's global javascript object (window).
        /// Any changes to the global object's properties will be available 
        /// after the next time a V8 context is created in the render process
        /// of this browser for a frame with this name.
        /// </summary>
        public JSObject GlobalObjectForFrame(string frameName) {
            JSObject obj;
            if(!frameGlobalObjects.TryGetValue(frameName, out obj)) {
                obj = new JSObject();
                obj.SetBrowser("window", this);
                frameGlobalObjects.Add(frameName, obj);
            }
            return obj;
        }


        /// <summary>
        /// Visit the DOM in the remote browser's main frame.
        /// Returns false if the remote browser is currently unavailable.
        /// If this function returns false, then |callback| will not be called. Otherwise,
        /// |callback| will be called according to the InvokeMode setting.
        /// 
        /// The document object passed to the callback represents a snapshot 
        /// of the DOM at the time the callback is executed.
        /// DOM objects are only valid for the scope of the callback. Do not
        /// keep references to or attempt to access any DOM objects outside the scope
        /// of the callback.
        /// Use with care:
        /// The callback may never be called if the render process gets killed prematurely.
        /// Do not keep a reference to the remote DOM or remote browser object after returning from the callback.
        /// Do not block the callback since it blocks the renderer thread.
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public bool VisitDom(Action<CfrDomDocument, CfrBrowser> callback) {
            var rb = remoteBrowser;
            if(rb == null) return false;
            try {
                var ctx = rb.CreateRemoteCallContext();
                ctx.Enter();
                try {
                    var taskRunner = CfrTaskRunner.GetForThread(CfxThreadId.Renderer);
                    var task = new VisitDomTask(this, callback);
                    taskRunner.PostTask(task);
                    return true;
                } finally {
                    ctx.Exit();
                }
            } catch(CfxRemotingException) {
                return false;
            }
        }

        private class VisitDomTask : CfrTask {

            ChromiumWebBrowser wb;
            Action<CfrDomDocument, CfrBrowser> callback;
            CfrDomVisitor visitor;

            internal VisitDomTask(ChromiumWebBrowser wb, Action<CfrDomDocument, CfrBrowser> callback) {
                this.wb = wb;
                this.callback = callback;
                this.Execute += Task_Execute;
                visitor = new CfrDomVisitor();
                visitor.Visit += (s, e) => {
                    if(wb.RemoteCallbacksWillInvoke)
                        wb.RenderThreadInvoke((MethodInvoker)(() => { callback(e.Document, wb.remoteBrowser); }));
                    else
                        callback(e.Document, wb.remoteBrowser);
                };
            }

            void Task_Execute(object sender, CfrEventArgs e) {
                wb.remoteBrowser.MainFrame.VisitDom(visitor);
            }
        }

        // Callbacks from the associated render process handler

        /// <summary>
        /// Called immediately after the V8 context for a frame has been created. To
        /// retrieve the JavaScript 'window' object use the
        /// CfrV8Context.GetGlobal() function. V8 handles can only be accessed
        /// from the thread on which they are created. A task runner for posting tasks
        /// on the associated thread can be retrieved via the
        /// CfrV8Context.GetTaskRunner() function.
        /// 
        /// All javascript properties/functions defined through GlobalObject or GlobalObjectForFrame
        /// are made available before this event is executed.
        /// 
        /// If RemoteCallbackInvokeMode is set to Invoke, then this event is executed on the 
        /// thread that owns the browser's underlying window handle.
        /// </summary>
        /// <remarks>
        /// See also the original CEF documentation in
        /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/capi/cef_render_process_handler_capi.h">cef/include/capi/cef_render_process_handler_capi.h</see>.
        /// </remarks>
        public event CfrOnContextCreatedEventHandler OnV8ContextCreated;

        internal void RaiseOnV8ContextCreated(CfrOnContextCreatedEventArgs e) {
            var eventHandler = OnV8ContextCreated;
            if(eventHandler == null) return;
            if(RemoteCallbacksWillInvoke)
                RenderThreadInvoke(() => eventHandler(this, e));
            else
                eventHandler(this, e);
        }


        /// <summary>
        /// Set a resource to be used for the specified URL.
        /// Note that these resources are kept in the memory.
        /// If you need to handle a lot of custom web resources,
        /// subscribing to RequestHandler.GetResourceHandler
        /// and loading from disk on demand
        /// might be a better choice.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="resource"></param>
        public void SetWebResource(string url, WebResource resource) {
            webResources[url] = resource;
        }

        /// <summary>
        /// Remove a resource previously set for the specified URL.
        /// </summary>
        /// <param name="url"></param>
        public void RemoveWebResource(string url) {
            webResources.Remove(url);
        }

        /// <summary>
        /// Raised after the CfxBrowser object for this WebBrowser has been created.
        /// The event is executed on the thread that owns this browser control's 
        /// underlying window handle.
        /// </summary>
        public event BrowserCreatedEventHandler BrowserCreated;

        /// <summary>
        /// Called after a remote browser has been created. When browsing cross-origin a new
        /// browser will be created before the old browser is destroyed.
        /// 
        /// Applications may keep a reference to the CfrBrowser object outside the scope 
        /// of this event, but you have to be aware that those objects become invalid as soon
        /// as the framework swaps render processes and/or recreates browsers.
        /// </summary>
        public event RemoteBrowserCreatedEventHandler RemoteBrowserCreated;

        private void InvokeCallback(MethodInvoker method) {

            if(!InvokeRequired) {
                method.Invoke();
                return;
            }

            // Use BeginInvoke and Wait instead of Invoke.
            // Invoke marshals exceptions back to the calling thread.
            // We want exceptions to be thrown in place.

            var waitLock = new object();
            lock(waitLock) {
                BeginInvoke((MethodInvoker)(() => {
                    try {
                        method.Invoke();
                    } finally {
                        lock(waitLock) {
                            Monitor.PulseAll(waitLock);
                        }
                    }
                }));
                Monitor.Wait(waitLock);
            }
        }

        [Obsolete("OnLoadingStateChange is deprecated. Please use LoadHandler.OnLoadingStateChange and check for invalid cross-thread operations.")]
        public event CfxOnLoadingStateChangeEventHandler OnLoadingStateChange {
            add {
                lock(browserSyncRoot) {
                    if(m_OnLoadingStateChange == null)
                        client.LoadHandler.OnLoadingStateChange += RaiseOnLoadingStateChange;
                    m_OnLoadingStateChange += value;
                }
            }
            remove {
                lock(browserSyncRoot) {
                    m_OnLoadingStateChange -= value;
                    if(m_OnLoadingStateChange == null)
                        client.LoadHandler.OnLoadingStateChange -= RaiseOnLoadingStateChange;
                }
            }
        }

        private CfxOnLoadingStateChangeEventHandler m_OnLoadingStateChange;
        private void RaiseOnLoadingStateChange(object sender, CfxOnLoadingStateChangeEventArgs e) {
            var handler = m_OnLoadingStateChange;
            if(handler != null) {
                InvokeCallback(() => { handler(this, e); });
            }
        }


        [Obsolete("OnBeforeContextMenu is deprecated. Please use ContextMenuHandler.OnBeforeContextMenu and check for invalid cross-thread operations.")]
        public event CfxOnBeforeContextMenuEventHandler OnBeforeContextMenu {
            add {
                lock(browserSyncRoot) {
                    if(m_OnBeforeContextMenu == null)
                        client.ContextMenuHandler.OnBeforeContextMenu += RaiseOnBeforeContextMenu;
                    m_OnBeforeContextMenu += value;
                }
            }
            remove {
                lock(browserSyncRoot) {
                    m_OnBeforeContextMenu -= value;
                    if(m_OnBeforeContextMenu == null)
                        client.ContextMenuHandler.OnBeforeContextMenu -= RaiseOnBeforeContextMenu;
                }
            }
        }

        private CfxOnBeforeContextMenuEventHandler m_OnBeforeContextMenu;
        private void RaiseOnBeforeContextMenu(object sender, CfxOnBeforeContextMenuEventArgs e) {
            var handler = m_OnBeforeContextMenu;
            if(handler != null) {
                InvokeCallback(() => { handler(this, e); });
            }
        }


        [Obsolete("OnContextMenuCommand is deprecated. Please use ContextMenuHandler.OnContextMenuCommand and check for invalid cross-thread operations.")]
        public event CfxOnContextMenuCommandEventHandler OnContextMenuCommand {
            add {
                lock(browserSyncRoot) {
                    if(m_OnContextMenuCommand == null)
                        client.ContextMenuHandler.OnContextMenuCommand += RaiseOnContextMenuCommand;
                    m_OnContextMenuCommand += value;
                }
            }
            remove {
                lock(browserSyncRoot) {
                    m_OnContextMenuCommand -= value;
                    if(m_OnContextMenuCommand == null)
                        client.ContextMenuHandler.OnContextMenuCommand -= RaiseOnContextMenuCommand;
                }
            }
        }

        private CfxOnContextMenuCommandEventHandler m_OnContextMenuCommand;
        private void RaiseOnContextMenuCommand(object sender, CfxOnContextMenuCommandEventArgs e) {
            var handler = m_OnContextMenuCommand;
            if(handler != null) {
                InvokeCallback(() => { handler(this, e); });
            }
        }


        private string m_loadUrlDeferred;
        private string m_loadStringDeferred;

        internal void OnBrowserCreated(CfxOnAfterCreatedEventArgs e) {

            Browser = e.Browser;
            BrowserHost = Browser.Host;
            browserWindowHandle = BrowserHost.WindowHandle;
            AddToBrowserCache(this);
            ResizeBrowserWindow();

            var handler = BrowserCreated;
            if(handler != null) {
                var e1 = new BrowserCreatedEventArgs(e.Browser);
                handler(this, e1);
            }

            System.Threading.ThreadPool.QueueUserWorkItem(AfterSetBrowserTasks);
        }

        private void AfterSetBrowserTasks(object state) {
            lock(browserSyncRoot) {
                if(m_loadUrlDeferred != null) {
                    if(m_loadStringDeferred != null) {
                        Browser.MainFrame.LoadString(m_loadStringDeferred, m_loadUrlDeferred);
                    } else {
                        Browser.MainFrame.LoadUrl(m_loadUrlDeferred);
                    }
                }
            }
        }

        internal void SetRemoteBrowser(CfrBrowser remoteBrowser, RenderProcess remoteProcess) {
            this.remoteBrowser = remoteBrowser;
            this.remoteProcess = remoteProcess;
            remoteProcess.OnExit += remoteProcess_OnExit;
            var h = RemoteBrowserCreated;
            if(h != null) {
                var e = new RemoteBrowserCreatedEventArgs(remoteBrowser);
                if(RemoteCallbacksWillInvoke && InvokeRequired) {
                    RenderThreadInvoke(() => { h(this, e); });
                } else {
                    h(this, e);
                }
            }
        }

        void remoteProcess_OnExit(RenderProcess process) {
            process.OnExit -= remoteProcess_OnExit;
            if(process == this.remoteProcess) {
                this.remoteBrowser = null;
                this.remoteProcess = null;
            }
        }


        //protected override void WndProc(ref Message m) {
        //    base.WndProc(ref m);
        //    System.Diagnostics.Debug.Print(m.ToString());
        //}   

        protected override void OnVisibleChanged(EventArgs e) {
            base.OnVisibleChanged(e);
            ResizeBrowserWindow();
            if(Visible)
                Refresh();
        }

        protected override void OnGotFocus(System.EventArgs e) {
            base.OnGotFocus(e);
            if(BrowserHost != null) BrowserHost.SetFocus(true);
        }

        protected override void OnResize(System.EventArgs e) {
            base.OnResize(e);
            ResizeBrowserWindow();
        }

        internal void ResizeBrowserWindow() {
            if(Visible) {
                if(browserWindowHandle != IntPtr.Zero && this.Height > 0 && this.Width > 0) {
                    int h;
                    if(m_findToolbar == null || !m_findToolbar.Visible) {
                        h = Height;
                    } else {
                        if(InvokeRequired) {
                            Invoke((MethodInvoker)(() => {
                                m_findToolbar.Width = Width;
                                m_findToolbar.Top = Height - m_findToolbar.Height;
                            }));
                        } else {
                            m_findToolbar.Width = Width;
                            m_findToolbar.Top = Height - m_findToolbar.Height;
                        }
                        h = m_findToolbar.Top;
                    }
                    SetWindowLong(browserWindowHandle, -16, (int)(WindowStyle.WS_CHILD | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_TABSTOP | WindowStyle.WS_VISIBLE));
                    SetWindowPos(browserWindowHandle, IntPtr.Zero, 0, 0, Width, h, SWP_NOMOVE | SWP_NOZORDER);
                }
            } else {
                if(browserWindowHandle != IntPtr.Zero)
                    SetWindowLong(browserWindowHandle, -16, (int)(WindowStyle.WS_CHILD | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_TABSTOP | WindowStyle.WS_DISABLED));
            }
        }


        [DllImport("user32", SetLastError = false)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private const uint SWP_NOMOVE = 0x2;
        private const uint SWP_NOZORDER = 0x4;
    }
}
