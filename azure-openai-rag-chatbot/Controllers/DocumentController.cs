using Microsoft.AspNetCore.Mvc;
using RAGChatbot.Models;
using RAGChatbot.Services;

namespace RAGChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly PdfService _pdfService;
        private readonly ChunkService _chunkService;
        //emnedding
        private readonly EmbeddingService _embeddingService;
        private readonly VectorStoreService _vectorStore;

        public DocumentController(PdfService pdfService, ChunkService chunkService, EmbeddingService embeddingService,VectorStoreService vectorStore)
        {
            _pdfService = pdfService;
            _chunkService = chunkService;
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No ile uploaded.");
            }

            const long maxSize = 1024 * 1024;

            if (file.Length > maxSize)
            {
                return BadRequest("PDF should be less than 1 MB.");
            }

            if (file.ContentType != "application/pdf")
            {
                return BadRequest("Only PDF files are allowed.");
            }

            var uploadFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Storage",
                "Uploads");

            Directory.CreateDirectory(uploadFolder);

            var filePath = Path.Combine(uploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var extractedText = _pdfService.ExtractText(filePath);

            //Generate chunks
            var chunks = _chunkService.CreateChunks(extractedText);

            // Clear old document chunks
            _vectorStore.Clear();

            // Generate embeddings for each chunk and store them
            foreach (var chunk in chunks)
            {
                var embedding =
                    await _embeddingService.GenerateEmbeddingAsync(chunk);

                _vectorStore.AddChunk(new ChunkEmbedding
                {
                    Text = chunk,
                    Vector = embedding
                });
            }

            return Ok(new
            {
                Filename = file.FileName,
                charcaterCount = extractedText.Length,
                //Preview = extractedText.Substring(
                //    0,
                //    Math.Min(500, extractedText.Length))
                TotalChunk = chunks.Count(),
                FirstChunk = chunks.FirstOrDefault(),
                LastChunk = chunks.LastOrDefault()
            });
        }
    }
}
