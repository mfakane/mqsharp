namespace Metasequoia
{
	public class GlobalLight
	{
		public Scene Scene { get; }
		public int Index { get; }

		public Point Direction
		{
			get
			{
				var idx = new int[] { Index };
				var val = new float[3];

				NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.SetMultilightIndex, idx);
				NativeMethods.MQScene_FloatValue(Scene.Handle, MQScene.GetMultilightDir, val);

				return new Point(val);
			}
			set
			{
				var idx = new int[] { Index };

				NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.SetMultilightIndex, idx);
				NativeMethods.MQScene_FloatValue(Scene.Handle, MQScene.SetMultilightDir, value.ToArray());
			}
		}

		public Color Color
		{
			get
			{
				var idx = new int[] { Index };
				var val = new float[3];

				NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.SetMultilightIndex, idx);
				NativeMethods.MQScene_FloatValue(Scene.Handle, MQScene.GetMultilightColor, val);

				return new Color(val);
			}
			set
			{
				var idx = new int[] { Index };

				NativeMethods.MQScene_IntValue(Scene.Handle, MQScene.SetMultilightIndex, idx);
				NativeMethods.MQScene_FloatValue(Scene.Handle, MQScene.SetMultilightColor, value.ToArray());
			}
		}

		public GlobalLight(Scene scene, int index)
		{
			Scene = scene;
			Index = index;
		}
	}
}
