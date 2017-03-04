using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class PinnedStructure<T> : SafeHandle
		where T : struct
	{
		public override bool IsInvalid =>
			handle == IntPtr.Zero;

		public T Value =>
			Marshal.PtrToStructure<T>(handle);

		public PinnedStructure()
			: this(default(T))
		{
		}

		public PinnedStructure(T value)
			: base(IntPtr.Zero, true)
		{
			handle = Marshal.AllocCoTaskMem(Marshal.SizeOf<T>());
			Marshal.StructureToPtr(value, handle, false);
		}

		protected override bool ReleaseHandle()
		{
			Marshal.DestroyStructure<T>(handle);
			Marshal.FreeCoTaskMem(handle);

			return true;
		}

		public static implicit operator IntPtr(PinnedStructure<T> ptr) =>
			ptr.DangerousGetHandle();
	}
}
