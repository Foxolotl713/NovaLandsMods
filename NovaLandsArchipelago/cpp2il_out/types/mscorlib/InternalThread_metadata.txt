Type: System.Threading.InternalThread:
	Base Class: 
		System.Runtime.ConstrainedExecution.CriticalFinalizerObject

	Interfaces:

	Field: lock_thread_id
		Type: System.Int32
		Offset in Defining Type: 0x10
		Has Default: False

	Field: handle
		Type: System.IntPtr
		Offset in Defining Type: 0x18
		Has Default: False

	Field: native_handle
		Type: System.IntPtr
		Offset in Defining Type: 0x20
		Has Default: False

	Field: name_chars
		Type: System.IntPtr
		Offset in Defining Type: 0x28
		Has Default: False

	Field: name_free
		Type: System.Int32
		Offset in Defining Type: 0x30
		Has Default: False

	Field: name_length
		Type: System.Int32
		Offset in Defining Type: 0x34
		Has Default: False

	Field: state
		Type: System.Threading.ThreadState
		Offset in Defining Type: 0x38
		Has Default: False

	Field: abort_exc
		Type: System.Object
		Offset in Defining Type: 0x40
		Has Default: False

	Field: abort_state_handle
		Type: System.Int32
		Offset in Defining Type: 0x48
		Has Default: False

	Field: thread_id
		Type: System.Int64
		Offset in Defining Type: 0x50
		Has Default: False

	Field: debugger_thread
		Type: System.IntPtr
		Offset in Defining Type: 0x58
		Has Default: False

	Field: static_data
		Type: System.UIntPtr
		Offset in Defining Type: 0x60
		Has Default: False

	Field: runtime_thread_info
		Type: System.IntPtr
		Offset in Defining Type: 0x68
		Has Default: False

	Field: current_appcontext
		Type: System.Object
		Offset in Defining Type: 0x70
		Has Default: False

	Field: root_domain_thread
		Type: System.Object
		Offset in Defining Type: 0x78
		Has Default: False

	Field: _serialized_principal
		Type: System.Byte[]
		Offset in Defining Type: 0x80
		Has Default: False

	Field: _serialized_principal_version
		Type: System.Int32
		Offset in Defining Type: 0x88
		Has Default: False

	Field: appdomain_refs
		Type: System.IntPtr
		Offset in Defining Type: 0x90
		Has Default: False

	Field: interruption_requested
		Type: System.Int32
		Offset in Defining Type: 0x98
		Has Default: False

	Field: longlived
		Type: System.IntPtr
		Offset in Defining Type: 0xA0
		Has Default: False

	Field: threadpool_thread
		Type: System.Boolean
		Offset in Defining Type: 0xA8
		Has Default: False

	Field: thread_interrupt_requested
		Type: System.Boolean
		Offset in Defining Type: 0xA9
		Has Default: False

	Field: stack_size
		Type: System.Int32
		Offset in Defining Type: 0xAC
		Has Default: False

	Field: apartment_state
		Type: System.Byte
		Offset in Defining Type: 0xB0
		Has Default: False

	Field: critical_region_level
		Type: System.Int32
		Offset in Defining Type: 0xB4
		Has Default: False

	Field: managed_id
		Type: System.Int32
		Offset in Defining Type: 0xB8
		Has Default: False

	Field: small_id
		Type: System.Int32
		Offset in Defining Type: 0xBC
		Has Default: False

	Field: manage_callback
		Type: System.IntPtr
		Offset in Defining Type: 0xC0
		Has Default: False

	Field: flags
		Type: System.IntPtr
		Offset in Defining Type: 0xC8
		Has Default: False

	Field: thread_pinning_ref
		Type: System.IntPtr
		Offset in Defining Type: 0xD0
		Has Default: False

	Field: abort_protected_block_count
		Type: System.IntPtr
		Offset in Defining Type: 0xD8
		Has Default: False

	Field: priority
		Type: System.Int32
		Offset in Defining Type: 0xE0
		Has Default: False

	Field: owned_mutex
		Type: System.IntPtr
		Offset in Defining Type: 0xE8
		Has Default: False

	Field: suspended_event
		Type: System.IntPtr
		Offset in Defining Type: 0xF0
		Has Default: False

	Field: self_suspended
		Type: System.Int32
		Offset in Defining Type: 0xF8
		Has Default: False

	Field: thread_state
		Type: System.IntPtr
		Offset in Defining Type: 0x100
		Has Default: False

	Field: netcore0
		Type: System.IntPtr
		Offset in Defining Type: 0x108
		Has Default: False

	Field: netcore1
		Type: System.IntPtr
		Offset in Defining Type: 0x110
		Has Default: False

	Field: netcore2
		Type: System.IntPtr
		Offset in Defining Type: 0x118
		Has Default: False

	Field: last
		Type: System.IntPtr
		Offset in Defining Type: 0x120
		Has Default: False

	Method: Thread_free_internal:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x0192AA10
		Ram Offset 0x18192bc10
		Virtual Method Slot: 65535

	Method: Finalize:
		Accessibility: Family
		Return Type: System.Void
		File Offset 0x0192AAB0
		Ram Offset 0x18192bcb0
		Virtual Method Slot: 1

	Method: .ctor:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x0192AB90
		Ram Offset 0x18192bd90
		Virtual Method Slot: 65535
