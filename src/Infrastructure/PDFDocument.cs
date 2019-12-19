using Entity;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace Infrastructure
{
    public class PDFDocument
    {
        public void SaveCommend(Commend commend, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(stream));
            Document document = new Document(pdfDocument);

            Text title = new Text($"Emcomienda: {commend.Number}");
            title.SetFontSize(30);
            title.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            document.Add(new Paragraph(title));
            Text sender = new Text($"Remitente: {commend.Sender.Name}");
            document.Add(new Paragraph(sender));
            document.Close();
        }
    }
}
