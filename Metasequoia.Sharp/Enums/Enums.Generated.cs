using System;

namespace Metasequoia
{
	public enum MQPlugin
	{
		/// <summary>
		/// MQPLUGIN_VERSION
		/// </summary>
		Version = 0x0456,
		/// <summary>
		/// MQPLUGIN_REQUIRED_EXE_VERSION
		/// </summary>
		RequiredExeVersion = 0x4506,
	}

	public enum MQPluginType
	{
		/// <summary>
		/// MQPLUGIN_TYPE_IMPORT
		/// </summary>
		Import = 1,
		/// <summary>
		/// MQPLUGIN_TYPE_EXPORT
		/// </summary>
		Export = 2,
		/// <summary>
		/// MQPLUGIN_TYPE_CREATE
		/// </summary>
		Create = 3,
		/// <summary>
		/// MQPLUGIN_TYPE_OBJECT
		/// </summary>
		Object = 4,
		/// <summary>
		/// MQPLUGIN_TYPE_SELECT
		/// </summary>
		Select = 5,
		/// <summary>
		/// MQPLUGIN_TYPE_STATION
		/// </summary>
		Station = 6,
		/// <summary>
		/// MQPLUGIN_TYPE_COMMAND
		/// </summary>
		Command = 7,
	}

	public enum MQEvent
	{
		/// <summary>
		/// MQEVENT_INITIALIZE
		/// </summary>
		Initialize = 1,
		/// <summary>
		/// MQEVENT_EXIT
		/// </summary>
		Exit = 2,
		/// <summary>
		/// MQEVENT_ENUM_SUBCOMMAND
		/// </summary>
		EnumSubcommand = 3,
		/// <summary>
		/// MQEVENT_SUBCOMMAND_STRING
		/// </summary>
		SubcommandString = 4,
		/// <summary>
		/// MQEVENT_ACTIVATE
		/// </summary>
		Activate = 100,
		/// <summary>
		/// MQEVENT_IS_ACTIVATED
		/// </summary>
		IsActivated = 101,
		/// <summary>
		/// MQEVENT_MINIMIZE
		/// </summary>
		Minimize = 102,
		/// <summary>
		/// MQEVENT_USER_MESSAGE
		/// </summary>
		UserMessage = 103,
		/// <summary>
		/// MQEVENT_SUBCOMMAND
		/// </summary>
		Subcommand = 104,
		/// <summary>
		/// MQEVENT_DRAW
		/// </summary>
		Draw = 110,
		/// <summary>
		/// MQEVENT_LBUTTON_DOWN
		/// </summary>
		LbuttonDown = 120,
		/// <summary>
		/// MQEVENT_LBUTTON_MOVE
		/// </summary>
		LbuttonMove = 121,
		/// <summary>
		/// MQEVENT_LBUTTON_UP
		/// </summary>
		LbuttonUp = 122,
		/// <summary>
		/// MQEVENT_MBUTTON_DOWN
		/// </summary>
		MbuttonDown = 123,
		/// <summary>
		/// MQEVENT_MBUTTON_MOVE
		/// </summary>
		MbuttonMove = 124,
		/// <summary>
		/// MQEVENT_MBUTTON_UP
		/// </summary>
		MbuttonUp = 125,
		/// <summary>
		/// MQEVENT_RBUTTON_DOWN
		/// </summary>
		RbuttonDown = 126,
		/// <summary>
		/// MQEVENT_RBUTTON_MOVE
		/// </summary>
		RbuttonMove = 127,
		/// <summary>
		/// MQEVENT_RBUTTON_UP
		/// </summary>
		RbuttonUp = 128,
		/// <summary>
		/// MQEVENT_MOUSE_MOVE
		/// </summary>
		MouseMove = 129,
		/// <summary>
		/// MQEVENT_MOUSE_WHEEL
		/// </summary>
		MouseWheel = 130,
		/// <summary>
		/// MQEVENT_KEY_DOWN
		/// </summary>
		KeyDown = 140,
		/// <summary>
		/// MQEVENT_KEY_UP
		/// </summary>
		KeyUp = 141,
		/// <summary>
		/// MQEVENT_NEW_DOCUMENT
		/// </summary>
		NewDocument = 200,
		/// <summary>
		/// MQEVENT_END_DOCUMENT
		/// </summary>
		EndDocument = 201,
		/// <summary>
		/// MQEVENT_SAVE_DOCUMENT
		/// </summary>
		SaveDocument = 202,
		/// <summary>
		/// MQEVENT_UNDO
		/// </summary>
		Undo = 210,
		/// <summary>
		/// MQEVENT_REDO
		/// </summary>
		Redo = 211,
		/// <summary>
		/// MQEVENT_UNDO_UPDATED
		/// </summary>
		UndoUpdated = 212,
		/// <summary>
		/// MQEVENT_OBJECT_LIST
		/// </summary>
		ObjectList = 220,
		/// <summary>
		/// MQEVENT_OBJECT_MODIFIED
		/// </summary>
		ObjectModified = 221,
		/// <summary>
		/// MQEVENT_OBJECT_SELECTED
		/// </summary>
		ObjectSelected = 222,
		/// <summary>
		/// MQEVENT_MATERIAL_LIST
		/// </summary>
		MaterialList = 230,
		/// <summary>
		/// MQEVENT_MATERIAL_MODIFIED
		/// </summary>
		MaterialModified = 231,
		/// <summary>
		/// MQEVENT_SCENE
		/// </summary>
		Scene = 240,
		/// <summary>
		/// MQEVENT_EDIT_OPTION
		/// </summary>
		EditOption = 250,
		/// <summary>
		/// MQEVENT_IMPORT_SUPPORT_BACKGROUND
		/// </summary>
		ImportSupportBackground = 300,
		/// <summary>
		/// MQEVENT_IMPORT_SET_OPTIONS
		/// </summary>
		ImportSetOptions = 301,
	}

