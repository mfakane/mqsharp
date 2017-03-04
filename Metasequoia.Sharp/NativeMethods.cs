using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace Metasequoia
{
	static partial class NativeMethods
	{
	}

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	delegate bool MQStationCallbackProc(IntPtr doc, IntPtr option);
}
