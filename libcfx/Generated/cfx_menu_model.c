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


// cef_menu_model

// CEF_EXPORT cef_menu_model_t* cef_menu_model_create(cef_menu_model_delegate_t* delegate);
static cef_menu_model_t* cfx_menu_model_create(cef_menu_model_delegate_t* delegate) {
    if(delegate) ((cef_base_t*)delegate)->add_ref((cef_base_t*)delegate);
    return cef_menu_model_create(delegate);
}
// clear
static int cfx_menu_model_clear(cef_menu_model_t* self) {
    return self->clear(self);
}

// get_count
static int cfx_menu_model_get_count(cef_menu_model_t* self) {
    return self->get_count(self);
}

// add_separator
static int cfx_menu_model_add_separator(cef_menu_model_t* self) {
    return self->add_separator(self);
}

// add_item
static int cfx_menu_model_add_item(cef_menu_model_t* self, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->add_item(self, command_id, &label);
}

// add_check_item
static int cfx_menu_model_add_check_item(cef_menu_model_t* self, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->add_check_item(self, command_id, &label);
}

// add_radio_item
static int cfx_menu_model_add_radio_item(cef_menu_model_t* self, int command_id, char16 *label_str, int label_length, int group_id) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->add_radio_item(self, command_id, &label, group_id);
}

// add_sub_menu
static cef_menu_model_t* cfx_menu_model_add_sub_menu(cef_menu_model_t* self, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->add_sub_menu(self, command_id, &label);
}

// insert_separator_at
static int cfx_menu_model_insert_separator_at(cef_menu_model_t* self, int index) {
    return self->insert_separator_at(self, index);
}

// insert_item_at
static int cfx_menu_model_insert_item_at(cef_menu_model_t* self, int index, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->insert_item_at(self, index, command_id, &label);
}

// insert_check_item_at
static int cfx_menu_model_insert_check_item_at(cef_menu_model_t* self, int index, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->insert_check_item_at(self, index, command_id, &label);
}

// insert_radio_item_at
static int cfx_menu_model_insert_radio_item_at(cef_menu_model_t* self, int index, int command_id, char16 *label_str, int label_length, int group_id) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->insert_radio_item_at(self, index, command_id, &label, group_id);
}

// insert_sub_menu_at
static cef_menu_model_t* cfx_menu_model_insert_sub_menu_at(cef_menu_model_t* self, int index, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->insert_sub_menu_at(self, index, command_id, &label);
}

// remove
static int cfx_menu_model_remove(cef_menu_model_t* self, int command_id) {
    return self->remove(self, command_id);
}

// remove_at
static int cfx_menu_model_remove_at(cef_menu_model_t* self, int index) {
    return self->remove_at(self, index);
}

// get_index_of
static int cfx_menu_model_get_index_of(cef_menu_model_t* self, int command_id) {
    return self->get_index_of(self, command_id);
}

// get_command_id_at
static int cfx_menu_model_get_command_id_at(cef_menu_model_t* self, int index) {
    return self->get_command_id_at(self, index);
}

// set_command_id_at
static int cfx_menu_model_set_command_id_at(cef_menu_model_t* self, int index, int command_id) {
    return self->set_command_id_at(self, index, command_id);
}

// get_label
static cef_string_userfree_t cfx_menu_model_get_label(cef_menu_model_t* self, int command_id) {
    return self->get_label(self, command_id);
}

// get_label_at
static cef_string_userfree_t cfx_menu_model_get_label_at(cef_menu_model_t* self, int index) {
    return self->get_label_at(self, index);
}

// set_label
static int cfx_menu_model_set_label(cef_menu_model_t* self, int command_id, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->set_label(self, command_id, &label);
}

// set_label_at
static int cfx_menu_model_set_label_at(cef_menu_model_t* self, int index, char16 *label_str, int label_length) {
    cef_string_t label = { label_str, label_length, 0 };
    return self->set_label_at(self, index, &label);
}

// get_type
static cef_menu_item_type_t cfx_menu_model_get_type(cef_menu_model_t* self, int command_id) {
    return self->get_type(self, command_id);
}

// get_type_at
static cef_menu_item_type_t cfx_menu_model_get_type_at(cef_menu_model_t* self, int index) {
    return self->get_type_at(self, index);
}

// get_group_id
static int cfx_menu_model_get_group_id(cef_menu_model_t* self, int command_id) {
    return self->get_group_id(self, command_id);
}

// get_group_id_at
static int cfx_menu_model_get_group_id_at(cef_menu_model_t* self, int index) {
    return self->get_group_id_at(self, index);
}

