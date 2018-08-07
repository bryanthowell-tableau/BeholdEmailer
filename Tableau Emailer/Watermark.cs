using PdfSharp.Drawing;
using System;

namespace Behold_Emailer
{
    // Abstract Watermark class is intended to be overridden by the specific Watermark classes below
    public abstract class Watermark
    {
        public string Message { get; set; }
        public int HeightOffsetMax { get; set; }
        public string LogoFileLocation { get; set; }
        protected string justification;
        protected string pageLocation;
        abstract public string PageLocation { get; set; }

        public int WidthOffset { get; set; }
        public int FontSize { get; set; }
        public string FontName { get; set; }
        public string FontStyle { get; set; }
        public bool AddTimestampFlag { get; set; }
        public double PageHeight;
        public double PageWidth;
        protected XRect drawingBox;
        public double BoxPaddingLeftRight;
        public double BoxPaddingTopBottom;
        protected XStringFormat stringFormat;

        // Constructor sets up with the simplest possible formatting
        public Watermark(string message)
        {
            this.Message = message;
            this.LogoFileLocation = null;
            this.FontSize = 8;
            this.FontName = "Times New Roman";
            this.FontStyle = "Regular";
            this.AddTimestampFlag = false;
            this.BoxPaddingLeftRight = 10;
            this.BoxPaddingTopBottom = 10;
            this.stringFormat = new XStringFormat();

            // There is about 40 pixels to work with before the viz starts and 40 from bottom after viz ends
            this.HeightOffsetMax = 40;
        }

        // Watermarks are drawn as a box so you need both the height and the width
        // Potentially could be refactored as getter / setter properties
        public void SetPageHeightAndWidth(double height, double width)
        {
            this.PageHeight = height;
            this.PageWidth = width;

            // Offset rules take into account padding.
            if (this.pageLocation.Contains("top"))
            {
                this.drawingBox = new XRect(0 + this.BoxPaddingLeftRight, 0 + this.BoxPaddingTopBottom,
                    this.PageWidth - (this.BoxPaddingLeftRight * 2), this.HeightOffsetMax);
            }
            else if (this.pageLocation.Contains("bottom"))
            {
                this.drawingBox = new XRect(0 + this.BoxPaddingLeftRight, this.PageHeight - this.HeightOffsetMax,
                this.PageWidth - (this.BoxPaddingLeftRight * 2), this.HeightOffsetMax - this.BoxPaddingTopBottom);
            }
        }

        public virtual void WriteWatermark(XGraphics gfx, int pageNumber, int pagesCount)
        {
            if (LogoFileLocation != null)
            {
                XImage img = XImage.FromFile(this.LogoFileLocation);
                // Align the image to the text
                double h = img.PointHeight;
                double w = img.PointWidth;
                // Push the text box by the width of the image
                this.drawingBox.X = this.drawingBox.X + w;
                this.drawingBox.Width = this.drawingBox.Width - w;

                XPoint imagePoint = new XPoint(this.drawingBox.Left - w, this.drawingBox.Top);
                gfx.DrawImage(img, imagePoint);
                //double new_text_width = 30.0 + 5.0 + w;
                //text_point = new XPoint(new_text_width, point.Y);
            }
            XFontStyle fstyle;
            switch (this.FontStyle)
            {
                case "Regular":
                    fstyle = XFontStyle.Regular;
                    break;

                case "Bold":
                    fstyle = XFontStyle.Bold;
                    break;

                case "Italic":
                    fstyle = XFontStyle.Italic;
                    break;

                default:
                    fstyle = XFontStyle.Regular;
                    break;
            };
            XFont font = new XFont(this.FontName, this.FontSize, fstyle);
            string finalMessage = this.Message;
            if (this.AddTimestampFlag == true)
            {
                finalMessage += " " + DateTime.UtcNow.ToString("s");
            }
            //gfx.DrawString(this.message, font, XBrushes.Black, text_point);

            gfx.DrawString(finalMessage, font, XBrushes.Black, this.drawingBox, this.stringFormat);
        }
    }

