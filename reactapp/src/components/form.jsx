import React from 'react';
import '../css/regis.css'
import {Regacc,CreateProd, GetCateg} from '../api/accapi'
import { HiOutlinePlus, HiX } from 'react-icons/hi';
import { IconContext } from 'react-icons';
import { useNavigate } from 'react-router-dom';
import axios from "axios"
import {Url} from '../api/accapi'


export   function Form(){
    const navigate = useNavigate()
    const event_form = (e) => {
        
        
        let pr = new FormData()
        
            pr.append('Name',Name)
            pr.append('Email',Email)
            pr.append('Password',Password)
            pr.append('File',File)       
        navigate('/katalog/1')
        Regacc(pr)
        
    }

   

    const [Email,setEmail] = React.useState(" ");
    const [Name,setName] = React.useState(" ");
    const [Password,setPassword] = React.useState(" ");
    const [File,setFile] = React.useState(); 
   
return(<div className="div">
<form onSubmit={e => event_form(e)}>
                
                <p>    
                    <input  placeholder="Name" className="popa" type="text" maxLength={20} required value={Name.value} onChange={e => setName(e.target.value)} />
                </p>
                <p>    
                    <input  placeholder="Email" className="popa" type="email"  value={Email.value} onChange={e => setEmail(e.target.value)} required />
                </p>
                <p>        
                    <input placeholder="Password" className="popa" type="password" minLength={8} required value={Password.value} onChange={e => setPassword(e.target.value)} />
                </p>
                <p>        
                    <input  type="file"  onChange={e => setFile(e.target.files[0])}/>
                </p>
                <p >
                    <input type="submit" value="Sign"   className="button" placeholder="Sign" />
                </p>
                
                </form>
</div>)
}

export   function Formprod(){
    const navigate = useNavigate()
    const event_form = (e) => {
        
        console.log(File)
        const formd = new FormData()
        formd.append('Name', Name)
        formd.append('Description',Description)
        formd.append('Price',Price)
        formd.append('File',File)
        let param = ''
        categ.map(value => {param = param.concat(`categ="${value}"&`)})
        CreateProd(formd,param)
        navigate('/katalog/1')
        
        
    }
    const click_event = (cat_name) => categ.includes(cat_name)? setcateg((e) => e = [...categ].splice(categ.indexOf(cat_name) - 1,1)) : setcateg((e) => e = [...categ,cat_name])

    const [Description,setDescription] = React.useState(" ");
    const [Name,setName] = React.useState(" ");
    const [Price,setPrice] = React.useState(" ");
    const [categ,setcateg] = React.useState([]); 
    const [categ2,setcateg2] = React.useState([]); 
    const [File,setFile] = React.useState(); 
   
    React.useEffect(() => {
        axios.get(`${Url}/api/Product/GetCateg`).then(res => { 
            setcateg2(res.data)
        }).catch(err => console.log(err)) 
    },[]);

return(
    
<div className="div">
    
<form >
               
                <p>    
                    <input  placeholder="Name" className="popa" type="text" required value={Name.value} onChange={e => setName(e.target.value)} />
                </p>
                <p>    
                    <input  placeholder="Description" className="popa" type="text"  value={Description.value} onChange={e => setDescription(e.target.value)} required />
                </p>
                <p>        
                    <input placeholder="Price" className="popa" type="number" required value={Price.value} onChange={e => setPrice(e.target.value)}/>
                </p>
                <p>        
                    <input  type="file"  onChange={e => setFile(e.target.files[0])}/>
                </p>
                <div id='category_prod'>
                    
                    {categ2.map((item,key) =>
                        <IconContext.Provider value={{ className: 'react-icons' }}>
                            <button type='button' key={key} className="category_item" onClick={() => click_event(item.name)}> {item.name} {categ.includes(item.name)? <HiX/>:<HiOutlinePlus /> } </button>
                        </IconContext.Provider>
                    )}
                </div>
                <p >
                    <input type="button"  value="Create" onClick={(e) => event_form(e)}  className="button"  />
                </p>
                
                </form>
                
</div>)
}