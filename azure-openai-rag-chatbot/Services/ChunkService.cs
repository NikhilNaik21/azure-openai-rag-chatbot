namespace RAGChatbot.Services
{
    public class ChunkService
    {
        public List<string> CreateChunks(string text, int chunkSize = 500, int overlap = 100)
        {
            var chunks = new List<string>();

            if (string.IsNullOrWhiteSpace(text))
                return chunks;

            int start = 0;

            while(start < text.Length)
            {
                int length = Math.Min(chunkSize, text.Length - start);

                chunks.Add(text.Substring(start, length));

                start += chunkSize - overlap;
            }

            return chunks;
        }
    }
}
