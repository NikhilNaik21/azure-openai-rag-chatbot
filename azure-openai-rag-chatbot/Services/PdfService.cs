using UglyToad.PdfPig;

namespace RAGChatbot.Services
{
    public class PdfService
    {
        public string ExtractText(string filePath)
        {
            string text = string.Empty;

            using var document = PdfDocument.Open(filePath);

            foreach(var page in document.GetPages())
            {
                text += page.Text + Environment.NewLine;
            }

            return text;
        }
    }
}