	public enum MQMessage
	{
		/// <summary>
		/// MQMESSAGE_ACTIVATE
		/// </summary>
		Activate = 100,
		/// <summary>
		/// MQMESSAGE_USER_MESSAGE
		/// </summary>
		UserMessage = 101,
		/// <summary>
		/// MQMESSAGE_NEW_DRAW_OBJECT
		/// </summary>
		NewDrawObject = 200,
		/// <summary>
		/// MQMESSAGE_NEW_DRAW_MATERIAL
		/// </summary>
		NewDrawMaterial = 201,
		/// <summary>
		/// MQMESSAGE_NEW_DRAW_TEXT
		/// </summary>
		NewDrawText = 202,
		/// <summary>
		/// MQMESSAGE_DELETE_DRAW_OBJECT
		/// </summary>
		DeleteDrawObject = 210,
		/// <summary>
		/// MQMESSAGE_DELETE_DRAW_MATERIAL
		/// </summary>
		DeleteDrawMaterial = 211,
		/// <summary>
		/// MQMESSAGE_DELETE_DRAW_TEXT
		/// </summary>
		DeleteDrawText = 212,
		/// <summary>
		/// MQMESSAGE_SET_DRAW_PROXY_OBJECT
		/// </summary>
		SetDrawProxyObject = 220,
		/// <summary>
		/// MQMESSAGE_GET_UNDO_STATE
		/// </summary>
		GetUndoState = 300,
		/// <summary>
		/// MQMESSAGE_UPDATE_UNDO
		/// </summary>
		UpdateUndo = 301,
		/// <summary>
		/// MQMESSAGE_REDRAW_SCENE
		/// </summary>
		RedrawScene = 400,
		/// <summary>
		/// MQMESSAGE_REDRAW_ALL_SCENE
		/// </summary>
		RedrawAllScene = 401,
		/// <summary>
		/// MQMESSAGE_GET_SCENE_OPTION
		/// </summary>
		GetSceneOption = 410,
		/// <summary>
		/// MQMESSAGE_HIT_TEST
		/// </summary>
		HitTest = 411,
		/// <summary>
		/// MQMESSAGE_GET_EDIT_OPTION
		/// </summary>
		GetEditOption = 500,
		/// <summary>
		/// MQMESSAGE_GET_SNAPPED_POS
		/// </summary>
		GetSnappedPos = 501,
		/// <summary>
		/// MQMESSAGE_GET_RESOURCE_CURSOR
		/// </summary>
		GetResourceCursor = 502,
		/// <summary>
		/// MQMESSAGE_GET_SETTING_ELEMENT
		/// </summary>
		GetSettingElement = 505,
		/// <summary>
		/// MQMESSAGE_GET_RESOURCE_TEXT
		/// </summary>
		GetResourceText = 506,
		/// <summary>
		/// MQMESSAGE_GET_SYSTEM_COLOR
		/// </summary>
		GetSystemColor = 507,
		/// <summary>
		/// MQMESSAGE_GET_TEXT_VALUE
		/// </summary>
		GetTextValue = 508,
		/// <summary>
		/// MQMESSAGE_GET_SCREEN_MOUSE_CURSOR
		/// </summary>
		GetScreenMouseCursor = 509,
		/// <summary>
		/// MQMESSAGE_GET_LUT
		/// </summary>
		GetLut = 510,
		/// <summary>
		/// MQMESSAGE_SET_MOUSE_CURSOR
		/// </summary>
		SetMouseCursor = 603,
		/// <summary>
		/// MQMESSAGE_SET_STATUS_STRING
		/// </summary>
		SetStatusString = 604,
		/// <summary>
		/// MQMESSAGE_SET_TEXT_VALUE
		/// </summary>
		SetTextValue = 608,
		/// <summary>
		/// MQMESSAGE_SET_SCREEN_MOUSE_CURSOR
		/// </summary>
		SetScreenMouseCursor = 609,
		/// <summary>
		/// MQMESSAGE_IMPORT_QUERY_CANCEL
		/// </summary>
		ImportQueryCancel = 700,
		/// <summary>
		/// MQMESSAGE_IMPORT_PROGRESS
		/// </summary>
		ImportProgress = 701,
		/// <summary>
		/// MQMESSAGE_REGISTER_KEYBUTTON
		/// </summary>
		RegisterKeybutton = 800,
	}

