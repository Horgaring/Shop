import React from "react";
import {isautch} from '../api/accapi'
import { IconContext } from 'react-icons';
import {IoMdChatboxes,IoMdNotifications } from 'react-icons/io';
import {BsFillPersonFill } from 'react-icons/bs';
export function Panel() {
    let [ref,refs] = React.useState([]);
    let [Wallet,setwal] = React.useState();
    let [addpr,i] = React.useState();
    React.useEffect(() =>{
        isautch().then(res => {
            
            if(res.status == 200) {
                i(<div><a href='/addprod' className='Add_Prod'>Add Product</a></div>)
                setwal(res.data.wallet)
                refs(res.data.ref.$values)
            }
        })
    },[])
    return(<>
    {addpr}
    <div><a >{Wallet} RUB </a></div>
    <IconContext.Provider value={{size: '30px'}}>
        <div><a href='/Chats'><IoMdChatboxes/> </a></div>
    </IconContext.Provider>
    <IconContext.Provider value={{size: '30px'}}>
        <ul><IoMdNotifications/> {ref?.map((key,ind) => <li key={ind}><a href={'/Buy/' + key}>сообщение</a></li>)}</ul>
    </IconContext.Provider>
    <IconContext.Provider value={{size: '30px'}}>
        <div><a href='/Profile/0'><BsFillPersonFill/> </a></div>
    </IconContext.Provider>
    </>
    )
}