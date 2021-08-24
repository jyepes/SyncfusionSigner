using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.IO;

namespace SignerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDkyMDg0QDMxMzkyZTMyMmUzMGdZSTl0aWVtZ096TWk5dEhhekc1NXlpZUpxODEzMUEzRVk4bFdNTU40UE09");
            Console.WriteLine("Hello World!");
            //CreatePDFDigitalSignature();
            CreatePDFDigitalSignaturesWithCustomAppearances();
        }

        static void CreatePDFDigitalSignature()
        {
            //Load existing PDF document.
            using Stream fs = File.OpenRead(@"unsigned.pdf");
            PdfLoadedDocument document = new PdfLoadedDocument(fs);

            //Load digital ID with password.
            using Stream cert = File.OpenRead("cert.pfx");
            PdfCertificate certificate = new(cert, "123456789");

            //Create a signature with loaded digital ID.
            PdfSignature signature = new(document, document.Pages[0], certificate, "DigitalSignature");

            //Save the PDF document.
            using FileStream output = File.Open("signed.pdf", FileMode.Create);
            document.Save(output);

            //Close the document.
            document.Close(true);
        }

        static void CreatePDFDigitalSignaturesWithCustomAppearances()
        {
            //Load existing PDF document.
            using Stream fs = File.OpenRead(@"unsigned.pdf");
            PdfLoadedDocument document = new PdfLoadedDocument(fs);

            //Load digital ID with password.
            using Stream cert = File.OpenRead("cert.pfx");
            PdfCertificate certificate = new(cert, "123456789");

            //Create a signature with loaded digital ID.
            PdfSignature signature = new PdfSignature(document, document.Pages[0], certificate, "DigitalSignature");
            
            //Set bounds to the signature.
            signature.Bounds = new Syncfusion.Drawing.RectangleF(40, 40, 350, 100);

            //Load image from file.
            using Stream image_fs = File.OpenRead(@"firma-transparente.png");
            PdfImage image = PdfImage.FromStream(image_fs);
                        
            //Create a font to draw text.
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

            //Drawing text, shape, and image into the signature appearance.
            signature.Appearance.Normal.Graphics.DrawRectangle(PdfPens.Black, PdfBrushes.White, new Syncfusion.Drawing.RectangleF(50, 0, 300, 100));
            signature.Appearance.Normal.Graphics.DrawImage(image, 0, 0, 100, 100);
            signature.Appearance.Normal.Graphics.DrawString("Firmado por JULIO CÉSAR YEPES RUIZ", font, PdfBrushes.Black, 120, 17);
            signature.Appearance.Normal.Graphics.DrawString("Reason: Prueba firma de contrato", font, PdfBrushes.Black, 120, 39);
            signature.Appearance.Normal.Graphics.DrawString("Location: Colombia", font, PdfBrushes.Black, 120, 60);

            //Save the PDF document.
            using FileStream output = File.Open("signed.pdf", FileMode.Create);
            document.Save(output);

            //Close the document.
            document.Close(true);
        }


    }
}
