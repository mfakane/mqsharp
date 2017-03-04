using System;

namespace Metasequoia
{
	/// <summary>
	/// MQAngle
	/// </summary>
	public struct Angle : IEquatable<Angle>
    {
		public static readonly Angle Zero = new Angle();

		public float Head { get; set; }
		public float Pitch { get; set; }
		public float Bank { get; set; }

		public Angle(float[] f) 
			: this(f[0], f[1], f[2])
		{
		}

		public Angle(float head, float pitch, float bank)
		{
			Head = head;
			Pitch = pitch;
			Bank = bank;
		}

		public static bool operator ==(Angle a, Angle b) => a.Head == b.Head && a.Pitch == b.Pitch && a.Bank == b.Bank;
		public static bool operator !=(Angle a, Angle b) => !(a == b);

		public override bool Equals(object obj) => obj is Angle angle && angle == this;
		public bool Equals(Angle other) => other == this;
		public override int GetHashCode() => typeof(Angle).GetHashCode() ^ Head.GetHashCode() ^ Pitch.GetHashCode() ^ Bank.GetHashCode();
		public override string ToString() => $"H:{Head} P:{Pitch} B:{Bank}";
		public float[] ToArray() => new[] { Head, Pitch, Bank };
	}
}
