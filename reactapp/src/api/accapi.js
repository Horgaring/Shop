import axio from "axios"


const axios = axio.create({headers: {Authorization: `Bearer ${window.localStorage.getItem(`token`)}`}});
export const Url = 'http://localhost:5136'
const setlocal = (res) =>{
        window.localStorage.setItem(`token`,String( res.data.accesstoken))
        window.localStorage.setItem(`refreshtoken`,res.data.refreshtoken)
} 
export  const Regacc =  (item) => {
    axios.post(`${Url}/api/Account/reg`,item, {headers: {"Content-Type": "multipart/form-data"}}).then(res => { 
        setlocal(res)
    }).catch(err => console.log(err))
}
export  const CreateProd =  (item,param) => {
    axios.post(`${Url}/api/Product/products?` + param,item, {headers: {"Content-Type": "multipart/form-data","Authorization": `Bearer ${window.localStorage.getItem(`token`)}`}}).then(res => { 
        console.log(res.data)
    }).catch(err => console.log(err))
    
}
export const GetProd =  (id,cat,sear='') =>   axios.get(Url + `/api/Product/products/${id}/${cat}${sear}`)
export const isautch = () => axios.get(Url + "/api/Account/isauth" )
export const GetCateg = () => axios.get(`${Url}/api/Product/categs`)
export const GetProd_id =  (id) =>   axios.get(Url + `/api/Product/products/${id}`)
export const Sigin = (form) => axios.post(`${Url}/api/Account/Sigin`,form).then(e => {console.log(e);setlocal(e)}).catch(e => console.log(e))
export const DeleteProd = (id) => axios.delete(Url + `/api/Product/products/${id}`).then(e => {console.log(e)}).catch(e => {console.log(e)});

axios.interceptors.response.use( (response) => response,err => {
    if (err.response.status == 401) {
        axios.post(`${Url}/api/Oauth`,{Accesstoken: window.localStorage.getItem(`token`),Refreshtoken: window.localStorage.getItem(`refreshtoken`)}).then(res => setlocal(res)).catch((err) => window.location.href = '/')
        
    }
    return err;
});

