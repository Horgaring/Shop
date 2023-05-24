
import {BrowserRouter as Router,Route,Routes, useNavigate} from "react-router-dom"
import Main from "./pages/Main";
import Sig from "./pages/Register";
import Kata from "./pages/katalog";
import Prodadd from './pages/AddProd'
import Profile from './pages/Profile'
import Buypage from './pages/Buypage'

function App() {
   
  return(
     <div>
      <Router>
         <Routes>
          <Route path="/" element = {<Main/>} />
          <Route path="/sigin" element = {<Sig/>} />
          <Route path="/katalog" element = {<Kata/>} />
          <Route path="/AddProd" element = {  <Prodadd/> } />
          <Route path="/Profile/:id" element = {  <Profile/> } />
          <Route path="/Buy/:id" element = {  <Buypage/> } />
         </Routes>
      </Router>
      </div>
    
      
  );
}



export default App;
