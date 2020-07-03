// -------------------------------------------------------------------------------------------------
// WebImageExtractor - © Copyright 2020 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

namespace WebImageExtractor.Blazor.Models
{
    public class DisplayImage
    {
        public string Link { get; set; }

        public bool IsSvg { get; set; }

        public string EncodedPixelData { get; set; }

        public string SvgContent { get; set; }
    }
}
