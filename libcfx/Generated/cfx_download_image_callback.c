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


// cef_download_image_callback

typedef struct _cfx_download_image_callback_t {
    cef_download_image_callback_t cef_download_image_callback;
    unsigned int ref_count;
    gc_handle_t gc_handle;
} cfx_download_image_callback_t;

void CEF_CALLBACK _cfx_download_image_callback_add_ref(struct _cef_base_t* base) {
    InterlockedIncrement(&((cfx_download_image_callback_t*)base)->ref_count);
}
int CEF_CALLBACK _cfx_download_image_callback_release(struct _cef_base_t* base) {
    int count = InterlockedDecrement(&((cfx_download_image_callback_t*)base)->ref_count);
    if(!count) {
        cfx_gc_handle_free(((cfx_download_image_callback_t*)base)->gc_handle);
        free(base);
        return 1;
    }
    return 0;
}
int CEF_CALLBACK _cfx_download_image_callback_has_one_ref(struct _cef_base_t* base) {
    return ((cfx_download_image_callback_t*)base)->ref_count == 1 ? 1 : 0;
}

static cfx_download_image_callback_t* cfx_download_image_callback_ctor(gc_handle_t gc_handle) {
    cfx_download_image_callback_t* ptr = (cfx_download_image_callback_t*)calloc(1, sizeof(cfx_download_image_callback_t));
    if(!ptr) return 0;
    ptr->cef_download_image_callback.base.size = sizeof(cef_download_image_callback_t);
    ptr->cef_download_image_callback.base.add_ref = _cfx_download_image_callback_add_ref;
    ptr->cef_download_image_callback.base.release = _cfx_download_image_callback_release;
    ptr->cef_download_image_callback.base.has_one_ref = _cfx_download_image_callback_has_one_ref;
    ptr->ref_count = 1;
    ptr->gc_handle = gc_handle;
    return ptr;
}

static gc_handle_t cfx_download_image_callback_get_gc_handle(cfx_download_image_callback_t* self) {
    return self->gc_handle;
}

// on_download_image_finished

void (CEF_CALLBACK *cfx_download_image_callback_on_download_image_finished_callback)(gc_handle_t self, char16 *image_url_str, int image_url_length, int http_status_code, cef_image_t* image);

void CEF_CALLBACK cfx_download_image_callback_on_download_image_finished(cef_download_image_callback_t* self, const cef_string_t* image_url, int http_status_code, cef_image_t* image) {
    cfx_download_image_callback_on_download_image_finished_callback(((cfx_download_image_callback_t*)self)->gc_handle, image_url ? image_url->str : 0, image_url ? (int)image_url->length : 0, http_status_code, image);
}


static void cfx_download_image_callback_set_managed_callback(cef_download_image_callback_t* self, int index, void* callback) {
    switch(index) {
    case 0:
        if(callback && !cfx_download_image_callback_on_download_image_finished_callback)
            cfx_download_image_callback_on_download_image_finished_callback = (void (CEF_CALLBACK *)(gc_handle_t self, char16 *image_url_str, int image_url_length, int http_status_code, cef_image_t* image)) callback;
        self->on_download_image_finished = callback ? cfx_download_image_callback_on_download_image_finished : 0;
        break;
    }
}

