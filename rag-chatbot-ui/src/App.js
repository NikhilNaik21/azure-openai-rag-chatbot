import "./App.css";
import UploadDocument from "./components/UploadDocument";
import ChatWindow from "./components/ChatWindow";
import { useState } from "react";

function App() {
  const [fileName, setFileName] = useState("");

  return (
    <div className="App">
      <h1>RAG Chatbot</h1>

      <UploadDocument onUploadSuccess={setFileName} />

      {fileName && (
        <p>
          Uploaded:
          <b>{fileName}</b>
        </p>
      )}

      <ChatWindow />
    </div>
  );
}

export default App;
