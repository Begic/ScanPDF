using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace ScanPDF.Logic
{
    public class ScanText
    {
        public async Task<string> Scan(Stream file)
        {
            var text = string.Empty;

            var tempFilePath = Path.GetTempFileName();
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fileStream);
            }

            using (PdfReader reader = new PdfReader(tempFilePath))
            {
                using (PdfDocument document = new PdfDocument(reader))
                {
                    for (int i = 1; i <= document.GetNumberOfPages(); i++)
                    {
                        PdfPage page = document.GetPage(i);
                        text = PdfTextExtractor.GetTextFromPage(page);
                        Console.WriteLine(text);
                    }
                }
            }

            File.Delete(tempFilePath);

            return text;
        }
    }
}
