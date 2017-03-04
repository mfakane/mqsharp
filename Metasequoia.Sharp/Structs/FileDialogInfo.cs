using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	/// <summary>
	/// MQFileDialogInfo
	/// </summary>
	public struct FileDialogInfo
	{
		/// <summary>
		/// 構造体のサイズ。
		/// </summary>
		public int Size { get; set; }
		/// <summary>
		/// 非表示にする機能の組み合わせ。
		/// </summary>
		public MQFileDialogHidden Hidden { get; set; }
		/// <summary>
		/// 拡大縮小率。
		/// </summary>
		public float Scale { get; set; }

		[MarshalAs(UnmanagedType.LPStr)] string softwareName;
		/// <summary>
		/// 座標軸の向きに適合するソフトウェア名。
		/// </summary>
		public string SoftwareName
		{
			get => softwareName;
			set => softwareName = value;
		}

		/// <summary>
		/// X 軸の向き。
		/// </summary>
		public MQFileType AxisX { get; set; }
		/// <summary>
		/// Y 軸の向き。
		/// </summary>
		public MQFileType AxisY { get; set; }
		/// <summary>
		/// Z 軸の向き。
		/// </summary>
		public MQFileType AxisZ { get; set; }
		/// <summary>
		/// 面の向きを反転するかどうか。
		/// </summary>
		public bool InvertFace { get; set; }
		/// <summary>
		/// 座標系が右手系もしくは左手系であるかどうか。
		/// </summary>
		public MQFileDialogHand HandType { get; set; }
		/// <summary>
		/// ダイアログ表示前および表示後に呼び出されるコールバック関数。
		/// </summary>
		public MQFileDialogCallback DialogCallback { get; set; }
		/// <summary>
		/// コールバック関数に渡される任意の引数。
		/// </summary>
		public IntPtr DialogCallbackPtr { get; set; }
		/// <summary>
		/// バックグラウンド処理かどうか。
		/// </summary>
		public bool IsBackground { get; set; }
		/// <summary>
		/// 読み込みオプション。
		/// </summary>
		public IntPtr OptionArgs { get; set; }
	}

	public delegate void MQFileDialogCallback(bool init, MQFileDialogCallbackParam param, IntPtr ptr);

	public struct MQFileDialogCallbackParam
	{
		public int DialogId;
		public int ParentFrameId;
	}

	/// <summary>
	/// ファイルダイアログで非表示にする機能の組み合わせを示します。
	/// </summary>
	[Flags]
	public enum MQFileDialogHidden
	{
		/// <summary>
		/// なし。
		/// </summary>
		None,
		/// <summary>
		/// 拡大・縮小率の設定。
		/// </summary>
		Scale,
		/// <summary>
		/// 座標軸の向きの設定。
		/// </summary>
		Axis,
		/// <summary>
		/// 面の向きの反転。
		/// </summary>
		InvertFace = 4,
	}

	/// <summary>
	/// 座標系が右手系もしくは左手系であることを示します。
	/// </summary>
	public enum MQFileDialogHand
	{
		/// <summary>
		/// 左手系。
		/// </summary>
		Left,
		/// <summary>
		/// 右手系。
		/// </summary>
		Right,
	}
}
