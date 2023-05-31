
import {BrowserRouter as Router,Route,Routes, useNavigate} from "react-router-dom"
import Main from "./pages/Main";
import Sig from "./pages/Register";
import Kata from "./pages/katalog";
import Prodadd from './pages/AddProd'
import Profile from './pages/Profile'
import Buypage from './pages/Buypage'
import {Chatlist} from './pages/ChatsList'

function App() {
   
  return(
     <div>
      <Router>
         <Routes>
          <Route path="/" element = {<Main/>} />
          <Route path="/Reg" element = {<Sig/>} />
          <Route path="/katalog" element = {<Kata/>} />
          <Route path="/AddProd" element = {  <Prodadd/> } />
          <Route path="/Profile/:id" element = {  <Profile/> } />
          <Route path="/Buy/:id" element = {  <Buypage/> } />
          <Route path="/Chats" element = {  <Chatlist/> } />
         </Routes>
      </Router>
      </div>
    
      
  );
}



export default App;
