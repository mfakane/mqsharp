namespace Metasequoia
{
	/// <summary>
	/// MQSelectVertex
	/// </summary>
	public struct SelectVertex
    {
		public int Object { get; set; }
		public int Vertex { get; set; }

		public SelectVertex(int obj, int vertex)
		{
			Object = obj;
			Vertex = vertex;
		}
	}
}
