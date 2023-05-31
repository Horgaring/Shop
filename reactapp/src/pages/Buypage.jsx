import React from 'react'
import '../css/Buy.css'
import { useParams } from 'react-router-dom';
import {GetProd_id,Url,isautch} from '../api/accapi'
import {Panel} from '../components/panel'
import {ChatComp} from '../components/Chat'


 const  Chat = (prop) => {
    
    const { id } = useParams();
    let [prod,setprod] = React.useState({});
    let [UserName,setname] = React.useState({});
   
    React.useEffect(() => {
        isautch().then(res => setname(res.data.name));
        GetProd_id(id).then(res => {setprod(res.data.prod)})
    },[])
    return(<main className='main-buypage'>
        <div className='text_catalog'>
        <Panel/>
        </div>
        <div className='main-buy'>
        <div className='buy'>
                <img className="buy_item_img"  src= {`${Url}/api/Product/image/` + prod.pathImage}/>
                <p>Название</p>
                <div className='Name'>{prod.name}</div>
                <p>Описание продукта</p>
                <div className='Desc'>{prod.description}</div>
               <div><input value='Перейти к Покупке' type='button' /></div>
                <a className='user'></a>
            </div>
        <div className='chat'>
               
            <ChatComp UserName={UserName} Groupname={null}  id={id} />
        </div>
        </div>
        </main>)
}
export default Chat