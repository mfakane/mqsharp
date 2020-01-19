using System;

namespace Metasequoia
{
	public abstract class CommandPlugin : StationPlugin
    {
        public MQPluginType PluginType => MQPluginType.Command;

		protected virtual bool OnMouseLeftButtonDown(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseLeftButtonMove(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseLeftButtonUp(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseMiddleButtonDown(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseMiddleButtonMove(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseMiddleButtonUp(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseRightButtonDown(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseRightButtonMove(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseRightButtonUp(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseMove(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnMouseWheel(Document doc, Scene scene, MouseState state) => false;
		protected virtual bool OnKeyDown(Document doc, Scene scene, KeyState state) => false;
		protected virtual bool OnKeyUp(Document doc, Scene scene, KeyState state) => false;

		public override bool OnEvent(Document doc, MQEvent eventType, IntPtr option)
		{
			switch (eventType)
			{
				case MQEvent.LbuttonDown:
				case MQEvent.LbuttonMove:
				case MQEvent.LbuttonUp:
				case MQEvent.MbuttonDown:
				case MQEvent.MbuttonMove:
				case MQEvent.MbuttonUp:
				case MQEvent.RbuttonDown:
				case MQEvent.RbuttonMove:
				case MQEvent.RbuttonUp:
				case MQEvent.MouseMove:
				case MQEvent.MouseWheel:
					using (var args = new NamedPtrDictionary(option))
					{
						var scene = Scene.FromHandle(args["scene"]);
						var state = new MouseState
						{
							X = MarshalEx.ReadInt32(args["mouse_pos_x"], 0),
							Y = MarshalEx.ReadInt32(args["mouse_pos_y"], 0),
							WheelDelta = MarshalEx.ReadInt32(args["mouse_wheel"], 0),
							ButtonState = (MQMouseButtons)MarshalEx.ReadInt32(args["button_state"], 0),
						};

						switch (eventType)
						{
							case MQEvent.LbuttonDown: return OnMouseLeftButtonDown(doc, scene, state);
							case MQEvent.LbuttonMove: return OnMouseLeftButtonMove(doc, scene, state);
							case MQEvent.LbuttonUp: return OnMouseLeftButtonUp(doc, scene, state);
							case MQEvent.MbuttonDown: return OnMouseMiddleButtonDown(doc, scene, state);
							case MQEvent.MbuttonMove: return OnMouseMiddleButtonMove(doc, scene, state);
							case MQEvent.MbuttonUp: return OnMouseMiddleButtonUp(doc, scene, state);
							case MQEvent.RbuttonDown: return OnMouseRightButtonDown(doc, scene, state);
							case MQEvent.RbuttonMove: return OnMouseRightButtonMove(doc, scene, state);
							case MQEvent.RbuttonUp: return OnMouseRightButtonUp(doc, scene, state);
							case MQEvent.MouseMove: return OnMouseMove(doc, scene, state);
							case MQEvent.MouseWheel: return OnMouseWheel(doc, scene, state);
							default: return false;
						}
					}
				case MQEvent.KeyDown:
				case MQEvent.KeyUp:
					using (var args = new NamedPtrDictionary(option))
					{
						var scene = Scene.FromHandle(args["scene"]);
						var state = new KeyState
						{
							Key = MarshalEx.ReadInt32(args["key"], 0),
							ButtonState = (MQMouseButtons)MarshalEx.ReadInt32(args["button_state"], 0),
						};

						switch (eventType)
						{
							case MQEvent.KeyDown: return OnKeyDown(doc, scene, state);
							case MQEvent.KeyUp: return OnKeyUp(doc, scene, state);
							default: return false;
						}
					}
				default:
					return base.OnEvent(doc, eventType, option);
			}
		}
	}
}
