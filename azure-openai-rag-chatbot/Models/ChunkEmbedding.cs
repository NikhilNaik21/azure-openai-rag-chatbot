namespace RAGChatbot.Models
{
    public class ChunkEmbedding
    {
        public string Text { get; set; } = string.Empty;

        public IReadOnlyList<float> Vector { get; set; }
            = new List<float>();
    }
}
