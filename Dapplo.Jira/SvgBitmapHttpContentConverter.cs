﻿#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#if NET45 || NET46
using Dapplo.HttpExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions.ContentConverter;
using Dapplo.HttpExtensions.Support;
using Dapplo.Log;
using Svg;
using Dapplo.HttpExtensions.Extensions;
using System.Drawing.Imaging;

namespace Dapplo.Jira
{
	/// <summary>
	/// This adds SVG image support as jira uses this.
	/// </summary>
	public class SvgBitmapHttpContentConverter : IHttpContentConverter
	{
		private static readonly LogSource Log = new LogSource();
		private static readonly IList<string> SupportedContentTypes = new List<string>();

		static SvgBitmapHttpContentConverter()
		{
			SupportedContentTypes.Add(MediaTypes.Svg.EnumValueOf());
		}

		/// <inheritdoc />
		public int Order => 0;

		public int Width { get; set; }

		public int Height { get; set; }

		/// <summary>
		///     This checks if the HttpContent can be converted to a Bitmap and is assignable to the specified Type
		/// </summary>
		/// <param name="typeToConvertTo">This should be something we can assign Bitmap to</param>
		/// <param name="httpContent">HttpContent to process</param>
		/// <returns>true if it can convert</returns>
		public bool CanConvertFromHttpContent(Type typeToConvertTo, HttpContent httpContent)
		{
			if (typeToConvertTo == typeof(object) || !typeToConvertTo.IsAssignableFrom(typeof(Bitmap)))
			{
				return false;
			}
			var httpBehaviour = HttpBehaviour.Current;
			return !httpBehaviour.ValidateResponseContentType || SupportedContentTypes.Contains(httpContent.GetContentType());
		}

		/// <inheritdoc />
		public async Task<object> ConvertFromHttpContentAsync(Type resultType, HttpContent httpContent, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!CanConvertFromHttpContent(resultType, httpContent))
			{
				var exMessage = "CanConvertFromHttpContent resulted in false, ConvertFromHttpContentAsync is not supposed to be called.";
				Log.Error().WriteLine(exMessage);
				throw new NotSupportedException(exMessage);
			}
			using (var memoryStream = (MemoryStream)await StreamHttpContentConverter.Instance.ConvertFromHttpContentAsync(typeof(MemoryStream), httpContent, cancellationToken).ConfigureAwait(false))
			{
				Log.Debug().WriteLine("Creating a Bitmap from the SVG.");

				var bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);// ImageHelper.CreateEmpty(Width, Height, PixelFormat.Format32bppArgb, Color.Transparent, 96, 96);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.Clear(Color.Transparent);
				}

				var svgDoc = SvgDocument.Open<SvgDocument>(memoryStream);
				svgDoc.Width = Width;
				svgDoc.Height = Height;
				svgDoc.Draw(bitmap);
				return bitmap;
			}
		}

		/// <inheritdoc />
		public bool CanConvertToHttpContent(Type typeToConvert, object content)
		{
			return false;
		}

		/// <inheritdoc />
		public HttpContent ConvertToHttpContent(Type typeToConvert, object content)
		{
			return null;
		}

		/// <inheritdoc />
		public void AddAcceptHeadersForType(Type resultType, HttpRequestMessage httpRequestMessage)
		{
			if (resultType == null)
			{
				throw new ArgumentNullException(nameof(resultType));
			}
			if (httpRequestMessage == null)
			{
				throw new ArgumentNullException(nameof(httpRequestMessage));
			}
			if (resultType == typeof(object) || !resultType.IsAssignableFrom(typeof(Bitmap)))
			{
				return;
			}
			httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypes.Svg.EnumValueOf()));
			Log.Debug().WriteLine("Modified the header(s) of the HttpRequestMessage: Accept: {0}", httpRequestMessage.Headers.Accept);
		}
	}

}

#endif