using InsuranceTgBot.Models;
using InsuranceTgBot.Services.Interfaces;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace InsuranceTgBot.Services
{
    public class DocumentGenerator(ILogger<DocumentGenerator> logger, IWebHostEnvironment env) : IDocumentGenerator
    {
        public string GenerateDocument(DriverLicense license, VehicleDocument vehicleDoc, User user)
        {
            //Here we create pdf doc and fill basic info
            var doc = new PdfDocument();
            doc.Info.Author = "InsuranceTgBot";
            var reportId = Guid.NewGuid().ToString();
            doc.Info.Title = $"{user.UserName}-{reportId}";
            var path = Path.Combine(env.WebRootPath, "Docs");
            var pathToFile = path + $"\\{doc.Info.Title}.pdf";

            // Here we ensure directory for docs is created
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Here we begin to work with doc
            var page = doc.AddPage();
            var gfx = XGraphics.FromPdfPage(page);


            var titleFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
            var headerFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
            var textFont = new XFont("Verdana", 12, XFontStyleEx.Regular);
            double y = 40;

            // Company info
            gfx.DrawString("InsuranceTgBot", titleFont, XBrushes.DarkBlue, new XRect(0, y, page.Width, 40), XStringFormats.TopCenter);
            y += 50;

            // Report info
            var policyNumber = $"POL-{reportId.ToUpper()}";
            var reportDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

            gfx.DrawString($"Policy Number: {policyNumber}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Date: {reportDate}", textFont, XBrushes.Black, 40, y);
            y += 30;

            // Divider line
            gfx.DrawLine(XPens.Black, 40, y, page.Width - 40, y);
            y += 20;


            //// Title
            //gfx.DrawString("Insurance Report", titleFont, XBrushes.Black, new XRect(0, y, page.Width, 40), XStringFormats.TopCenter);
            //y += 60;

            // Driver License Section
            gfx.DrawString("Driver License", headerFont, XBrushes.Black, 40, y);
            y += 30;

            gfx.DrawString($"Name: {license.FirstName} {license.LastName}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Country: {license.CountryCode}, State: {license.State}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"ID Number: {license.IdentificationNumber}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Category: {license.Category}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Date of Birth: {license.DateOfBirth:yyyy:MM:dd}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Issued: {license.Issued:yyyy:MM:dd}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Expires: {license.Expires:yyyy:MM:dd}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"DD Number: {license.DDNumber}", textFont, XBrushes.Black, 40, y);
            y += 30;

            // Divider line
            gfx.DrawLine(XPens.Black, 40, y, page.Width - 40, y);
            y += 20;

            // Vehicle document Section
            gfx.DrawString("Driver License", headerFont, XBrushes.Black, 40, y);
            y += 30;

            gfx.DrawString($"Vehicle ID Number: {vehicleDoc.VehicleIdNumber}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Manufacturer: {vehicleDoc.Manufacturer}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Model: {vehicleDoc.Model}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Issued: {vehicleDoc.Issued:yyyy-MM-dd}", textFont, XBrushes.Black, 40, y);
            y += 20;
            gfx.DrawString($"Manufactured: {vehicleDoc.Manufactured:yyyy-MM-dd}", textFont, XBrushes.Black, 40, y);
            y += 30;

            // Divider line
            gfx.DrawLine(XPens.Black, 40, y, page.Width - 40, y);
            y += 20;

            // Signature
            gfx.DrawString("Signature:________________________", textFont, XBrushes.Black, 40, y);
            y += 30;
            gfx.DrawString("(Electronic signature)", textFont, XBrushes.Gray, 45, y);

            try
            {
                doc.Save(pathToFile);
                doc.Close();
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
            return pathToFile;
        }
    }
}
