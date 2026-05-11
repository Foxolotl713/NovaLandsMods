Type: Internal.Runtime.Augments.RuntimeThread:
	Base Class: 
		System.Object

	Interfaces:

	Static Field: OptimalMaxSpinWaitsPerSpinIteration
		Type: System.Int32
		Offset in Defining Type: 0x0
		Has Default: False

	Field: thread
		Type: System.Threading.Thread
		Offset in Defining Type: 0x10
		Has Default: False

	Method: .ctor:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x00386E50
		Ram Offset 0x180388050
		Virtual Method Slot: 65535

		Parameter 0:
			Name: t
			Type: System.Threading.Thread
			Default Value: 
	Method: Create:
		Accessibility: Public
		Return Type: Internal.Runtime.Augments.RuntimeThread
		File Offset 0x016C5820
		Ram Offset 0x1816c6a20
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.Threading.ParameterizedThreadStart
			Default Value: 
		Parameter 1:
			Name: maxStackSize
			Type: System.Int32
			Default Value: 
	Method: set_IsBackground:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x016C59B0
		Ram Offset 0x1816c6bb0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Boolean
			Default Value: 
	Method: Start:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x016C59D0
		Ram Offset 0x1816c6bd0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: state
			Type: System.Object
			Default Value: 
	Method: Sleep:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x016C59F0
		Ram Offset 0x1816c6bf0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: millisecondsTimeout
			Type: System.Int32
			Default Value: 
	Method: Yield:
		Accessibility: Public
		Return Type: System.Boolean
		File Offset 0x016C5A00
		Ram Offset 0x1816c6c00
		Virtual Method Slot: 65535

	Method: SpinWait:
		Accessibility: Public
		Return Type: System.Boolean
		File Offset 0x003B0090
		Ram Offset 0x1803b1290
		Virtual Method Slot: 65535

		Parameter 0:
			Name: iterations
			Type: System.Int32
			Default Value: 
	Method: GetCurrentProcessorId:
		Accessibility: Public
		Return Type: System.Int32
		File Offset 0x00789EC0
		Ram Offset 0x18078b0c0
		Virtual Method Slot: 65535

	Method: .cctor:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x016C5A20
		Ram Offset 0x1816c6c20
		Virtual Method Slot: 65535
