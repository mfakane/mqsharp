using System;

namespace Metasequoia
{
	/// <summary>
	/// MQMatrix
	/// </summary>
	public struct Matrix
	{
		public static readonly Matrix Identity = new Matrix
		(
			1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1
		);

		public float M11 { get; set; }
		public float M12 { get; set; }
		public float M13 { get; set; }
		public float M14 { get; set; }
		public float M21 { get; set; }
		public float M22 { get; set; }
		public float M23 { get; set; }
		public float M24 { get; set; }
		public float M31 { get; set; }
		public float M32 { get; set; }
		public float M33 { get; set; }
		public float M34 { get; set; }
		public float M41 { get; set; }
		public float M42 { get; set; }
		public float M43 { get; set; }
		public float M44 { get; set; }

		public Matrix(float[] t)
		{
			M11 = t[00]; M12 = t[01]; M13 = t[02]; M14 = t[03];
			M21 = t[04]; M22 = t[05]; M23 = t[06]; M24 = t[07];
			M31 = t[08]; M32 = t[09]; M33 = t[10]; M34 = t[11];
			M41 = t[12]; M42 = t[13]; M43 = t[14]; M44 = t[15];
		}

		public Matrix(float[,] d)
		{
			M11 = d[0, 0]; M12 = d[0, 1]; M13 = d[0, 2]; M14 = d[0, 3];
			M21 = d[1, 0]; M22 = d[1, 1]; M23 = d[1, 2]; M24 = d[1, 3];
			M31 = d[2, 0]; M32 = d[2, 1]; M33 = d[2, 2]; M34 = d[2, 3];
			M41 = d[3, 0]; M42 = d[3, 1]; M43 = d[3, 2]; M44 = d[3, 3];
		}

		public Matrix
		(
			float m11, float m12, float m13, float m14,
			float m21, float m22, float m23, float m24,
			float m31, float m32, float m33, float m34,
			float m41, float m42, float m43, float m44
		)
		{
			M11 = m11; M12 = m12; M13 = m13; M14 = m14;
			M21 = m21; M22 = m22; M23 = m23; M24 = m24;
			M31 = m31; M32 = m32; M33 = m33; M34 = m34;
			M41 = m41; M42 = m42; M43 = m43; M44 = m44;
		}

		public float[] ToArray() =>
			new[]
			{
				M11, M12, M13, M14,
				M21, M22, M23, M24,
				M31, M32, M33, M34,
				M41, M42, M43, M44,
			};

		public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll) =>
			CreateFromYawPitchRoll(new Angle(yaw, pitch, roll));

		public static Matrix CreateFromYawPitchRoll(Angle angle) =>
			Identity.SetTransform(Point.One, angle, Point.Zero);

		public static Matrix CreateFromAxisAngle(Point axis, float radians)
		{
			var x = axis.X;
			var y = axis.Y;
			var z = axis.Z;
			var num = (float)Math.Sin(radians);
			var num2 = (float)Math.Cos(radians);
			var num3 = x * x;
			var num4 = y * y;
			var num5 = z * z;
			var num6 = x * y;
			var num7 = x * z;
			var num8 = y * z;

			return new Matrix
			(
				num3 + num2 * (1f - num3),
				num6 - num2 * num6 + num * z,
				num7 - num2 * num7 - num * y,
				0,
				num6 - num2 * num6 - num * z,
				num4 + num2 * (1f - num4),
				num8 - num2 * num8 + num * x,
				0,
				num7 - num2 * num7 + num * y,
				num8 - num2 * num8 - num * x,
				num5 + num2 * (1f - num5),
				0,
				0, 0, 0, 1
			);
		}

		public static Matrix CreateTranslation(Point p) =>
			Identity.SetTransform(Point.One, Angle.Zero, p);

		public static Matrix CreateScale(Point s) =>
			Identity.SetTransform(s, Angle.Zero, Point.Zero);

		public static Matrix Invert(Matrix matrix)
		{
			var result = new Matrix();
			var m = matrix.M11;
			var m2 = matrix.M12;
			var m3 = matrix.M13;
			var m4 = matrix.M14;
			var m5 = matrix.M21;
			var m6 = matrix.M22;
			var m7 = matrix.M23;
			var m8 = matrix.M24;
			var m9 = matrix.M31;
			var m10 = matrix.M32;
			var m11 = matrix.M33;
			var m12 = matrix.M34;
			var m13 = matrix.M41;
			var m14 = matrix.M42;
			var m15 = matrix.M43;
			var m16 = matrix.M44;
			var num = m11 * m16 - m12 * m15;
			var num2 = m10 * m16 - m12 * m14;
			var num3 = m10 * m15 - m11 * m14;
			var num4 = m9 * m16 - m12 * m13;
			var num5 = m9 * m15 - m11 * m13;
			var num6 = m9 * m14 - m10 * m13;
			var num7 = m6 * num - m7 * num2 + m8 * num3;
			var num8 = -(m5 * num - m7 * num4 + m8 * num5);
			var num9 = m5 * num2 - m6 * num4 + m8 * num6;
			var num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
			var num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
			var num12 = m7 * m16 - m8 * m15;
			var num13 = m6 * m16 - m8 * m14;
			var num14 = m6 * m15 - m7 * m14;
			var num15 = m5 * m16 - m8 * m13;
			var num16 = m5 * m15 - m7 * m13;
			var num17 = m5 * m14 - m6 * m13;
			var num18 = m7 * m12 - m8 * m11;
			var num19 = m6 * m12 - m8 * m10;
			var num20 = m6 * m11 - m7 * m10;
			var num21 = m5 * m12 - m8 * m9;
			var num22 = m5 * m11 - m7 * m9;
			var num23 = m5 * m10 - m6 * m9;

			result.M11 = num7 * num11;
			result.M21 = num8 * num11;
			result.M31 = num9 * num11;
			result.M41 = num10 * num11;
			result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
			result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
			result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
			result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
			result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
			result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
			result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
			result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
			result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
			result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;

			return result;
		}

