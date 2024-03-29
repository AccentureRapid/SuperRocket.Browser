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


// cef_stream_writer

// CEF_EXPORT cef_stream_writer_t* cef_stream_writer_create_for_file(const cef_string_t* fileName);
static cef_stream_writer_t* cfx_stream_writer_create_for_file(char16 *fileName_str, int fileName_length) {
    cef_string_t fileName = { fileName_str, fileName_length, 0 };
    return cef_stream_writer_create_for_file(&fileName);
}
// CEF_EXPORT cef_stream_writer_t* cef_stream_writer_create_for_handler(cef_write_handler_t* handler);
static cef_stream_writer_t* cfx_stream_writer_create_for_handler(cef_write_handler_t* handler) {
    if(handler) ((cef_base_t*)handler)->add_ref((cef_base_t*)handler);
    return cef_stream_writer_create_for_handler(handler);
}
// write
static size_t cfx_stream_writer_write(cef_stream_writer_t* self, const void* ptr, size_t size, size_t n) {
    return self->write(self, ptr, size, n);
}

// seek
static int cfx_stream_writer_seek(cef_stream_writer_t* self, int64 offset, int whence) {
    return self->seek(self, offset, whence);
}

// tell
static int64 cfx_stream_writer_tell(cef_stream_writer_t* self) {
    return self->tell(self);
}

// flush
static int cfx_stream_writer_flush(cef_stream_writer_t* self) {
    return self->flush(self);
}

// may_block
static int cfx_stream_writer_may_block(cef_stream_writer_t* self) {
    return self->may_block(self);
}


