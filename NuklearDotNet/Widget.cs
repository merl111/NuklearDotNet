﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NuklearDotNet {
	[Flags]
	public enum nk_panel_flags {
		NK_WINDOW_BORDER = (1 << (0)),
		NK_WINDOW_MOVABLE = (1 << (1)),
		NK_WINDOW_SCALABLE = (1 << (2)),
		NK_WINDOW_CLOSABLE = (1 << (3)),
		NK_WINDOW_MINIMIZABLE = (1 << (4)),
		NK_WINDOW_NO_SCROLLBAR = (1 << (5)),
		NK_WINDOW_TITLE = (1 << (6)),
		NK_WINDOW_SCROLL_AUTO_HIDE = (1 << (7)),
		NK_WINDOW_BACKGROUND = (1 << (8)),
		NK_WINDOW_SCALE_LEFT = (1 << (9)),
		NK_WINDOW_NO_INPUT = (1 << (10))
	}

	[Flags]
	public enum nk_panel_type {
		NK_PANEL_WINDOW = (1 << (0)),
		NK_PANEL_GROUP = (1 << (1)),
		NK_PANEL_POPUP = (1 << (2)),
		NK_PANEL_CONTEXTUAL = (1 << (4)),
		NK_PANEL_COMBO = (1 << (5)),
		NK_PANEL_MENU = (1 << (6)),
		NK_PANEL_TOOLTIP = (1 << (7))
	}

	public enum nk_panel_set {
		NK_PANEL_SET_NONBLOCK = nk_panel_type.NK_PANEL_CONTEXTUAL | nk_panel_type.NK_PANEL_COMBO | nk_panel_type.NK_PANEL_MENU | nk_panel_type.NK_PANEL_TOOLTIP,
		NK_PANEL_SET_POPUP = NK_PANEL_SET_NONBLOCK | nk_panel_type.NK_PANEL_POPUP,
		NK_PANEL_SET_SUB = NK_PANEL_SET_POPUP | nk_panel_type.NK_PANEL_GROUP
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct nk_chart_slot {
		nk_chart_type type;
		nk_color color;
		nk_color highlight;
		float min;
		float max;
		float range;
		int count;
		nk_vec2 last;
		int index;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct nk_chart {
		int slot;
		float x;
		float y;
		float w;
		float h;
		nk_chart_slot slot0;
		nk_chart_slot slot1;
		nk_chart_slot slot2;
		nk_chart_slot slot3;
	}

	public enum nk_panel_row_layout_type {
		NK_LAYOUT_DYNAMIC_FIXED = 0,
		NK_LAYOUT_DYNAMIC_ROW,
		NK_LAYOUT_DYNAMIC_FREE,
		NK_LAYOUT_DYNAMIC,
		NK_LAYOUT_STATIC_FIXED,
		NK_LAYOUT_STATIC_ROW,
		NK_LAYOUT_STATIC_FREE,
		NK_LAYOUT_STATIC,
		NK_LAYOUT_TEMPLATE,
		NK_LAYOUT_COUNT
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_row_layout {
		nk_panel_row_layout_type type;
		int index;
		float height;
		float min_height;
		int columns;
		float* ratio;
		float item_width;
		float item_height;
		float item_offset;
		float filled;
		nk_rect item;
		int tree_depth;
		fixed float templates[16];
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct nk_popup_buffer {
		IntPtr begin_nksize;
		IntPtr parent_nksize;
		IntPtr last_nksize;
		IntPtr end_nksize;
		int active;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct nk_menu_state {
		float x;
		float y;
		float w;
		float h;
		nk_scroll offset;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_panel {
		nk_panel_type type;
		uint flags_nkflags;
		nk_rect bounds;
		uint* offset_x;
		uint* offset_y;
		float at_x;
		float at_y;
		float max_x;
		float footer_height;
		float header_height;
		float border;
		uint has_scrolling;
		nk_rect clip;
		nk_menu_state menu;
		nk_row_layout row;
		nk_chart chart;
		nk_command_buffer* buffer;
		nk_panel* parent;
	}

	[Flags]
	public enum nk_window_flags : int {
		NK_WINDOW_PRIVATE = (1 << (11)),
		NK_WINDOW_DYNAMIC = NK_WINDOW_PRIVATE,
		NK_WINDOW_ROM = (1 << (12)),
		NK_WINDOW_NOT_INTERACTIVE = NK_WINDOW_ROM | nk_panel_flags.NK_WINDOW_NO_INPUT,
		NK_WINDOW_HIDDEN = (1 << (13)),
		NK_WINDOW_CLOSED = (1 << (14)),
		NK_WINDOW_MINIMIZED = (1 << (15)),
		NK_WINDOW_REMOVE_ROM = (1 << (16))
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_popup_state {
		nk_window* win;
		nk_panel_type type;
		nk_popup_buffer buf;
		uint name_nkhash;
		int active;
		uint combo_count;
		uint con_count;
		uint con_old;
		uint active_con;
		nk_rect header;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct nk_edit_state {
		uint name_nkhash;
		uint seq;
		uint old;
		int active;
		int prev;
		int cursor;
		int sel_start;
		int sel_end;
		nk_scroll scrollbar;
		byte mode;
		byte single_line;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_property_state {
		int active;
		int prev;
		fixed byte buffer[64];
		int length;
		int cursor;
		int select_start;
		int select_end;
		uint name_nkhash;
		uint seq;
		uint old;
		int state;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_window {
		uint seq;
		uint name_nkhash;
		fixed byte name_string[64];
		uint flags_nkflags;

		nk_rect bounds;
		nk_scroll scrollbar;
		nk_command_buffer buffer;
		nk_panel* layout;
		float scrollbar_hiding_timer;

		nk_property_state property;
		nk_popup_state popup;
		nk_edit_state edit;
		uint scrolled;

		nk_table* tables;
		uint table_count;

		nk_window* next;
		nk_window* prev;
		nk_window* parent;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct nk_list_view {
		int begin;
		int end;
		int count;

		int total_height;
		nk_context* ctx;
		uint* scroll_pointer;
		uint scroll_value;
	}

	public enum nk_widget_layout_states {
		NK_WIDGET_INVALID,
		NK_WIDGET_VALID,
		NK_WIDGET_ROM
	}

	[Flags]
	public enum nk_widget_states {
		NK_WIDGET_STATE_MODIFIED = (1 << (1)),
		NK_WIDGET_STATE_INACTIVE = (1 << (2)),
		NK_WIDGET_STATE_ENTERED = (1 << (3)),
		NK_WIDGET_STATE_HOVER = (1 << (4)),
		NK_WIDGET_STATE_ACTIVED = (1 << (5)),
		NK_WIDGET_STATE_LEFT = (1 << (6)),
		NK_WIDGET_STATE_HOVERED = NK_WIDGET_STATE_HOVER | NK_WIDGET_STATE_MODIFIED,
		NK_WIDGET_STATE_ACTIVE = NK_WIDGET_STATE_ACTIVED | NK_WIDGET_STATE_MODIFIED
	}

	[Flags]
	public enum nk_text_align {
		NK_TEXT_ALIGN_LEFT = 0x01,
		NK_TEXT_ALIGN_CENTERED = 0x02,
		NK_TEXT_ALIGN_RIGHT = 0x04,
		NK_TEXT_ALIGN_TOP = 0x08,
		NK_TEXT_ALIGN_MIDDLE = 0x10,
		NK_TEXT_ALIGN_BOTTOM = 0x20
	}

	public enum nk_text_alignment {
		NK_TEXT_LEFT = nk_text_align.NK_TEXT_ALIGN_MIDDLE | nk_text_align.NK_TEXT_ALIGN_LEFT,
		NK_TEXT_CENTERED = nk_text_align.NK_TEXT_ALIGN_MIDDLE | nk_text_align.NK_TEXT_ALIGN_CENTERED,
		NK_TEXT_RIGHT = nk_text_align.NK_TEXT_ALIGN_MIDDLE | nk_text_align.NK_TEXT_ALIGN_RIGHT
	}

	[Flags]
	public enum nk_edit_flags {
		NK_EDIT_DEFAULT = 0,
		NK_EDIT_READ_ONLY = (1 << (0)),
		NK_EDIT_AUTO_SELECT = (1 << (1)),
		NK_EDIT_SIG_ENTER = (1 << (2)),
		NK_EDIT_ALLOW_TAB = (1 << (3)),
		NK_EDIT_NO_CURSOR = (1 << (4)),
		NK_EDIT_SELECTABLE = (1 << (5)),
		NK_EDIT_CLIPBOARD = (1 << (6)),
		NK_EDIT_CTRL_ENTER_NEWLINE = (1 << (7)),
		NK_EDIT_NO_HORIZONTAL_SCROLL = (1 << (8)),
		NK_EDIT_ALWAYS_INSERT_MODE = (1 << (9)),
		NK_EDIT_MULTILINE = (1 << (10)),
		NK_EDIT_GOTO_END_ON_ACTIVATE = (1 << (11))
	}

	public enum nk_edit_types {
		NK_EDIT_SIMPLE = nk_edit_flags.NK_EDIT_ALWAYS_INSERT_MODE,
		NK_EDIT_FIELD = NK_EDIT_SIMPLE | nk_edit_flags.NK_EDIT_SELECTABLE | nk_edit_flags.NK_EDIT_CLIPBOARD,
		NK_EDIT_BOX = nk_edit_flags.NK_EDIT_ALWAYS_INSERT_MODE | nk_edit_flags.NK_EDIT_SELECTABLE | nk_edit_flags.NK_EDIT_MULTILINE | nk_edit_flags.NK_EDIT_ALLOW_TAB | nk_edit_flags.NK_EDIT_CLIPBOARD,
		NK_EDIT_EDITOR = nk_edit_flags.NK_EDIT_SELECTABLE | nk_edit_flags.NK_EDIT_MULTILINE | nk_edit_flags.NK_EDIT_ALLOW_TAB | nk_edit_flags.NK_EDIT_CLIPBOARD
	}

	[Flags]
	public enum nk_edit_events {
		NK_EDIT_ACTIVE = (1 << (0)),
		NK_EDIT_INACTIVE = (1 << (1)),
		NK_EDIT_ACTIVATED = (1 << (2)),
		NK_EDIT_DEACTIVATED = (1 << (3)),
		NK_EDIT_COMMITED = (1 << (4))
	}

	public delegate float nk_value_getter_fun(IntPtr user, int index);

	public unsafe delegate void nk_item_getter_fun(IntPtr user, int i, byte** idk);

	// [DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
	public static unsafe partial class Nuklear {
		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_window* nk_window_find(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_rect nk_window_get_bounds(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_window_get_position(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_window_get_size(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_window_get_width(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_window_get_height(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_panel* nk_window_get_panel(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_rect nk_window_get_content_region(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_window_get_content_region_min(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_window_get_content_region_max(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_window_get_content_region_size(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_command_buffer* nk_window_get_canvas(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_has_focus(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_collapsed(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_closed(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_hidden(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_active(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_hovered(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_window_is_any_hovered(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_item_is_any_active(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_set_bounds(nk_context* ctx, byte* name, nk_rect bounds);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_set_position(nk_context* ctx, byte* name, nk_vec2 pos);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_set_size(nk_context* ctx, byte* name, nk_vec2 sz);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_set_focus(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_close(nk_context* ctx, byte* name);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_collapse(nk_context* ctx, byte* name, nk_collapse_states state);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_collapse_if(nk_context* ctx, byte* name, nk_collapse_states state, int cond);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_show(nk_context* ctx, byte* name, nk_show_states state);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_window_show_if(nk_context* ctx, byte* name, nk_show_states state, int cond);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_group_begin(nk_context* ctx, byte* title, uint nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_group_scrolled_offset_begin(nk_context* ctx, uint* x_offset, uint* y_offset, byte* s, uint nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_group_scrolled_begin(nk_context* ctx, nk_scroll* scroll, byte* title, uint nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_group_scrolled_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_group_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_list_view_begin(nk_context* ctx, nk_list_view* nlv_out, byte* id, uint nkflags, int row_height, int row_count);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_list_view_end(nk_list_view* nlv);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_tree_push_hashed(nk_context* ctx, nk_tree_type tree_type, byte* title, nk_collapse_states initial_state, byte* hash, int len, int seed);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_tree_image_push_hashed(nk_context* ctx, nk_tree_type tree_type, nk_image img, byte* title, nk_collapse_states initial_state, byte* hash, int len, int seed);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_tree_pop(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_tree_state_push(nk_context* ctx, nk_tree_type tree_type, byte* title, nk_collapse_states* state);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_tree_state_image_push(nk_context* ctx, nk_tree_type tree_type, nk_image img, byte* title, nk_collapse_states* state);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_tree_state_pop(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_widget_layout_states nk_widget(nk_rect* r, nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_widget_layout_states nk_widget_fitting(nk_rect* r, nk_context* ctx, nk_vec2 v);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_rect nk_widget_bounds(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_widget_position(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_vec2 nk_widget_size(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_widget_width(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_widget_height(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_widget_is_hovered(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_widget_is_mouse_clicked(nk_context* ctx, nk_buttons buttons);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_widget_has_mouse_click_down(nk_context* ctx, nk_buttons buttons, int down);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_spacing(nk_context* ctx, int cols);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_text(nk_context* ctx, byte* s, int i, uint flags_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_text_colored(nk_context* ctx, byte* s, int i, uint flags_nkflags, nk_color color);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_text_wrap(nk_context* ctx, byte* s, int i);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_text_wrap_colored(nk_context* ctx, byte* s, int i, nk_color color);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_label(nk_context* ctx, byte* s, uint align_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_label_colored(nk_context* ctx, byte* s, uint align_nkflags, nk_color color);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_label_wrap(nk_context* ctx, byte* s);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_label_colored_wrap(nk_context* ctx, byte* s, nk_color color);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_image(nk_context* ctx, nk_image img);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_text(nk_context* ctx, byte* title, int len);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_label(nk_context* ctx, byte* title);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_label(nk_context* ctx, string title);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_color(nk_context* ctx, nk_color color);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol(nk_context* ctx, nk_symbol_type symtype);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image(nk_context* ctx, nk_image img);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol_label(nk_context* ctx, nk_symbol_type stype, byte* s, uint text_alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol_text(nk_context* ctx, nk_symbol_type stype, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image_label(nk_context* ctx, nk_image img, byte* s, uint text_alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image_text(nk_context* ctx, nk_image img, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_text_styled(nk_context* ctx, nk_style_button* bstyle, byte* title, int len);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_label_styled(nk_context* ctx, nk_style_button* bstyle, byte* title);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol_styled(nk_context* ctx, nk_style_button* bstyle, nk_symbol_type stype);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image_styled(nk_context* ctx, nk_style_button* bstyle, nk_image img);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol_text_styled(nk_context* ctx, nk_style_button* bstyle, nk_symbol_type stype, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_symbol_label_styled(nk_context* ctx, nk_style_button* bstyle, nk_symbol_type stype, byte* title, uint align_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image_label_styled(nk_context* ctx, nk_style_button* bstyle, nk_image img, byte* s, uint text_alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_image_text_styled(nk_context* ctx, nk_style_button* bstyle, nk_image img, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_button_set_behavior(nk_context* ctx, nk_button_behavior behavior);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_push_behavior(nk_context* ctx, nk_button_behavior behavior);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_button_pop_behavior(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_check_label(nk_context* ctx, byte* s, int active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_check_text(nk_context* ctx, byte* s, int i, int active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_check_flags_label(nk_context* ctx, byte* s, uint flags, uint val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_check_flags_text(nk_context* ctx, byte* s, int i, uint flags, uint val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_checkbox_label(nk_context* ctx, byte* s, int* active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_checkbox_text(nk_context* ctx, byte* s, int i, int* active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_checkbox_flags_label(nk_context* ctx, byte* s, uint* flags, uint val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_checkbox_flags_text(nk_context* ctx, byte* s, int i, uint* flags, uint val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_radio_label(nk_context* ctx, byte* s, int* active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_radio_text(nk_context* ctx, byte* s, int i, int* active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_option_label(nk_context* ctx, byte* s, int active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_option_text(nk_context* ctx, byte* s, int i, int active);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_selectable_label(nk_context* ctx, byte* s, uint align_nkflags, int* val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_selectable_text(nk_context* ctx, byte* s, int i, uint align_nkflags, int* val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_selectable_image_label(nk_context* ctx, nk_image img, byte* s, uint align_nkflags, int* val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_selectable_image_text(nk_context* ctx, nk_image img, byte* s, int i, uint align_nkflags, int* val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_select_label(nk_context* ctx, byte* s, uint align_nkflags, int val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_select_text(nk_context* ctx, byte* s, int i, uint align_nkflags, int val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_select_image_label(nk_context* ctx, nk_image img, byte* s, uint align_nkflags, int val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_select_image_text(nk_context* ctx, nk_image img, byte* s, int i, uint align_nkflags, int val);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_slide_float(nk_context* ctx, float min, float val, float max, float step);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_slide_int(nk_context* ctx, int min, int val, int max, int step);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_slider_float(nk_context* ctx, float min, float* val, float max, float step);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_slider_int(nk_context* ctx, int min, int* val, int max, int step);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_progress(nk_context* ctx, IntPtr* cur_nksize, IntPtr max_nksize, int modifyable);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern IntPtr nk_prog(nk_context* ctx, IntPtr cur_nksize, IntPtr max_nksize, int modifyable);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern nk_color nk_color_picker(nk_context* ctx, nk_color color, nk_color_format cfmt);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_color_pick(nk_context* ctx, nk_color* color, nk_color_format cfmt);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_property_int(nk_context* ctx, byte* name, int min, int* val, int max, int step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_property_float(nk_context* ctx, byte* name, float min, float* val, float max, float step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_property_double(nk_context* ctx, byte* name, double min, double* val, double max, double step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_propertyi(nk_context* ctx, byte* name, int min, int val, int max, int step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern float nk_propertyf(nk_context* ctx, byte* name, float min, float val, float max, float step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern double nk_propertyd(nk_context* ctx, byte* name, double min, double val, double max, double step, float inc_per_pixel);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_edit_string(nk_context* ctx, uint flags_nkflags, byte* buffer, int* len, int max, nk_plugin_filter_t filterfun);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_edit_string_zero_terminated(nk_context* ctx, uint flags_nkflags, byte* buffer, int max, nk_plugin_filter_t filterfun);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_edit_buffer(nk_context* ctx, uint flags_nkflags, nk_text_edit* textedit, nk_plugin_filter_t filterfun);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_edit_focus(nk_context* ctx, uint flags_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_edit_unfocus(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_chart_begin(nk_context* ctx, nk_chart_type chatype, int num, float min, float max);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_chart_begin_colored(nk_context* ctx, nk_chart_type chatype, nk_color color, nk_color active, int num, float min, float max);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_chart_add_slot(nk_context* ctx, nk_chart_type chatype, int count, float min_value, float max_value);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_chart_add_slot_colored(nk_context* ctx, nk_chart_type chatype, nk_color color, nk_color active, int count, float min_value, float max_value);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_chart_push(nk_context* ctx, float f);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern uint nk_chart_push_slot(nk_context* ctx, float f, int i);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_chart_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_plot(nk_context* ctx, nk_chart_type chatype, float* values, int count, int offset);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_plot_function(nk_context* ctx, nk_chart_type chatype, IntPtr userdata, nk_value_getter_fun getterfun, int count, int offset);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_popup_begin(nk_context* ctx, nk_popup_type type, byte* s, uint flags_nkflags, nk_rect bounds);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_popup_close(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_popup_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo(nk_context* ctx, byte** items, int count, int selected, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_separator(nk_context* ctx, byte* items_separated_by_separator, int separator, int selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_string(nk_context* ctx, byte* items_separated_by_zeros, int selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_callback(nk_context* ctx, nk_item_getter_fun getterfun, IntPtr userdata, int selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combobox(nk_context* ctx, byte** items, int count, int* selected, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combobox_string(nk_context* ctx, byte* items_separated_by_zeros, int* selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combobox_separator(nk_context* ctx, byte* items_separated_by_separator, int separator, int* selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combobox_callback(nk_context* ctx, nk_item_getter_fun getterfun, IntPtr userdata, int* selected, int count, int item_height, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_text(nk_context* ctx, char* selected, int i, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_label(nk_context* ctx, char* selected, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_color(nk_context* ctx, nk_color color, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_symbol(nk_context* ctx, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_symbol_label(nk_context* ctx, char* selected, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_symbol_text(nk_context* ctx, char* selected, int i, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_image(nk_context* ctx, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_image_label(nk_context* ctx, char* selected, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_begin_image_text(nk_context* ctx, char* selected, int i, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_label(nk_context* ctx, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_text(nk_context* ctx, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_image_label(nk_context* ctx, nk_image img, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_image_text(nk_context* ctx, nk_image img, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_symbol_label(nk_context* ctx, nk_symbol_type stype, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_combo_item_symbol_text(nk_context* ctx, nk_symbol_type stype, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combo_close(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_combo_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_begin(nk_context* ctx, uint flags_nkflags, nk_vec2 v, nk_rect trigger_bounds);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_text(nk_context* ctx, byte* s, int i, uint align_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_label(nk_context* ctx, byte* s, uint align_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_image_label(nk_context* ctx, nk_image img, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_image_text(nk_context* ctx, nk_image img, byte* s, int len, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_symbol_label(nk_context* ctx, nk_symbol_type stype, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_contextual_item_symbol_text(nk_context* ctx, nk_symbol_type stype, byte* s, int i, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_contextual_close(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_contextual_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_tooltip(nk_context* ctx, byte* s);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_tooltip_begin(nk_context* ctx, float width);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_tooltip_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_menubar_begin(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_menubar_end(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_text(nk_context* ctx, byte* title, int title_len, uint align_nkflags, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_label(nk_context* ctx, byte* s, uint align_nkflags, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_image(nk_context* ctx, byte* s, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_image_text(nk_context* ctx, byte* s, int slen, uint align_nkflags, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_image_label(nk_context* ctx, byte* s, uint align_nkflags, nk_image img, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_symbol(nk_context* ctx, byte* s, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_symbol_text(nk_context* ctx, byte* s, int slen, uint align_nkflags, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_begin_symbol_label(nk_context* ctx, byte* s, uint align_nkflags, nk_symbol_type stype, nk_vec2 size);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_text(nk_context* ctx, byte* s, int slen, uint align_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_label(nk_context* ctx, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_image_label(nk_context* ctx, nk_image img, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_image_text(nk_context* ctx, nk_image img, byte* s, int slen, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_symbol_text(nk_context* ctx, nk_symbol_type stype, byte* s, int slen, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern int nk_menu_item_symbol_label(nk_context* ctx, nk_symbol_type stype, byte* s, uint alignment_nkflags);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_menu_close(nk_context* ctx);

		[DllImport(DllName, CallingConvention = CConv, CharSet = CSet)]
		public static extern void nk_menu_end(nk_context* ctx);
	}
}