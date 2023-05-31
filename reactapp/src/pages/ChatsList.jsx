import '../css/Chatslist.css'
import {Panel} from '../components/panel'
import {ChatComp} from '../components/Chat'
import React from 'react'
import {GetChats,isautch,Url} from '../api/accapi'




export const Chatlist = () => {
    const [activeid, setid] = React.useState();
    const [chats, setchats] = React.useState([]);
    const [chatcontainer, setchatc] = React.useState();
    let [UserName,setname] = React.useState('');
    React.useEffect(() =>{
        isautch().then(res => setname(res.data.name));
        GetChats().then(res => setchats(res.data.groups.$values))
    },[activeid])
    const itemclick =(index,val) => {
        setid(index)
        
        setchatc(<ChatComp UserName={UserName} Groupname={val.groupName}  id={val.productId} />)
    }
    return(<>
        <div className='text_catalog'>
        <Panel/>
        </div>
        <div className="conteiner">
            <div className="Chatlist">
                <div><h1>Message</h1></div>
                <div className='List'>
                    {chats.map((val,ind) =>{
                        return(
                            <div className={activeid == ind?'list-item active': 'list-item'} id={ind} onClick={e => itemclick(ind,val)} key={ind}>
                            <img  src={Url+"/api/Account/image/"+val.users.$values[0].pathImage}></img>
                            <div>
                            <div className='li_Name'> {val.users.$values[0].name}</div>
                        </div>
                        <span  className='nemas'>+{val.notification}</span>
                    </div>
                        )
                    })}
                    
                </div>
            </div>
            <div className='chat'>
                {chatcontainer}
            </div>
        </div>
    </>

    )
}