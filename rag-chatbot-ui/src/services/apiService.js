import axios from "axios";

const API = axios.create({
  baseURL: "https://localhost:7051/api",
});

export const uploadPDF = async (file) => {
  const formData = new FormData();
  formData.append("file", file);

  return await API.post("/document/upload", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const askQuestion = async (question) => {
  return await API.post("/rag/ask", {
    question: question,
  });
};
