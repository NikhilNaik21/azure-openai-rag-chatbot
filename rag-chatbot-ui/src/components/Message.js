function Message({ sender, text }) {
    return (
        <div className={sender ==="user"
            ? "user-message"
            : "bot-message"
        }>
            <strong>
                {sender === "user" ? "You" : "Bot"};
            </strong>
            <p>{text}</p>
        </div>
    );
}

export default Message;