		/// <summary>
		/// 行列のうち左上 3x3 成分のみでベクトルと行列の積を計算します。
		/// const MQPoint MQMatrix::Mult3(const MQPoint&amp; p);
		/// </summary>
		/// <param name="p">ベクトル</param>
		/// <returns>ベクトルと行列の積</returns>
		public Point Mult3(Point p) =>
			new Point
			(
				p.X * M11 + p.Y * M21 + p.Z * M31,
				p.X * M12 + p.Y * M22 + p.Z * M32,
				p.X * M13 + p.Y * M23 + p.Z * M33
			);

		/// <summary>
		/// 行列のうち左上 3x3 成分のみを転置します。
		/// void MQMatrix::Transpose3(void);
		/// </summary>
		/// <returns>転置行列</returns>
		public Matrix Transpose3() =>
			new Matrix
			(
				M11, M21, M31, M14,
				M12, M22, M32, M24,
				M13, M23, M33, M34,
				M41, M42, M43, M44
			);

		/// <summary>
		/// SRT 変換行列から拡大成分を抽出して、その XYZ ごとの要素を MQPoint 型として取得します。
		/// MQPoint MQMatrix::GetScaling(void) const;
		/// </summary>
		/// <returns>拡大成分</returns>
		public Point GetScaling()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(ToArray(), (int)MQMatrix.GetScaling, val);

			return new Point(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列から回転成分を抽出して、その角度（オイラー角）を MQAngle 型として取得します。
		/// MQAngle MQMatrix::GetRotation(void) const;
		/// </summary>
		/// <returns>回転成分</returns>
		public Angle GetRotation()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(ToArray(), (int)MQMatrix.GetRotation, val);

			return new Angle(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列から平行移動成分を抽出して、その移動量を MQPoint 型として取得します。
		/// MQPoint MQMatrix::GetTranslation(void) const;
		/// </summary>
		/// <returns>平行移動成分</returns>
		public Point GetTranslation()
		{
			var val = new float[3];

			NativeMethods.MQMatrix_FloatValue(ToArray(), (int)MQMatrix.GetTranslation, val);

			return new Point(val[0], val[1], val[2]);
		}

		/// <summary>
		/// SRT 変換行列を設定します。
		/// void MQMatrix::SetTransform(const MQPoint *scaling, const MQAngle *rotation, const MQPoint *trans);
		/// </summary>
		/// <param name="scaling">拡大成分</param>
		/// <param name="rotation">回転成分</param>
		/// <param name="trans">平行移動成分</param>
		/// <returns>設定された行列</returns>
		public Matrix SetTransform(Point scaling, Angle rotation, Point trans)
		{
			var val = new float[]
			{
				scaling.X, scaling.Y, scaling.Z,
				rotation.Head, rotation.Pitch, rotation.Bank,
				trans.X, trans.Y, trans.Z
			};
			var rt = ToArray();

			NativeMethods.MQMatrix_FloatValue(rt, (int)MQMatrix.SetTransform, val);

			return new Matrix(rt);
		}

		/// <summary>
		/// SRT 変換逆行列を設定します。
		/// void MQMatrix::SetInverseTransform(const MQPoint *scaling, const MQAngle *rotation, const MQPoint *trans);
		/// </summary>
		/// <param name="scaling">拡大成分</param>
		/// <param name="rotation">回転成分</param>
		/// <param name="trans">平行移動成分</param>
		/// <returns>設定された逆行列</returns>
		public Matrix SetInverseTransform(Point scaling, Angle rotation, Point trans)
		{
			var val = new float[]
			{
				scaling.X, scaling.Y, scaling.Z,
				rotation.Head, rotation.Pitch, rotation.Bank,
				trans.X, trans.Y, trans.Z
			};
			var rt = ToArray();

			NativeMethods.MQMatrix_FloatValue(rt, (int)MQMatrix.SetInverseTransform, val);

			return new Matrix(rt);
		}

		public static Matrix operator +(Matrix a, Matrix b) =>
			new Matrix
			(
				a.M11 + b.M11, a.M12 + b.M12, a.M13 + b.M13, a.M14 + b.M14,
				a.M21 + b.M21, a.M22 + b.M22, a.M23 + b.M23, a.M24 + b.M24,
				a.M31 + b.M31, a.M32 + b.M32, a.M33 + b.M33, a.M34 + b.M34,
				a.M41 + b.M41, a.M42 + b.M42, a.M43 + b.M43, a.M44 + b.M44
			);

		public static Matrix operator -(Matrix a, Matrix b) =>
			new Matrix
			(
				a.M11 - b.M11, a.M12 - b.M12, a.M13 - b.M13, a.M14 - b.M14,
				a.M21 - b.M21, a.M22 - b.M22, a.M23 - b.M23, a.M24 - b.M24,
				a.M31 - b.M31, a.M32 - b.M32, a.M33 - b.M33, a.M34 - b.M34,
				a.M41 - b.M41, a.M42 - b.M42, a.M43 - b.M43, a.M44 - b.M44
			);

		public static Matrix operator *(Matrix a, Matrix b) =>
			new Matrix
			(
				a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
				a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
				a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
				a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,
				a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
				a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
				a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
				a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,
				a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
				a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
				a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
				a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,
				a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
				a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
				a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
				a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
			);
	}
}