// set_group_id
static int cfx_menu_model_set_group_id(cef_menu_model_t* self, int command_id, int group_id) {
    return self->set_group_id(self, command_id, group_id);
}

// set_group_id_at
static int cfx_menu_model_set_group_id_at(cef_menu_model_t* self, int index, int group_id) {
    return self->set_group_id_at(self, index, group_id);
}

// get_sub_menu
static cef_menu_model_t* cfx_menu_model_get_sub_menu(cef_menu_model_t* self, int command_id) {
    return self->get_sub_menu(self, command_id);
}

// get_sub_menu_at
static cef_menu_model_t* cfx_menu_model_get_sub_menu_at(cef_menu_model_t* self, int index) {
    return self->get_sub_menu_at(self, index);
}

// is_visible
static int cfx_menu_model_is_visible(cef_menu_model_t* self, int command_id) {
    return self->is_visible(self, command_id);
}

// is_visible_at
static int cfx_menu_model_is_visible_at(cef_menu_model_t* self, int index) {
    return self->is_visible_at(self, index);
}

// set_visible
static int cfx_menu_model_set_visible(cef_menu_model_t* self, int command_id, int visible) {
    return self->set_visible(self, command_id, visible);
}

// set_visible_at
static int cfx_menu_model_set_visible_at(cef_menu_model_t* self, int index, int visible) {
    return self->set_visible_at(self, index, visible);
}

// is_enabled
static int cfx_menu_model_is_enabled(cef_menu_model_t* self, int command_id) {
    return self->is_enabled(self, command_id);
}

// is_enabled_at
static int cfx_menu_model_is_enabled_at(cef_menu_model_t* self, int index) {
    return self->is_enabled_at(self, index);
}

// set_enabled
static int cfx_menu_model_set_enabled(cef_menu_model_t* self, int command_id, int enabled) {
    return self->set_enabled(self, command_id, enabled);
}

// set_enabled_at
static int cfx_menu_model_set_enabled_at(cef_menu_model_t* self, int index, int enabled) {
    return self->set_enabled_at(self, index, enabled);
}

// is_checked
static int cfx_menu_model_is_checked(cef_menu_model_t* self, int command_id) {
    return self->is_checked(self, command_id);
}

// is_checked_at
static int cfx_menu_model_is_checked_at(cef_menu_model_t* self, int index) {
    return self->is_checked_at(self, index);
}

// set_checked
static int cfx_menu_model_set_checked(cef_menu_model_t* self, int command_id, int checked) {
    return self->set_checked(self, command_id, checked);
}

// set_checked_at
static int cfx_menu_model_set_checked_at(cef_menu_model_t* self, int index, int checked) {
    return self->set_checked_at(self, index, checked);
}

// has_accelerator
static int cfx_menu_model_has_accelerator(cef_menu_model_t* self, int command_id) {
    return self->has_accelerator(self, command_id);
}

// has_accelerator_at
static int cfx_menu_model_has_accelerator_at(cef_menu_model_t* self, int index) {
    return self->has_accelerator_at(self, index);
}

// set_accelerator
static int cfx_menu_model_set_accelerator(cef_menu_model_t* self, int command_id, int key_code, int shift_pressed, int ctrl_pressed, int alt_pressed) {
    return self->set_accelerator(self, command_id, key_code, shift_pressed, ctrl_pressed, alt_pressed);
}

// set_accelerator_at
static int cfx_menu_model_set_accelerator_at(cef_menu_model_t* self, int index, int key_code, int shift_pressed, int ctrl_pressed, int alt_pressed) {
    return self->set_accelerator_at(self, index, key_code, shift_pressed, ctrl_pressed, alt_pressed);
}

// remove_accelerator
static int cfx_menu_model_remove_accelerator(cef_menu_model_t* self, int command_id) {
    return self->remove_accelerator(self, command_id);
}

// remove_accelerator_at
static int cfx_menu_model_remove_accelerator_at(cef_menu_model_t* self, int index) {
    return self->remove_accelerator_at(self, index);
}

// get_accelerator
static int cfx_menu_model_get_accelerator(cef_menu_model_t* self, int command_id, int* key_code, int* shift_pressed, int* ctrl_pressed, int* alt_pressed) {
    return self->get_accelerator(self, command_id, key_code, shift_pressed, ctrl_pressed, alt_pressed);
}

// get_accelerator_at
static int cfx_menu_model_get_accelerator_at(cef_menu_model_t* self, int index, int* key_code, int* shift_pressed, int* ctrl_pressed, int* alt_pressed) {
    return self->get_accelerator_at(self, index, key_code, shift_pressed, ctrl_pressed, alt_pressed);
}


