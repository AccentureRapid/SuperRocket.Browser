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
    /// <summary>
    /// Structure representing CfxExecuteProcess arguments.
    /// </summary>
    /// <remarks>
    /// See also the original CEF documentation in
    /// <see href="https://bitbucket.org/chromiumfx/chromiumfx/src/tip/cef/include/internal/cef_types_linux.h">cef/include/internal/cef_types_linux.h</see>.
    /// </remarks>
    internal sealed partial class CfxMainArgsLinux : CfxStructure {

        public CfxMainArgsLinux() : base(CfxApi.MainArgsLinux.cfx_main_args_linux_ctor, CfxApi.MainArgsLinux.cfx_main_args_linux_dtor) { CfxApi.CheckPlatformOS(CfxPlatformOS.Linux); }

        public int Argc {
            get {
                int value;
                CfxApi.MainArgsLinux.cfx_main_args_linux_get_argc(nativePtrUnchecked, out value);
                return value;
            }
            set {
                CfxApi.MainArgsLinux.cfx_main_args_linux_set_argc(nativePtrUnchecked, value);
            }
        }

        public IntPtr Argv {
            get {
                IntPtr value;
                CfxApi.MainArgsLinux.cfx_main_args_linux_get_argv(nativePtrUnchecked, out value);
                return value;
            }
            set {
                CfxApi.MainArgsLinux.cfx_main_args_linux_set_argv(nativePtrUnchecked, value);
            }
        }

    }
}
