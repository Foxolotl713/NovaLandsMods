Type: System.Threading.Thread:
	Base Class: 
		System.Runtime.ConstrainedExecution.CriticalFinalizerObject

	Interfaces:

	Static Field: s_LocalDataStoreMgr
		Type: System.LocalDataStoreMgr
		Offset in Defining Type: 0x0
		Has Default: False

	Static Field: s_asyncLocalCurrentCulture
		Type: System.Threading.AsyncLocal`1<System.Globalization.CultureInfo>
		Offset in Defining Type: 0x8
		Has Default: False

	Static Field: s_asyncLocalCurrentUICulture
		Type: System.Threading.AsyncLocal`1<System.Globalization.CultureInfo>
		Offset in Defining Type: 0x10
		Has Default: False

	Field: internal_thread
		Type: System.Threading.InternalThread
		Offset in Defining Type: 0x10
		Has Default: False

	Field: m_ThreadStartArg
		Type: System.Object
		Offset in Defining Type: 0x18
		Has Default: False

	Field: pending_exception
		Type: System.Object
		Offset in Defining Type: 0x20
		Has Default: False

	Field: m_Delegate
		Type: System.MulticastDelegate
		Offset in Defining Type: 0x28
		Has Default: False

	Field: m_ExecutionContext
		Type: System.Threading.ExecutionContext
		Offset in Defining Type: 0x30
		Has Default: False

	Field: m_ExecutionContextBelongsToOuterScope
		Type: System.Boolean
		Offset in Defining Type: 0x38
		Has Default: False

	Field: principal
		Type: System.Security.Principal.IPrincipal
		Offset in Defining Type: 0x40
		Has Default: False

	Field: principal_version
		Type: System.Int32
		Offset in Defining Type: 0x48
		Has Default: False

	Static Field: s_LocalDataStore
		Type: System.LocalDataStoreHolder
		Offset in Defining Type: 0xFFFFFFFF80000000
		Has Default: False

	Static Field: m_CurrentCulture
		Type: System.Globalization.CultureInfo
		Offset in Defining Type: 0xFFFFFFFF80000008
		Has Default: False

	Static Field: m_CurrentUICulture
		Type: System.Globalization.CultureInfo
		Offset in Defining Type: 0xFFFFFFFF80000010
		Has Default: False

	Static Field: current_thread
		Type: System.Threading.Thread
		Offset in Defining Type: 0xFFFFFFFF80000018
		Has Default: False

	Method: AsyncLocalSetCurrentCulture:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01920F80
		Ram Offset 0x181922180
		Virtual Method Slot: 65535

		Parameter 0:
			Name: args
			Type: System.Threading.AsyncLocalValueChangedArgs`1<System.Globalization.CultureInfo>
			Default Value: 
	Method: .ctor:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01921090
		Ram Offset 0x181922290
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.Threading.ThreadStart
			Default Value: 
	Method: .ctor:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01921100
		Ram Offset 0x181922300
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.Threading.ParameterizedThreadStart
			Default Value: 
		Parameter 1:
			Name: maxStackSize
			Type: System.Int32
			Default Value: 
	Method: Start:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x019211E0
		Ram Offset 0x1819223e0
		Virtual Method Slot: 65535

	Method: Start:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01921200
		Ram Offset 0x181922400
		Virtual Method Slot: 65535

		Parameter 0:
			Name: parameter
			Type: System.Object
			Default Value: 
	Method: Start:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01921310
		Ram Offset 0x181922510
		Virtual Method Slot: 65535

		Parameter 0:
			Name: stackMark
			Type: System.Threading.StackCrawlMark
			Default Value: 
	Method: GetExecutionContextReader:
		Accessibility: Assembly
		Return Type: System.Threading.ExecutionContext/Reader
		File Offset 0x01921510
		Ram Offset 0x181922710
		Virtual Method Slot: 65535

	Method: get_ExecutionContextBelongsToCurrentScope:
		Accessibility: Assembly
		Return Type: System.Boolean
		File Offset 0x01921570
		Ram Offset 0x181922770
		Virtual Method Slot: 65535

	Method: set_ExecutionContextBelongsToCurrentScope:
		Accessibility: Assembly
		Return Type: System.Void
		File Offset 0x01921580
		Ram Offset 0x181922780
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Boolean
			Default Value: 
	Method: GetMutableExecutionContext:
		Accessibility: Assembly
		Return Type: System.Threading.ExecutionContext
		File Offset 0x01921590
		Ram Offset 0x181922790
		Virtual Method Slot: 65535

	Method: SetExecutionContext:
		Accessibility: Assembly
		Return Type: System.Void
		File Offset 0x019216B0
		Ram Offset 0x1819228b0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Threading.ExecutionContext
			Default Value: 
		Parameter 1:
			Name: belongsToCurrentScope
			Type: System.Boolean
			Default Value: 
	Method: SetExecutionContext:
		Accessibility: Assembly
		Return Type: System.Void
		File Offset 0x019216B0
		Ram Offset 0x1819228b0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Threading.ExecutionContext/Reader
			Default Value: 
		Parameter 1:
			Name: belongsToCurrentScope
			Type: System.Boolean
			Default Value: 
	Method: SleepInternal:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01921710
		Ram Offset 0x181922910
		Virtual Method Slot: 65535

		Parameter 0:
			Name: millisecondsTimeout
			Type: System.Int32
			Default Value: 
	Method: Sleep:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x019217A0
		Ram Offset 0x1819229a0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: millisecondsTimeout
			Type: System.Int32
			Default Value: 
	Method: YieldInternal:
		Accessibility: Private
		Return Type: System.Boolean
		File Offset 0x016C5A00
		Ram Offset 0x1816c6c00
		Virtual Method Slot: 65535

	Method: Yield:
		Accessibility: Public
		Return Type: System.Boolean
		File Offset 0x016C5A00
		Ram Offset 0x1816c6c00
		Virtual Method Slot: 65535

	Method: SetStartHelper:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x019218A0
		Ram Offset 0x181922aa0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.Delegate
			Default Value: 
		Parameter 1:
			Name: maxStackSize
			Type: System.Int32
			Default Value: 
	Method: get_CurrentUICulture:
		Accessibility: Public
		Return Type: System.Globalization.CultureInfo
		File Offset 0x01921A60
		Ram Offset 0x181922c60
		Virtual Method Slot: 65535

	Method: GetCurrentUICultureNoAppX:
		Accessibility: Assembly
		Return Type: System.Globalization.CultureInfo
		File Offset 0x01921AA0
		Ram Offset 0x181922ca0
		Virtual Method Slot: 65535

	Method: get_CurrentCulture:
		Accessibility: Public
		Return Type: System.Globalization.CultureInfo
		File Offset 0x01921C40
		Ram Offset 0x181922e40
		Virtual Method Slot: 65535

	Method: set_CurrentCulture:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01921C80
		Ram Offset 0x181922e80
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Globalization.CultureInfo
			Default Value: 
	Method: GetCurrentCultureNoAppX:
		Accessibility: Private
		Return Type: System.Globalization.CultureInfo
		File Offset 0x01922000
		Ram Offset 0x181923200
		Virtual Method Slot: 65535

	Method: nativeInitCultureAccessors:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01922170
		Ram Offset 0x181923370
		Virtual Method Slot: 65535

	Method: MemoryBarrier:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x019223B0
		Ram Offset 0x1819235b0
		Virtual Method Slot: 65535

	Method: ConstructInternalThread:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x019223C0
		Ram Offset 0x1819235c0
		Virtual Method Slot: 65535

	Method: get_Internal:
		Accessibility: Private
		Return Type: System.Threading.InternalThread
		File Offset 0x01922400
		Ram Offset 0x181923600
		Virtual Method Slot: 65535

	Method: get_CurrentContext:
		Accessibility: Public
		Return Type: System.Runtime.Remoting.Contexts.Context
		File Offset 0x0174C000
		Ram Offset 0x18174d200
		Virtual Method Slot: 65535

	Method: GetCurrentThread_icall:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01922460
		Ram Offset 0x181923660
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.Thread
			Default Value: 
	Method: GetCurrentThread:
		Accessibility: Private
		Return Type: System.Threading.Thread
		File Offset 0x019224E0
		Ram Offset 0x1819236e0
		Virtual Method Slot: 65535

	Method: get_CurrentThread:
		Accessibility: Public
		Return Type: System.Threading.Thread
		File Offset 0x01922570
		Ram Offset 0x181923770
		Virtual Method Slot: 65535

	Method: get_CurrentThreadId:
		Accessibility: Assembly
		Return Type: System.Int32
		File Offset 0x018F06F0
		Ram Offset 0x1818f18f0
		Virtual Method Slot: 65535

	Method: GetDomainID:
		Accessibility: Public
		Return Type: System.Int32
		File Offset 0x018F06D0
		Ram Offset 0x1818f18d0
		Virtual Method Slot: 65535

	Method: Thread_internal:
		Accessibility: Private
		Return Type: System.Boolean
		File Offset 0x01922660
		Ram Offset 0x181923860
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.MulticastDelegate
			Default Value: 
	Method: Finalize:
		Accessibility: Family
		Return Type: System.Void
		File Offset 0x01922670
		Ram Offset 0x181923870
		Virtual Method Slot: 1

	Method: get_IsThreadPoolThread:
		Accessibility: Public
		Return Type: System.Boolean
		File Offset 0x019226B0
		Ram Offset 0x1819238b0
		Virtual Method Slot: 65535

	Method: get_IsThreadPoolThreadInternal:
		Accessibility: Assembly
		Return Type: System.Boolean
		File Offset 0x019226B0
		Ram Offset 0x1819238b0
		Virtual Method Slot: 65535

	Method: get_IsAlive:
		Accessibility: Public
		Return Type: System.Boolean
		File Offset 0x01922710
		Ram Offset 0x181923910
		Virtual Method Slot: 65535

	Method: set_IsBackground:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01922770
		Ram Offset 0x181923970
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.Boolean
			Default Value: 
	Method: SetName_icall:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x019228E0
		Ram Offset 0x181923ae0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.InternalThread
			Default Value: 
		Parameter 1:
			Name: name
			Type: System.Char*
			Default Value: 
		Parameter 2:
			Name: nameLength
			Type: System.Int32
			Default Value: 
	Method: SetName_internal:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x019228F0
		Ram Offset 0x181923af0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.InternalThread
			Default Value: 
		Parameter 1:
			Name: name
			Type: System.String
			Default Value: 
	Method: set_Name:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01922920
		Ram Offset 0x181923b20
		Virtual Method Slot: 65535

		Parameter 0:
			Name: value
			Type: System.String
			Default Value: 
	Method: SpinWait_nop:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x00379480
		Ram Offset 0x18037a680
		Virtual Method Slot: 65535

	Method: SpinWait:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x019229A0
		Ram Offset 0x181923ba0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: iterations
			Type: System.Int32
			Default Value: 
	Method: StartInternal:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x019229B0
		Ram Offset 0x181923bb0
		Virtual Method Slot: 65535

		Parameter 0:
			Name: principal
			Type: System.Object
			Default Value: 
		Parameter 1:
			Name: stackMark
			Type: System.Threading.StackCrawlMark
			Default Value: 
	Method: SetState:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01922A60
		Ram Offset 0x181923c60
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.InternalThread
			Default Value: 
		Parameter 1:
			Name: set
			Type: System.Threading.ThreadState
			Default Value: 
	Method: ClrState:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01922A70
		Ram Offset 0x181923c70
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.InternalThread
			Default Value: 
		Parameter 1:
			Name: clr
			Type: System.Threading.ThreadState
			Default Value: 
	Method: GetState:
		Accessibility: Private
		Return Type: System.Threading.ThreadState
		File Offset 0x01922A80
		Ram Offset 0x181923c80
		Virtual Method Slot: 65535

		Parameter 0:
			Name: thread
			Type: System.Threading.InternalThread
			Default Value: 
	Method: SystemMaxStackStize:
		Accessibility: Private
		Return Type: System.Int32
		File Offset 0x01922AF0
		Ram Offset 0x181923cf0
		Virtual Method Slot: 65535

	Method: GetProcessDefaultStackSize:
		Accessibility: Private
		Return Type: System.Int32
		File Offset 0x01922B00
		Ram Offset 0x181923d00
		Virtual Method Slot: 65535

		Parameter 0:
			Name: maxStackSize
			Type: System.Int32
			Default Value: 
	Method: SetStart:
		Accessibility: Private
		Return Type: System.Void
		File Offset 0x01922B80
		Ram Offset 0x181923d80
		Virtual Method Slot: 65535

		Parameter 0:
			Name: start
			Type: System.MulticastDelegate
			Default Value: 
		Parameter 1:
			Name: maxStackSize
			Type: System.Int32
			Default Value: 
	Method: get_ManagedThreadId:
		Accessibility: Public
		Return Type: System.Int32
		File Offset 0x01922C40
		Ram Offset 0x181923e40
		Virtual Method Slot: 65535

	Method: BeginCriticalRegion:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01922CA0
		Ram Offset 0x181923ea0
		Virtual Method Slot: 65535

	Method: EndCriticalRegion:
		Accessibility: Public
		Return Type: System.Void
		File Offset 0x01922D20
		Ram Offset 0x181923f20
		Virtual Method Slot: 65535

	Method: GetHashCode:
		Accessibility: Public
		Return Type: System.Int32
		File Offset 0x01922C40
		Ram Offset 0x181923e40
		Virtual Method Slot: 2

	Method: ValidateThreadState:
		Accessibility: Private
		Return Type: System.Threading.ThreadState
		File Offset 0x01922DA0
		Ram Offset 0x181923fa0
		Virtual Method Slot: 65535
