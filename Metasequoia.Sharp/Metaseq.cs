using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	public static class Metaseq
	{
		public static IntPtr WindowHandle => NativeMethods.MQ_GetWindowHandle();

		public static void ShowFileDialog(string title, ref FileDialogInfo info)
		{
			info.Size = Marshal.SizeOf<FileDialogInfo>();
			NativeMethods.MQ_ShowFileDialog(title, ref info);
		}

		public static void ImportAxis(FileDialogInfo info, Point[] pts) =>
			NativeMethods.MQ_ImportAxis(ref info, pts, pts.Length);

		public static void ExportAxis(FileDialogInfo info, Point[] pts) =>
			NativeMethods.MQ_ExportAxis(ref info, pts, pts.Length);

		public static string GetSystemPath(MQFolder type)
		{
			var sb = new StringBuilder(260);

			NativeMethods.MQ_GetSystemPathW(sb, (int)type);

			return sb.ToString();
		}

		public static void RefreshView() =>
			NativeMethods.MQ_RefreshView(IntPtr.Zero);

		public static void StationCallback(StationCallback callback) =>
			NativeMethods.MQ_StationCallback((doc, option) => callback(Document.FromHandle(doc)), IntPtr.Zero);

		public static bool SendMessage(MQMessage messageType, SendMessageInfo info) =>
			NativeMethods.MQ_SendMessage((int)messageType, ref info);

		public static bool SendMessage(MQMessage messageType, IntPtr option) =>
			SendMessage(messageType, new SendMessageInfo
			{
				ProductId = EntryPoint.PluginInstance.AuthorId,
				PluginId = EntryPoint.PluginInstance.PluginId,
				Option = option,
			});
	}

	public delegate bool StationCallback(Document doc);
}
