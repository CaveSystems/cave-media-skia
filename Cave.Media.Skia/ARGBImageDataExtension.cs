﻿#region CopyRight 2018
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

using System;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace Cave.Media
{
	/// <summary>
	/// Provides extensions for <see cref="ARGBImageData"/>
	/// </summary>
	public static class ARGBImageDataExtension
    {
		/// <summary>
		/// Loads the specified bitmap.
		/// </summary>
		/// <param name="bitmap">The bitmap.</param>
		/// <returns></returns>
		public static ARGBImageData ToARGBImageData(this SKBitmap bitmap)
		{
			var pix = bitmap.Bytes;
			int[] data = new int[bitmap.Width * bitmap.Height];
			Buffer.BlockCopy(pix, 0, data, 0, 4 * data.Length);
			return new ARGBImageData(data, bitmap.Width, bitmap.Height, pix.Length / bitmap.Height);
		}

		/// <summary>
		/// Writes all data to a new <see cref="SKBitmap"/> instance
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception">Invalid length!</exception>
		public static SKBitmap ToSKBitmap(this ARGBImageData imageData)
		{
			SKImageInfo imgInfo = new SKImageInfo(imageData.Width, imageData.Height, SKColorType.Rgba8888, SKAlphaType.Unpremul);
			var bitmap = SKBitmap.FromImage(SkiaSharp.SKImage.Create(imgInfo));
			IntPtr len;
			IntPtr ptr = bitmap.GetPixels(out len);
			int byteLen = imageData.Data.Length * 4;
			if (len.ToInt32() != byteLen) throw new Exception("Invalid length!");
			Marshal.Copy(imageData.Data, 0, ptr, imageData.Data.Length);
			return bitmap;
		}
	}
}
