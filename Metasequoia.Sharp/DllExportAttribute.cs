namespace System.Runtime.InteropServices
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	sealed class DllExportAttribute : Attribute
	{
		public string EntryPoint { get; }
		public CallingConvention CallingConvention { get; }

		public DllExportAttribute(CallingConvention callingConvention)
			: this(null, callingConvention)
		{
		}

		public DllExportAttribute(string entryPoint, CallingConvention callingConvention)
		{
			EntryPoint = entryPoint;
			CallingConvention = callingConvention;
		}
	}
}