	public enum MQFileType
	{
		/// <summary>
		/// MQFILE_TYPE_LEFT
		/// </summary>
		Left = 0,
		/// <summary>
		/// MQFILE_TYPE_RIGHT
		/// </summary>
		Right = 1,
		/// <summary>
		/// MQFILE_TYPE_UP
		/// </summary>
		Up = 2,
		/// <summary>
		/// MQFILE_TYPE_DOWN
		/// </summary>
		Down = 3,
		/// <summary>
		/// MQFILE_TYPE_FRONT
		/// </summary>
		Front = 4,
		/// <summary>
		/// MQFILE_TYPE_BACK
		/// </summary>
		Back = 5,
	}

	public enum MQFolder
	{
		/// <summary>
		/// MQFOLDER_ROOT
		/// </summary>
		Root = 1,
		/// <summary>
		/// MQFOLDER_METASEQ_EXE
		/// </summary>
		MetaseqExe = 2,
		/// <summary>
		/// MQFOLDER_METASEQ_INI
		/// </summary>
		MetaseqIni = 3,
		/// <summary>
		/// MQFOLDER_DATA
		/// </summary>
		Data = 4,
		/// <summary>
		/// MQFOLDER_PLUGINS
		/// </summary>
		Plugins = 5,
	}

	[Flags]
	public enum MQDocClearselect
	{
		/// <summary>
		/// MQDOC_CLEARSELECT_VERTEX
		/// </summary>
		Vertex = 1,
		/// <summary>
		/// MQDOC_CLEARSELECT_LINE
		/// </summary>
		Line = 2,
		/// <summary>
		/// MQDOC_CLEARSELECT_FACE
		/// </summary>
		Face = 4,
		/// <summary>
		/// MQDOC_CLEARSELECT_ALL
		/// </summary>
		All = 7,
	}

	public enum MQMapping
	{
		/// <summary>
		/// MQMAPPING_TEXTURE
		/// </summary>
		Texture = 1,
		/// <summary>
		/// MQMAPPING_ALPHA
		/// </summary>
		Alpha = 2,
		/// <summary>
		/// MQMAPPING_BUMP
		/// </summary>
		Bump = 3,
		/// <summary>
		/// MQMAPPING_HLSL
		/// </summary>
		Hlsl = 4,
	}

	public enum MQUserdata
	{
		/// <summary>
		/// MQUSERDATA_OBJECT
		/// </summary>
		Object = 1,
		/// <summary>
		/// MQUSERDATA_MATERIAL
		/// </summary>
		Material = 2,
		/// <summary>
		/// MQUSERDATA_VERTEX
		/// </summary>
		Vertex = 3,
		/// <summary>
		/// MQUSERDATA_FACE
		/// </summary>
		Face = 4,
	}

