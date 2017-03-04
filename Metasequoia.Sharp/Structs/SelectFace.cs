namespace Metasequoia
{
	/// <summary>
	/// MQSelectFace
	/// </summary>
	public struct SelectFace
    {
		public int Object { get; set; }
		public int Face { get; set; }

		public SelectFace(int obj, int face)
		{
			Object = obj;
			Face = face;
		}
	}
}
