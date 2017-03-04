using System;

namespace Metasequoia
{
	/// <summary>
	/// MQColor
	/// </summary>
	public struct Color : IEquatable<Color>
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }

		public Color(float value)
			: this(value, value, value)
		{
		}

		public Color(float[] f)
			: this(f[0], f[1], f[2])
		{
		}

		public Color(float r, float g, float b)
		{
			R = r;
			G = g;
			B = b;
		}

		public static Color operator +(Color self) => self;
		public static Color operator -(Color self) => new Color(-self.R, -self.G, -self.B);
		public static Color operator +(Color a, Color b) => new Color(a.R + b.R, a.G + b.G, a.B + b.B);
		public static Color operator -(Color a, Color b) => new Color(a.R - b.R, a.G - b.G, a.B - b.B);
		public static Color operator +(Color a, float b) => new Color(a.R + b, a.G + b, a.B + b);
		public static Color operator -(Color a, float b) => new Color(a.R - b, a.G - b, a.B - b);
		public static Color operator -(float a, Color b) => new Color(a - b.R, a - b.R, a - b.B);
		public static Color operator *(Color a, float b) => new Color(a.R * b, a.G * b, a.B * b);
		public static Color operator /(Color a, float b) => new Color(a.R / b, a.G / b, a.B / b);
		public static bool operator ==(Color a, Color b) => a.R == b.R && a.G == b.G && a.B == b.B;
		public static bool operator !=(Color a, Color b) => !(a == b);

		public override bool Equals(object obj) => obj is Color color && color == this;
		public bool Equals(Color other) => other == this;
		public override int GetHashCode() => typeof(Color).GetHashCode() ^ R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
		public override string ToString() => $"R:{R} G:{G} B:{B}";
		public float[] ToArray() => new[] { R, G, B };
	}
}
