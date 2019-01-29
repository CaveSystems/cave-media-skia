#region CopyRight 2018
/*
    Copyright (c) 2016-2018 Andreas Rohleder (andreas@rohleder.cc)
    All rights reserved
*/
#endregion
#region License LGPL-3
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU Lesser General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion License
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:
 */
#endregion Authors & Contributors

using System.IO;
using System.Text;
using Cave.IO;
using SkiaSharp;

namespace Cave.Media
{
	/// <summary>
	/// Provides platform independent 32 bit argb bitmap functions
	/// </summary>
	/// <seealso cref="System.IDisposable" />
	/// <seealso cref="Cave.Media.IBitmap32" />
	public class SkiaBitmap32Loader : IBitmap32Loader
	{
		/// <summary>Creates a bitmap instance from the specified stream.</summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		public Bitmap32 FromStream(Stream stream)
		{
			return new SkiaBitmap32(SKBitmap.Decode(stream));
		}

		/// <summary>Creates a bitmap instance from the specified file.</summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public Bitmap32 FromFile(string fileName)
		{
			return Create(File.ReadAllBytes(fileName));
		}

		/// <summary>Creates a bitmap instance from the specified data.</summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		public Bitmap32 Create(byte[] data)
		{
			return new SkiaBitmap32(SKBitmap.Decode(data));
		}

		/// <summary>Creates a bitmap instance from the specified data.</summary>
		public Bitmap32 Create(ARGBImageData data)
		{
			return new SkiaBitmap32(data);
		}

		/// <summary>Creates a bitmap instance from the specified data.</summary>
		/// <returns></returns>
		public Bitmap32 Create(int width, int height)
		{
			return new SkiaBitmap32(width, height);
		}

		/// <summary>
		/// Creates a new bitmap instance
		/// </summary>
		/// <param name="fontName">Name of the font</param>
		/// <param name="fontSize">Size in points</param>
		/// <param name="foreColor">ForeColor</param>
		/// <param name="backColor">BackColor</param>
		/// <param name="text">text to draw</param>
		public Bitmap32 Create(string fontName, float fontSize, ARGB foreColor, ARGB backColor, string text)
		{
			var paint = new SKPaint();
			float emSize = fontSize / 4f * 3f;
			paint.TextSize = emSize;
			paint.TextEncoding = SKTextEncoding.Utf8;
			paint.Color = foreColor.AsUInt32;
			if (fontName != null) paint.Typeface = SKTypeface.FromFamilyName(fontName);
			paint.IsAntialias = true;
			float height = paint.GetFontMetrics(out SKFontMetrics metrics);
			float width = paint.MeasureText(text);
			var bitmap = new SKBitmap(1 + (int)width, 1 + (int)height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul);
			using (var canvas = new SKCanvas(bitmap))
			{
				canvas.Clear(new SKColor(backColor.AsUInt32));
				canvas.DrawText(Encoding.UTF8.GetBytes(text), 0, -metrics.Ascent, paint);
			}
			return new SkiaBitmap32(bitmap);
		}
	}
}
