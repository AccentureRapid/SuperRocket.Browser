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


// cef_menu_model_delegate

typedef struct _cfx_menu_model_delegate_t {
    cef_menu_model_delegate_t cef_menu_model_delegate;
    unsigned int ref_count;
    gc_handle_t gc_handle;
} cfx_menu_model_delegate_t;

void CEF_CALLBACK _cfx_menu_model_delegate_add_ref(struct _cef_base_t* base) {
    InterlockedIncrement(&((cfx_menu_model_delegate_t*)base)->ref_count);
}
int CEF_CALLBACK _cfx_menu_model_delegate_release(struct _cef_base_t* base) {
    int count = InterlockedDecrement(&((cfx_menu_model_delegate_t*)base)->ref_count);
    if(!count) {
        cfx_gc_handle_free(((cfx_menu_model_delegate_t*)base)->gc_handle);
        free(base);
        return 1;
    }
    return 0;
}
int CEF_CALLBACK _cfx_menu_model_delegate_has_one_ref(struct _cef_base_t* base) {
    return ((cfx_menu_model_delegate_t*)base)->ref_count == 1 ? 1 : 0;
}

static cfx_menu_model_delegate_t* cfx_menu_model_delegate_ctor(gc_handle_t gc_handle) {
    cfx_menu_model_delegate_t* ptr = (cfx_menu_model_delegate_t*)calloc(1, sizeof(cfx_menu_model_delegate_t));
    if(!ptr) return 0;
    ptr->cef_menu_model_delegate.base.size = sizeof(cef_menu_model_delegate_t);
    ptr->cef_menu_model_delegate.base.add_ref = _cfx_menu_model_delegate_add_ref;
    ptr->cef_menu_model_delegate.base.release = _cfx_menu_model_delegate_release;
    ptr->cef_menu_model_delegate.base.has_one_ref = _cfx_menu_model_delegate_has_one_ref;
    ptr->ref_count = 1;
    ptr->gc_handle = gc_handle;
    return ptr;
}

static gc_handle_t cfx_menu_model_delegate_get_gc_handle(cfx_menu_model_delegate_t* self) {
    return self->gc_handle;
}

// execute_command

void (CEF_CALLBACK *cfx_menu_model_delegate_execute_command_callback)(gc_handle_t self, cef_menu_model_t* menu_model, int command_id, cef_event_flags_t event_flags);

void CEF_CALLBACK cfx_menu_model_delegate_execute_command(cef_menu_model_delegate_t* self, cef_menu_model_t* menu_model, int command_id, cef_event_flags_t event_flags) {
    cfx_menu_model_delegate_execute_command_callback(((cfx_menu_model_delegate_t*)self)->gc_handle, menu_model, command_id, event_flags);
}


// menu_will_show

void (CEF_CALLBACK *cfx_menu_model_delegate_menu_will_show_callback)(gc_handle_t self, cef_menu_model_t* menu_model);

void CEF_CALLBACK cfx_menu_model_delegate_menu_will_show(cef_menu_model_delegate_t* self, cef_menu_model_t* menu_model) {
    cfx_menu_model_delegate_menu_will_show_callback(((cfx_menu_model_delegate_t*)self)->gc_handle, menu_model);
}


static void cfx_menu_model_delegate_set_managed_callback(cef_menu_model_delegate_t* self, int index, void* callback) {
    switch(index) {
    case 0:
        if(callback && !cfx_menu_model_delegate_execute_command_callback)
            cfx_menu_model_delegate_execute_command_callback = (void (CEF_CALLBACK *)(gc_handle_t self, cef_menu_model_t* menu_model, int command_id, cef_event_flags_t event_flags)) callback;
        self->execute_command = callback ? cfx_menu_model_delegate_execute_command : 0;
        break;
    case 1:
        if(callback && !cfx_menu_model_delegate_menu_will_show_callback)
            cfx_menu_model_delegate_menu_will_show_callback = (void (CEF_CALLBACK *)(gc_handle_t self, cef_menu_model_t* menu_model)) callback;
        self->menu_will_show = callback ? cfx_menu_model_delegate_menu_will_show : 0;
        break;
    }
}

