namespace Metasequoia
{
	public struct MouseState
    {
		public int X { get; set; }
		public int Y { get; set; }
		public int WheelDelta { get; set; }
		public MQMouseButtons ButtonState { get; set; }
	}
}
