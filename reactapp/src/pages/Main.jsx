import React from "react";
import  '../css/style.css';
import  '../css/style2.css';
import youtube from "../iconnet/youtube.svg";
import facebook from "../iconnet/facebook.svg";
import inst from "../iconnet/inst.svg";
import {Sigin} from '../api/accapi'
import f from '../img/OIP30.png';
import { useNavigate } from "react-router-dom";
import {Panel} from '../components/panel'

export default function Main(){
        
        const [Email,setEmail] = React.useState('')
        const [password,setpass] = React.useState('')

        React.useEffect(()=>{
            const ob = new IntersectionObserver((ent => {
                ent.forEach(val => {
                    if(val.isIntersecting){
                        if(val.target.className.indexOf('lii') != -1){
                            val.target.classList.add('lii2')
                            return
                        }
                        if(val.target.className.indexOf('p1') != -1){
                            val.target.classList.add('p11')
                            return
                        }
                        val.target.classList.add('divde20')
                    }
                })
            }))
            document.querySelectorAll('.anim').forEach((i) => ob.observe(i))
        }
        ,[])
        const nav = useNavigate()
        const submit = ()=>{
            
            const formd = new FormData()
            formd.append('Email', Email)
            formd.append('Password',password)
            Sigin(formd)
           window.location = '/katalog'
        }
        return(
           <>
           <div className='text_catalog'>
        <Panel/>
        </div>
            <header>
            
            
           <div className="frame"></div>
            
            
           <div className="form">
                
                <form  onSubmit={submit}>
                <p>    
                    <input name="Email"  placeholder="Email" className="popa" value={Email} onChange={e => setEmail(e.target.value)} type="text"  />
                </p>
                <p>        
                    <input name="Password"  placeholder="Password" className="popa" value={password} onChange={e => setpass(e.target.value)} type="password"  />
                </p>
    
                <p >
                    <input  type="Submit"   value="Sign"  className="button" placeholder="Sign" />
                </p>
                <a  href="/Reg">Register</a>
                </form>
                
                <div className="Network">
                    <a href="/" className="inst"><img src={inst} width="40" height="40"/></a>
                    <a href="/" className="facebook"><img src={facebook} width="40" height="40" /></a>
                    <a href="/" className="youtube"><img src={youtube} width="40" height="40" /></a>
                </div>
            </div>
            </header>
            
            <div className="divde"><div className="divde2 anim"></div><div id="ul"><ul>
                <li className="lii anim">мы предлагаем широкий выбор игр, чтобы удовлетворить интересы любого игрока. Мы предлагаем игры разных жанров, от стратегий до спортивных симуляторов, что дает возможность пользователям выбирать из множества вариантов.</li>
                <li className="lii anim">мы предлагаем выгодные условия для игроков, такие как низкие комиссии за транзакции и быструю обработку платежей. Это позволяет нашим пользователям сэкономить деньги и время на транзакциях.</li>
                <li className="lii anim">мы предоставляем удобный интерфейс и функционал, который делает использование нашей биржи быстрым и простым. Наш интерфейс интуитивно понятен, что позволяет игрокам быстро найти нужную игру и легко совершать транзакции.</li>
                </ul ></div></div>
            <div className="DP">
                <img src={f} width={'1000px'} height={'1000px'} className="DwarfImg"></img>
                <div className="p1 anim">
                    VirtualVendor - это онлайн-биржа, специализирующаяся на торговле игровыми предметами. С момента своего создания, VirtualVendor установил себя как ведущий инноватор в области технологии продажи игровых предметов, предоставляя высококачественные услуги клиентам по всему миру.
                    Одной из основных особенностей VirtualVendor является то, что все предметы продаваемые на платформе проходят строгую проверку перед тем, как они появятся на продаже. Это гарантирует, что все предметы на платформе VirtualVendor являются легитимными, безопасными и соответствуют стандартам качества. 
                    В VirtualVendor имеется огромное количество предметов, которые доступны для покупки и продажи, включая одежду, оружие, драгоценности, карты и многое другое. При этом платформа предоставляет широкий выбор игр и игровых проектов, среди которых можно найти как старые классики, так и новинки в мире киберспорта.
                    В целом, VirtualVendor - это прогрессивная платформа в мире продажи игровых предметов, которая предоставляет своим клиентам высококачественный сервис, безопасность, и огромный выбор продуктов. Они продолжают инвестировать в новые технологии и инструменты, чтобы обеспечить своим клиентам наилучший опыт покупки и продажи игровых предметов на рынке.
                </div>
            </div>
            
            
           </>

        )
            
    
}

