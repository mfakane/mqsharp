using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Metasequoia
{
	using MQFileDialogInfo = FileDialogInfo;
	using MQSendMessageInfo = SendMessageInfo;
	using MQPoint = Point;
	using MQUserDataInfo = UserDataInfo;
	using MQCoordinate = Coordinate;
	using MQColor = Color;
	using HWND = IntPtr;
	using BOOL = Boolean;
	using UINT = UInt32;
	using DWORD = UInt32;
	
	partial class NativeMethods
	{
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern HWND MQ_GetWindowHandle();
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQ_CreateObject();
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQ_CreateMaterial();
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQ_ShowFileDialog([MarshalAs(UnmanagedType.LPStr)] string title, ref MQFileDialogInfo  info);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQ_ImportAxis(ref MQFileDialogInfo  info, MQPoint[] pts, int pts_count);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQ_ExportAxis(ref MQFileDialogInfo  info, MQPoint[] pts, int pts_count);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern BOOL MQ_LoadImage([MarshalAs(UnmanagedType.LPStr)] string filename, out IntPtr header, out IntPtr buffer, DWORD reserved);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQ_LoadImageW([MarshalAs(UnmanagedType.LPWStr)] string filename, out IntPtr header, out IntPtr buffer, DWORD reserved);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern BOOL MQ_GetSystemPath([MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQ_GetSystemPathW([MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer, int type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQ_RefreshView(IntPtr reserved);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQ_StationCallback(MQStationCallbackProc proc, IntPtr option);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQ_SendMessage(int message_type, ref MQSendMessageInfo  info);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetObjectCount(IntPtr doc);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetObject(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetObjectFromUniqueID(IntPtr doc, int id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetCurrentObjectIndex(IntPtr doc);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_SetCurrentObjectIndex(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_AddObject(IntPtr doc, IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_DeleteObject(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetObjectIndex(IntPtr doc, IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQDoc_GetUnusedObjectName(IntPtr doc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int buffer_size, [MarshalAs(UnmanagedType.LPStr)] string base_name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetMaterialCount(IntPtr doc);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetMaterial(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetMaterialFromUniqueID(IntPtr doc, int id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetCurrentMaterialIndex(IntPtr doc);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_SetCurrentMaterialIndex(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_AddMaterial(IntPtr doc, IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_DeleteMaterial(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQDoc_GetUnusedMaterialName(IntPtr doc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int buffer_size, [MarshalAs(UnmanagedType.LPStr)] string base_name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern BOOL MQDoc_FindMappingFile(IntPtr doc, [MarshalAs(UnmanagedType.LPStr)] StringBuilder out_path, [MarshalAs(UnmanagedType.LPStr)] string filename, DWORD map_type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_FindMappingFileW(IntPtr doc, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder out_path, [MarshalAs(UnmanagedType.LPWStr)] string filename, DWORD map_type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern BOOL MQDoc_GetMappingImage(IntPtr doc, [MarshalAs(UnmanagedType.LPStr)] string filename, DWORD map_type, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_GetMappingImageW(IntPtr doc, [MarshalAs(UnmanagedType.LPWStr)] string filename, DWORD map_type, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_Compact(IntPtr doc);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_ClearSelect(IntPtr doc, DWORD flag);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_AddSelectVertex(IntPtr doc, int objindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_DeleteSelectVertex(IntPtr doc, int objindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_IsSelectVertex(IntPtr doc, int objindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_AddSelectLine(IntPtr doc, int objindex, int faceindex, int lineindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_DeleteSelectLine(IntPtr doc, int objindex, int faceindex, int lineindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_IsSelectLine(IntPtr doc, int objindex, int faceindex, int lineindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_AddSelectFace(IntPtr doc, int objindex, int faceindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_DeleteSelectFace(IntPtr doc, int objindex, int faceindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_IsSelectFace(IntPtr doc, int objindex, int faceindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_AddSelectUVVertex(IntPtr doc, int objindex, int faceindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_DeleteSelectUVVertex(IntPtr doc, int objindex, int faceindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_IsSelectUVVertex(IntPtr doc, int objindex, int faceindex, int vertindex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetScene(IntPtr doc, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetParentObject(IntPtr doc, IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_GetChildObjectCount(IntPtr doc, IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQDoc_GetChildObject(IntPtr doc, IntPtr obj, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_GetGlobalMatrix(IntPtr doc, IntPtr obj, out Matrix matrix);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_GetGlobalInverseMatrix(IntPtr doc, IntPtr obj, out Matrix matrix);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_InsertObject(IntPtr doc, IntPtr obj, IntPtr before);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQDoc_CreateUserData(IntPtr doc, ref MQUserDataInfo  info);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQDoc_DeleteUserData(IntPtr doc, int userdata_type, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQDoc_Triangulate(IntPtr doc, MQPoint[] points, int points_num, int[] index_array, int index_num);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQScene_InitSize(IntPtr scene, int width, int height);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQScene_GetProjMatrix(IntPtr scene, out Matrix matrix);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQScene_GetViewMatrix(IntPtr scene, out Matrix matrix);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQScene_FloatValue(IntPtr scene, MQScene type_id, float[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQScene_GetVisibleFace(IntPtr scene, IntPtr obj, bool[] visible);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQScene_IntValue(IntPtr scene, MQScene type_id, int[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_Delete(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern IntPtr MQObj_Clone(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_Merge(IntPtr dest, IntPtr source);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_Freeze(IntPtr obj, DWORD flag);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQObj_GetName(IntPtr obj, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetVertexCount(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_GetVertex(IntPtr obj, int index, out MQPoint pts);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetVertex(IntPtr obj, int index, ref MQPoint  pts);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_GetVertexArray(IntPtr obj, MQPoint[] ptsarray);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetFaceCount(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetFacePointCount(IntPtr obj, int face);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_GetFacePointArray(IntPtr obj, int face, int[] vertex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_GetFaceCoordinateArray(IntPtr obj, int face, MQCoordinate[] uvarray);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetFaceMaterial(IntPtr obj, int face);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern UINT MQObj_GetFaceUniqueID(IntPtr obj, int face);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetFaceIndexFromUniqueID(IntPtr obj, UINT unique_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQObj_SetName(IntPtr obj, [MarshalAs(UnmanagedType.LPStr)] string buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_AddVertex(IntPtr obj, ref MQPoint  p);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_DeleteVertex(IntPtr obj, int index, BOOL del_vert);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetVertexRefCount(IntPtr obj, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern UINT MQObj_GetVertexUniqueID(IntPtr obj, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetVertexIndexFromUniqueID(IntPtr obj, UINT unique_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetVertexRelatedFaces(IntPtr obj, int vertex, int[] faces);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQObj_GetVertexWeight(IntPtr obj, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetVertexWeight(IntPtr obj, int index, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_CopyVertexAttribute(IntPtr obj, int vert1, IntPtr obj2, int vert2);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_AddFace(IntPtr obj, int count, int[] index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_InsertFace(IntPtr obj, int face_index, int count, int[] vert_index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_DeleteFace(IntPtr obj, int index, BOOL del_vert);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_InvertFace(IntPtr obj, int index);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFaceMaterial(IntPtr obj, int face, int material);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFaceCoordinateArray(IntPtr obj, int face, MQCoordinate[] uvarray);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern DWORD MQObj_GetFaceVertexColor(IntPtr obj, int face, int vertex);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFaceVertexColor(IntPtr obj, int face, int vertex, DWORD color);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQObj_GetFaceEdgeCrease(IntPtr obj, int face, int line);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFaceEdgeCrease(IntPtr obj, int face, int line, float crease);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_GetFaceVisible(IntPtr obj, int face);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFaceVisible(IntPtr obj, int face, BOOL flag);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_OptimizeVertex(IntPtr obj, float distance, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1)] bool[] apply);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_Compact(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern DWORD MQObj_GetVisible(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetVisible(IntPtr obj, DWORD visible);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern DWORD MQObj_GetPatchType(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetPatchType(IntPtr obj, DWORD type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetPatchSegment(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetPatchSegment(IntPtr obj, int segment);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetShading(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetShading(IntPtr obj, int type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQObj_GetSmoothAngle(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetSmoothAngle(IntPtr obj, float degree);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetMirrorType(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetMirrorType(IntPtr obj, int type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern DWORD MQObj_GetMirrorAxis(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetMirrorAxis(IntPtr obj, DWORD axis);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQObj_GetMirrorDistance(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetMirrorDistance(IntPtr obj, float dis);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetLatheType(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetLatheType(IntPtr obj, int type);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern DWORD MQObj_GetLatheAxis(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetLatheAxis(IntPtr obj, DWORD axis);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetLatheSegment(IntPtr obj);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetLatheSegment(IntPtr obj, int segment);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQObj_GetIntValue(IntPtr obj, MQObjId type_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_GetFloatArray(IntPtr obj, MQObjId type_id, float[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetIntValue(IntPtr obj, MQObjId type_id, int value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_SetFloatArray(IntPtr obj, MQObjId type_id, float[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_PointerArray(IntPtr obj, MQObjId type_id, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_AllocUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_FreeUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_GetUserData(IntPtr obj, int userdata_id, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_SetUserData(IntPtr obj, int userdata_id, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_AllocVertexUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_FreeVertexUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_GetVertexUserData(IntPtr obj, int userdata_id, int vertex_start_index, int copy_vertex_num, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_SetVertexUserData(IntPtr obj, int userdata_id, int vertex_start_index, int copy_vertex_num, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_AllocFaceUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQObj_FreeFaceUserData(IntPtr obj, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_GetFaceUserData(IntPtr obj, int userdata_id, int face_start_index, int copy_face_num, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQObj_SetFaceUserData(IntPtr obj, int userdata_id, int face_start_index, int copy_face_num, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_Delete(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern int MQMat_GetIntValue(IntPtr mat, MQMatId type_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_GetFloatArray(IntPtr mat, MQMatId type_id, float[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_GetName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_GetColor(IntPtr mat, ref MQColor  color);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetAlpha(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetDiffuse(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetAmbient(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetEmission(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetSpecular(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern float MQMat_GetPower(IntPtr mat);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_GetTextureName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_GetAlphaName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_GetBumpName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] StringBuilder buffer, int size);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetIntValue(IntPtr mat, MQMatId type_id, int value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetFloatArray(IntPtr mat, MQMatId type_id, float[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_SetName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetColor(IntPtr mat, ref MQColor  color);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetAlpha(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetDiffuse(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetAmbient(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetEmission(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetSpecular(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetPower(IntPtr mat, float value);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_SetTextureName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_SetAlphaName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe", CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		public static extern void MQMat_SetBumpName(IntPtr mat, [MarshalAs(UnmanagedType.LPStr)] string name);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQMat_AllocUserData(IntPtr mat, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_FreeUserData(IntPtr mat, int userdata_id);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQMat_GetUserData(IntPtr mat, int userdata_id, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQMat_SetUserData(IntPtr mat, int userdata_id, int offset, int copy_bytes, byte[] buffer);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_GetValueArray(IntPtr mat, MQMatId type_id, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMat_SetValueArray(IntPtr mat, MQMatId type_id, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQShaderNode_GetValueArray(IntPtr shader, int type_id, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQShaderNode_SetValueArray(IntPtr shader, int type_id, IntPtr[] array);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQMatrix_FloatValue(float[] mtx, int type_id, float[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQXmlElem_Value(IntPtr elem, MQXmlelem type_id, IntPtr[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQXmlDoc_Value(IntPtr doc, MQXmldoc type_id, IntPtr[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern BOOL MQWidget_Value(int widget_id, int type_id, IntPtr[] values);
		[SuppressUnmanagedCodeSecurity, DllImport("Metaseq.exe")]
		public static extern void MQCanvas_Value(IntPtr canvas, int type_id, IntPtr[] values);
	}
}
