
using RAGChatbot.Middleware;
using RAGChatbot.Models;
using RAGChatbot.Services;

namespace RAGChatbot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Register services
            builder.Services.AddScoped<PdfService>();
            builder.Services.AddScoped<ChunkService>();
            builder.Services.Configure<AzureOpenAISettings>(
            builder.Configuration.GetSection("AzureOpenAI"));
            builder.Services.AddScoped<OpenAIService>();
            builder.Services.AddScoped<EmbeddingService>();
            builder.Services.AddSingleton<VectorStoreService>();
            builder.Services.AddScoped<SearchService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("ReactPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
