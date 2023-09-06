using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace BusinessTime.Controllers;

public class FilesController : Controller
{
    static FilesController()
        => QuestPDF.Settings.License = LicenseType.Community;
    
    // GET
    public IActionResult Download()
    {
        var pdf = GetPdfDocuments();
        // the existence of the file name tells the browser
        // to initiate a download of the file
        return File(pdf, "application/pdf", "download.pdf");
    }

    public IActionResult Show()
    {
        var pdf = GetPdfDocuments();
        // the lack of a filename tells the browser
        // to open up the file if the mime-type is associated
        // with an application.
        // Most browsers can render PDF natively, so it
        // just opens in the current browser session.
        return File(pdf, "application/pdf");
    }

    private static byte[] GetPdfDocuments()
    {
        var pdf =
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Hello PDF!")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(Placeholders.LoremIpsum());
                            x.Item().Image(Placeholders.Image(200, 100));
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf();
        
        return pdf;
    }
}