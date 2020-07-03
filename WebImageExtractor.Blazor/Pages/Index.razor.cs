// -------------------------------------------------------------------------------------------------
// WebImageExtractor - © Copyright 2020 - Jam-Es.com
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using WebImageExtractor.Blazor.Models;

namespace WebImageExtractor.Blazor.Pages
{
    // SVGs not working as they can't be used with binary data

    /// <summary>
    /// Code-behind for Index Page.
    /// </summary>
    public partial class Index
    {
        private readonly string corsAnywhereUri = "https://cors-anywhere.herokuapp.com/";
        private string spinnerDisplay = "none";
        private bool showImages = false;
        private string selectedExtractMode = "Extract All Images";
        private string modeDropdownClass = string.Empty;
        private List<DisplayImage> displayImages;

        private string EnteredUri { get; set; }

        private bool RecurseHyperlinks { get; set; } = false;

        private bool RecurseUri { get; set; } = false;

        private bool SvgOnly { get; set; }

        public async void SearchPressed()
        {
            displayImages = new List<DisplayImage>();
            spinnerDisplay = "block";

            ExtractionSettings settings = new ExtractionSettings(true, SvgOnly, RecurseUri, RecurseHyperlinks, 1)
            {
                HttpClient = Http,
                UseCorsAnywhere = true,
                DisableValidityCheck = true,
            };
            settings.OnFoundImage += async (WebImage image) =>
            {
                // Note: Magick.NET isn't full compatible with Blazor so instead we can download the image directly

                string uri = GetCorsAnywhereUri(image);
                if (IsSvg(uri))
                {
                    string svgContent = await Http.GetStringAsync(uri);
                    if (!string.IsNullOrEmpty(svgContent))
                    {
                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(svgContent);
                        if (doc != null)
                        {
                            HtmlNode node = doc.DocumentNode.SelectSingleNode("//svg");
                            if (node != null)
                            {
                                node.Attributes.Add("width", "300");
                                node.Attributes.Add("height", "300");
                                displayImages.Add(new DisplayImage()
                                {
                                    Link = image.Uri,
                                    IsSvg = true,
                                    SvgContent = node.OuterHtml,
                                });
                            }
                        }
                    }
                }
                else
                {
                    // Get Pixel Data
                    byte[] imageBytes = await Http.GetByteArrayAsync(uri);
                    displayImages.Add(new DisplayImage()
                    {
                        Link = image.Uri,
                        IsSvg = false,
                        EncodedPixelData = Convert.ToBase64String(imageBytes),
                    });
                }

                showImages = true;
                StateHasChanged();
            };

            switch (selectedExtractMode)
            {
                case "Extract All Images":
                    await Extractor.GetAllImages($"https://{EnteredUri}", settings);
                    break;
                case "Favicons Only":
                    await Extractor.GetFavicons($"https://{EnteredUri}", settings);
                    break;
                case "Apple Touch Icons Only":
                    await Extractor.GetAppleTouchIcons($"https://{EnteredUri}", settings);
                    break;
            }

            spinnerDisplay = "none";
            showImages = true;
            StateHasChanged();
        }

        public bool IsSvg(string uri)
        {
            return uri.ToLower().EndsWith(".svg");
        }

        public string GetCorsAnywhereUri(WebImage image)
        {
            string uriString = image.Uri;
            int i = uriString.IndexOf(':');
            if (i > 0)
            {
                uriString = uriString.Substring(i + 1);
            }

            while (uriString[0] == '/')
            {
                uriString = uriString.Substring(1);
            }

            return corsAnywhereUri + uriString;
        }

        private void ModeItemClick(int index)
        {
            switch (index)
            {
                case 0:
                    selectedExtractMode = "Extract All Images";
                    break;
                case 1:
                    selectedExtractMode = "Favicons Only";
                    break;
                case 2:
                    selectedExtractMode = "Apple Touch Icons Only";
                    break;
            }
        }

        private void DropDownToggle()
        {
            if (modeDropdownClass == "open")
            {
                modeDropdownClass = string.Empty;
            }
            else
            {
                modeDropdownClass = "open";
            }
        }
    }
}