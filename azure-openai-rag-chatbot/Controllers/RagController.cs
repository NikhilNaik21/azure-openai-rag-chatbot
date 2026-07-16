using Microsoft.AspNetCore.Mvc;
using RAGChatbot.Models;
using RAGChatbot.Services;

namespace RAGChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RagController : ControllerBase
    {
        private readonly EmbeddingService _embeddingService;
        private readonly SearchService _searchService;
        private readonly OpenAIService _openAIService;

        public RagController(
            EmbeddingService embeddingService,
            SearchService searchService,
            OpenAIService openAIService)
        {
            _embeddingService = embeddingService;
            _searchService = searchService;
            _openAIService = openAIService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask(RagRequest request)
        {
            // Generate embedding for user question
            var questionEmbedding =
                await _embeddingService.GenerateEmbeddingAsync(
                    request.Question);

            // Retrieve most relevant chunks
            var relevantChunks =
                _searchService.Search(questionEmbedding);

            // Ask GPT using retrieved context
            var answer =
                await _openAIService.GetRagResponseAsync(
                    request.Question,
                    relevantChunks);

            return Ok(new
            {
                Question = request.Question,
                RetrievedChunks = relevantChunks,
                Answer = answer
            });
        }
    }
}
