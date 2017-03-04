using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class UnicodeString : SafeHandle
    {
		public override bool IsInvalid =>
			handle == IntPtr.Zero;

		public string Value => Marshal.PtrToStringUni(handle);

		public UnicodeString(int capacity)
			: base(IntPtr.Zero, true) =>
			handle = Marshal.AllocCoTaskMem(capacity * 2);

		public UnicodeString(string value)
			: base(IntPtr.Zero, true) =>
			handle = Marshal.StringToCoTaskMemUni(value);

		protected override bool ReleaseHandle()
		{
			Marshal.FreeCoTaskMem(handle);
			
			return true;
		}

		public static implicit operator IntPtr(UnicodeString ptr) =>
			ptr.DangerousGetHandle();
	}
}
