using System;

namespace Metasequoia
{
	[Flags]
    public enum DrawingObjectVisibility
    {
		None,
		Point,
		Line,
		Face = 4,
    }
}
