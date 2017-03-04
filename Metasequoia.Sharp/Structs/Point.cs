using System;

namespace Metasequoia
{
	/// <summary>
	/// MQPoint
	/// </summary>
	public struct Point : IEquatable<Point>
	{
		public static readonly Point Zero = new Point();
		public static readonly Point One = new Point(1);

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public Point(float value)
			: this(value, value, value)
		{
		}

		public Point(float[] f)
			: this(f[0], f[1], f[2])
		{
		}

		public Point(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		float Index(int idx)
		{
			switch (idx)
			{
				case 0: return X;
				case 1: return Y;
				case 2: return Z;
				default: throw new IndexOutOfRangeException();
			}
		}

		/// <summary>
		/// ベクトルのサイズの二乗
		/// float GetNorm(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルのサイズの二乗</returns>
		public float GetSizeSquared() =>
			X * X + Y * Y + Z * Z;

		/// <summary>
		/// ベクトルのサイズ
		/// float GetSize(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルのサイズ</returns>
		public float GetSize() =>
			(float)Math.Sqrt(GetSizeSquared());

		/// <summary>
		/// ベクトルの正規化
		/// MQPoint Normalize(const MQPoint&amp; p)
		/// </summary>
		/// <returns>ベクトルの正規化</returns>
		public Point Normalize()
		{
			var s = GetSize();

			return s == 0
				? Zero
				: this / s;
		}

		/// <summary>
		/// 3 点からなる面の法線を得る
		/// MQPoint GetNormal(const MQPoint&amp; p0, const MQPoint&amp; p1, const MQPoint&amp; p2)
		/// </summary>
		/// <param name="p0">点 1</param>
		/// <param name="p1">点 2</param>
		/// <param name="p2">点 3</param>
		/// <returns>面の法線</returns>
		public static Point GetNormal(Point p0, Point p1, Point p2)
		{
			var ep = GetCrossProduct(p1 - p2, p0 - p1);

			return ep == Zero
				? Zero
				: ep / ep.GetSize();
		}

		/// <summary>
		/// 4 点からなる面の法線を得る
		/// MQPoint GetQuadNormal(const MQPoint&amp; p0, const MQPoint&amp; p1, const MQPoint&amp; p2, const MQPoint&amp; p3)
		/// </summary>
		/// <param name="p0">点 1</param>
		/// <param name="p1">点 2</param>
		/// <param name="p2">点 3</param>
		/// <param name="p3">点 4</param>
		/// <returns>面の法線</returns>
		public static Point GetNormal(Point p0, Point p1, Point p2, Point p3)
		{
			var n1a = GetNormal(p0, p1, p2);
			var n1b = GetNormal(p0, p2, p3);
			var n2a = GetNormal(p1, p2, p3);
			var n2b = GetNormal(p1, p3, p0);

			return GetInnerProduct(n1a, n1b) > GetInnerProduct(n2a, n2b)
				? (n1a + n1b).Normalize()
				: (n2a + n2b).Normalize();
		}

		/// <summary>
		/// 多角形面の法線を得る
		/// MQPoint GetPolyNormal(const MQPoint *pts, int num)
		/// </summary>
		/// <returns>多角形面の法線</returns>
		public static Point GetNormal(Point[] pts)
		{
			var num = pts.Length;

			if (num < 3)
				return Zero;
			else if (num == 3)
				return GetNormal(pts[0], pts[1], pts[2]);
			else if (num == 4)
				return GetNormal(pts[0], pts[1], pts[2], pts[3]);

			var boxmin = new Point(float.MaxValue);
			var boxmax = new Point(-float.MaxValue);

			foreach (var i in pts)
			{
				if (boxmin.X > i.X) boxmin.X = i.X;
				if (boxmin.Y > i.Y) boxmin.Y = i.Y;
				if (boxmin.Z > i.Z) boxmin.Z = i.Z;
				if (boxmax.X < i.X) boxmax.X = i.X;
				if (boxmax.Y < i.Y) boxmax.Y = i.Y;
				if (boxmax.Z < i.Z) boxmax.Z = i.Z;
			}

			int ax1, ax2;
			var size = boxmax - boxmin;

			if (size.X >= size.Y)
				if (size.X >= size.Z)
				{
					ax1 = 0;
					ax2 = size.Y >= size.Z ? 1 : 2;
				}
				else
				{
					ax1 = 2;
					ax2 = 0;
				}
			else if (size.Y >= size.Z)
			{
				ax1 = 1;
				ax2 = size.X >= size.Z ? 0 : 2;
			}
			else
			{
				ax1 = 2;
				ax2 = 1;
			}

			var minId = -1;

			for (var i = 0; i < num; i++)
			{
				var p1 = pts[i].Index(ax1);

				if (p1 == boxmin.Index(ax1) &&
					(minId == -1 || pts[i].Index(ax2) < pts[minId].Index(ax2)))
					minId = i;
			}

			var g = Zero;

			foreach (var i in pts)
				g += i;

			g /= num;

			var gv = (pts[minId] - g).Normalize();
			var maxIp = GetInnerProduct(pts[minId] - g, gv);

			for (var i = 0; i < num; i++)
				if (i != minId)
				{
					var ip = GetInnerProduct(pts[i] - g, gv);

					if (maxIp < ip)
					{
						minId = i;
						maxIp = ip;
					}
				}

			var baseNormal = GetNormal(pts[(minId - 1 + pts.Length) % num], pts[minId], pts[(minId + 1) % num]);
			var nv = Zero;

			for (var i = 0; i < num; i++)
			{
				var v1 = pts[(minId + i) % num];
				var v2 = pts[(minId + i) % num];
				var p = GetNormal(g, v1, v2);

				if (GetInnerProduct(baseNormal, p) >= 0)
					nv += p * GetTriangleArea(g, v1, v2);
				else
					nv -= p * GetTriangleArea(g, v1, v2);
			}

			return nv.Normalize();
		}

		/// <summary>
		/// 3 点からなる三角形の面積を得る
		/// float GetTriangleArea(const MQPoint&amp; p1, const MQPoint&amp; p2, const MQPoint&amp; p3)
		/// </summary>
		/// <returns>3 点からなる三角形の面積</returns>
		public static float GetTriangleArea(Point p1, Point p2, Point p3)
		{
			var v1 = p2 - p1;
			var v2 = p3 - p1;
			var v3 = GetCrossProduct(v1, v2);

			return 0.5f * v3.GetSize();
		}

		/// <summary>
		/// 2 ベクトルの交差する角度をラジアン単位の 0 から π までの値で得る
		/// float GetCrossingAngle(const MQPoint&amp; v1, const MQPoint&amp; v2)
		/// </summary>
		/// <returns>ベクトルの交差する角度</returns>
		public static float GetCrossingAngle(Point v1, Point v2)
		{
			var d = v1.GetSize() * v2.GetSize();

			if (d == 0)
				return 0;

			var c = GetInnerProduct(v1, v2) / d;

			if (c >= 1)
				return 0;
			else if (c <= -1)
				return (float)Math.PI;

			return (float)Math.Acos(c);
		}

		/// <summary>
		/// 内積の値を得る
		/// float GetInnerProduct(const MQPoint&amp; pa, const MQPoint&amp; pb)
		/// </summary>
		/// <param name="pa">ベクトル 1</param>
		/// <param name="pb">ベクトル 2</param>
		/// <returns>内積の値</returns>
		public static float GetInnerProduct(Point pa, Point pb) =>
			pa.X * pb.X + pa.Y * pb.Y + pa.Z * pb.Z;

		/// <summary>
		/// 外積ベクトルを得る
		/// MQPoint GetCrossProduct(const MQPoint&amp; v1, const MQPoint&amp; v2)
		/// </summary>
		/// <param name="v1">ベクトル 1</param>
		/// <param name="v2">ベクトル 2</param>
		/// <returns>外積ベクトル</returns>
		public static Point GetCrossProduct(Point v1, Point v2) =>
			new Point
			(
				v1.Y * v2.Z - v1.Z * v2.Y,
				v1.Z * v2.X - v1.X * v2.Z,
				v1.X * v2.Y - v1.Y * v2.X
			);

		public static Point operator +(Point self) => self;
		public static Point operator -(Point self) => new Point(-self.X, -self.Y, -self.Z);
		public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		public static Point operator +(Point a, float b) => new Point(a.X + b, a.Y + b, a.Z + b);
		public static Point operator -(Point a, float b) => new Point(a.X - b, a.Y - b, a.Z - b);
		public static Point operator -(float a, Point b) => new Point(a - b.X, a - b.X, a - b.Z);
		public static Point operator *(Point a, float b) => new Point(a.X * b, a.Y * b, a.Z * b);
		public static Point operator /(Point a, float b) => new Point(a.X / b, a.Y / b, a.Z / b);
		public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		public static bool operator !=(Point a, Point b) => !(a == b);

		public override bool Equals(object obj) => obj is Point pt && pt == this;
		public bool Equals(Point other) => other == this;
		public override int GetHashCode() => typeof(Point).GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		public override string ToString() => $"X:{X} Y:{Y} Z:{Z}";
		public float[] ToArray() => new[] { X, Y, Z };
	}
}
