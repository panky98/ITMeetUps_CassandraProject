import React, {useState} from 'react'
import useFetch from '../services/useFetch'


function FormCreateCompany(){
    const [newPib, setNewPib]=useState("");
    const [newNaziv, setNewNaziv]=useState("");
    const [newAdresa, setNewAdresa]=useState("");
    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>    
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>PIB: </label> <input type="number" class="form-control" onChange={(event)=>setNewPib(event.target.value)}/> {(newPib.length!=13)&&<p style={{color:"red", display:"inline"}}>Duzina PIB-a mora biti 13 karaktera!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Naziv: </label> <input type="text"class="form-control" onChange={(event)=>setNewNaziv(event.target.value)}/><br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Adresa: </label> <input type="text" class="form-control" onChange={(event)=>setNewAdresa(event.target.value)}/><br/> <br/>
           </div>
           <div class="float-child" style={{width:"10%"}}>
               <h5 style={{color:"white"}}> ____</h5>
            <button class="btn btn-dark"  disabled={((newPib.length==13) && (newNaziv.length>0) && (newAdresa.length>0)) ? false : true} onClick={async ()=>{await CreateCompany(newPib,newNaziv,newAdresa);}}>Dodaj</button>
            </div>
        </div>
    );
}

async function CreateCompany(newPib,newNaziv,newAdresa)
{
    await fetch("https://localhost:44353/Firma/DodajFirmu",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body: JSON.stringify({"pib":newPib,"naziv":newNaziv,"adresa":newAdresa})    
    }).then(p=>{
        if(p.ok){
            console.log("Uspesno dodato!");
        }
    }).catch(exc=>{
        console.log(exc);
    });
    window.location.reload(false);
}

export default FormCreateCompany;