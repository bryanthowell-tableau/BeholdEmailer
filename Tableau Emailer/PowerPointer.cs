using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Drawing = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing;
using P = DocumentFormat.OpenXml.Presentation;
using D = DocumentFormat.OpenXml.Drawing;

using DocumentFormat.OpenXml.Office2010.Drawing;
using System.Windows.Forms;

namespace Behold_Emailer
{
    internal class PowerPointer
    {
        private PresentationPart OpenPresentationPart;
        private PresentationDocument OpenPresentationDocument;
        public SimpleLogger Logger;
        public PowerPointer(string powerPointFileLocation, SimpleLogger logger)
        {
            Logger = logger;
            try
            {
                OpenPresentationDocument = PresentationDocument.Open(powerPointFileLocation, true);
                OpenPresentationPart = OpenPresentationDocument.PresentationPart;
            }
            catch(Exception e)
            {
                
            }
        }

        public bool ReplaceImageInSlide(SlidePart slidePart, string imageFileLocation) {

            this.Logger.Log("Trying to replace image in slide Part");
            var mySlideLayoutPart = slidePart.SlideLayoutPart;
            ImagePart imagePart = slidePart.AddImagePart("image/png");
            var imgRelId = slidePart.GetIdOfPart(imagePart);

            FileStream stream = new FileStream(imageFileLocation, FileMode.Open);

            imagePart.FeedData(stream);

            stream.Close();


            // https://blogs.msdn.microsoft.com/brian_jones/2008/11/18/creating-a-presentation-report-based-on-data/

            // Find the first image element and replace
            try
            {
                Drawing.Blip blip = slidePart.Slide.Descendants<Blip>().First();
                blip.Embed = imgRelId;
                this.Logger.Log("Saving slide Part");
                slidePart.Slide.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public SlidePart FindSlidePartBySlideNumber(int slideNumberFromOne)
        {
            // Work through all the slides
            SlideIdList slideIdList = OpenPresentationPart.Presentation.SlideIdList;
            var i = 1;
            var slideNumber = slideNumberFromOne;
            SlidePart mySlidePart = null;
            foreach (SlideId slideId in slideIdList.ChildElements)
            {

                if (i == slideNumber)
                {
                    mySlidePart = OpenPresentationPart.GetPartById(slideId.RelationshipId) as SlidePart;
                    return mySlidePart;
                }
                i++;
            }
            // Throw NotFoundException if the slide number does not exist
            throw new NotFoundException();
        }

        public void SavePresentation()
        {
            this.OpenPresentationPart.Presentation.Save();
        }

        public void ClosePresentation()
        {
            OpenPresentationDocument.Close();
        }

        /*
        // From https://docs.microsoft.com/en-us/office/open-xml/how-to-insert-a-new-slide-into-a-presentation 
        // Insert a slide into the specified presentation.
        public static void InsertNewSlide(string presentationFile, int position, string slideTitle)
        {
            // Open the source document as read/write. 
            using (PresentationDocument presentationDocument = PresentationDocument.Open(presentationFile, true))
            {
                // Pass the source document and the position and title of the slide to be inserted to the next method.
                InsertNewSlide(presentationDocument, position, slideTitle);
            }
        }

        // Insert the specified slide into the presentation at the specified position.
        public static void InsertNewSlide(PresentationDocument presentationDocument, int position, string slideTitle)
        {
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            if (slideTitle == null)
            {
                throw new ArgumentNullException("slideTitle");
            }

            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }

            // Declare and instantiate a new slide.
            Slide slide = new Slide(new CommonSlideData(new ShapeTree()));
            uint drawingObjectId = 1;

            // Construct the slide content.            
            // Specify the non-visual properties of the new slide.
            NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new NonVisualGroupShapeProperties());
            nonVisualProperties.NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = 1, Name = "" };
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties();
            nonVisualProperties.ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties();

            // Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(new GroupShapeProperties());

            // Declare and instantiate the title shape of the new slide.
            Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());

            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties
                (new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Title" },
                new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Type = PlaceholderValues.Title }));
            titleShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph(new Drawing.Run(new Drawing.Text() { Text = slideTitle })));

            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());
            drawingObjectId++;

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(
                    new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the body shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph());
            // Create the slide part for the new slide.
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            SlideId prevSlideId = null;

            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Get the ID of the previous slide.
            SlidePart lastSlidePart;

            if (prevSlideId != null)
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(prevSlideId.RelationshipId);
            }
            else
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(((SlideId)(slideIdList.ChildElements[0])).RelationshipId);
            }

            // Use the same slide layout as that of the previous slide.
            //if (null != lastSlidePart.SlideLayoutPart)
            //{
            //    slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            //}

            // Insert the new slide into the slide list after the previous slide.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);

            // Save the modified presentation.
            presentationPart.Presentation.Save();
        }

        // Update a slide at a given position
        public static void UpdateSlide(PresentationDocument presentationDocument, int position, string slideTitle, string slideText)
        {
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            if (slideTitle == null)
            {
                throw new ArgumentNullException("slideTitle");
            }

            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }

            // Declare and instantiate a new slide.
            //Slide slide = new Slide(new CommonSlideData(new ShapeTree()));
            //uint drawingObjectId = 1;
            var slideIdList = presentationPart.Presentation.SlideIdList;
            SlideId slideId = new SlideId();
            slideId.

            // Construct the slide content.            
            // Specify the non-visual properties of the new slide.
            NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new NonVisualGroupShapeProperties());
            nonVisualProperties.NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = 1, Name = "" };
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties();
            nonVisualProperties.ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties();

            // Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(new GroupShapeProperties());

            // Declare and instantiate the title shape of the new slide.
            Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());

            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties
                (new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Title" },
                new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Type = PlaceholderValues.Title }));
            titleShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph(new Drawing.Run(new Drawing.Text() { Text = slideTitle })));

            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());
            drawingObjectId++;

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(
                    new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the body shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph());
            // Create the slide part for the new slide.
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            SlideId prevSlideId = null;

            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Get the ID of the previous slide.
            SlidePart lastSlidePart;

            if (prevSlideId != null)
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(prevSlideId.RelationshipId);
            }
            else
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(((SlideId)(slideIdList.ChildElements[0])).RelationshipId);
            }

            // Use the same slide layout as that of the previous slide.
            //if (null != lastSlidePart.SlideLayoutPart)
            //{
            //    slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            //}

            // Insert the new slide into the slide list after the previous slide.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);

            // Save the modified presentation.
            presentationPart.Presentation.Save();
        }*/

       /* private SlidePart CreateSlidePart(PresentationPart presentationPart)
        {
            SlidePart slidePart1 = presentationPart
                .AddNewPart<slidepart>("rId2");
            ImagePart imagePart = slidePart1
                .AddNewPart<imagepart>("image/png", "rId2");
            string filepath = @"C:\******\image1.PNG";
            FileStream stream = new FileStream(filepath, FileMode.Open);
            imagePart.FeedData(stream);
            stream.Close();
            slidePart1.Slide = new Slide(
                    new CommonSlideData(
                        new ShapeTree(
                            new P.NonVisualGroupShapeProperties(
                                new P.NonVisualDrawingProperties()
                                { Id = (UInt32Value)1U, Name = "" },
                                new P.NonVisualGroupShapeDrawingProperties(),
                                new ApplicationNonVisualDrawingProperties()),
                            new GroupShapeProperties(new TransformGroup()),
                            new P.Shape(
                                new P.NonVisualShapeProperties(
                                    new P.NonVisualDrawingProperties()
                                    { Id = (UInt32Value)2U, Name = "Title 1" },
                                    new P.NonVisualShapeDrawingProperties(
                                        new ShapeLocks() { NoGrouping = true }),
                                    new ApplicationNonVisualDrawingProperties(
                                        new PlaceholderShape())),
                                new P.ShapeProperties(),
                                new P.TextBody(
                                    new BodyProperties(),
                                    new ListStyle(),
                                    new Paragraph(
                                        new EndParagraphRunProperties()
                                        { Language = "en-US" }))))),
                    new ColorMapOverride(new MasterColorMapping()));
            P.Picture picture = new P.Picture();
            P.BlipFill blipFill = new P.BlipFill();
            D.Blip blip = new D.Blip() { Embed = "rId2" };
            Stretch stretch = new Stretch();
            FillRectangle fillRectangle = new FillRectangle();
            stretch.Append(fillRectangle);
            blipFill.Append(blip);
            blipFill.Append(stretch);
            BlipExtensionList blipExtensionList1 = new BlipExtensionList();
            BlipExtension blipExtension1 = new BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" };

            UseLocalDpi useLocalDpi1 = new UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14",
                "http://schemas.microsoft.com/office/drawing/2010/main");

            blipExtension1.Append(useLocalDpi1);
            blipExtensionList1.Append(blipExtension1);
            blip.Append(blipExtensionList1);
            picture.Append(blipFill);
            P.ShapeProperties shapeProperties1 = new P.ShapeProperties();

            D.Transform2D transform2D1 = new D.Transform2D();
            D.Offset offset1 = new D.Offset() { X = 467544L, Y = 548680L };
            D.Extents extents1 = new D.Extents() { Cx = 3743848L, Cy = 3886743L };

            transform2D1.Append(offset1);
            transform2D1.Append(extents1);

            D.PresetGeometry presetGeometry1 = new D.PresetGeometry() { Preset = D.ShapeTypeValues.Rectangle };
            D.AdjustValueList adjustValueList1 = new D.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            picture.Append(shapeProperties1);

            slidePart1.Slide.CommonSlideData.ShapeTree.Append(picture);
            return slidePart1;
        }*/

    }
}