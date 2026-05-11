Type: System.Net.TimerThread:
	Base Class: 
		System.Object

	Interfaces:

	Nested Types:
		System.Net.TimerThread/Queue
		System.Net.TimerThread/Timer
		System.Net.TimerThread/Callback
		System.Net.TimerThread/TimerQueue
		System.Net.TimerThread/InfiniteTimerQueue
		System.Net.TimerThread/TimerNode
		System.Net.TimerThread/InfiniteTimer

	Static Field: s_Queues
		Type: System.Collections.Generic.LinkedList`1<System.WeakReference>
		Offset in Defining Type: 0x0
		Has Default: False

	Static Field: s_NewQueues
		Type: System.Collections.Generic.LinkedList`1<System.WeakReference>
		Offset in Defining Type: 0x8
		Has Default: False

	Static Field: s_ThreadState
		Type: System.Int32
		Offset in Defining Type: 0x10
		Has Default: False

	Static Field: s_ThreadReadyEvent
		Type: System.Threading.AutoResetEvent
		Offset in Defining Type: 0x18
		Has Default: False

	Static Field: s_ThreadShutdownEvent
		Type: System.Threading.ManualResetEvent
		Offset in Defining Type: 0x20
		Has Default: False

	Static Field: s_ThreadEvents
		Type: System.Threading.WaitHandle[]
		Offset in Defining Type: 0x28
		Has Default: False

	Static Field: s_CacheScanIteration
		Type: System.Int32
		Offset in Defining Type: 0x30
		Has Default: False

	Static Field: s_QueuesCache
		Type: System.Collections.Hashtable
		Offset in Defining Type: 0x38
		Has Default: False

	Method: .cctor:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01F49860
		Ram Offset 0x181f4aa60
		Virtual Method Slot: 65535

	Method: CreateQueue:
		Accessibility: Assembly
		Return Type: System.Net.TimerThread/Queue
		File Offset 0x01F49D40
		Ram Offset 0x181f4af40
		Virtual Method Slot: 65535

		Parameter 0:
			Name: durationMilliseconds
			Type: System.Int32
			Default Value: 
	Method: GetOrCreateQueue:
		Accessibility: Assembly
		Return Type: System.Net.TimerThread/Queue
		File Offset 0x01F4A010
		Ram Offset 0x181f4b210
		Virtual Method Slot: 65535

		Parameter 0:
			Name: durationMilliseconds
			Type: System.Int32
			Default Value: 
	Method: Prod:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01F4A970
		Ram Offset 0x181f4bb70
		Virtual Method Slot: 65535

	Method: ThreadProc:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01F4AB20
		Ram Offset 0x181f4bd20
		Virtual Method Slot: 65535

	Method: StopTimerThread:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01F4B3B0
		Ram Offset 0x181f4c5b0
		Virtual Method Slot: 65535

	Method: IsTickBetween:
		Accessibility: Private
		Return Type: System.Boolean
		File Offset 0x01F4B480
		Ram Offset 0x181f4c680
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.Int32
			Default Value: 
		Parameter 1:
			Name: end
			Type: System.Int32
			Default Value: 
		Parameter 2:
			Name: comparand
			Type: System.Int32
			Default Value: 
	Method: OnDomainUnload:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01F4B4A0
		Ram Offset 0x181f4c6a0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: sender
			Type: System.Object
			Default Value: 
		Parameter 1:
			Name: e
			Type: System.EventArgs
			Default Value: 