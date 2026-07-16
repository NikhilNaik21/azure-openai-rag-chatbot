namespace RAGChatbot.Services
{
    public class SearchService
    {
        private readonly VectorStoreService _vectorStore;

        public SearchService(VectorStoreService vectorStore)
        {
            _vectorStore = vectorStore;
        }

        public List<string> Search(
            IReadOnlyList<float> questionVector,
            int topK = 3)
        {
            return _vectorStore.GetAllChunks()
                .Select(chunk => new
                {
                    chunk.Text,
                    Score = CosineSimilarity(
                        questionVector,
                        chunk.Vector)
                })
                .OrderByDescending(x => x.Score)
                .Take(topK)
                .Select(x => x.Text)
                .ToList();
        }

        private double CosineSimilarity(
            IReadOnlyList<float> v1,
            IReadOnlyList<float> v2)
        {
            double dot = 0;
            double mag1 = 0;
            double mag2 = 0;

            for (int i = 0; i < v1.Count; i++)
            {
                dot += v1[i] * v2[i];
                mag1 += Math.Pow(v1[i], 2);
                mag2 += Math.Pow(v2[i], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }
}