	public enum MQScene
	{
		/// <summary>
		/// MQSCENE_GET_CAMERA_POS
		/// </summary>
		GetCameraPos = 0x101,
		/// <summary>
		/// MQSCENE_GET_CAMERA_ANGLE
		/// </summary>
		GetCameraAngle = 0x102,
		/// <summary>
		/// MQSCENE_GET_LOOK_AT_POS
		/// </summary>
		GetLookAtPos = 0x103,
		/// <summary>
		/// MQSCENE_GET_ROTATION_CENTER
		/// </summary>
		GetRotationCenter = 0x104,
		/// <summary>
		/// MQSCENE_GET_FOV
		/// </summary>
		GetFov = 0x105,
		/// <summary>
		/// MQSCENE_GET_DIRECTIONAL_LIGHT
		/// </summary>
		GetDirectionalLight = 0x106,
		/// <summary>
		/// MQSCENE_GET_AMBIENT_COLOR
		/// </summary>
		GetAmbientColor = 0x107,
		/// <summary>
		/// MQSCENE_GET_FRONT_Z
		/// </summary>
		GetFrontZ = 0x108,
		/// <summary>
		/// MQSCENE_GET_ORTHO
		/// </summary>
		GetOrtho = 0x109,
		/// <summary>
		/// MQSCENE_GET_ZOOM
		/// </summary>
		GetZoom = 0x10,
		/// <summary>
		/// MQSCENE_GET_FRONT_CLIP
		/// </summary>
		GetFrontClip = 0x10,
		/// <summary>
		/// MQSCENE_GET_BACK_CLIP
		/// </summary>
		GetBackClip = 0x10,
		/// <summary>
		/// MQSCENE_SET_CAMERA_POS
		/// </summary>
		SetCameraPos = 0x201,
		/// <summary>
		/// MQSCENE_SET_CAMERA_ANGLE
		/// </summary>
		SetCameraAngle = 0x202,
		/// <summary>
		/// MQSCENE_SET_LOOK_AT_POS
		/// </summary>
		SetLookAtPos = 0x203,
		/// <summary>
		/// MQSCENE_SET_ROTATION_CENTER
		/// </summary>
		SetRotationCenter = 0x204,
		/// <summary>
		/// MQSCENE_SET_FOV
		/// </summary>
		SetFov = 0x205,
		/// <summary>
		/// MQSCENE_SET_DIRECTIONAL_LIGHT
		/// </summary>
		SetDirectionalLight = 0x206,
		/// <summary>
		/// MQSCENE_SET_AMBIENT_COLOR
		/// </summary>
		SetAmbientColor = 0x207,
		/// <summary>
		/// MQSCENE_SET_ORTHO
		/// </summary>
		SetOrtho = 0x209,
		/// <summary>
		/// MQSCENE_SET_ZOOM
		/// </summary>
		SetZoom = 0x20,
		/// <summary>
		/// MQSCENE_CONVERT_3D_TO_SCREEN
		/// </summary>
		Convert3dToScreen = 0x300,
		/// <summary>
		/// MQSCENE_CONVERT_SCREEN_TO_3D
		/// </summary>
		ConvertScreenTo3d = 0x301,
		/// <summary>
		/// MQSCENE_ADD_MULTILIGHT
		/// </summary>
		AddMultilight = 0x400,
		/// <summary>
		/// MQSCENE_DELETE_MULTILIGHT
		/// </summary>
		DeleteMultilight = 0x401,
		/// <summary>
		/// MQSCENE_GET_MULTILIGHT_NUMBER
		/// </summary>
		GetMultilightNumber = 0x402,
		/// <summary>
		/// MQSCENE_SET_MULTILIGHT_INDEX
		/// </summary>
		SetMultilightIndex = 0x403,
		/// <summary>
		/// MQSCENE_GET_MULTILIGHT_DIR
		/// </summary>
		GetMultilightDir = 0x404,
		/// <summary>
		/// MQSCENE_SET_MULTILIGHT_DIR
		/// </summary>
		SetMultilightDir = 0x405,
		/// <summary>
		/// MQSCENE_GET_MULTILIGHT_COLOR
		/// </summary>
		GetMultilightColor = 0x406,
		/// <summary>
		/// MQSCENE_SET_MULTILIGHT_COLOR
		/// </summary>
		SetMultilightColor = 0x407,
	}

	public enum MQObjectType
	{
		/// <summary>
		/// MQOBJECT_TYPE_NORMAL
		/// </summary>
		Normal = 0,
		/// <summary>
		/// MQOBJECT_TYPE_POINT_LIGHT
		/// </summary>
		PointLight = 1,
		/// <summary>
		/// MQOBJECT_TYPE_DIRECTIONAL_LIGHT
		/// </summary>
		DirectionalLight = 2,
	}

	[Flags]
	public enum MQObjectFreeze
	{
		/// <summary>
		/// MQOBJECT_FREEZE_PATCH
		/// </summary>
		Patch = 0x00000001,
		/// <summary>
		/// MQOBJECT_FREEZE_MIRROR
		/// </summary>
		Mirror = 0x00000002,
		/// <summary>
		/// MQOBJECT_FREEZE_LATHE
		/// </summary>
		Lathe = 0x00000004,
		/// <summary>
		/// MQOBJECT_FREEZE_ALL
		/// </summary>
		All = 0x7,
	}

	public enum MQObjectPatch
	{
		/// <summary>
		/// MQOBJECT_PATCH_MAX
		/// </summary>
		Max = 4,
		/// <summary>
		/// MQOBJECT_PATCH_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_PATCH_SPLINE1
		/// </summary>
		Spline1 = 1,
		/// <summary>
		/// MQOBJECT_PATCH_SPLINE2
		/// </summary>
		Spline2 = 2,
		/// <summary>
		/// MQOBJECT_PATCH_CATMULL
		/// </summary>
		Catmull = 3,
		/// <summary>
		/// MQOBJECT_PATCH_OPENSUBDIV
		/// </summary>
		Opensubdiv = 4,
	}

