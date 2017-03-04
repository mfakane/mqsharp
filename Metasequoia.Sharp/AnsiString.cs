using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class AnsiString : SafeHandle
	{
		public override bool IsInvalid =>
			handle == IntPtr.Zero;

		public string Value => Marshal.PtrToStringAnsi(handle);

		public AnsiString(int capacity)
			: base(IntPtr.Zero, true) =>
			handle = Marshal.AllocCoTaskMem(capacity);

		public AnsiString(string value)
			: base(IntPtr.Zero, true) =>
			handle = Marshal.StringToCoTaskMemAnsi(value);

		protected override bool ReleaseHandle()
		{
			Marshal.FreeCoTaskMem(handle);

			return true;
		}

		public static implicit operator IntPtr(AnsiString ptr) =>
			ptr.DangerousGetHandle();
	}
}
