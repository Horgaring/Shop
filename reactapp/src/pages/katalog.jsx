import img from '../img/R.jpg';
import React from "react";
import { ImSearch } from 'react-icons/im';
import { IconContext } from 'react-icons';
import "../css/katalog.css"
import {Panel} from '../components/panel'
import {Url, GetProd,GetCateg} from '../api/accapi'



 
const Katalog = () => {
    
    const [ id,setid ] = React.useState(1);
    let [item,ig] = React.useState([]);
    let [categ,gc] = React.useState([]);
    let [prodleng,prodlen] = React.useState();
    let sele = React.useRef();
    let inp = React.useRef();
    function anav(){
        const list = []
        const i = id - 3
        for (let index = i < 1? 1: i; index < id + 5; index++) {
            list.push(<a className={index == id?'prodnav_item_active prodnav_item':'prodnav_item'} onClick={() => clicknavigate_a(index)}>{index}</a>)
        }
        list.push(<a className={'prodnav_item'} onClick={() => clicknavigate_a(prodleng)}>{prodleng}</a>)
        return(list)
     }
    React.useEffect( () => {
        console.log(id)
        GetProd(id,sele.current.value,inp.current.value != ''?`?sear=${inp.current.value}` : '').then(arr => {console.log('|||||||||||||||||||||4'+arr);ig(arr.data.prod.$values);prodlen(arr.data.length);console.log(arr)} )
        GetCateg().then(res => gc(res.data))
        
    },[id])
    const click = () => {
        setid(1);
    }
    const clicknavigate_a = (i) => {
        setid(i)
    } 
    
    return(
    <div>
        
        <div className='text_catalog'>
        <Panel/>
        </div>

        <div className="Filters">
            <div className='Categ'>
                <select ref={sele}>
                    <option  value='All'>All</option>
                    {categ?.map((it, i) =>  <option key={i} value={it.name}>{it.name}</option> )} 
                </select>
                <input ref={inp}  type="text"  placeholder="Text" onenter/>
                <div className='result'></div>
                <IconContext.Provider value={{style: { fontSize: '25px'}}}>
                <button className='button_filt' onClick={click}><ImSearch/></button></IconContext.Provider>
            </div>
        </div>  
        <div className="katalog">
           { item.map(function(key2, i){
            return(
                <div key={i} className = "katalog_item">
                    <img className="katalog_item_img"  src= {`${Url}/api/Product/image/` + key2.pathImage}/>
                    <div className="name">{key2.name}</div>
                    <div className="katalog_item_price">{key2.price} RUB</div>
                    <div  className='katalog_item_buy'> <a href={"/Buy/" + key2.id}>BUY</a></div>
                    
                </div>
            )
           })}
        </div>  
        <div className='prodnav'>{anav()}</div>
    </div>
    )
        
    
    
    
    
}
export default Katalog