	public enum MQObjectInterp
	{
		/// <summary>
		/// MQOBJECT_INTERP_EDGE_ONLY
		/// </summary>
		EdgeOnly = 1,
		/// <summary>
		/// MQOBJECT_INTERP_EDGE_AND_CORNER
		/// </summary>
		EdgeAndCorner = 2,
		/// <summary>
		/// MQOBJECT_INTERP_ALWAY_SHARP
		/// </summary>
		AlwaySharp = 3,
	}

	public enum MQObjectUvinterp
	{
		/// <summary>
		/// MQOBJECT_UVINTERP_NONE
		/// </summary>
		None = 1,
		/// <summary>
		/// MQOBJECT_UVINTERP_CORNERS_ONLY
		/// </summary>
		CornersOnly = 4,
		/// <summary>
		/// MQOBJECT_UVINTERP_CORNERS_PLUS1
		/// </summary>
		CornersPlus1 = 2,
		/// <summary>
		/// MQOBJECT_UVINTERP_CORNERS_PLUS2
		/// </summary>
		CornersPlus2 = 5,
		/// <summary>
		/// MQOBJECT_UVINTERP_BOUNDARIES
		/// </summary>
		Boundaries = 3,
		/// <summary>
		/// MQOBJECT_UVINTERP_ALL
		/// </summary>
		All = 0,
	}

	public enum MQObjectShade
	{
		/// <summary>
		/// MQOBJECT_SHADE_MAX
		/// </summary>
		Max = 1,
		/// <summary>
		/// MQOBJECT_SHADE_FLAT
		/// </summary>
		Flat = 0,
		/// <summary>
		/// MQOBJECT_SHADE_GOURAUD
		/// </summary>
		Gouraud = 1,
	}

	public enum MQObjectMirror
	{
		/// <summary>
		/// MQOBJECT_MIRROR_MAX
		/// </summary>
		Max = 2,
		/// <summary>
		/// MQOBJECT_MIRROR_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_MIRROR_NORMAL
		/// </summary>
		Normal = 1,
		/// <summary>
		/// MQOBJECT_MIRROR_JOIN
		/// </summary>
		Join = 2,
	}

	[Flags]
	public enum MQObjectMirrorAxis
	{
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_X
		/// </summary>
		X = 1,
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_Y
		/// </summary>
		Y = 2,
		/// <summary>
		/// MQOBJECT_MIRROR_AXIS_Z
		/// </summary>
		Z = 4,
	}

	public enum MQObjectLathe
	{
		/// <summary>
		/// MQOBJECT_LATHE_MAX
		/// </summary>
		Max = 3,
		/// <summary>
		/// MQOBJECT_LATHE_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQOBJECT_LATHE_FRONT
		/// </summary>
		Front = 1,
		/// <summary>
		/// MQOBJECT_LATHE_BACK
		/// </summary>
		Back = 2,
		/// <summary>
		/// MQOBJECT_LATHE_BOTH
		/// </summary>
		Both = 3,
	}

	public enum MQObjectLatheAxis
	{
		/// <summary>
		/// MQOBJECT_LATHE_X
		/// </summary>
		X = 0,
		/// <summary>
		/// MQOBJECT_LATHE_Y
		/// </summary>
		Y = 1,
		/// <summary>
		/// MQOBJECT_LATHE_Z
		/// </summary>
		Z = 2,
	}

	public enum MQMaterialShader
	{
		/// <summary>
		/// MQMATERIAL_SHADER_CLASSIC
		/// </summary>
		Classic = 0,
		/// <summary>
		/// MQMATERIAL_SHADER_CONSTANT
		/// </summary>
		Constant = 1,
		/// <summary>
		/// MQMATERIAL_SHADER_LAMBERT
		/// </summary>
		Lambert = 2,
		/// <summary>
		/// MQMATERIAL_SHADER_PHONG
		/// </summary>
		Phong = 3,
		/// <summary>
		/// MQMATERIAL_SHADER_BLINN
		/// </summary>
		Blinn = 4,
		/// <summary>
		/// MQMATERIAL_SHADER_HLSL
		/// </summary>
		Hlsl = 5,
	}

	public enum MQMaterialVertexColor
	{
		/// <summary>
		/// MQMATERIAL_VERTEXCOLOR_DISABLE
		/// </summary>
		Disable = 0,
		/// <summary>
		/// MQMATERIAL_VERTEXCOLOR_DIFFUSE
		/// </summary>
		Diffuse = 1,
	}

	public enum MQMaterialProjection
	{
		/// <summary>
		/// MQMATERIAL_PROJECTION_UV
		/// </summary>
		Uv = 0,
		/// <summary>
		/// MQMATERIAL_PROJECTION_FLAT
		/// </summary>
		Flat = 1,
		/// <summary>
		/// MQMATERIAL_PROJECTION_CYLINDER
		/// </summary>
		Cylinder = 2,
		/// <summary>
		/// MQMATERIAL_PROJECTION_SPHERE
		/// </summary>
		Sphere = 3,
	}

