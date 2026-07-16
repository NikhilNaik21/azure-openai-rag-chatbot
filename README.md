# Azure OpenAI RAG Chatbot

An AI-powered Retrieval-Augmented Generation (RAG) chatbot built using **ASP.NET Core**, **React**, and **Azure OpenAI**. The application allows users to upload PDF documents and ask natural language questions based solely on the uploaded content.

Instead of generating answers from general knowledge, the chatbot retrieves relevant document sections using vector embeddings and provides context-aware responses, reducing hallucinations and improving answer accuracy.

---

## Features

- Upload PDF documents
- Automatic text extraction
- Intelligent document chunking
- Vector embedding generation using Azure OpenAI
- Semantic similarity search
- Context-aware AI responses
- React-based responsive UI
- Backend REST APIs built with ASP.NET Core
- Secure API key management using .NET User Secrets
- File validation and upload restrictions
- Logging and error handling

---

## Architecture

```
                +----------------------+
                |      React UI        |
                +----------+-----------+
                           |
                    Upload PDF / Ask Question
                           |
                           v
                +----------------------+
                | ASP.NET Core Web API |
                +----------+-----------+
                           |
          +----------------+----------------+
          |                                 |
          |                                 |
     Extract Text                     User Question
          |                                 |
          +---------------+-----------------+
                          |
                    Chunk Document
                          |
                          |
              Azure OpenAI Embeddings
                          |
                          |
                  Vector Store (Memory)
                          |
                          |
               Cosine Similarity Search
                          |
                          |
             Relevant Document Chunks
                          |
                          |
                 Azure OpenAI Chat Model
                          |
                          |
                    AI Generated Answer
                          |
                          v
                     React Frontend
```

---

# How It Works

### Step 1

User uploads a PDF.

---

### Step 2

The backend

- validates the file
- extracts text
- splits the content into smaller chunks

Example

```
Chunk 1
Chunk 2
Chunk 3
...
Chunk N
```

---

### Step 3

Each chunk is converted into a vector embedding using

**Azure OpenAI Embedding Model**

These embeddings capture semantic meaning rather than simple keywords.

---

### Step 4

The embeddings are stored in an in-memory vector store.

(Current implementation)

---

### Step 5

When a user asks a question,

Example

> What technologies has Nikhil worked with?

The question is also converted into an embedding.

---

### Step 6

Cosine similarity is used to retrieve the most relevant document chunks.

---

### Step 7

Only the relevant chunks are sent to Azure OpenAI Chat Model.

This reduces token usage and improves answer accuracy.

---

### Step 8

The generated answer is returned to the React frontend.

---

# Tech Stack

## Frontend

- React
- JavaScript
- Axios
- CSS

## Backend

- ASP.NET Core 8 Web API
- C#

## AI

- Azure OpenAI
- GPT-4o
- text-embedding-3-small

## Libraries

- UglyToad.PdfPig
- Azure.AI.OpenAI

## Development Tools

- Visual Studio 2022
- Git
- GitHub
- Postman
- Swagger

---

# Project Structure

```
RAGChatbot
│
├── azure-openai-rag-chatbot
│   ├── Controllers
│   ├── Services
│   ├── Models
│   ├── Middleware
│   ├── Storage
│   ├── Program.cs
│   └── appsettings.json
│
├── rag-chatbot-ui
│   ├── src
│   ├── public
│   └── package.json
│
└── README.md
```

---

# API Endpoints

## Upload PDF

```
POST /api/document/upload
```

Uploads a PDF, extracts text and creates vector embeddings.

---

## Ask Question

```
POST /api/chat/ask
```

Accepts a user question and returns an AI-generated answer based on the uploaded document.

---

# Getting Started

## Clone Repository

```bash
git clone https://github.com/NikhilNaik21/azure-openai-rag-chatbot.git
```

---

## Backend Setup

Navigate to

```
azure-openai-rag-chatbot
```

Restore packages

```bash
dotnet restore
```

Run

```bash
dotnet run
```

Backend runs on

```
https://localhost:7051
```

Swagger

```
https://localhost:7051/swagger
```

---

## Frontend Setup

Navigate to

```
rag-chatbot-ui
```

Install dependencies

```bash
npm install
```

Start React

```bash
npm start
```

Frontend

```
http://localhost:3000
```

---

# Configuration

The project uses **.NET User Secrets** to securely store Azure OpenAI credentials.

Example

```
AzureOpenAI
    Endpoint
    ApiKey
    ChatDeployment
    EmbeddingDeployment
```

No API keys are committed to GitHub.

---

# Security

Implemented

- PDF validation
- File size restriction
- CORS configuration
- Secure secret management
- Error handling
- Request logging

---

# Current Limitations

- In-memory vector store
- Single uploaded document
- No authentication
- No persistent storage
- No conversation history

---

# Future Improvements

- Azure AI Search / PostgreSQL pgvector
- Multiple document support
- JWT Authentication
- Docker support
- Redis caching
- Streaming responses
- Conversation history
- Semantic Kernel integration
- CI/CD with GitHub Actions
- Azure deployment

---

# Resume Highlights

This project demonstrates practical experience with

- Retrieval-Augmented Generation (RAG)
- Azure OpenAI
- Vector Embeddings
- Semantic Search
- Prompt Engineering
- ASP.NET Core Web API
- React
- REST APIs
- Secure Configuration Management
- AI-powered Document Question Answering

---

# Author

**Nikhil Naik**

- GitHub: https://github.com/NikhilNaik21
- LinkedIn: https://www.linkedin.com/in/nikhilnaik211097/
