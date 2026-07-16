using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Embeddings;
using RAGChatbot.Models;

namespace RAGChatbot.Services
{
    public class EmbeddingService
    {
        private readonly AzureOpenAISettings _settings;

        public EmbeddingService(IOptions<AzureOpenAISettings> options)
        {
            _settings = options.Value;
        }

        public async Task<IReadOnlyList<float>> GenerateEmbeddingAsync(string text)
        {
            var client = new AzureOpenAIClient(
                    new Uri(_settings.Endpoint),
                    new AzureKeyCredential(_settings.ApiKey));


            EmbeddingClient embeddingClient =
           client.GetEmbeddingClient(
               _settings.EmbeddingDeploymentName);

            var response =
                await embeddingClient.GenerateEmbeddingAsync(text);

            return response.Value.ToFloats().ToArray();
        }
    }
}
