using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	/// <summary>
	/// MQObject
	/// </summary>
	public sealed class Object : IDisposable
	{
		public IntPtr Handle { get; }
		public bool IsDrawingObject { get; private set; }
		public bool IsInstantDrawingObject { get; private set; }

		/// <summary>
		/// オブジェクトのドキュメントを取得します。
		/// </summary>
		public Document Document { get; set; }

		/// <summary>
		/// オブジェクトのユニーク ID を取得します。
		/// UINT MQObject::GetUniqueID();
		/// </summary>
		/// <remarks>
		/// ユニーク ID はドキュメントに登録された時点で割り当てられます。
		/// </remarks>
		public uint UniqueId => (uint)NativeMethods.MQObj_GetIntValue(Handle, MQObjId.UniqueId);

		/// <summary>
		/// オブジェクトのインデックスを取得します。
		/// </summary>
		public int Index => NativeMethods.MQDoc_GetObjectIndex(Document.Handle, Handle);

		/// <summary>
		/// 親オブジェクトを取得します。
		/// MQObject MQDocument::GetParentObject(MQObject obj);
		/// </summary>
		public Object Parent
		{
			get => FromHandle(Document, NativeMethods.MQDoc_GetParentObject(Document.Handle, Handle));
		}

		/// <summary>
		/// オブジェクトに存在する頂点のコレクションを取得します。
		/// </summary>
		public ObjectVertexCollection Vertices { get; }

		/// <summary>
		/// オブジェクトに存在する面のコレクションを取得します。
		/// </summary>
		public ObjectFaceCollection Faces { get; }

		/// <summary>
		/// 子オブジェクトを取得します。
		/// MQObject MQDocument::GetChildObject(MQObject obj, int index);
		/// </summary>
		public ReadOnlyIndexer<Object> Children { get; }

		#region Properties

		/// <summary>
		/// オブジェクトの種類を取得または設定します。
		/// int MQObject::GetType();
		/// </summary>
		public MQObjectType Type
		{
			get => (MQObjectType)NativeMethods.MQObj_GetIntValue(Handle, MQObjId.Type);
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.Type, (int)value);
		}

		/// <summary>
		/// オブジェクトの名前を取得または設定します。
		/// void MQObject::GetName(char *buffer, int size);
		/// </summary>
		public string Name
		{
			get
			{
				var sb = new StringBuilder(256);

				NativeMethods.MQObj_GetName(Handle, sb, sb.Capacity);

				return sb.ToString();
			}
			set => NativeMethods.MQObj_SetName(Handle, value);
		}

		/// <summary>
		/// オブジェクトの選択状態を取得または設定します。
		/// BOOL MQObject::GetSelected();
		/// </summary>
		public bool IsSelected
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.Selected) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.Selected, value ? 1 : 0);
		}

		/// <summary>
		/// オブジェクトの可視状態を取得または設定します。
		/// DWORD MQObject::GetVisible();
		/// </summary>
		public bool IsVisible
		{
			get => NativeMethods.MQObj_GetVisible(Handle) != 0;
			set => NativeMethods.MQObj_SetVisible(Handle, value ? 0xFFFFFFFF : 0);
		}

		/// <summary>
		/// スムージングの種類を取得または設定します。
		/// int MQObject::GetShading();
		/// </summary>
		public MQObjectShade Shading
		{
			get => (MQObjectShade)NativeMethods.MQObj_GetShading(Handle);
			set => NativeMethods.MQObj_SetShading(Handle, (int)value);
		}

		/// <summary>
		/// スムージング角度を取得または設定します。
		/// float MQObject::GetSmoothAngle();
		/// </summary>
		public float SmoothAngle
		{
			get => NativeMethods.MQObj_GetSmoothAngle(Handle);
			set => NativeMethods.MQObj_SetSmoothAngle(Handle, value);
		}

		/// <summary>
		/// 鏡面の種類を取得または設定します。
		/// int MQObject::GetMirrorType();
		/// </summary>
		public MQObjectMirror MirrorType
		{
			get => (MQObjectMirror)NativeMethods.MQObj_GetMirrorType(Handle);
			set => NativeMethods.MQObj_SetMirrorType(Handle, (int)value);
		}

		/// <summary>
		/// 鏡面の軸を取得または設定します。
		/// DWORD MQObject::GetMirrorAxis();
		/// </summary>
		public MQObjectMirrorAxis MirrorAxis
		{
			get => (MQObjectMirrorAxis)NativeMethods.MQObj_GetMirrorAxis(Handle);
			set => NativeMethods.MQObj_SetMirrorAxis(Handle, (uint)value);
		}

		/// <summary>
		/// 鏡面の接続距離を取得または設定します。
		/// float MQObgject::GetMirrorDistance();
		/// </summary>
		public float MirrorDistance
		{
			get => NativeMethods.MQObj_GetMirrorDistance(Handle);
			set => NativeMethods.MQObj_SetMirrorDistance(Handle, value);
		}

		/// <summary>
		/// オブジェクトの階層の深さを取得または設定します。
		/// int MQObject::GetDepth();
		/// </summary>
		public int Depth
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.Depth);
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.Depth, value);
		}

		/// <summary>
		/// オブジェクトパネル上で子オブジェクトを折りたたんでいるかどうかを取得または設定します。
		/// BOOL MQObject::GetFolding();
		/// </summary>
		public bool IsCollapsed
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.Folding) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.Folding, value ? 1 : 0);
		}

		/// <summary>
		/// オブジェクトが編集禁止であるかどうかを取得または設定します。
		/// BOOL MQObject::GetLocking();
		/// </summary>
		public bool IsLocked
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.Locking) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.Locking, value ? 1 : 0);
		}

		/// <summary>
		/// 頂点および辺の表示色を取得または設定します。
		/// MQColor MQObject::GetColor();
		/// </summary>
		public Color Color
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.Color, val);

				return new Color(val);
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.Color, value.ToArray());
		}

		/// <summary>
		/// 頂点および辺に表示色を適用するかどうかを取得または設定します。
		/// BOOL MQObject::GetColorValid();
		/// </summary>
		public bool UseColor
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.ColorValid) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.ColorValid, value ? 1 : 0);
		}

		#endregion
		#region Patch

		/// <summary>
		/// 曲面の種類を取得または設定します。
		/// DWORD MQObject::GetPatchType();
		/// </summary>
		public MQObjectPatch PatchType
		{
			get => (MQObjectPatch)NativeMethods.MQObj_GetPatchType(Handle);
			set => NativeMethods.MQObj_SetPatchType(Handle, (uint)value);
		}

		/// <summary>
		/// 曲面の分割数を取得または設定します。
		/// int MQObject::GetPatchSegment();
		/// </summary>
		public int PatchSegment
		{
			get => NativeMethods.MQObj_GetPatchSegment(Handle);
			set => NativeMethods.MQObj_SetPatchSegment(Handle, value);
		}

		/// <summary>
		/// Catmull-Clark 曲面において三角形を三角形に分割するかどうかを取得または設定します。
		/// BOOL MQObject::GetPatchTriangle();
		/// </summary>
		public bool PatchTriangle
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.PatchTriangle) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.PatchTriangle, value ? 1 : 0);
		}

		/// <summary>
		/// OpenSubdiv 曲面において三角形のスムージングを行うかどうかを取得または設定します。
		/// BOOL MQObject::GetPatchSmoothTriangle();
		/// </summary>
		public bool PatchSmoothTriangle
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.PatchSmoothTriangle) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.PatchSmoothTriangle, value ? 1 : 0);
		}

		/// <summary>
		/// OpenSubdiv 曲面において極限サーフェイスの適用を行うかどうかを取得または設定します。
		/// BOOL MQObject::GetPatchLimitSurface();
		/// </summary>
		public bool PatchLimitSurface
		{
			get => NativeMethods.MQObj_GetIntValue(Handle, MQObjId.PatchLimitSurface) != 0;
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.PatchLimitSurface, value ? 1 : 0);
		}

		/// <summary>
		/// OpenSubdiv 曲面において開いた面の境界の補間方法を取得または設定します。
		/// int MQObject::GetPatchMeshInterp();
		/// </summary>
		public MQObjectInterp PatchMeshInterpolation
		{
			get => (MQObjectInterp)NativeMethods.MQObj_GetIntValue(Handle, MQObjId.PatchMeshInterp);
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.PatchMeshInterp, (int)value);
		}

		/// <summary>
		/// OpenSubdiv 曲面において開いた UV の境界の補間方法を取得または設定します。
		/// int MQObject::GetPatchUVInterp();
		/// </summary>
		public MQObjectUvinterp PatchUVInterpolation
		{
			get => (MQObjectUvinterp)NativeMethods.MQObj_GetIntValue(Handle, MQObjId.PatchUvInterp);
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.PatchUvInterp, (int)value);
		}

		#endregion
		#region Lathe

		/// <summary>
		/// 回転体の種類を取得または設定します。
		/// int MQObject::GetLatheType();
		/// </summary>
		public MQObjectLathe LatheType
		{
			get => (MQObjectLathe)NativeMethods.MQObj_GetLatheType(Handle);
			set => NativeMethods.MQObj_SetLatheType(Handle, (int)value);
		}

		/// <summary>
		/// 回転体の軸を取得または設定します。
		/// DWORD MQobject::GetLatheAxis();
		/// </summary>
		public MQObjectLatheAxis LatheAxis
		{
			get => (MQObjectLatheAxis)NativeMethods.MQObj_GetLatheAxis(Handle);
			set => NativeMethods.MQObj_SetLatheAxis(Handle, (uint)value);
		}

		/// <summary>
		/// 回転体の分割数を取得または設定します。
		/// int MQObject::GetLatheSegment();
		/// </summary>
		public int LatheSegment
		{
			get => NativeMethods.MQObj_GetLatheSegment(Handle);
			set => NativeMethods.MQObj_SetLatheSegment(Handle, value);
		}

		#endregion
		#region Local Transform

		/// <summary>
		/// ローカル座標の拡大を取得または設定します。
		/// MQPoint MQObject::GetScaling();
		/// </summary>
		public Point Scaling
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.Scaling, val);

				return new Point(val);
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.Scaling, value.ToArray());
		}

		/// <summary>
		/// ローカル座標の回転を取得または設定します。
		/// MQAngle MQObject::SetRotation();
		/// </summary>
		public Angle Rotation
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.Rotation, val);

				return new Angle(val);
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.Rotation, value.ToArray());
		}

		/// <summary>
		/// ローカル座標の平行移動を取得または設定します。
		/// MQPoint MQObject::GetTranslation();
		/// </summary>
		public Point Translation
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.Translation, val);

				return new Point(val);
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.Translation, value.ToArray());
		}

		/// <summary>
		/// ローカル座標の変換行列を取得または設定します。
		/// void MQObject::GetLocalMatrix(MQMatrix&amp; mtx);
		/// </summary>
		public Matrix LocalMatrix
		{
			get
			{
				var val = new float[16];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.LocalMatrix, val);

				return new Matrix(val);
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.LocalMatrix, value.ToArray());
		}

		/// <summary>
		/// ローカル座標の変換行列が適用された、グローバル座標の変換行列を取得します。
		/// void MQDocument::GetGlobalMatrix(MQObject obj, MQMatrix&amp; mtx);
		/// </summary>
		public Matrix GlobalMatrix
		{
			get
			{
				NativeMethods.MQDoc_GetGlobalMatrix(Document.Handle, Handle, out var rt);

				return rt;
			}
		}

		#endregion
		#region Light

		/// <summary>
		/// 光源オブジェクトの強度を取得または設定します。
		/// float MQObject::GetLightValue();
		/// </summary>
		public float LightValue
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.LightValue, val);

				return val[0];
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.LightValue, new[] { value });
		}

		/// <summary>
		/// 光源オブジェクトの光減衰度を取得または設定します。
		/// int MQObject::GetLightAttenuation();
		/// </summary>
		public MQObjectLightAttenuation LightAttenuation
		{
			get => (MQObjectLightAttenuation)NativeMethods.MQObj_GetIntValue(Handle, MQObjId.LightAttenuation);
			set => NativeMethods.MQObj_SetIntValue(Handle, MQObjId.LightAttenuation, (int)value);
		}

		/// <summary>
		/// 光源オブジェクトの減衰終了距離を取得または設定します。
		/// float MQObject::GetLightFallOffEnd();
		/// </summary>
		public float LightFalloffEnd
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.LightFalloffEnd, val);

				return val[0];
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.LightFalloffEnd, new[] { value });
		}

		/// <summary>
		/// 光源オブジェクトの減衰半減距離を取得または設定します。
		/// float MQObject::GetLightFallOffHalf();
		/// </summary>
		public float LightFalloffHalf
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQObj_GetFloatArray(Handle, MQObjId.LightFalloffHalf, val);

				return val[0];
			}
			set => NativeMethods.MQObj_SetFloatArray(Handle, MQObjId.LightFalloffHalf, new[] { value });
		}

		#endregion

		public Object()
			: this(null, NativeMethods.MQ_CreateObject())
		{
		}

		Object(Document doc, IntPtr ptr)
		{
			Document = doc;
			Handle = ptr;
			Vertices = new ObjectVertexCollection(this);
			Faces = new ObjectFaceCollection(this);
			Children = new ReadOnlyIndexer<Object>(() => NativeMethods.MQDoc_GetChildObjectCount(Document.Handle, Handle),
				i => FromHandle(Document, NativeMethods.MQDoc_GetChildObject(Document.Handle, Handle, i)));
		}

		public static Object FromHandle(Document doc, IntPtr ptr) => ptr == IntPtr.Zero ? null : new Object(doc, ptr);
		public static Object FromIndex(Document doc, int index) => new Object(doc, NativeMethods.MQDoc_GetObject(doc.Handle, index));
		public static Object FromUniqueId(Document doc, uint uniqueId) => new Object(doc, NativeMethods.MQDoc_GetObjectFromUniqueID(doc.Handle, (int)uniqueId));

		/// <summary>
		/// OnDraw 時に描画するオブジェクトを作成します。
		/// MQObject MQStationPlugin::CreateDrawingObject(MQDocument doc, DRAW_OBJECT_VISIBILITY visibility, BOOL instant);
		/// </summary>
		/// <param name="doc">親となるドキュメント。</param>
		/// <param name="visibility">表示する要素。</param>
		/// <param name="isInstant">描画完了時に自動的に破棄するかどうか。false を指定した場合、不要になった時点で <see cref="Dispose"/> を呼び出してください。</param>
		/// <returns>描画用オブジェクト。</returns>
		public static Object CreateDrawingObject(Document doc, DrawingObjectVisibility visibility, bool isInstant = true)
		{
			using (var visibilityPtr = new PinnedStructure<int>((int)visibility))
			using (var isInstantPtr = new PinnedStructure<bool>(isInstant))
			using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["document"] = doc.Handle,
				["visibility"] = visibilityPtr,
				["instant"] = isInstantPtr,
				["result"] = IntPtr.Zero,
			}))
			{
				Metaseq.SendMessage(MQMessage.NewDrawObject, args);

				return new Object(doc, args["result"])
				{
					IsDrawingObject = true,
					IsInstantDrawingObject = isInstant,
				};
			}
		}

		/// <summary>
		/// 基になるオブジェクトを指定し、OnDraw 時に描画するオブジェクトを作成します。
		/// MQObject MQStationPlugin::CreateDrawingObject(MQDocument doc, DRAW_OBJECT_VISIBILITY visibility, BOOL instant);
		/// </summary>
		/// <param name="doc">親となるドキュメント。</param>
		/// <param name="source">基になるオブジェクト。</param>
		/// <param name="visibility">表示する要素。</param>
		/// <param name="isInstant">描画完了時に自動的に破棄するかどうか。false を指定した場合、不要になった時点で <see cref="Dispose"/> を呼び出してください。</param>
		/// <returns>描画用オブジェクト。</returns>
		public static Object CreateDrawingObject(Document doc, Object source, DrawingObjectVisibility visibility, bool isInstant = true)
		{
			using (var visibilityPtr = new PinnedStructure<int>((int)visibility))
			using (var isInstantPtr = new PinnedStructure<bool>(isInstant))
			using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["document"] = doc.Handle,
				["clone_source"] = source.Handle,
				["visibility"] = visibilityPtr,
				["instant"] = isInstantPtr,
				["result"] = IntPtr.Zero,
			}))
			{
				Metaseq.SendMessage(MQMessage.NewDrawObject, args);

				return new Object(doc, args["result"])
				{
					IsDrawingObject = true,
					IsInstantDrawingObject = isInstant,
				};
			}
		}

		/// <summary>
		/// 描画時にこのオブジェクトを代替して描画するオブジェクトを指定します。
		/// void MQStationPlugin::SetDrawProxyObject(MQObject obj, MQObject proxy);
		/// </summary>
		/// <param name="proxy">代替として描画するオブジェクト、または代替描画を解除する場合は null。</param>
		/// <remarks>
		/// このメソッドによって代替されるのは描画のみであり、操作には影響しません。
		/// 代替描画は代替として指定されたオブジェクトが破棄されるか、<paramref name="proxy"/> に null が指定されるまで継続します。
		/// </remarks>
		public void SetDrawProxyObject(Object proxy)
		{
			using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["object"] = Handle,
				["proxy"] = proxy?.Handle ?? IntPtr.Zero,
			}))
				Metaseq.SendMessage(MQMessage.SetDrawProxyObject, args);
		}

		/// <summary>
		/// 指定された項目を強制的に表示にします。
		/// void MQObject::AddRenderFlag(MQOBJECT_RENDER_FLAG flag);
		/// </summary>
		/// <param name="flags">強制的に表示にする表示項目。</param>
		public void ForceShow(MQObjectRenderFlag flags) =>
			ModifyRenderFlag(MQObjId.AddRenderFlag, flags);

		/// <summary>
		/// 指定された項目の表示の強制を解除します。
		/// void MQObject::RemoveRenderFlag(MQOBJECT_RENDER_FLAG flag);
		/// </summary>
		/// <param name="flags">表示の強制を解除する表示項目。</param>
		public void UnforceShow(MQObjectRenderFlag flags) =>
			ModifyRenderFlag(MQObjId.RemoveRenderFlag, flags);

		/// <summary>
		/// 指定された項目を強制的に非表示にします。
		/// void MQObject::AddRenderEraseFlag(MQOBJECT_RENDER_FLAG flag);
		/// </summary>
		/// <param name="flags">強制的に非表示にする表示盲目。</param>
		public void ForceHide(MQObjectRenderFlag flags) =>
			ModifyRenderFlag(MQObjId.AddEraseFlag, flags);

		/// <summary>
		/// 指定された項目の非表示の強制を解除します。
		/// void MQObject::RemoveRenderEraseFlag(MQOBJECT_RENDER_FLAG flag);
		/// </summary>
		/// <param name="flags">非表示の強制を解除する表示項目。</param>
		public void UnforceHide(MQObjectRenderFlag flags) =>
			ModifyRenderFlag(MQObjId.RemoveEraseFlag, flags);

		void ModifyRenderFlag(MQObjId id, MQObjectRenderFlag flags)
		{
			var ptr = new IntPtr[2];

			foreach (var i in RenderFlagsToString())
			{
				var p = Marshal.StringToCoTaskMemAnsi(i);

				try
				{
					ptr[0] = p;
					NativeMethods.MQObj_PointerArray(Handle, id, ptr);
				}
				finally
				{
					Marshal.FreeCoTaskMem(p);
				}
			}

			IEnumerable<string> RenderFlagsToString()
			{
				if ((flags & MQObjectRenderFlag.Point) != MQObjectRenderFlag.None) yield return "point";
				if ((flags & MQObjectRenderFlag.Line) != MQObjectRenderFlag.None) yield return "line";
				if ((flags & MQObjectRenderFlag.Face) != MQObjectRenderFlag.None) yield return "face";
				if ((flags & MQObjectRenderFlag.OverwriteLine) != MQObjectRenderFlag.None) yield return "overwriteline";
				if ((flags & MQObjectRenderFlag.OverwriteFace) != MQObjectRenderFlag.None) yield return "overwriteface";
				if ((flags & MQObjectRenderFlag.AlphaBlend) != MQObjectRenderFlag.None) yield return "alphablend";
				if ((flags & MQObjectRenderFlag.MultiLight) != MQObjectRenderFlag.None) yield return "multilight";
				if ((flags & MQObjectRenderFlag.Shadow) != MQObjectRenderFlag.None) yield return "shadow";
				if ((flags & MQObjectRenderFlag.VertexColorPoint) != MQObjectRenderFlag.None) yield return "vcolpoint";
				if ((flags & MQObjectRenderFlag.Screen) != MQObjectRenderFlag.None) yield return "screen";
			}
		}

		/// <summary>
		/// 現在のオブジェクトの複製を作成します。
		/// MQObject MQObject::Clone();
		/// </summary>
		/// <returns>複製されたオブジェクト。</returns>
		public Object Clone() =>
			FromHandle(Document, NativeMethods.MQObj_Clone(Handle));

		/// <summary>
		/// 指定されたオブジェクトを現在のオブジェクトへ合成します。
		/// void MQObject::Merge(MQObject source);
		/// </summary>
		/// <param name="source">合成するオブジェクト。</param>
		public void Merge(Object source) =>
			NativeMethods.MQObj_Merge(Handle, source.Handle);

		/// <summary>
		/// 現在のオブジェクトの曲面や鏡面のフリーズを行います。
		/// void MQObject::Freeze(DWORD flag);
		/// </summary>
		/// <param name="flag">フリーズする属性。</param>
		public void Freeze(MQObjectFreeze flag) =>
			NativeMethods.MQObj_Freeze(Handle, (uint)flag);

		/// <summary>
		/// 現在のオブジェクトに含まれる参照されていない頂点や面を削除します。
		/// void MQObject::Compat();
		/// </summary>
		public void Compact() =>
			NativeMethods.MQDoc_Compact(Handle);

		/// <summary>
		/// 現在のオブジェクトを破棄し解放します.
		/// void MQObject::DeleteThis();
		/// void MQStationPlugin::DeleteDrawingObject(MQDocument doc, MQObject obj);
		/// </summary>
		public void Dispose()
		{
			if (!IsDrawingObject)
				NativeMethods.MQObj_Delete(Handle);
			else if (!IsInstantDrawingObject)
				using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["document"] = Document.Handle,
					["object"] = Handle,
				}))
					Metaseq.SendMessage(MQMessage.DeleteDrawObject, args);
		}

		public override bool Equals(object obj) => obj is Object mqObj && mqObj.UniqueId == UniqueId;
		public override int GetHashCode() => typeof(Object).GetHashCode() ^ Handle.GetHashCode();
		public override string ToString() => $"MQObject: {UniqueId}, \"{Name}\"";
	}
}
