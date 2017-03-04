using System;

namespace Metasequoia
{
	[Flags]
    public enum MQObjectRenderFlag
    {
		None,
		Point,
		Line,
		Face = 4,
		OverwriteLine = 8,
		OverwriteFace = 16,
		AlphaBlend = 32,
		MultiLight = 64,
		Shadow = 128,
		VertexColorPoint = 256,
		Screen = 512,
    }
}