    // Most of Text_Watermark is implemented in the base class
    public class TextWatermark : Watermark
    {
        public override string PageLocation
        {
            get { return pageLocation; }
            set
            {
                this.justification = value;
                this.pageLocation = value;

                // Vertical alignment
                if (value.Contains("top"))
                {
                    this.stringFormat.LineAlignment = XLineAlignment.Near;
                }
                if (value.Contains("bottom"))
                {
                    this.stringFormat.LineAlignment = XLineAlignment.Far;
                }

                // Horizontal alignment
                if (value.Contains("left"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Near;
                }
                else if (value.Contains("center"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Center;
                }
                else if (value.Contains("right"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Far;
                }
            }
        }

        public TextWatermark(string message) : base(message)
        {
        }
    }

    //
    public class ImageWatermark : Watermark
    {
        public string ImageLocation;

        public override string PageLocation
        {
            get { return pageLocation; }
            set
            {
                this.justification = value;
                this.pageLocation = value;

                // Vertical alignment
                if (value.Contains("top"))
                {
                    this.stringFormat.LineAlignment = XLineAlignment.Near;
                }
                if (value.Contains("bottom"))
                {
                    this.stringFormat.LineAlignment = XLineAlignment.Far;
                }

                // Horizontal alignment
                if (value.Contains("left"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Near;
                }
                else if (value.Contains("center"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Center;
                }
                else if (value.Contains("right"))
                {
                    this.stringFormat.Alignment = XStringAlignment.Far;
                }
            }
        }

        public ImageWatermark(string imageLocation)
            : base("")
        {
            this.ImageLocation = imageLocation;
        }

        public override void WriteWatermark(XGraphics gfx, int page_number, int pages_count)
        {
            XImage img = XImage.FromFile(this.ImageLocation);
            // Align the image to the text
            double h = img.PointHeight;
            double w = img.PointWidth;
            // Push the text box by the width of the image
            this.drawingBox.X = this.drawingBox.X + w;
            this.drawingBox.Width = this.drawingBox.Width - w;

            XPoint imagePoint = new XPoint(this.drawingBox.Left - w, this.drawingBox.Top);
            gfx.DrawImage(img, imagePoint);
        }
    }

    // Page Numberer is a variation of Text Watermarker that adds page numbers or # of Total
    public class PageNumberer : TextWatermark
    {
        public bool PageOfTotalFlag { get; set; }
        public string PageNumberPrefix { get; set; }
        public string PageOfSeparatorText { get; set; }

        // Default constructor assuming English 'of' as in-between if showing total pages
        public PageNumberer(string pageNumberPrefix, bool pageOfTotalFlag)
            : base(pageNumberPrefix)
        {
            // page_number_prefix becomes message of Text_Watermark parent class
            this.PageOfTotalFlag = pageOfTotalFlag;
            this.PageOfSeparatorText = "of";
        }

        public PageNumberer(string pageNumberPrefix, bool pageOfTotalFlag, string pageOfSeparatorText)
            : base(pageNumberPrefix)
        {
            // page_number_prefix becomes message of Text_Watermark parent class
            this.PageOfTotalFlag = pageOfTotalFlag;
            this.PageOfSeparatorText = pageOfSeparatorText;
        }

        public override void WriteWatermark(XGraphics gfx, int pageNumber, int pagesCount)
        {
            string pageNumberText;
            // X of N
            if (this.PageOfTotalFlag == true)
            {
                pageNumberText = String.Format("{0} {1} {2}", pageNumber + 1, PageOfSeparatorText, pagesCount);
            }
            else
            {
                pageNumberText = String.Format("{0}", pageNumber + 1);
            }

            if (this.Message != "")
            {
                pageNumberText = String.Format("{0} {1}", this.Message, pageNumberText);
            }
            XFontStyle fstyle;
            switch (this.FontStyle)
            {
                case "Regular":
                    fstyle = XFontStyle.Regular;
                    break;

                case "Bold":
                    fstyle = XFontStyle.Bold;
                    break;

                case "Italic":
                    fstyle = XFontStyle.Italic;
                    break;

                default:
                    fstyle = XFontStyle.Regular;
                    break;
            };

            XFont pnfont = new XFont(this.FontName, this.FontSize, fstyle);
            gfx.DrawString(pageNumberText, pnfont, XBrushes.Black, this.drawingBox, this.stringFormat);
        }
    }
}