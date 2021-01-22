import React, {useState} from 'react'

import KomentariPoUserima from './KomentariPoUserima'
import PrijavaPoUserima from './PrijavaPoUserima'
function User({user}) {

    const [prikaziKomentare, setPrikaziKomentare]=useState(false);
    const [prikaziPrijave, setPrikaziPrijave]=useState(false);
    return(
        <div class="float-child card" style={{width:"30%", marginLeft:"20px", marginTop:"20px"}}>
        <div className="divUser" class=" card-body d-flex flex-column align-items-left" key={user.username}>
            <h2 style={{color:"#3399FF"}}>Username: {user.username}</h2>
            <h3>{user.ime} {user.prezime}</h3>
            
            <ul>
            {user.interesovanja.map(interesovanje=>{
                return(
                    <li key={interesovanje}>
                        {interesovanje}
                    </li>
                )
            })}
            </ul>

            <div>
                <button class="btn btn-primary" onClick={()=>setPrikaziKomentare(!prikaziKomentare)}>Prikazi komentare</button>
            </div>
            
            {prikaziKomentare && <KomentariPoUserima username={user.username}/>}
            <h5> </h5>
            <div >
                <button class="btn btn-primary" onClick={()=>setPrikaziPrijave(!prikaziPrijave)}>Prikazi dosadasnje prijave</button>
            </div>

            {prikaziPrijave && <PrijavaPoUserima username={user.username}/>}
        </div>
        </div>
    )
}

export default User
