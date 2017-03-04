using System;

namespace Metasequoia
{
	/// <summary>
	/// MQCoordinate
	/// </summary>
	public struct Coordinate : IEquatable<Coordinate>
	{
		public static readonly Coordinate Zero = new Coordinate();
		public static readonly Coordinate One = new Coordinate(1, 1);

		public float U { get; set; }
		public float V { get; set; }

		public Coordinate(float u, float v)
		{
			U = u;
			V = v;
		}

		public static Coordinate operator +(Coordinate self) => self;
		public static Coordinate operator -(Coordinate self) => new Coordinate(-self.U, -self.V);
		public static Coordinate operator +(Coordinate a, Coordinate b) => new Coordinate(a.U + b.U, a.V + b.V);
		public static Coordinate operator -(Coordinate a, Coordinate b) => new Coordinate(a.U - b.U, a.V - b.V);
		public static Coordinate operator +(Coordinate a, float b) => new Coordinate(a.U + b, a.V + b);
		public static Coordinate operator -(Coordinate a, float b) => new Coordinate(a.U - b, a.V - b);
		public static Coordinate operator -(float a, Coordinate b) => new Coordinate(a - b.U, a - b.U);
		public static Coordinate operator *(Coordinate a, float b) => new Coordinate(a.U * b, a.V * b);
		public static Coordinate operator /(Coordinate a, float b) => new Coordinate(a.U / b, a.V / b);
		public static bool operator ==(Coordinate a, Coordinate b) => a.U == b.U && a.V == b.V;
		public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);

		public override bool Equals(object obj) => obj is Coordinate coord && coord == this;
		public bool Equals(Coordinate other) => other == this;
		public override int GetHashCode() => typeof(Coordinate).GetHashCode() ^ U.GetHashCode() ^ V.GetHashCode();
		public override string ToString() => $"U:{U} V:{V}";
	}
}
