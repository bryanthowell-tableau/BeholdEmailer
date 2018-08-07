using DocumentFormat.OpenXml.Packaging;

namespace Behold_Emailer
{
    internal class PowerPointer
    {
        public PowerPointer(string fileLocation)
        {
            using (PresentationDocument presentationDocument = PresentationDocument.Open(fileLocation, true))
            {
                PresentationPart presentationPart = presentationDocument.PresentationPart;
                // Slide slide = new Slide(new )
            }
        }
    }
}