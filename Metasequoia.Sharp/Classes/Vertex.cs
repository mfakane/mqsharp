using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	/// <summary>
	/// オブジェクトの頂点を表します。
	/// </summary>
	public struct Vertex
	{
		/// <summary>
		/// 頂点が含まれるオブジェクトを取得します。
		/// </summary>
		public Object Object { get; }

		/// <summary>
		/// <para>頂点のユニーク ID を取得します。</para>
		/// <para>UINT MQObject::GetVertexUniqueID(int index);</para>
		/// </summary>
		public uint UniqueId { get; }

		/// <summary>
		/// <para>頂点のインデックスを取得します。</para>
		/// <para>int MQObject::GetVertexIndexFromUniqueID(UINT unique_id);</para>
		/// </summary>
		public int Index => NativeMethods.MQObj_GetVertexIndexFromUniqueID(Object.Handle, UniqueId);

		/// <summary>
		/// <para>頂点の面からの参照数を取得します。</para>
		/// <para>int MQObject::GetVertexRefCount(int index);</para>
		/// </summary>
		public int RefCount => NativeMethods.MQObj_GetVertexRefCount(Object.Handle, Index);

		/// <summary>
		/// <para>頂点の位置を取得または設定します。</para>
		/// <para>MQPoint MQObject::GetVertex(int index);</para>
		/// </summary>
		public Point Position
		{
			get
			{
				NativeMethods.MQObj_GetVertex(Object.Handle, Index, out var pt);

				return pt;
			}
			set => NativeMethods.MQObj_SetVertex(Object.Handle, Index, ref value);
		}

		/// <summary>
		/// <para>曲面に対する重みを取得または設定します。</para>
		/// <para>float MQObject::GetVertexWeight(int index);</para>
		/// </summary>
		public float Weight
		{
			get => NativeMethods.MQObj_GetVertexWeight(Object.Handle, Index);
			set => NativeMethods.MQObj_SetVertexWeight(Object.Handle, Index, value);
		}

		/// <summary>
		/// <para>頂点の選択状態を取得または設定します。</para>
		/// <para>BOOL MQDocument::IsSelectVertex(int objindex, int vertindex);</para>
		/// </summary>
		public bool IsSelected
		{
			get => NativeMethods.MQDoc_IsSelectVertex(Object.Document.Handle, Object.Index, Index);
			set
			{
				if (value)
					NativeMethods.MQDoc_AddSelectVertex(Object.Document.Handle, Object.Index, Index);
				else
					NativeMethods.MQDoc_DeleteSelectVertex(Object.Document.Handle, Object.Index, Index);
			}
		}

		Vertex(Object obj, uint uniqueId)
		{
			Object = obj;
			UniqueId = uniqueId;
		}

		public static Vertex FromIndex(Object obj, int index) =>
			new Vertex(obj, NativeMethods.MQObj_GetVertexUniqueID(obj.Handle, index));

		/// <summary>
		/// <para>現在の頂点を使用している面を取得します。</para>
		/// <para>UINT MQObject::GetVertexRelatedFaces(int vertex, int *faces);</para>
		/// </summary>
		/// <returns>現在の頂点を使用している面。</returns>
		public IEnumerable<Face> GetVertexRelatedFaces()
		{
			var obj = Object;
			var count = NativeMethods.MQObj_GetVertexRelatedFaces(Object.Handle, Index, null);
			var faceIndices = new int[count];

			NativeMethods.MQObj_GetVertexRelatedFaces(Object.Handle, Index, faceIndices);

			return faceIndices.Select(i => Face.FromIndex(obj, i));
		}

		/// <summary>
		/// <para>指定された頂点から位置以外の属性を現在の頂点にコピーします。</para>
		/// <para>void MQObject::CopyVertexAttribute(int vert1, MQObject obj2, int vert2);</para>
		/// </summary>
		/// <param name="vertex">属性のコピー元となる頂点。</param>
		public void CopyAttributesFrom(Vertex vertex) =>
			NativeMethods.MQObj_CopyVertexAttribute(Object.Handle, Index, vertex.Object.Handle, vertex.Index);

		/// <summary>
		/// <para>現在の頂点を削除します。</para>
		/// <para>BOOL MQObject::DeleteVertex(int index);</para>
		/// </summary>
		public void Remove() =>
			NativeMethods.MQObj_DeleteVertex(Object.Handle, Index, true);

		public override bool Equals(object obj) => obj is Vertex vertex && vertex.UniqueId == UniqueId;
		public override int GetHashCode() => typeof(Vertex).GetHashCode() ^ UniqueId.GetHashCode();

		public static bool operator ==(Vertex a, Vertex b) => a.UniqueId == b.UniqueId;
		public static bool operator !=(Vertex a, Vertex b) => !a.Equals(b);
	}
}
