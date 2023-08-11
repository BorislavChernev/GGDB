"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, timestamp) {

    debugger;
    //No InnerHTML used to prevent HTML injection
    let li = document.createElement('li');
    li.classList.add("flex");
    li.classList.add("space-x-2");

    let div1 = document.createElement('div');
    div1.classList.add('flex-none');

    let span1 = document.createElement('span');
    span1.classList.add('font-semibold');
    span1.textContent = user;

    let span2 = document.createElement('span');
    span2.classList.add('text-gray-500');
    span2.textContent = ` ${timestamp}`;

    div1.appendChild(span1);
    div1.appendChild(span2);

    let div2 = document.createElement('div');
    div2.classList.add('flex-grow');

    let p = document.createElement('p');
    p.textContent = message;

    div2.appendChild(p);

    li.appendChild(div1);
    li.appendChild(div2);

    document.getElementById("messagesList").appendChild(li);

    document.getElementById("messageInput").value = '';
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var discussionId = parseInt(document.getElementById("discussionInput").value);
    connection.invoke("SendMessage", user, message, discussionId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});