	public enum MQMaterialShaderParamType
	{
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_NONE
		/// </summary>
		None = 0,
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_BOOL
		/// </summary>
		Bool = 1,
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_INT
		/// </summary>
		Int = 2,
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_FLOAT
		/// </summary>
		Float = 3,
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_COLOR
		/// </summary>
		Color = 4,
		/// <summary>
		/// MQMATERIAL_SHADER_PARAM_TYPE_STRING
		/// </summary>
		String = 5,
	}

	public enum MQObjId
	{
		/// <summary>
		/// MQOBJ_ID_DEPTH
		/// </summary>
		Depth = 0x101,
		/// <summary>
		/// MQOBJ_ID_FOLDING
		/// </summary>
		Folding = 0x102,
		/// <summary>
		/// MQOBJ_ID_LOCKING
		/// </summary>
		Locking = 0x103,
		/// <summary>
		/// MQOBJ_ID_UNIQUE_ID
		/// </summary>
		UniqueId = 0x104,
		/// <summary>
		/// MQOBJ_ID_TYPE
		/// </summary>
		Type = 0x105,
		/// <summary>
		/// MQOBJ_ID_SELECTED
		/// </summary>
		Selected = 0x106,
		/// <summary>
		/// MQOBJ_ID_PATCH_TRIANGLE
		/// </summary>
		PatchTriangle = 0x107,
		/// <summary>
		/// MQOBJ_ID_PATCH_SMOOTH_TRIANGLE
		/// </summary>
		PatchSmoothTriangle = 0x108,
		/// <summary>
		/// MQOBJ_ID_PATCH_MESH_INTERP
		/// </summary>
		PatchMeshInterp = 0x109,
		/// <summary>
		/// MQOBJ_ID_PATCH_UV_INTERP
		/// </summary>
		PatchUvInterp = 0x10,
		/// <summary>
		/// MQOBJ_ID_PATCH_LIMIT_SURFACE
		/// </summary>
		PatchLimitSurface = 0x10,
		/// <summary>
		/// MQOBJ_ID_COLOR
		/// </summary>
		Color = 0x201,
		/// <summary>
		/// MQOBJ_ID_COLOR_VALID
		/// </summary>
		ColorValid = 0x201,
		/// <summary>
		/// MQOBJ_ID_SCALING
		/// </summary>
		Scaling = 0x301,
		/// <summary>
		/// MQOBJ_ID_ROTATION
		/// </summary>
		Rotation = 0x302,
		/// <summary>
		/// MQOBJ_ID_TRANSLATION
		/// </summary>
		Translation = 0x303,
		/// <summary>
		/// MQOBJ_ID_LOCAL_MATRIX
		/// </summary>
		LocalMatrix = 0x304,
		/// <summary>
		/// MQOBJ_ID_LOCAL_INVERSE_MATRIX
		/// </summary>
		LocalInverseMatrix = 0x305,
		/// <summary>
		/// MQOBJ_ID_LIGHT_VALUE
		/// </summary>
		LightValue = 0x401,
		/// <summary>
		/// MQOBJ_ID_LIGHT_ATTENUATION
		/// </summary>
		LightAttenuation = 0x402,
		/// <summary>
		/// MQOBJ_ID_LIGHT_FALLOFF_END
		/// </summary>
		LightFalloffEnd = 0x403,
		/// <summary>
		/// MQOBJ_ID_LIGHT_FALLOFF_HALF
		/// </summary>
		LightFalloffHalf = 0x404,
		/// <summary>
		/// MQOBJ_ID_ADD_RENDER_FLAG
		/// </summary>
		AddRenderFlag = 0x501,
		/// <summary>
		/// MQOBJ_ID_REMOVE_RENDER_FLAG
		/// </summary>
		RemoveRenderFlag = 0x502,
		/// <summary>
		/// MQOBJ_ID_ADD_ERASE_FLAG
		/// </summary>
		AddEraseFlag = 0x503,
		/// <summary>
		/// MQOBJ_ID_REMOVE_ERASE_FLAG
		/// </summary>
		RemoveEraseFlag = 0x504,
	}

