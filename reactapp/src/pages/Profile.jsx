import React from "react";
import { useParams } from "react-router-dom"
import "../css/Profile.css"
import img from '../img/R.jpg';
import axios from 'axios';
import {Url,DeleteProd} from '../api/accapi'
import {ImCross} from 'react-icons/im'
import { IconContext } from "react-icons";

const Profile = () =>{
    const { id } = useParams();
    const [aa,a] = React.useState({})
    const [prod,ap] = React.useState([])
    React.useEffect(() => {
            
            axios.get(`${Url}/api/Account/${id}`,{headers: {Authorization: `Bearer ${window.localStorage.getItem(`token`)}`}} ).then(res => { 
              a(res.data)
              ap(res.data.products.$values)
            }).catch(err => console.log(err)) 
            
        
    },[])
    console.log(prod)
    console.log(aa)
    
    return(
        <main>
        
            <div className="Profile_head">
                <img src="https://yobte.ru/uploads/posts/2019-11/rycar-82-foto-12.jpg"/>
                <div className="data">Name {aa.name}</div>
                <div className="data2">About me <p> dszhhyu8hgiuhgiudhgu8ihdrghedyrhgbdygydrhgy8shg8yusehuy8sgusnghuysnh8uysg8yusehgysgsrhgbnseugbnesврвероеотчовчаяероаевчовеароваероваеоревоварверваероер</p></div>

            </div>
            <div className="Prod_profile">
            <div  className = "katalog_item">
                    <img className="katalog_item_img" src={img}/>
                    <div className="name">aa</div>
                    <div className="katalog_item_price">400 RUB</div>
                    {id != '0'?<div  className='katalog_item_buy'>BUY</div>
                    : <IconContext.Provider value={{size: '40px' , color: 'blue'}}>
                    <div  className='katalog_item_Delete'><ImCross/></div></IconContext.Provider>
                    }
                </div>
            { prod.map(function(key2, i){
            return(
                <div key={i} className = "katalog_item">
                    {key2.PathImage === null ? <img className="katalog_item_img" src={img}/> : <img className="katalog_item_img"  src= {`${Url}/api/Product/GetImg/` + key2.pathImage}/>}
                    <div className="name">{key2.name}</div>
                    <div className="katalog_item_price">{key2.price} RUB</div>
                    
                    {id != '0'?<div  className='katalog_item_buy'> <a href={"/Buy/" + key2.id}>BUY</a></div>
                    : <IconContext.Provider value={{size: '40px' , color: 'blue'}}>
                    <div onClick={DeleteProd(key2.id)} className='katalog_item_Delete'><ImCross/></div></IconContext.Provider>
                    }
                </div>
            )
           })}
           </div>
        </main>
        
    )
}
export default Profile