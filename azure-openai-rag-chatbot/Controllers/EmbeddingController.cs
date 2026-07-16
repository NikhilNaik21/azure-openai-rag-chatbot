using Microsoft.AspNetCore.Mvc;
using RAGChatbot.Models;
using RAGChatbot.Services;

namespace RAGChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbeddingController : Controller
    {
        private readonly EmbeddingService _embeddingService;

        public EmbeddingController(
            EmbeddingService embeddingService)
        {
            _embeddingService = embeddingService;
        }

        [HttpPost]
        public async Task<IActionResult> Generate(
            ChatRequest request)
        {
            var vector =
                await _embeddingService
                    .GenerateEmbeddingAsync(request.Message);

            return Ok(new
            {
                Dimension = vector.Count,
                First10Values = vector.Take(10)
            });
        }
    }
}
