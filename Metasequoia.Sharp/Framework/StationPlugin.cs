using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	public abstract class StationPlugin : Plugin, IStationPlugin
	{
		readonly Dictionary<int, IntPtr> commandIdentifiers = new Dictionary<int, IntPtr>();
		readonly Dictionary<int, IntPtr> commandCaptions = new Dictionary<int, IntPtr>();

		public virtual bool IsActive { get; set; }
		public abstract string Caption { get; }
		public IStationPluginCommand[] Commands { get; }

		protected virtual bool OnInitialize() => true;
		protected virtual void OnExit() { }
		protected virtual void OnActivate(Document doc) => IsActive = true;
		protected virtual void OnDeactivate(Document doc) => IsActive = false;
		protected virtual void OnMinimize(Document doc) { }
		protected virtual void OnRestore(Document doc) { }
		protected virtual int OnUserMessageReceive(Document doc, uint authorId, uint pluginId, string description, IntPtr message) => 0;
		protected virtual void OnDraw(Document doc, Scene scene, int width, int height) { }
		protected virtual void OnNewDocument(Document doc, string filename, NewDocumentParameters param) { }
		protected virtual void OnEndDocument(Document doc) { }
		protected virtual void OnSaveDocument(Document doc, string filename, SaveDocumentParameters param) { }
		protected virtual void OnUndo(Document doc, int undoState) { }
		protected virtual void OnRedo(Document doc, int undoState) { }
		protected virtual void OnUndoUpdate(Document doc, int undoState, int undoSize) { }
		protected virtual void OnObjectModified(Document doc) { }
		protected virtual void OnObjectSelected(Document doc) { }
		protected virtual void OnObjectListUpdate(Document doc) { }
		protected virtual void OnMaterialModified(Document doc) { }
		protected virtual void OnMaterialListUpdate(Document doc) { }
		protected virtual void OnSceneUpdate(Document doc, Scene scene) { }
		protected virtual void OnEditOptionChange(Document doc, MQEditOption trigger) { }
		protected virtual bool OnExecuteCallback(Document doc, object state) => false;

		public void WindowClose()
		{
			using (var flag = new PinnedStructure<bool>(false))
				Metaseq.SendMessage(MQMessage.Activate, flag);
		}

		public void BeginCallback(object state) =>
			NativeMethods.MQ_StationCallback((doc, option) => OnExecuteCallback(Document.FromHandle(doc), state), IntPtr.Zero);

		public virtual bool OnEvent(Document doc, MQEvent eventType, IntPtr option)
		{
			using (var args = new NamedPtrDictionary(option))
				switch (eventType)
				{
					case MQEvent.Initialize:
						return OnInitialize();
					case MQEvent.Exit:
						OnExit();

						foreach (var i in commandIdentifiers.Values) Marshal.FreeCoTaskMem(i);
						foreach (var i in commandCaptions.Values) Marshal.FreeCoTaskMem(i);

						commandIdentifiers.Clear();
						commandCaptions.Clear();

						return true;
					case MQEvent.EnumSubcommand:
						{
							if (Commands == null) return false;

							var index = MarshalEx.ReadInt32(args["index"], 0);

							if (index < Commands.Length)
								Marshal.WriteIntPtr(args["result"], commandIdentifiers.ContainsKey(index)
									? commandIdentifiers[index]
									: commandIdentifiers[index] = Marshal.StringToCoTaskMemAnsi(Commands[index].Identifier));
							else
								Marshal.WriteIntPtr(args["result"], IntPtr.Zero);

							return true;
						}
					case MQEvent.SubcommandString:
						{
							if (Commands == null) return false;

							var index = MarshalEx.ReadInt32(args["index"], 0);

							if (index < Commands.Length)
								Marshal.WriteIntPtr(args["result"], commandCaptions.ContainsKey(index)
									? commandCaptions[index]
									: commandCaptions[index] = Marshal.StringToCoTaskMemUni(Commands[index].Caption));
							else
								Marshal.WriteIntPtr(args["result"], IntPtr.Zero);

							return true;
						}
					case MQEvent.Subcommand:
						{
							if (Commands == null) return false;

							var index = MarshalEx.ReadInt32(args["index"], 0);

							return index < Commands.Length
								&& Commands[index].Execute(doc);
						}
					case MQEvent.Activate:
						if (Marshal.ReadInt32(option) == 0)
							OnDeactivate(doc);
						else
							OnActivate(doc);

						return IsActive;
					case MQEvent.IsActivated:
						return IsActive;
					case MQEvent.Minimize:
						if (Marshal.ReadInt32(option) == 0)
							OnRestore(doc);
						else
							OnMinimize(doc);

						return true;
					case MQEvent.UserMessage:
						{
							var rt = OnUserMessageReceive
							(
								doc,
								(uint)MarshalEx.ReadInt32(args["src_product"], 0), 
								(uint)MarshalEx.ReadInt32(args["src_id"], 0), 
								Marshal.PtrToStringAnsi(args["description"]), 
								args["message"]
							);

							if (args["result"] != IntPtr.Zero)
								Marshal.WriteInt32(args["result"], rt);

							return true;
						}
					case MQEvent.Draw:
						OnDraw(doc, Scene.FromHandle(args["scene"]), MarshalEx.ReadInt32(args["width"], 1), MarshalEx.ReadInt32(args["height"], 1));

						return true;
					case MQEvent.NewDocument:
						OnNewDocument(doc, Marshal.PtrToStringAnsi(args["filename"]), new NewDocumentParameters(XmlElement.FromHandle(args["xml_elem"])));

						return true;
					case MQEvent.EndDocument:
						OnEndDocument(doc);

						return true;
					case MQEvent.SaveDocument:
						var param = new SaveDocumentParameters(XmlElement.FromHandle(args["xml_elem"]));

						OnSaveDocument(doc, Marshal.PtrToStringAnsi(args["filename"]), param);

						if (param.SaveUniqueId && args["save_uid"] != IntPtr.Zero)
							Marshal.WriteInt32(args["save_uid"], 1);

						return true;
					case MQEvent.Undo:
						OnUndo(doc, MarshalEx.ReadInt32(args["state"], 0));

						return false;
					case MQEvent.Redo:
						OnRedo(doc, MarshalEx.ReadInt32(args["state"], 0));

						return false;
					case MQEvent.UndoUpdated:
						OnUndoUpdate(doc, MarshalEx.ReadInt32(args["state"], 0), MarshalEx.ReadInt32(args["size"], 0));

						return true;
					case MQEvent.ObjectList:
						OnObjectListUpdate(doc);

						return true;
					case MQEvent.ObjectModified:
						OnObjectModified(doc);

						return true;
					case MQEvent.ObjectSelected:
						OnObjectSelected(doc);

						return true;
					case MQEvent.MaterialList:
						OnMaterialListUpdate(doc);

						return true;
					case MQEvent.MaterialModified:
						OnMaterialModified(doc);

						return true;
					case MQEvent.Scene:
						OnSceneUpdate(doc, Scene.FromHandle(args["scene"]));

						return true;
					case MQEvent.EditOption:
						var trigger = MQEditOption.Unknown;

						switch (Marshal.PtrToStringAnsi(args["trigger"]))
						{
							case "screen": trigger = MQEditOption.Screen; break;
							case "world": trigger = MQEditOption.World; break;
							case "local": trigger = MQEditOption.Local; break;
						}

						OnEditOptionChange(doc, trigger);

						return true;
					default:
						return false;
				}
		}
	}
}
