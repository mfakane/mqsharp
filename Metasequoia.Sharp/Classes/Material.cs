using System;
using System.Collections.Generic;
using System.Text;

namespace Metasequoia
{
	public sealed class Material : IDisposable
	{
		int? index;

		public IntPtr Handle { get; }
		public Document Document { get; set; }
		public bool IsDrawingMaterial { get; private set; }
		public bool IsInstantDrawingMaterial { get; private set; }
		public uint UniqueId => (uint)NativeMethods.MQMat_GetIntValue(Handle, MQMatId.UniqueId);

		public int Index
		{
			get => index ?? Document.Materials.IndexOf(this);
			private set => index = value;
		}

		#region Properties

		public string Name
		{
			get
			{
				var sb = new StringBuilder(256);

				NativeMethods.MQMat_GetName(Handle, sb, sb.Capacity);

				return sb.ToString();
			}
			set => NativeMethods.MQMat_SetName(Handle, value);
		}

		public bool IsSelected
		{
			get => NativeMethods.MQMat_GetIntValue(Handle, MQMatId.Selected) != 0;
			set => NativeMethods.MQMat_SetIntValue(Handle, MQMatId.Selected, value ? 1 : 0);
		}

		public MQMaterialShader Shader
		{
			get => (MQMaterialShader)NativeMethods.MQMat_GetIntValue(Handle, MQMatId.Shader);
			set => NativeMethods.MQMat_SetIntValue(Handle, MQMatId.Shader, (int)value);
		}

		public MQMaterialVertexColor VertexColorType
		{
			get => (MQMaterialVertexColor)NativeMethods.MQMat_GetIntValue(Handle, MQMatId.Vertexcolor);
			set => NativeMethods.MQMat_SetIntValue(Handle, MQMatId.Vertexcolor, (int)value);
		}

		public bool IsDoubleSided
		{
			get => NativeMethods.MQMat_GetIntValue(Handle, MQMatId.Doublesided) != 0;
			set => NativeMethods.MQMat_SetIntValue(Handle, MQMatId.Doublesided, value ? 1 : 0);
		}

		public float Alpha
		{
			get => NativeMethods.MQMat_GetAlpha(Handle);
			set => NativeMethods.MQMat_SetAlpha(Handle, value);
		}

		public Color Color
		{
			get
			{
				var rt = new Color();

				NativeMethods.MQMat_GetColor(Handle, ref rt);

				return rt;
			}
			set => NativeMethods.MQMat_SetColor(Handle, ref value);
		}

		public Color AmbientColor
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.AmbientColor, val);

