using System;
using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AIJobAssistant.Web.Services
{
    public static class FileTextExtractor
    {
        public static string ExtractText(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return string.Empty;

            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => ExtractPdfText(filePath),
                ".docx" => ExtractDocxText(filePath),
                ".txt" => File.ReadAllText(filePath),
                ".doc" => "Legacy .doc format not supported directly — please convert to .docx.",
                _ => "Unsupported file format."
            };
        }

        private static string ExtractPdfText(string filePath)
        {
            var sb = new StringBuilder();

            using var reader = new PdfReader(filePath);
            using var pdfDoc = new PdfDocument(reader);

            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                var page = pdfDoc.GetPage(i);
                sb.Append(PdfTextExtractor.GetTextFromPage(page));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static string ExtractDocxText(string filePath)
        {
            var sb = new StringBuilder();

            using var doc = WordprocessingDocument.Open(filePath, false);
            var body = doc.MainDocumentPart.Document.Body;

            foreach (var para in body.Elements<Paragraph>())
                sb.AppendLine(para.InnerText);

            return sb.ToString();
        }
    }
}
