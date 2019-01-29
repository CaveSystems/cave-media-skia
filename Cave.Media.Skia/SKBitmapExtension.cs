#region CopyRight 2018
/*
    Copyright (c) 2003-2018 Andreas Rohleder (andreas@rohleder.cc)
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

using Cave.IO;
using SkiaSharp;
using System;
using System.IO;

namespace Cave.Media
{
    /// <summary>
    /// Provides extensions for <see cref="SKBitmap"/> and <see cref="SKImage"/> instances.
    /// </summary>
    public static class SKBitmapExtension
    {
        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        public static void Save(this SKBitmap bitmap, string fileName, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
        {
            using (var img = SKImage.FromBitmap(bitmap)) Save(img, fileName, format, quality);
        }

        /// <summary>
        /// Saves the specified stream.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        public static void Save(this SKBitmap bitmap, Stream stream, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
        {
            using (var img = SKImage.FromBitmap(bitmap)) Save(img, stream, format, quality);
        }

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        public static void Save(this SKImage image, string fileName, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
        {
            using (var file = File.Create(fileName))
            {
                Save(image, file, format, quality);
            }
        }

        /// <summary>
        /// Saves the specified stream.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        /// <param name="quality">The quality.</param>
        /// <exception cref="ArgumentOutOfRangeException">quality</exception>
        public static void Save(this SKImage image, Stream stream, SKEncodedImageFormat format = SKEncodedImageFormat.Png, int quality = 100)
        {
            if (quality < 1 || quality > 100) throw new ArgumentOutOfRangeException(nameof(quality));
            using (var data = image.Encode(format, quality))
            {
                data.SaveTo(stream);
            }
        }
    }
}
