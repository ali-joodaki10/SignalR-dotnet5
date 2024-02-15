var chatBox = $("#chat-box");
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.start();

//connection.invoke("SendNewMassage", "علی", "پیام علی است");

function showChatDialog() {
    chatBox.css("display", "block");
}

function init() {
    setTimeout(showChatDialog(), 1000);

    var newMessageFrom = $("#new-message-from");
    newMessageFrom.on("submit", function (e) {
        e.preventDefault();

        var message = e.target[0].value;

        e.target[0].value = '';

        sendMessage(message);
    });

}

function sendMessage(text) {  
    connection.invoke("SendNewMessage", "علی", text);
}

///دریافت پیام از سرور
connection.on('getNewMessage', getMessage);

function getMessage(sender,message,time) {
    $("#messages").append(`
            <li>
                <div>
                    <span class="name">${sender}</span>
                    <span class="time">${time}</span>
                </div>
                <div class="message">${message}</div>
            </li>
    `);
}

$(document).ready(function () {
    init();
   
});