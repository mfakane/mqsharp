using System;

namespace Metasequoia
{
	/// <summary>
	/// MQColorRGBA
	/// </summary>
	public struct ColorRGBA : IEquatable<ColorRGBA>
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }

		public ColorRGBA(float value)
			: this(value, value, value, value)
		{
		}

		public ColorRGBA(float r, float g, float b, float a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public static ColorRGBA operator +(ColorRGBA self) => self;
		public static ColorRGBA operator -(ColorRGBA self) => new ColorRGBA(-self.R, -self.G, -self.B, -self.A);
		public static ColorRGBA operator +(ColorRGBA a, ColorRGBA b) => new ColorRGBA(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
		public static ColorRGBA operator -(ColorRGBA a, ColorRGBA b) => new ColorRGBA(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
		public static ColorRGBA operator +(ColorRGBA a, float b) => new ColorRGBA(a.R + b, a.G + b, a.B + b, a.A + b);
		public static ColorRGBA operator -(ColorRGBA a, float b) => new ColorRGBA(a.R - b, a.G - b, a.B - b, a.A - b);
		public static ColorRGBA operator -(float a, ColorRGBA b) => new ColorRGBA(a - b.R, a - b.R, a - b.B, a - b.A);
		public static ColorRGBA operator *(ColorRGBA a, float b) => new ColorRGBA(a.R * b, a.G * b, a.B * b, a.A * b);
		public static ColorRGBA operator /(ColorRGBA a, float b) => new ColorRGBA(a.R / b, a.G / b, a.B / b, a.A / b);
		public static bool operator ==(ColorRGBA a, ColorRGBA b) => a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
		public static bool operator !=(ColorRGBA a, ColorRGBA b) => !(a == b);

		public override bool Equals(object obj) => obj is ColorRGBA color && color == this;
		public bool Equals(ColorRGBA other) => other == this;
		public override int GetHashCode() => typeof(ColorRGBA).GetHashCode() ^ R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
		public override string ToString() => $"R:{R} G:{G} B:{B} A:{A}";
	}
}
