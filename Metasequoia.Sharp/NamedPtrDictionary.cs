using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class NamedPtrDictionary : SafeHandle
	{
		public bool IsNative { get; }

		public override bool IsInvalid =>
			handle == IntPtr.Zero;

		public IntPtr this[string key]
		{
			get
			{
				var ptr = handle;
				IntPtr keyPtr;

				while ((keyPtr = Marshal.ReadIntPtr(ptr)) != IntPtr.Zero)
				{
					var keyString = Marshal.PtrToStringAnsi(keyPtr);

					if (keyString == key)
						return Marshal.ReadIntPtr(ptr + 4);

					ptr += 8;
				}

				return IntPtr.Zero;
			}
		}

		public NamedPtrDictionary(IntPtr option)
			: base(IntPtr.Zero, false)
		{
			handle = option;
			IsNative = true;
		}

		public NamedPtrDictionary(IDictionary<string, IntPtr> items)
			: base(IntPtr.Zero, true)
		{
			var ptr = handle = Marshal.AllocCoTaskMem(items.Count * 8 + 4);

			foreach (var i in items)
			{
				Marshal.WriteIntPtr(ptr, Marshal.StringToCoTaskMemAnsi(i.Key));
				Marshal.WriteIntPtr(ptr + 4, i.Value);
				ptr += 8;
			}

			Marshal.WriteIntPtr(ptr, IntPtr.Zero);
		}

		protected override bool ReleaseHandle()
		{
			if (IsNative) return true;

			var ptr = handle;
			IntPtr keyPtr;

			while ((keyPtr = Marshal.ReadIntPtr(ptr)) != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(keyPtr);
				ptr += 8;
			}

			Marshal.FreeCoTaskMem(handle);

			return true;
		}

		public static implicit operator IntPtr(NamedPtrDictionary ptr) =>
			ptr.DangerousGetHandle();
	}
}
