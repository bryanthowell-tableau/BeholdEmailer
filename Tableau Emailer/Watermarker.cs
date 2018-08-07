using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;

namespace Behold_Emailer
{
    // Watermarker class creates the actual Watermark objects based on the settings files
    // It also contains the definitions of the Watermarking class, which is what handles the PDF watermarking itself
    internal class Watermarker
    {
        public double PageHeight;
        public double PageWidth;
        public Dictionary<string, Watermark> PageLocations;

        public Watermarker()
        {
            this.PageLocations = new Dictionary<string, Watermark>
            {
                ["top_left"] = null,
                ["top_right"] = null,
                ["top_center"] = null,
                ["bottom_left"] = null,
                ["bottom_center"] = null,
                ["bottom_right"] = null
            };
        }

        // Reads the configuration settings and creates the appropriate Watermark objects
        public bool SetPageLocationWatermarkFromConfig(string pageLocation, SerializableStringDictionary configuration)
        {
            if (configuration != null)
            {
                if (configuration["watermark_type"] == "text")
                {
                    PageLocations[pageLocation] = new TextWatermark(configuration["text"])
                    {
                        FontName = configuration["font_name"],
                        FontSize = Int32.Parse(configuration["font_size"]),
                        FontStyle = configuration["font_style"],
                        PageLocation = pageLocation
                    };
                    if (configuration["add_timestamp"] == "Yes")
                    {
                        PageLocations[pageLocation].AddTimestampFlag = true;
                    }
                }
                else if (configuration["watermark_type"] == "image")
                {
                    PageLocations[pageLocation] = new ImageWatermark(configuration["image_location"])
                    {
                        PageLocation = pageLocation
                    };
                }
                else if (configuration["watermark_type"] == "page_number")
                {
                    bool show_total = false;
                    if (configuration["show_total"] == "Yes")
                    {
                        show_total = true;
                    }
                    PageLocations[pageLocation] = new PageNumberer(configuration["text"], show_total)
                    {
                        FontName = configuration["font_name"],
                        FontSize = Int32.Parse(configuration["font_size"]),
                        FontStyle = configuration["font_style"],
                        PageLocation = pageLocation
                    };
                }
            }
            return true;
        }

        // Opens up an existing PDF file and watermarks each page. Then closes the modified file.
        public void AddWatermarkToPdf(string inputPdfFilename)
        {
            // Don't bother doing anything if no watermark is set
            bool DoWatermarkingFlag = false;
            foreach (string page_location in this.PageLocations.Keys)
            {
                if (PageLocations[page_location] != null)
                {
                    DoWatermarkingFlag = true;
                }
            }
            if (DoWatermarkingFlag == false)
            {
                return;
            }

            PdfDocument document = PdfReader.Open(inputPdfFilename);

            int pageCount = document.Pages.Count;

            /* if (this.first_page_only == true)
             {
                 page_count = 1;
             }*/
            for (int i = 0; i < pageCount; i++)
            {
                PdfPage page = document.Pages[i];
                XGraphics gfx = XGraphics.FromPdfPage(page);
                this.PageHeight = page.Height.Point;
                this.PageWidth = page.Width.Point;

                foreach (string pageLocation in PageLocations.Keys)
                {
                    if (PageLocations[pageLocation] != null)
                    {
                        PageLocations[pageLocation].SetPageHeightAndWidth(this.PageHeight, this.PageWidth);
                        PageLocations[pageLocation].WriteWatermark(gfx, i, pageCount);
                    }
                }

                gfx.Dispose();
            }

            document.Save(inputPdfFilename);

            document.Close();
            document.Dispose();
        }
    }
}