	public enum MQMatId
	{
		/// <summary>
		/// MQMAT_ID_SHADER
		/// </summary>
		Shader = 0x101,
		/// <summary>
		/// MQMAT_ID_VERTEXCOLOR
		/// </summary>
		Vertexcolor = 0x102,
		/// <summary>
		/// MQMAT_ID_UNIQUE_ID
		/// </summary>
		UniqueId = 0x103,
		/// <summary>
		/// MQMAT_ID_DOUBLESIDED
		/// </summary>
		Doublesided = 0x104,
		/// <summary>
		/// MQMAT_ID_SELECTED
		/// </summary>
		Selected = 0x106,
		/// <summary>
		/// MQMAT_ID_REFLECTION
		/// </summary>
		Reflection = 0x208,
		/// <summary>
		/// MQMAT_ID_REFRACTION
		/// </summary>
		Refraction = 0x209,
		/// <summary>
		/// MQMAT_ID_AMBIENT_COLOR
		/// </summary>
		AmbientColor = 0x210,
		/// <summary>
		/// MQMAT_ID_SPECULAR_COLOR
		/// </summary>
		SpecularColor = 0x211,
		/// <summary>
		/// MQMAT_ID_EMISSION_COLOR
		/// </summary>
		EmissionColor = 0x212,
		/// <summary>
		/// MQMAT_ID_MAPPROJ
		/// </summary>
		Mapproj = 0x301,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_POSITION
		/// </summary>
		MapprojPosition = 0x302,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_SCALING
		/// </summary>
		MapprojScaling = 0x303,
		/// <summary>
		/// MQMAT_ID_MAPPROJ_ANGLE
		/// </summary>
		MapprojAngle = 0x304,
		/// <summary>
		/// MQMAT_ID_SHADER_NAME
		/// </summary>
		ShaderName = 0x401,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_NUM
		/// </summary>
		ShaderParamNum = 0x402,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_NAME
		/// </summary>
		ShaderParamName = 0x403,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_VALUE_TYPE
		/// </summary>
		ShaderParamValueType = 0x404,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_ARRAY_SIZE
		/// </summary>
		ShaderParamArraySize = 0x405,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_INT_VALUE
		/// </summary>
		ShaderParamIntValue = 0x406,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_FLOAT_VALUE
		/// </summary>
		ShaderParamFloatValue = 0x407,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_COLOR_VALUE
		/// </summary>
		ShaderParamColorValue = 0x408,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_NUM
		/// </summary>
		ShaderMapNum = 0x409,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_NAME
		/// </summary>
		ShaderMapName = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_FILENAME
		/// </summary>
		ShaderMapFilename = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_FILENAME_W
		/// </summary>
		ShaderMapFilenameW = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_OPTION
		/// </summary>
		ShaderOption = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_OPTION
		/// </summary>
		ShaderParamOption = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_OPTION
		/// </summary>
		ShaderMapOption = 0x40,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_NUM
		/// </summary>
		ShaderMapParamNum = 0x410,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_NAME
		/// </summary>
		ShaderMapParamName = 0x411,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_VALUE_TYPE
		/// </summary>
		ShaderMapParamValueType = 0x412,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_ARRAY_SIZE
		/// </summary>
		ShaderMapParamArraySize = 0x413,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_INT_VALUE
		/// </summary>
		ShaderMapParamIntValue = 0x414,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_FLOAT_VALUE
		/// </summary>
		ShaderMapParamFloatValue = 0x415,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_COLOR_VALUE
		/// </summary>
		ShaderMapParamColorValue = 0x416,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_OPTION
		/// </summary>
		ShaderMapParamOption = 0x417,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_CONNECT_NODE_ID
		/// </summary>
		ShaderMapConnectNodeId = 0x418,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_CONNECT_NODE
		/// </summary>
		ShaderMapConnectNode = 0x419,
		/// <summary>
		/// MQMAT_ID_SHADER_OUTPUT_NUM
		/// </summary>
		ShaderOutputNum = 0x420,
		/// <summary>
		/// MQMAT_ID_SHADER_OUTPUT_NAME
		/// </summary>
		ShaderOutputName = 0x421,
		/// <summary>
		/// MQMAT_ID_SHADER_OUTPUT_OPTION
		/// </summary>
		ShaderOutputOption = 0x422,
		/// <summary>
		/// MQMAT_ID_SHADER_OUTPUT_VALUE_TYPE
		/// </summary>
		ShaderOutputValueType = 0x423,
		/// <summary>
		/// MQMAT_ID_SHADER_PARAM_STRING_VALUE
		/// </summary>
		ShaderParamStringValue = 0x424,
		/// <summary>
		/// MQMAT_ID_SHADER_MAP_PARAM_STRING_VALUE
		/// </summary>
		ShaderMapParamStringValue = 0x425,
		/// <summary>
		/// MQMAT_ID_SUBSHADER_CREATE
		/// </summary>
		SubshaderCreate = 0x500,
		/// <summary>
		/// MQMAT_ID_SUBSHADER_DELETE
		/// </summary>
		SubshaderDelete = 0x501,
		/// <summary>
		/// MQMAT_ID_SUBSHADER
		/// </summary>
		Subshader = 0x502,
		/// <summary>
		/// MQMAT_ID_SUBSHADER_NUM
		/// </summary>
		SubshaderNum = 0x503,
		/// <summary>
		/// MQMAT_ID_SUBSHADER_ID
		/// </summary>
		SubshaderId = 0x504,
	}

