using System;

namespace Metasequoia
{
	[Flags]
    public enum MQMouseButtons
    {
		None,
		RightButton,
		LeftButton,
		Shift = 4,
		Control = 8,
		MiddleButton = 16,
		Alt = 32,
    }
}
