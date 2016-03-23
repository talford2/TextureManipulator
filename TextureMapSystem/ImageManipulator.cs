using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace TextureMapSystem
{
	public static class ImageManipulator
	{
		#region Private Members

		private static Dictionary<byte, byte> _gammaValueDictionary;

		private static Dictionary<byte, byte> GammaValueDictionary
		{
			get
			{
				if (_gammaValueDictionary == null)
				{
					_gammaValueDictionary = new Dictionary<byte, byte>();
					for (byte i = 0; i < byte.MaxValue; i++)
					{
						_gammaValueDictionary.Add(i, (byte)(Math.Pow(((double)i) / ((double)byte.MaxValue), 2.2) * (double)byte.MaxValue));
					}
					_gammaValueDictionary.Add(byte.MaxValue, byte.MaxValue);
				}
				return _gammaValueDictionary;
			}
		}

		#endregion

		#region Public Static Methods

		public static void FixAlpha(Bitmap bmp)
		{
			AlterBitmap(bmp, c =>
			{
				c.A = GammaValueDictionary[c.A];
				return c;
			});
		}

		public static Bitmap Add(Bitmap bmp1, Bitmap bmp2)
		{
			return BlendBitmaps(bmp1, bmp2, (c1, c2) => c1 + c2);
		}

		public static Bitmap Subtract(Bitmap bmp1, Bitmap bmp2)
		{
			return BlendBitmaps(bmp1, bmp2, (c1, c2) => c1 - c2);
		}

		public static Bitmap Multiply(Bitmap bmp1, Bitmap bmp2)
		{
			return BlendBitmaps(bmp1, bmp2, (c1, c2) => c1 * c2);
		}

		public static Bitmap Overlay(Bitmap bmp1, Bitmap bmp2)
		{
			return BlendBitmaps(bmp1, bmp2, (c1, c2) =>
			{
				var b1 = c1.GetNormalised();
				var b2 = c2.GetNormalised();

				var sss = new RGBAColorNormalised
				{
					R = OverlayCombine(b1.R, b2.R),
					G = OverlayCombine(b1.G, b2.G),
					B = OverlayCombine(b1.B, b2.B),
					A = OverlayCombine(b1.A, b2.A)
				};

				return sss.GetUnormalised();
			});
		}

		public static Bitmap ColorDodge(Bitmap bmp1, Bitmap bmp2)
		{
			return BlendBitmaps(bmp1, bmp2, (target, blend) =>
			{
				var n1 = target.GetNormalised();
				var n2 = blend.GetNormalised();
				return RGBAColorNormalised.ProcessAllChannels(n1, n2, (t, b) => t / (1 - b), true).GetUnormalised();
			});
		}

		private static float OverlayCombine(float a, float b)
		{
			var res = 0f;
			if (a > 0.5)
			{
				res = a * b * 2;
			}
			else
			{
				res = 1 - 2 * (1 - a) * (1 - b);
			}
			return Clamp(res, 0, 1);
		}

		private static float Clamp(float value, float min, float max)
		{
			var res = value;
			if (value < min)
			{
				value = min;
			}
			if (value > max)
			{
				value = max;
			}
			return value;
		}

		public static void AlterBitmap(Bitmap bmp, Func<RGBAColor, RGBAColor> process)
		{
			var imgData = bmp.LockBits(GetImageRect(bmp), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
			var dataLength = bmp.Width * imgData.Height * 4;
			var byteData = new byte[dataLength];

			Marshal.Copy(imgData.Scan0, byteData, 0, dataLength);

			var index = 0;
			var clr = new RGBAColor();

			for (var y = 0; y < bmp.Height; y++)
			{
				for (var x = 0; x < bmp.Width; x++)
				{
					index = y * imgData.Stride + x * 4;

					clr.R = byteData[index + 2];
					clr.G = byteData[index + 1];
					clr.B = byteData[index + 0];
					clr.A = byteData[index + 3];

					var res = process(clr);

					byteData[index + 2] = res.R;
					byteData[index + 1] = res.G;
					byteData[index + 0] = res.B;
					byteData[index + 3] = res.A;
				}
			}

			Marshal.Copy(byteData, 0, imgData.Scan0, byteData.Length);
			bmp.UnlockBits(imgData);
		}

		public static void AlterBitmap(Bitmap bmp, Func<RGBAColorNormalised, RGBAColorNormalised> process)
		{
			AlterBitmap(bmp, c => { return process(c.GetNormalised()).GetUnormalised(); });
		}

		public static Bitmap BlendBitmaps(Bitmap bmp1, Bitmap bmp2, Func<RGBAColor, RGBAColor, RGBAColor> process)
		{
			return BlendBitmaps(new List<Bitmap> { bmp1, bmp2 }, (a) => process(a[0], a[1]));
		}

		public static Bitmap BlendBitmaps(Bitmap bmp1, Bitmap bmp2, Bitmap bmp3, Func<RGBAColor, RGBAColor, RGBAColor, RGBAColor> process)
		{
			return BlendBitmaps(new List<Bitmap> { bmp1, bmp2, bmp3 }, (a) => process(a[0], a[1], a[2]));
		}

		public static Bitmap BlendBitmaps(Bitmap bmp1, Bitmap bmp2, Bitmap bmp3, Bitmap bmp4, Func<RGBAColor, RGBAColor, RGBAColor, RGBAColor, RGBAColor> process)
		{
			return BlendBitmaps(new List<Bitmap> { bmp1, bmp2, bmp3, bmp4 }, (a) => process(a[0], a[1], a[2], a[3]));
		}

		public static Bitmap BlendBitmaps(List<Bitmap> bmps, Func<List<RGBAColor>, RGBAColor> process)
		{
			var size = new Size(bmps.Min(b => b.Size.Width), bmps.Min(b => b.Size.Height));
			var rect = new Rectangle(0, 0, size.Width, size.Height);

			var finalBmp = new Bitmap(size.Width, size.Height);

			var bitmapData = new List<BitmapData>();
			foreach (var bmp in bmps)
			{
				bitmapData.Add(bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb));
			}
			var final = finalBmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			var dataLength = finalBmp.Width * finalBmp.Height * 4;

			var bytesList = new List<byte[]>();

			for (var i = 0; i < bmps.Count; i++)
			{
				bytesList.Add(new byte[dataLength]);
				Marshal.Copy(bitmapData[i].Scan0, bytesList[i], 0, dataLength);
			}
			bytesList.Add(new byte[dataLength]);

			Marshal.Copy(final.Scan0, bytesList.Last(), 0, dataLength);

			var clrList = new List<RGBAColor>();
			for (var i = 0; i < bmps.Count; i++)
			{
				clrList.Add(new RGBAColor());
			}
			var index = 0;
			for (var y = 0; y < finalBmp.Height; y++)
			{
				for (var x = 0; x < finalBmp.Width; x++)
				{
					index = y * final.Stride + x * 4;

					for (var i = 0; i < clrList.Count; i++)
					{
						clrList[i].R = bytesList[i][index + 2];
						clrList[i].G = bytesList[i][index + 1];
						clrList[i].B = bytesList[i][index + 0];
						clrList[i].A = bytesList[i][index + 3];
					}

					var res = process(clrList);

					bytesList.Last()[index + 2] = res.R;
					bytesList.Last()[index + 1] = res.G;
					bytesList.Last()[index + 0] = res.B;
					bytesList.Last()[index + 3] = res.A;
				}
			}

			for (var i = 0; i < bmps.Count; i++)
			{
				Marshal.Copy(bytesList[i], 0, bitmapData[i].Scan0, bytesList[i].Length);
				bmps[i].UnlockBits(bitmapData[i]);
			}
			Marshal.Copy(bytesList.Last(), 0, final.Scan0, bytesList.Last().Length);
			finalBmp.UnlockBits(final);

			return finalBmp;
		}

		#endregion

		#region Private Static Methods

		private static Rectangle GetImageRect(Bitmap img)
		{
			return new Rectangle(0, 0, img.Width, img.Height);
		}

		#endregion

	}

	public class RGBAColor
	{
		public byte R { get; set; }

		public byte G { get; set; }

		public byte B { get; set; }

		public byte A { get; set; }

		public RGBAColor() { }

		public RGBAColor(byte r, byte g, byte b, byte a = 1)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public RGBAColorNormalised GetNormalised()
		{
			return new RGBAColorNormalised(ByteToFloat(R), ByteToFloat(G), ByteToFloat(B), ByteToFloat(A));
		}

		private float ByteToFloat(float value)
		{
			return (float)((float)value / (float)byte.MaxValue);
		}

		public static RGBAColor operator +(RGBAColor c1, RGBAColor c2)
		{
			return new RGBAColor
			{
				R = AddBytes(c1.R, c2.R),
				G = AddBytes(c1.G, c2.G),
				B = AddBytes(c1.B, c2.B),
				A = AddBytes(c1.A, c2.A)
			};
		}

		public static RGBAColor operator -(RGBAColor c1, RGBAColor c2)
		{
			return new RGBAColor
			{
				R = SubtractBytes(c1.R, c2.R),
				G = SubtractBytes(c1.G, c2.G),
				B = SubtractBytes(c1.B, c2.B),
				A = SubtractBytes(c1.A, c2.A)
			};
		}

		public static RGBAColor operator *(RGBAColor c1, RGBAColor c2)
		{
			var n1 = c1.GetNormalised();
			var n2 = c2.GetNormalised();
			return new RGBAColorNormalised
			{
				R = n1.R * n2.R,
				G = n1.G * n2.G,
				B = n1.B * n2.B,
				A = n1.A * n2.A
			}.GetUnormalised();
		}

		private static byte AddBytes(byte b1, byte b2)
		{
			return (byte)Math.Min(b1 + b2, (int)byte.MaxValue);
		}

		private static byte SubtractBytes(byte b1, byte b2)
		{
			return (byte)Math.Max(b1 - b2, 0);
		}
	}

	public class RGBAColorNormalised
	{
		public float R { get; set; }

		public float G { get; set; }

		public float B { get; set; }

		public float A { get; set; }

		public RGBAColorNormalised() { }

		public RGBAColorNormalised(float r, float g, float b, float a = 1f)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public RGBAColor GetUnormalised(bool clamp = true)
		{
			var r = this;
			if (clamp)
			{
				r = Clamp(this);
			}
			return new RGBAColor(FloatToByte(r.R), FloatToByte(r.G), FloatToByte(r.B), FloatToByte(r.A));
		}

		public static RGBAColorNormalised Clamp(RGBAColorNormalised clr)
		{
			return new RGBAColorNormalised
			{
				R = MathUtility.Clamp(clr.R, 0, 1),
				G = MathUtility.Clamp(clr.G, 0, 1),
				B = MathUtility.Clamp(clr.B, 0, 1),
				A = MathUtility.Clamp(clr.A, 0, 1)
			};
		}

		public static RGBAColorNormalised ProcessAllChannels(RGBAColorNormalised v1, RGBAColorNormalised v2, Func<float, float, float> process, bool excludeAlpha = false)
		{
			return new RGBAColorNormalised
			{
				R = process(v1.R, v2.R),
				G = process(v1.G, v2.G),
				B = process(v1.B, v2.B),
				A = excludeAlpha ? v1.A : process(v1.A, v2.A)
			};
		}

		private byte FloatToByte(float value)
		{
			return (byte)(value * (float)byte.MaxValue);
		}
	}

	public class MathUtility
	{

		public static float Clamp(float v, float min, float max)
		{
			if (v < min)
			{
				return min;
			}
			if (v > max)
			{
				return max;
			}
			return v;
		}
	}
}
