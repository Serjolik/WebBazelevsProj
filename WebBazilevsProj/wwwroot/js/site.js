const sendMessage = async () => {
    const message = document.getElementById("message-input").value;

    const response = await fetch("/chat/sendmessage", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            message: message,
        }),
    });

    const result = await response.text();
    console.log(result);
};