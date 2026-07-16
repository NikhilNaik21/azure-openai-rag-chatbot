using Microsoft.AspNetCore.Mvc;
using RAGChatbot.Models;
using RAGChatbot.Services;

namespace RAGChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIService _openAIService;

        public ChatController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat(ChatRequest request)
        {
            var answer = await _openAIService
                .GetChatResponseAsync(request.Message);

            return Ok(new
            {
                Answer = answer
            });
        }
    }
}
