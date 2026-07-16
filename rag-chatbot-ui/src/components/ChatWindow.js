import React, {useState} from "react";
import Message from "./Message";
import { askQuestion } from "../services/apiService";

function ChatWindow() {
    const [question, setQuestion] = useState("");
    const [messages, setMessages] = useState([]);

    const sendQuestion = async () => {
        if(!question.trim())
            return;

        setMessages(prev => [
            ...prev,
            {sender: "user", text: question}
        ]);

        const currentQuestion = question;

        setQuestion("");

        try{
            const response = await askQuestion(currentQuestion);

            setMessages(prev => [
                ...prev,
                {
                    sender: "bot",
                    text: response.data.answer
                }
            ]);
        }
        catch {
            setMessages(prev => [
                ...prev,
                {
                    sender: "bot",
                    text: "Error getting response"
                }
            ]);
        }
    };

    return(
        <div>
            <div className="chat-box">
                {messages.map((m, index) => (
                    <Message
                        key={index}
                        sender={m.sender}
                        text={m.text}
                    />
                ))}
            </div>
            <input
                value={question}
                onChange={(e) => setQuestion(e.target.value)}
                placeholder="Ask a question..."
            />

            <button onClick={sendQuestion}>
                send
            </button>
       </div>
    );
}

export default ChatWindow;