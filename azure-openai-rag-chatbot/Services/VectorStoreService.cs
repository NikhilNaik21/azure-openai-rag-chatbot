using RAGChatbot.Models;

namespace RAGChatbot.Services
{
    public class VectorStoreService
    {
        private static readonly List<ChunkEmbedding> _chunks = new();

        public void AddChunk(ChunkEmbedding chunk)
        {
            _chunks.Add(chunk);
        }

        public List<ChunkEmbedding> GetAllChunks()
        {
            return _chunks;
        }

        public List<ChunkEmbedding> SearchSimilarChunks(float[] questionEmbedding, int topK = 3)
        {
            return _chunks.OrderByDescending(chunk => CosineSimilarity(questionEmbedding, (float[])chunk.Vector))
                .Take(topK)
                .ToList();
        }

        private float CosineSimilarity(float[] vectorA, float[] vectorB)
        {
            float dotProduct = 0;
            float magnitudeA = 0;
            float magnitudeB = 0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];

                magnitudeA += vectorA[i] * vectorA[i];

                magnitudeB += vectorB[i] * vectorB[i];
            }

            magnitudeA = (float)Math.Sqrt(magnitudeA);
            magnitudeB = (float)Math.Sqrt(magnitudeB);

            return dotProduct / (magnitudeA * magnitudeB);
        }

        public void Clear()
        {
            _chunks.Clear();
        }
    }
}