	public enum MQMatrix
	{
		/// <summary>
		/// MQMATRIX_GET_SCALING
		/// </summary>
		GetScaling = 0x101,
		/// <summary>
		/// MQMATRIX_GET_ROTATION
		/// </summary>
		GetRotation = 0x102,
		/// <summary>
		/// MQMATRIX_GET_TRANSLATION
		/// </summary>
		GetTranslation = 0x103,
		/// <summary>
		/// MQMATRIX_GET_INVERSE_TRANSFORM
		/// </summary>
		GetInverseTransform = 0x105,
		/// <summary>
		/// MQMATRIX_SET_TRANSFORM
		/// </summary>
		SetTransform = 0x204,
		/// <summary>
		/// MQMATRIX_SET_INVERSE_TRANSFORM
		/// </summary>
		SetInverseTransform = 0x205,
	}

	public enum MQXmlelem
	{
		/// <summary>
		/// MQXMLELEM_ADD_CHILD_ELEMENT
		/// </summary>
		AddChildElement = 0x101,
		/// <summary>
		/// MQXMLELEM_REMOVE_CHILD_ELEMENT
		/// </summary>
		RemoveChildElement = 0x102,
		/// <summary>
		/// MQXMLELEM_FIRST_CHILD_ELEMENT
		/// </summary>
		FirstChildElement = 0x201,
		/// <summary>
		/// MQXMLELEM_NEXT_CHILD_ELEMENT
		/// </summary>
		NextChildElement = 0x202,
		/// <summary>
		/// MQXMLELEM_GET_PARENT_ELEMENT
		/// </summary>
		GetParentElement = 0x203,
		/// <summary>
		/// MQXMLELEM_GET_NAME
		/// </summary>
		GetName = 0x301,
		/// <summary>
		/// MQXMLELEM_GET_TEXT
		/// </summary>
		GetText = 0x302,
		/// <summary>
		/// MQXMLELEM_GET_ATTRIBUTE
		/// </summary>
		GetAttribute = 0x303,
		/// <summary>
		/// MQXMLELEM_SET_TEXT
		/// </summary>
		SetText = 0x402,
		/// <summary>
		/// MQXMLELEM_SET_ATTRIBUTE
		/// </summary>
		SetAttribute = 0x403,
	}

	public enum MQXmldoc
	{
		/// <summary>
		/// MQXMLDOC_CREATE
		/// </summary>
		Create = 0x001,
		/// <summary>
		/// MQXMLDOC_DELETE
		/// </summary>
		Delete = 0x002,
		/// <summary>
		/// MQXMLDOC_LOAD
		/// </summary>
		Load = 0x003,
		/// <summary>
		/// MQXMLDOC_SAVE
		/// </summary>
		Save = 0x004,
		/// <summary>
		/// MQXMLDOC_CREATE_ROOT_ELEMENT
		/// </summary>
		CreateRootElement = 0x101,
		/// <summary>
		/// MQXMLDOC_GET_ROOT_ELEMENT
		/// </summary>
		GetRootElement = 0x102,
	}

	public enum MQWidget
	{
		/// <summary>
		/// MQWIDGET_EXECUTE
		/// </summary>
		Execute = 0x001,
		/// <summary>
		/// MQWIDGET_FIND
		/// </summary>
		Find = 0x100,
		/// <summary>
		/// MQWIDGET_CREATE
		/// </summary>
		Create = 0x101,
		/// <summary>
		/// MQWIDGET_DELETE
		/// </summary>
		Delete = 0x102,
		/// <summary>
		/// MQWIDGET_ADD_CHILD
		/// </summary>
		AddChild = 0x201,
		/// <summary>
		/// MQWIDGET_REMOVE_CHILD
		/// </summary>
		RemoveChild = 0x202,
		/// <summary>
		/// MQWIDGET_ADD_CHILD_WINDOW
		/// </summary>
		AddChildWindow = 0x203,
		/// <summary>
		/// MQWIDGET_REMOVE_CHILD_WINDOW
		/// </summary>
		RemoveChildWindow = 0x204,
		/// <summary>
		/// MQWIDGET_GET
		/// </summary>
		Get = 0x301,
		/// <summary>
		/// MQWIDGET_SET
		/// </summary>
		Set = 0x302,
		/// <summary>
		/// MQWIDGET_EDIT
		/// </summary>
		Edit = 0x303,
		/// <summary>
		/// MQWIDGET_ADD_EVENT
		/// </summary>
		AddEvent = 0x304,
		/// <summary>
		/// MQWIDGET_REMOVE_EVENT
		/// </summary>
		RemoveEvent = 0x305,
	}

}
