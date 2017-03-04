namespace Metasequoia
{
	public struct DrawingTextParameters
    {
		public float? FontScale { get; set; }
		public MQHorizontalAlignment HorizontalAlignment { get; set; }
		public MQVerticalAlignment VerticalAlignment { get; set; }

		public DrawingTextParameters(float? fontScale, MQHorizontalAlignment horizontalAlignment, MQVerticalAlignment verticalAlignment)
		{
			FontScale = fontScale;
			HorizontalAlignment = horizontalAlignment;
			VerticalAlignment = verticalAlignment;
		}
	}
}
