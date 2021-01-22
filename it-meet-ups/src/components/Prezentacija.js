import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import { Link, NavLink,useParams } from "react-router-dom";

import Spinner from './Spinner';
import KomentariPoPrezentacijama from './KomentariPoPrezentacijama';
import ListaPrijavljenih from './ListaPrijavljenih';
import PreFormSigninForPresentation from './PreFormSigninForPresentation'
import PreFormLeaveComment from "./PreFormLeaveComment"

function Prezentacija() {

    const {naziv}=useParams();


    const [nazivPrezentacije, setNazivPrezentacije]=useState(naziv);

    const {data:prezentacija, loading, error}=useFetch("Prezentacija/Prezentacija/"+nazivPrezentacije);
    const [showListaPrijavljenih, setShowListaPrijavljenih]=useState(false);
    const [showForm, setShowForm]=useState(false);
    const [leftComm, setLeftComm]=useState(false);


    if (error) throw error;
    if (loading) return <Spinner />;
    return (
        
        <div class=" card-body d-flex flex-column align-items-center">
            <div class="card">
                <div class="card-body">
            <h2 style={{color:"#3399FF"}}> Naziv prezentacije: {nazivPrezentacije}</h2>
            <h3 >Predavac: {prezentacija[0].predavac}</h3>
            
            <p style={{color:"#3399FF"}}>Kljucne reci prezentacije:</p>
            <ul>
            {prezentacija.map(prez=>{
                return(
                    <li key={prez.interesovanje}>
                        {prez.interesovanje}
                    </li>
                )
                
            })}
            <h4> </h4>
            <label style={{color:"#3399FF"}}>Komentari: </label>
            <KomentariPoPrezentacijama nazivPrezentacije={nazivPrezentacije}/>
            </ul>
            <button class="btn btn-primary" onClick={()=>{setShowForm(true);setLeftComm(false);}}>Prijavi se</button> <button class="btn btn-primary" onClick={()=>setShowListaPrijavljenih(!showListaPrijavljenih)}> Prikazi listu prijavljenih</button> <button class="btn btn-primary" onClick={()=>{setShowForm(false);setLeftComm(true);}}>Ostavi komentar</button>
            {showListaPrijavljenih && <ListaPrijavljenih nazivPrezentacije={nazivPrezentacije}/>}
            {showForm && <PreFormSigninForPresentation nazivPrezentacije={nazivPrezentacije}/>}
            {leftComm && <PreFormLeaveComment nazivPrezentacije={nazivPrezentacije}/>}
        </div>
        </div>
            </div>
    )
}
export default Prezentacija
