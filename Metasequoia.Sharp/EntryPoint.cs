using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class EntryPoint
	{
		static Lazy<IPlugin> pluginInstance = new Lazy<IPlugin>(() =>
			(IPlugin)Activator.CreateInstance(Assembly.GetExecutingAssembly()
													  .GetExportedTypes()
													  .First(i => i.IsClass && !i.IsAbstract && typeof(IPlugin).IsAssignableFrom(i))));
		public static IPlugin PluginInstance => pluginInstance.Value;

		[DllExport(CallingConvention.Cdecl)]
		public static uint MQCheckVersion(uint exeVersion) =>
			exeVersion >= (uint)MQPlugin.RequiredExeVersion ? (uint)MQPlugin.Version : 0;

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQInit([MarshalAs(UnmanagedType.LPStr)] string exeName) =>
			true;

		[DllExport(CallingConvention.Cdecl)]
		public static void MQGetPlugInID(ref uint product, ref uint id)
		{
			product = PluginInstance.AuthorId;
			id = PluginInstance.PluginId;
		}

		[DllExport(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQGetPlugInName() => PluginInstance.DisplayName;

		[DllExport(CallingConvention.Cdecl)]
		public static MQPluginType MQGetPlugInType()
		{
			switch (PluginInstance)
			{
				case IImportPlugin _: return MQPluginType.Import;
				case IExportPlugin _: return MQPluginType.Export;
				case ICreatePlugin _: return MQPluginType.Create;
				case IObjectPlugin _: return MQPluginType.Object;
				case ISelectPlugin _: return MQPluginType.Select;
				case IStationPlugin plugin: return plugin.PluginType;
				default: throw new InvalidOperationException();
			}
		}

		[DllExport(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQEnumFileType(int index) =>
			PluginInstance is IImportExportPlugin plugin &&
			index < (plugin.AvailableFileTypes?.Length ?? 0)
				? plugin.AvailableFileTypes[index].DisplayName
				: null;

		[DllExport(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQEnumFileExt(int index) =>
			PluginInstance is IImportExportPlugin plugin &&
			index < (plugin.AvailableFileTypes?.Length ?? 0)
				? plugin.AvailableFileTypes[index].Extension
				: null;

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQImportFile(int index, [MarshalAs(UnmanagedType.LPStr)] string filename, IntPtr doc) =>
			PluginInstance is IImportPlugin plugin &&
			index < (plugin.AvailableFileTypes?.Length ?? 0) &&
			plugin.Import(plugin.AvailableFileTypes[index], filename, Document.FromHandle(doc));

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQExportFile(int index, [MarshalAs(UnmanagedType.LPStr)] string filename, IntPtr doc) =>
			PluginInstance is IExportPlugin plugin &&
			index < (plugin.AvailableFileTypes?.Length ?? 0) &&
			plugin.Export(plugin.AvailableFileTypes[index], filename, Document.FromHandle(doc));

		[DllExport(CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static string MQEnumString(int index) =>
			PluginInstance is IMenuPlugin menuPlugin && index < (menuPlugin.Commands?.Length ?? 0) ? menuPlugin.Commands[index].Caption :
			PluginInstance is IStationPlugin stationPlugin && index == 0 ? stationPlugin.Caption : null;

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQCreate(int index, IntPtr doc) =>
			PluginInstance is ICreatePlugin plugin &&
			index < (plugin.Commands?.Length ?? 0) &&
			plugin.Commands[index].Execute(Document.FromHandle(doc));

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQModifyObject(int index, IntPtr doc) =>
			PluginInstance is IObjectPlugin plugin &&
			index < (plugin.Commands?.Length ?? 0) &&
			plugin.Commands[index].Execute(Document.FromHandle(doc));

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQModifySelect(int index, IntPtr doc) =>
			PluginInstance is ISelectPlugin plugin &&
			index < (plugin.Commands?.Length ?? 0) &&
			plugin.Commands[index].Execute(Document.FromHandle(doc));

		[DllExport(CallingConvention.Cdecl)]
		public static bool MQOnEvent(IntPtr doc, int eventType, IntPtr option)
		{
			if (PluginInstance is IStationPlugin stationPlugin)
				return stationPlugin.OnEvent(Document.FromHandle(doc), (MQEvent)eventType, option);
			else if (PluginInstance is IImportPlugin importPlugin)
				switch ((MQEvent)eventType)
				{
					case MQEvent.ImportSupportBackground:
						return importPlugin.SupportsBackgroundLoading;
					case MQEvent.ImportSetOptions:
						using (var args = new NamedPtrDictionary(option))
						{
							importPlugin.IsBackground = MarshalEx.ReadInt32(args["background"], 0) != 0;
							importPlugin.ImportOptions = args["args"];
						}

						return true;
				}

			return false;
		}
	}
}
