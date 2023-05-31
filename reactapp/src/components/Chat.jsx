import React from 'react'
import '../css/Buy.css'
import { IoMdSend } from 'react-icons/io';
import * as signalR from '@microsoft/signalr'


export const ChatComp = (prop) => {

    
    const messageElement1= (name = 'Me',mess,right) =>{
        let messageElement = document.createElement("div");
        if (right) {
            messageElement.style.float = "right";
            messageElement.style.textAlign = "right";
            name = 'Я'
        }
        messageElement.innerHTML = `<div class="NameMess">${name}</div>`;
        messageElement.className = "msg";
        messageElement.innerHTML += mess;
        return messageElement;
    }
    const [hubConnection,sethubconn] = React.useState(new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5136/chat", {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory: () => window.localStorage.getItem('token'),
                
              })
            .build())
    const click = () => {
        let message = document.getElementById("inp").value;
                console.log(message + '||||||||||| send')
                console.log(prop.UserName)
                hubConnection.invoke("Send", message,parseInt(prop.id),prop.UserName)
                    .catch(function (err) {
                        console.log(123 + hubConnection.connectionId);
                        return console.log(err.toString());
                    });
    }
    React.useEffect( () => {
            hubConnection.on("Receive", function(message,username) {
                document.getElementById("mass").appendChild(messageElement1(username,message,username == prop.UserName?true:false));
                console.log(prop.UserName)
            });
           
            hubConnection.start().then(() => hubConnection.invoke("Enter",parseInt(prop.id),prop.Groupname ?prop.Groupname : null))
            .catch(function (err) {
                return console.log(err.toString());
            });
            console.log('||||||||||||||||'+prop.Groupname)
    },[])
    return(<>
        
                <div id='mass'>
                    
                </div>
            <div>
<input type="text" name="" id='inp' placeholder="Text"/>
<button onClick={click} class="btn-send"><IoMdSend/></button>
            </div>
        
        
        </>)
}
