using System;
using System.Collections.Generic;

namespace Metasequoia
{
	/// <summary>
	/// MQScene
	/// </summary>
	public class Scene
	{
		public IntPtr Handle { get; }
		public SceneGlobalLightCollection Lights { get; }

		public Matrix View
		{
			get
			{
				NativeMethods.MQScene_GetViewMatrix(Handle, out var rt);

				return rt;
			}
		}

		public Matrix Projection
		{
			get
			{
				NativeMethods.MQScene_GetProjMatrix(Handle, out var rt);

				return rt;
			}
		}

		#region Camera

		public Point CameraPosition
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetCameraPos, val);

				return new Point(val);
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetCameraPos, value.ToArray());
		}

		public Angle CameraAngle
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetCameraAngle, val);

				return new Angle(val);
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetCameraAngle, value.ToArray());
		}

		public Point LookAt
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetLookAtPos, val);

				return new Point(val);
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.GetLookAtPos, value.ToArray());
		}

		public Point RotationCenter
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetRotationCenter, val);

				return new Point(val);
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetRotationCenter, value.ToArray());
		}

		public float FieldOfView
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetFov, val);

				return val[0];
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetFov, new[] { value });
		}

		public float Zoom
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetZoom, val);

				return val[0];
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetZoom, new[] { value });
		}

		public bool IsOrtho
		{
			get
			{
				var val = new int[1];

				NativeMethods.MQScene_IntValue(Handle, MQScene.GetOrtho, val);

				return val[0] != 0;
			}
			set => NativeMethods.MQScene_IntValue(Handle, MQScene.SetOrtho, new[] { value ? 1 : 0 });
		}

		public float FrontClip
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetFrontClip, val);

				return val[0];
			}
		}

		public float BackClip
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetBackClip, val);

				return val[0];
			}
		}

		#endregion

		public Color AmbientColor
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetAmbientColor, val);

				return new Color(val);
			}
			set => NativeMethods.MQScene_FloatValue(Handle, MQScene.SetAmbientColor, value.ToArray());
		}

		public float FrontZ
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQScene_FloatValue(Handle, MQScene.GetFrontZ, val);

				return val[0];
			}
		}

		public SceneOptions SceneOptions
		{
			get
			{
				using (var options = new PinnedStructure<SceneOptions>())
				using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["scene"] = Handle,
					["show_vertex"] = IntPtr.Add(options, 0),
					["show_line"] = IntPtr.Add(options, 4),
					["show_face"] = IntPtr.Add(options, 8),
					["front_only"] = IntPtr.Add(options, 12),
					["show_bkimg"] = IntPtr.Add(options, 16),
				}))
				{
					Metaseq.SendMessage(MQMessage.GetSceneOption, args);

					return options.Value;
				}
			}
		}

		Scene(IntPtr ptr)
		{
			Handle = ptr;
			Lights = new SceneGlobalLightCollection(this);
		}

		public static Scene FromHandle(IntPtr ptr) => new Scene(ptr);

		public Point ConvertToScreen(Point point3d, out float w)
		{
			var val = new float[] { point3d.X, point3d.Y, point3d.Z, 0, 0, 0, 0 };

			NativeMethods.MQScene_FloatValue(Handle, MQScene.Convert3dToScreen, val);
			w = val[6];

			return new Point(val[3], val[4], val[5]);
		}

		public Point ConvertTo3d(Point pointScreen)
		{
			var val = new float[] { pointScreen.X, pointScreen.Y, pointScreen.Z, 0, 0, 0 };

			NativeMethods.MQScene_FloatValue(Handle, MQScene.ConvertScreenTo3d, val);

			return new Point(val[3], val[4], val[5]);
		}
		
		public override bool Equals(object obj) => obj is Scene scene && scene.Handle == Handle;
		public override int GetHashCode() => typeof(Scene).GetHashCode() ^ Handle.GetHashCode();
	}
}
