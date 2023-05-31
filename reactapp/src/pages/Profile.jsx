import React from "react";
import { useParams } from "react-router-dom"
import "../css/Profile.css"
import img from '../img/R.jpg';
import axios from 'axios';
import {Url,DeleteProd} from '../api/accapi'
import {ImCross} from 'react-icons/im'
import {MdOutlineModeEditOutline} from 'react-icons/md'
import { IconContext } from "react-icons";
import {Panel} from '../components/panel'
import {Updateacc} from '../components/UpdateFormData'

const Profile = () =>{
    const { id } = useParams();
    const [acc,setacc] = React.useState({})
    const [prod,ap] = React.useState([])
    let [updateform,setupd] = React.useState();
   
    React.useEffect(() => {
        
            axios.get(`${Url}/api/Account/${id}`,{headers: {Authorization: `Bearer ${window.localStorage.getItem(`token`)}`}} ).then(res => { 
              setacc(res.data)
              ap(res.data.products.$values)
            }).catch(err => console.log(err)) 
            
        
    },[])
    
    const EditClick =()=>{
        setupd(<Updateacc></Updateacc>)
    }
    return(
        <main>
            
            <div className='text_catalog'>
        <Panel/>
        </div>
        
            <div className="Profile_head">
            
                {id == '0'?<IconContext.Provider value={{size: '40px' , color: 'blue'}} ><div  className='Edit' onClick={EditClick}><MdOutlineModeEditOutline/></div></IconContext.Provider>
                    : null
                    }
                <img src={Url+"/api/Account/image/"+acc.pathImage}/>
                <div className="data">Name {acc.name}</div>
                <div className="data2">About me <p> {acc.Description}</p></div>
                
            </div>
            <div className="Prod_profile">
            { prod.map(function(key2, i){
            return(
                <div key={i} className = "katalog_item">
                    {key2.PathImage === null ? <img className="katalog_item_img" src={img}/> : <img className="katalog_item_img"  src= {`${Url}/api/Product/image/` + key2.pathImage}/>}
                    <div className="name">{key2.name}</div>
                    <div className="katalog_item_price">{key2.price} RUB</div>
                    
                    {id != '0'?<div  className='katalog_item_buy'> <a href={"/Buy/" + key2.id}>BUY</a></div>
                    : <IconContext.Provider value={{size: '40px' , color: 'blue'}}>
                    <div onClick={p => DeleteProd(key2.id)} className='katalog_item_Delete'><ImCross/></div></IconContext.Provider>
                    }
                </div>
            )
           })}
           
           </div>
           {updateform}
        </main>
        
    )
}
export default Profile