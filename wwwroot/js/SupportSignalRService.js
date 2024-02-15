
var roomListEl = document.getElementById("roomList")

///اتصال به هاب پشتیبان ها
var supportConnection = new signalR.HubConnectionBuilder()
    .withUrl("/supportHub")
    .build();

function init() {
    supportConnection.start();
}

///GetRooms
///متد فوق در بک اند است
supportConnection.on("GetRooms", loadRoom);

function loadRoom(rooms) {
    if (!rooms) return;

    var roomIds = Object.keys(rooms);
    if (!roomIds.length) return;

    removeAllChildren(roomListEl);


    roomIds.forEach(function (id) {
        var roomInfo = rooms[id];
        if (!roomInfo) return;

        return $("#roomList").append(`
            <a class="list-group-item list-group-item-action d-flex justify-content-center align-items-center data-id='${id}' href="#" ">${roomInfo}</a>
        `);
    })


}

function removeAllChildren(node) {
    if (!node) return;

    while (node.lastChild) {
        node.removeChild(node.lastChild);
    }
}

$(document).ready(function () {
    console.log("support ready...");

    init();
});