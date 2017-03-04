using System;

namespace Metasequoia
{
	/// <summary>
	/// オブジェクトの面を表します。
	/// </summary>
	public sealed class Face
	{
		int PointCount => NativeMethods.MQObj_GetFacePointCount(Object.Handle, Index);

		public Object Object { get; }
		public uint UniqueId { get; }
		public int Index => NativeMethods.MQObj_GetFaceIndexFromUniqueID(Object.Handle, UniqueId);
		public ReadOnlyIndexer<Vertex> Vertices { get; }
		public ReadWriteIndexer<uint> VertexColors { get; }
		public ReadWriteIndexer<bool> EdgeCrease { get; }
		public ReadWriteIndexer<bool> IsLineSelected { get; }
		public ReadWriteIndexer<bool> IsUVVertexSelected { get; }

		public bool IsSelected
		{
			get => NativeMethods.MQDoc_IsSelectFace(Object.Document.Handle, Object.Index, Index);
			set
			{
				if (value)
					NativeMethods.MQDoc_AddSelectFace(Object.Document.Handle, Object.Index, Index);
				else
					NativeMethods.MQDoc_DeleteSelectFace(Object.Document.Handle, Object.Index, Index);
			}
		}

		public Coordinate[] Coordinates
		{
			get
			{
				var uvs = new Coordinate[PointCount];

				NativeMethods.MQObj_GetFaceCoordinateArray(Object.Handle, Index, uvs);

				return uvs;
			}
			set => NativeMethods.MQObj_SetFaceCoordinateArray(Object.Handle, Index, value);
		}

		public bool IsVisible
		{
			get => NativeMethods.MQObj_GetFaceVisible(Object.Handle, Index);
			set => NativeMethods.MQObj_SetFaceVisible(Object.Handle, Index, value);
		}

		public int MaterialIndex
		{
			get => NativeMethods.MQObj_GetFaceMaterial(Object.Handle, Index);
			set => NativeMethods.MQObj_SetFaceMaterial(Object.Handle, Index, value);
		}

		public Point Normal
		{
			get
			{
				var indices = new int[PointCount];
				var pts = new Point[indices.Length];

				NativeMethods.MQObj_GetFacePointArray(Object.Handle, Index, indices);

				for (var i = 0; i < indices.Length; i++)
					NativeMethods.MQObj_GetVertex(Object.Handle, indices[i], out pts[i]);

				return Point.GetNormal(pts);
			}
		}

		Face(Object obj, uint uniqueId)
		{
			Object = obj;
			UniqueId = uniqueId;
			Vertices = new ReadOnlyIndexer<Vertex>(() => PointCount, i =>
			{
				var indices = new int[PointCount];

				NativeMethods.MQObj_GetFacePointArray(Object.Handle, Index, indices);

				return Vertex.FromIndex(Object, indices[i]);
			});
			VertexColors = new ReadWriteIndexer<uint>(() => PointCount,
				i => NativeMethods.MQObj_GetFaceVertexColor(Object.Handle, Index, i),
				(i, value) => NativeMethods.MQObj_SetFaceVertexColor(Object.Handle, Index, i, value));
			EdgeCrease = new ReadWriteIndexer<bool>(() => PointCount,
				i => NativeMethods.MQObj_GetFaceEdgeCrease(Object.Handle, Index, i) != 0,
				(i, value) => NativeMethods.MQObj_SetFaceEdgeCrease(Object.Handle, Index, i, value ? 1 : 0));
			IsLineSelected = new ReadWriteIndexer<bool>(() => PointCount == 2 ? 1 : PointCount,
				i => NativeMethods.MQDoc_IsSelectLine(Object.Document.Handle, Object.Index, Index, i),
				(i, value) =>
				{
					if (value)
						NativeMethods.MQDoc_AddSelectLine(Object.Document.Handle, Object.Index, Index, i);
					else
						NativeMethods.MQDoc_DeleteSelectLine(Object.Document.Handle, Object.Index, Index, i);
				});
			IsUVVertexSelected = new ReadWriteIndexer<bool>(() => PointCount,
				i => NativeMethods.MQDoc_IsSelectUVVertex(Object.Document.Handle, Object.Index, Index, i),
				(i, value) =>
				{
					if (value)
						NativeMethods.MQDoc_AddSelectUVVertex(Object.Document.Handle, Object.Index, Index, i);
					else
						NativeMethods.MQDoc_DeleteSelectUVVertex(Object.Document.Handle, Object.Index, Index, i);
				});
		}

		public static Face FromIndex(Object obj, int index) =>
			new Face(obj, NativeMethods.MQObj_GetFaceUniqueID(obj.Handle, index));

		public void CopyAttributesFrom(Face face)
		{
			MaterialIndex = face.MaterialIndex;
			Coordinates = face.Coordinates;
			IsVisible = face.IsVisible;

			var count = Math.Min(PointCount, face.PointCount);

			for (var i = 0; i < count; i++)
			{
				VertexColors[i] = face.VertexColors[i];
				EdgeCrease[i] = face.EdgeCrease[i];
			}
		}

		public void Invert() =>
			NativeMethods.MQObj_InvertFace(Object.Handle, Index);

		public void Remove(bool deleteVertices = true) =>
			NativeMethods.MQObj_DeleteFace(Object.Handle, Index, deleteVertices);

		public override bool Equals(object obj) => obj is Face face && face.UniqueId == UniqueId;
		public override int GetHashCode() => typeof(Face).GetHashCode() ^ UniqueId.GetHashCode();

		public static bool operator ==(Face a, Face b) => a?.UniqueId == b?.UniqueId;
		public static bool operator !=(Face a, Face b) => !(a == b);
	}
}