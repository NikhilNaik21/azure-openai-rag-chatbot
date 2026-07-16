using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using RAGChatbot.Models;

namespace RAGChatbot.Services
{ 
    public class OpenAIService
    {
        private readonly AzureOpenAISettings _settings;

        public OpenAIService(IOptions<AzureOpenAISettings> options)
        {
            _settings = options.Value;
        }

        public async Task<string> GetChatResponseAsync(string message)
        {
            var client = new AzureOpenAIClient(
                new Uri(_settings.Endpoint),
                new AzureKeyCredential(_settings.ApiKey));

            ChatClient chatClient =
                client.GetChatClient(_settings.DeploymentName);

            //var response = await chatClient.CompleteChatAsync(
            //[
            //    new UserChatMessage(message)
            //]);

            //Limit output tokens : Now GPT can never generate more than about 300 tokens.
            var options = new ChatCompletionOptions
            {
                MaxOutputTokenCount = 300,
                Temperature = 0.2f
            };

            var response = await chatClient.CompleteChatAsync(
            [
                new UserChatMessage(message)
            ],
            options);

            return response.Value.Content[0].Text;
        }

        public async Task<string> GetRagResponseAsync(string question, List<string> contextChunks)
        {
            var context = string.Join("\n\n", contextChunks);

            var prompt = $@"
                Answer the question only from the context below.

                Context:
                {context}

                Question:
                {question}

                If the answer is not present in the context, say:
                'I could not find the answer in the document.'
                ";

            var client = new AzureOpenAIClient(
                new Uri(_settings.Endpoint),
                new AzureKeyCredential(_settings.ApiKey));

            ChatClient chatClient =
                client.GetChatClient(_settings.DeploymentName);

            var response = await chatClient.CompleteChatAsync(
            [
                new UserChatMessage(prompt)
            ]);

            return response.Value.Content[0].Text;
        }
    }
}
