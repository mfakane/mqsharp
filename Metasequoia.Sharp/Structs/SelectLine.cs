namespace Metasequoia
{
	/// <summary>
	/// MQSelectLine
	/// </summary>
	public struct SelectLine
    {
		public int Object { get; set; }
		public int Face { get; set; }
		public int Line { get; set; }

		public SelectLine(int obj, int face, int line)
		{
			Object = obj;
			Face = face;
			Line = line;
		}
	}
}
