using System;
using System.Collections.Generic;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// MQDocument
	/// </summary>
	public class Document
	{
		public IntPtr Handle { get; }

		/// <summary>
		/// ドキュメントに含まれるオブジェクトを取得します。
		/// </summary>
		public DocumentObjectCollection Objects { get; }

		/// <summary>
		/// ドキュメントに含まれる材質を取得します。
		/// </summary>
		public DocumentMaterialCollection Materials { get; }

		/// <summary>
		/// <para>アクティブなオブジェクトを取得または設定します。</para>
		/// <para>int MQDocument::GetCurrentObjectIndex();</para>
		/// </summary>
		public Object CurrentObject
		{
			get => Objects[NativeMethods.MQDoc_GetCurrentObjectIndex(Handle)];
			set => NativeMethods.MQDoc_SetCurrentObjectIndex(Handle, value.Index);
		}

		/// <summary>
		/// <para>アクティブな材質を取得または設定します。</para>
		/// <para>int MQDocument::GetCurrentObjectIndex();</para>
		/// </summary>
		public Material CurrentMaterial
		{
			get => Materials[NativeMethods.MQDoc_GetCurrentMaterialIndex(Handle)];
			set => NativeMethods.MQDoc_SetCurrentMaterialIndex(Handle, value.Index);
		}

		public int CurrentUndoState
		{
			get
			{
				using (var result = new PinnedStructure<int>())
				using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["document"] = Handle,
					["result"] = result,
				}))
				{
					Metaseq.SendMessage(MQMessage.GetUndoState, args);

					return result.Value;
				}
			}
		}

		/// <summary>
		/// <para>パースペクティブビューを取得します。</para>
		/// <para>MQScene MQDocument::GetScene(int index);</para>
		/// </summary>
		public Scene PerspectiveView => Scene.FromHandle(NativeMethods.MQDoc_GetScene(Handle, 0));

		Document(IntPtr ptr)
		{
			Handle = ptr;
			Objects = new DocumentObjectCollection(this);
			Materials = new DocumentMaterialCollection(this);
		}

		public static Document FromHandle(IntPtr ptr) => ptr == IntPtr.Zero ? null : new Document(ptr);

		/// <summary>
		/// <para>ドキュメント中のオブジェクトやマテリアル配列を切り詰めます。</para>
		/// <para>void MQDocument::Compact();</para>
		/// </summary>
		public void Compact() =>
			NativeMethods.MQDoc_Compact(Handle);

		/// <summary>
		/// <para>ドキュメント中で使用されていない新しいオブジェクト名を取得します。</para>
		/// <para>void MQDocument::GetUnusedObjectName(char *buffer, int buffer_size);</para>
		/// </summary>
		/// <returns>新しい名前。</returns>
		public string GetUnusedObjectName() =>
			GetUnusedObjectName(null);

		/// <summary>
		/// <para>ベースとなる名前を指定し、ドキュメント中で使用されていない新しいオブジェクト名を取得します。</para>
		/// <para>void MQDocument::GetUnusedObjectName(char *buffer, int buffer_size, const char *base_name);</para>
		/// </summary>
		/// <param name="baseName">新しい名前のベースとなる名前、または null。</param>
		/// <returns>新しい名前。</returns>
		public string GetUnusedObjectName(string baseName)
		{
			var sb = new StringBuilder(256);

			NativeMethods.MQDoc_GetUnusedObjectName(Handle, sb, sb.Capacity, baseName);

			return sb.ToString();
		}

		/// <summary>
		/// <para>ドキュメント中で使用されていない新しい材質名を取得します。</para>
		/// <para>void MQDocument::GetUnusedMaterialName(char *buffer, int buffer_size);</para>
		/// </summary>
		/// <returns>新しい名前。</returns>
		public string GetUnusedMaterialName() =>
			GetUnusedMaterialName(null);

		/// <summary>
		/// <para>ベースとなる名前を指定し、ドキュメント中で使用されていない新しい材質名を取得します。</para>
		/// <para>void MQDocument::GetUnusedObjectName(char *buffer, int buffer_size, const char *base_name);</para>
		/// </summary>
		/// <param name="baseName">新しい名前のベースとなる名前、または null。</param>
		/// <returns>新しい名前。</returns>
		public string GetUnusedMaterialName(string baseName)
		{
			var sb = new StringBuilder(256);

			NativeMethods.MQDoc_GetUnusedMaterialName(Handle, sb, sb.Capacity, baseName);

			return sb.ToString();
		}

		/// <summary>
		/// <see cref="Material.TextureName"/>, <see cref="Material.AlphaName"/>, <see cref="Material.BumpName"/> で得られるファイル名を絶対パスに変換します。
		/// </summary>
		/// <param name="filename">変換するファイル名。</param>
		/// <param name="mapType">マッピング画像の種類。</param>
		/// <returns>ファイル名が変換された絶対パス。</returns>
		public string FindMappingFile(string filename, MQMapping mapType)
		{
			var sb = new StringBuilder(260);

			if (NativeMethods.MQDoc_FindMappingFileW(Handle, sb, filename, (uint)mapType))
				return sb.ToString();

			return null;
		}

		public override bool Equals(object obj) => obj is Document doc && doc.Handle == Handle;
		public override int GetHashCode() => typeof(Document).GetHashCode() ^ Handle.GetHashCode();
	}
}
