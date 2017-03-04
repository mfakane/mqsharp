using System;
using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	public abstract class Plugin : IPlugin
	{
		readonly Lazy<PluginAttribute> pluginAttribute;

		public virtual uint AuthorId => pluginAttribute.Value.AuthorId;
		public virtual uint PluginId => pluginAttribute.Value.PluginId;
		public virtual string DisplayName => pluginAttribute.Value.DisplayName;

		public IntPtr ScreenCursorHandle
		{
			get
			{
				using (var result = new PinnedStructure<IntPtr>())
				using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr> { ["cursor"] = result }))
				{
					Metaseq.SendMessage(MQMessage.GetScreenMouseCursor, option);

					return result.Value;
				}
			}
			set
			{
				using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr> { ["cursor"] = value }))
					Metaseq.SendMessage(MQMessage.SetScreenMouseCursor, option);
			}
		}

		public Plugin() =>
			pluginAttribute = new Lazy<PluginAttribute>(() => GetType().GetCustomAttributes(typeof(PluginAttribute), false).Cast<PluginAttribute>().First());

		public string GetResourceString(string id)
		{
			using (var name = new AnsiString(id))
			using (var result = new UnicodeString(4096))
			using (var resultSize = new PinnedStructure<int>())
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["name"] = name,
				["result"] = result,
				["result_size"] = resultSize,
			}))
				if (Metaseq.SendMessage(MQMessage.GetResourceText, option))
					return result.Value;

			return null;
		}

		public string GetSettingValue(MQSettingValue type)
		{
			using (var name = new AnsiString(GetSettingName()))
			using (var result = new UnicodeString(100))
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["name"] = name,
				["result_w"] = result,
			}))
			{
				Metaseq.SendMessage(MQMessage.GetTextValue, option);

				return result.Value;
			}

			string GetSettingName()
			{
				switch (type)
				{
					case MQSettingValue.Language: return "language";
					case MQSettingValue.RotationHandle: return "rotation_handle";
					case MQSettingValue.HandleSize: return "handle_size";
					case MQSettingValue.HandleScale: return "handle_scale";
					case MQSettingValue.NormalMapFlip: return "normalmap_flip";
					default: return null;
				}
			}
		}

		public IntPtr GetResourceCursorHandle(MQCursorType cursorType)
		{
			using (var cursorTypePin = new PinnedObject((int)cursorType))
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["index"] = cursorTypePin,
				["result"] = IntPtr.Zero,
			}))
			{
				Metaseq.SendMessage(MQMessage.GetResourceCursor, option);

				return option["result"];
			}
		}

		public Color GetSystemColor(MQSystemColor colorType)
		{
			using (var name = new AnsiString(GetSystemColorName()))
			using (var result = new PinnedStructure<Color>())
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["name"] = name,
				["result.rgb"] = result,
			}))
			{
				Metaseq.SendMessage(MQMessage.GetSystemColor, option);

				return result.Value;
			}

			string GetSystemColorName()
			{
				switch (colorType)
				{
					case MQSystemColor.Object: return "object";
					case MQSystemColor.Select: return "select";
					case MQSystemColor.Temp: return "temp";
					case MQSystemColor.Highlight: return "highlight";
					case MQSystemColor.Inactive: return "unactive";
					case MQSystemColor.MeshYZ: return "mesh_yz";
					case MQSystemColor.MeshXZ: return "mesh_xz";
					case MQSystemColor.MeshXY: return "mesh_xy";
					case MQSystemColor.BlobPlus: return "blob_plus";
					case MQSystemColor.BlobMinus: return "blob_minus";
					case MQSystemColor.UV: return "uv";
					case MQSystemColor.AxisX: return "axis_x";
					case MQSystemColor.AxisY: return "axis_y";
					case MQSystemColor.AxisZ: return "axis_z";
					case MQSystemColor.AxisW: return "axis_w";
					case MQSystemColor.AxisActive: return "axis_active";
					case MQSystemColor.PreviewBackground: return "preview_back";
					default: return null;
				}
			}
		}

		public int SendUserMessage(Document doc, uint authorId, uint pluginId, string description, IntPtr message)
		{
			using (var authorIdPin = new PinnedObject(authorId))
			using (var pluginIdPin = new PinnedObject(pluginId))
			using (var descriptionPin = new AnsiString(description))
			using (var result = new PinnedStructure<int>())
			using (var option = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["document"] = doc.Handle,
				["target_product"] = authorIdPin,
				["target_id"] = pluginIdPin,
				["description"] = descriptionPin,
				["message"] = message,
				["result"] = result,
			}))
			{
				Metaseq.SendMessage(MQMessage.UserMessage, option);

				return result.Value;
			}
		}
	}
}
