import React from 'react'
import '../css/Buy.css'
import { IoMdSend } from 'react-icons/io';
import * as signalR from '@microsoft/signalr'
import { useParams } from 'react-router-dom';
import {GetProd_id,Url,isautch} from '../api/accapi'

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
 const  Chat = (prop) => {
    
    const { id } = useParams();
    let [prod,setprod] = React.useState({});
    let [UserName,setname] = React.useState({});
    let [conid ,setconn] = React.useState(id + `_ ${Math.random()}`);
    const [hubConnection,drdrydrydry] = React.useState(new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5136/chat", {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets,
                accessTokenFactory: () => window.localStorage.getItem('token'),
                
              })
            .build())
    const click = () => {
        
        let message = document.getElementById("inp").value;
                console.log(message + '||||||||||| send')
                hubConnection.invoke("Send", message,id,UserName)
                    .catch(function (err) {
                        console.log(123 + hubConnection.connectionId);
                        return console.log(err.toString());
                    });
    }
    React.useEffect(() => {
        isautch().then(res => setname(res.data.name));
        document.getElementById("mass").appendChild(messageElement1('12346346743576','aboab'));
        GetProd_id(id).then(res => setprod(res.data))
        console.log(conid)
            hubConnection.on("Receive", function(message,username) {
                console.log(`${username}:${message}|||||||||||recive`)
                
                document.getElementById("mass").appendChild(messageElement1(username,message,username == UserName?true:false));
            });
           
            hubConnection.start().then(() => hubConnection.invoke("Enter",parseInt(id),prop?null:prop))
            .catch(function (err) {
                return console.log(err.toString());
            });
            
           
            
            
    },[])
    return(<main className='main-buypage'>
        <div className='main-buy'>
        <div className='buy'>
                <img className="buy_item_img"  src= {`${Url}/api/Product/GetImg/` + prod.pathImage}/>
                <p>Название</p>
                <div className='Name'>{prod.name}</div>
                <p>Описание продукта</p>
                <div className='Desc'>{prod.description}</div>
               <div><input value='Перейти к Покупке' type='button' /></div>
                <a className='user'></a>
            </div>
        <div className='chat'>
                <div id='mass'>
                    
                </div>
            <div>
<input type="text" name="" id='inp' placeholder="Text"/>
<button onClick={click} class="btn-send"><IoMdSend/></button>
            </div>
        </div>
        </div>
        </main>)
}
export default Chat