				return new Color(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.AmbientColor, value.ToArray());
		}

		public Color EmissionColor
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.EmissionColor, val);

				return new Color(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.EmissionColor, value.ToArray());
		}

		public Color SpecularColor
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.SpecularColor, val);

				return new Color(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.SpecularColor, value.ToArray());
		}

		public float Diffuse
		{
			get => NativeMethods.MQMat_GetDiffuse(Handle);
			set => NativeMethods.MQMat_SetDiffuse(Handle, value);
		}

		public float Ambient
		{
			get => NativeMethods.MQMat_GetAmbient(Handle);
			set => NativeMethods.MQMat_SetAmbient(Handle, value);
		}

		public float Emission
		{
			get => NativeMethods.MQMat_GetEmission(Handle);
			set => NativeMethods.MQMat_SetEmission(Handle, value);
		}

		public float Specular
		{
			get => NativeMethods.MQMat_GetSpecular(Handle);
			set => NativeMethods.MQMat_SetSpecular(Handle, value);
		}

		public float Power
		{
			get => NativeMethods.MQMat_GetPower(Handle);
			set => NativeMethods.MQMat_SetPower(Handle, value);
		}

		public float Reflection
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.Reflection, val);

				return val[0];
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.Reflection, new[] { value });
		}

		public float Refraction
		{
			get
			{
				var val = new float[1];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.Refraction, val);

				return val[0];
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.Refraction, new[] { value });
		}

		#endregion
		#region Mapping

		public string TextureName
		{
			get
			{
				var sb = new StringBuilder(256);

				NativeMethods.MQMat_GetTextureName(Handle, sb, sb.Capacity);

				return sb.ToString();
			}
			set => NativeMethods.MQMat_SetTextureName(Handle, value);
		}

		public string AlphaName
		{
			get
			{
				var sb = new StringBuilder(256);

				NativeMethods.MQMat_GetAlphaName(Handle, sb, sb.Capacity);

				return sb.ToString();
			}
			set => NativeMethods.MQMat_SetAlphaName(Handle, value);
		}

		public string BumpName
		{
			get
			{
				var sb = new StringBuilder(256);

				NativeMethods.MQMat_GetBumpName(Handle, sb, sb.Capacity);

				return sb.ToString();
			}
			set => NativeMethods.MQMat_SetBumpName(Handle, value);
		}

		public MQMaterialProjection MappingType
		{
			get => (MQMaterialProjection)NativeMethods.MQMat_GetIntValue(Handle, MQMatId.Mapproj);
			set => NativeMethods.MQMat_SetIntValue(Handle, MQMatId.Mapproj, (int)value);
		}

		public Point MappingPosition
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.MapprojPosition, val);

				return new Point(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.MapprojPosition, value.ToArray());
		}

		public Point MappingScaling
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.MapprojScaling, val);

				return new Point(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.MapprojScaling, value.ToArray());
		}

		public Angle MappingAngle
		{
			get
			{
				var val = new float[3];

				NativeMethods.MQMat_GetFloatArray(Handle, MQMatId.MapprojAngle, val);

				return new Angle(val);
			}
			set => NativeMethods.MQMat_SetFloatArray(Handle, MQMatId.MapprojAngle, value.ToArray());
		}

		#endregion
		#region Shader

		public string ShaderName
		{
			get
			{
				using (var size = new PinnedStructure<int>(64))
				using (var length = new PinnedStructure<int>())
				using (var buffer = new AnsiString(size.Value))
				{
					var val = new IntPtr[]
					{
						buffer,
						size,
						length,
					};

					NativeMethods.MQMat_GetValueArray(Handle, MQMatId.ShaderName, val);

					return buffer.Value;
				}
			}
			set
			{
				using (var result = new PinnedStructure<bool>())
				using (var name = new AnsiString(value))
				{
					var val = new IntPtr[]
					{
						name,
						result,
					};

					NativeMethods.MQMat_SetValueArray(Handle, MQMatId.ShaderName, val);
				}
			}
		}

		#endregion

		public Material()
			: this(null, NativeMethods.MQ_CreateMaterial())
		{
		}

		Material(Document doc, IntPtr ptr)
		{
			Document = doc;
			Handle = ptr;
		}

		public static Material FromHandle(Document doc, IntPtr ptr) => new Material(doc, ptr);
		public static Material FromIndex(Document doc, int index) => new Material(doc, NativeMethods.MQDoc_GetMaterial(doc.Handle, index)) { Index = index };
		public static Material FromUniqueId(Document doc, uint uniqueId) => new Material(doc, NativeMethods.MQDoc_GetMaterialFromUniqueID(doc.Handle, (int)uniqueId));

		public static Material CreateDrawingMaterial(Document doc, bool isInstant = true)
		{
			using (var index = new PinnedStructure<int>())
			using (var isInstantPtr = new PinnedStructure<bool>(isInstant))
			using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
			{
				["document"] = doc.Handle,
				["index"] = index,
				["instant"] = isInstantPtr,
				["result"] = IntPtr.Zero,
			}))
			{
				Metaseq.SendMessage(MQMessage.NewDrawMaterial, args);

				return new Material(doc, args["result"])
				{
					IsDrawingMaterial = true,
					IsInstantDrawingMaterial = isInstant,
					Index = index.Value,
				};
			}
		}

		public void Dispose()
		{
			if (!IsDrawingMaterial)
				NativeMethods.MQMat_Delete(Handle);
			else if (!IsInstantDrawingMaterial)
				using (var args = new NamedPtrDictionary(new Dictionary<string, IntPtr>
				{
					["document"] = Document.Handle,
					["material"] = Handle,
				}))
					Metaseq.SendMessage(MQMessage.DeleteDrawMaterial, args);
		}

		public override bool Equals(object obj) => obj is Material mat && mat.Handle == Handle;
		public override int GetHashCode() => typeof(Material).GetHashCode() ^ Handle.GetHashCode();
		public override string ToString() => $"MQMaterial: {UniqueId}, \"{Name}\"";
	}
}
