import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import {useParams } from "react-router-dom";



function FormCreateEvent(){
    const [firma,setFirma]=useState("");
    const [naziv,setNaziv]=useState("");
    const [predavac,setPredavac]=useState("");
    const [interesovanja,setInteresovanja]=useState("");
    const [datum,setDatum]=useState("");
    const [exsistingFirma,setExistingFirma]=useState(false);
    return (
        <div class="float-container" style={{textAlign:"left"}} className={"formCreate"}>
            <h1> </h1>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>PIB firme:</label><input class="form-control" type="text" onChange={(event)=>setFirma(event.target.value)}/>{(firma.length==0)&&<p style={{color:"red", display:"inline"}}>*Obavezno polje</p>}{exsistingFirma&&<p style={{color:"red", display:"inline"}}>*Firma ne postoji!</p>}<br/>    
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Naziv prezentacije:</label><input class="form-control" type="text" onChange={(event)=>setNaziv(event.target.value)}/>{(naziv.length==0)&&<p style={{color:"red", display:"inline"}}>*Obavezno polje</p>}<br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Predavac:</label><input class="form-control" type="text" onChange={(event)=>setPredavac(event.target.value)}/>{(predavac.length==0)&&<p style={{color:"red", display:"inline"}}>*Obavezno polje</p>}<br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Interesovanja:</label><input class="form-control" type="text" onChange={(event)=>setInteresovanja(event.target.value)}/><p style={{color:"black", display:"inline"}}>*Unesite kljucne reci sa razmakom izmedju svake dve</p><br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
            <label style={{color:"#3399FF"}}>Datum:</label><input class="form-control" type="text" onChange={(event)=>setDatum(event.target.value)}/>{(datum.length==0)&&<p style={{color:"red", display:"inline"}}>*Obavezno polje</p>}<p style={{display:"inline"}}> FORMAT DATUMA:'2017-05-05'</p><br/>
            </div>
            <div class="float-child" style={{width:"30%"}}>
                <h5 style={{color:"white"}}> _</h5>
            <button class="btn btn-primary" disabled={((naziv.length>5) && (predavac.length>0) && (datum.length>0)) ? false : true} onClick={async ()=>{await CreateEvent();}}>Prijavi se</button>
            </div>
        </div>
    )

    async function CreateEvent()     {
        setExistingFirma(false);
        fetch("https://localhost:44353/Firma/Firma/"+firma,{
            method:"GET"
        }).then(p=>{
            if(p.ok){
                console.log(p);
                p.json().then(async data=>{
                    if(data==false){
                        setExistingFirma(true);
                    }
                    else{
                        let splitString=interesovanja.split(" ");
                        for(let i=0;i<splitString.length;i++){
                            await fetch("https://localhost:44353/Prezentacija/DodajPrezentaciju/",{
                                method:"POST",
                                headers:{"Content-Type":"application/json"},
                                body:JSON.stringify({"naziv_prezentacije":naziv,"datum":datum,"interesovanje":splitString[i],"predavac":predavac})
                            }).then(odgvoro=>{
                                if(odgvoro.ok){
                                    console.log(odgvoro);
                                    if(i==splitString.length-1){
                                        fetch("https://localhost:44353/PredavaciPoPrezentacijama/DodajPredavacaPoPrezentaciji/",{
                                            method:"POST",
                                            headers:{"Content-Type":"application/json"},
                                            body:JSON.stringify({"predavac":predavac,"prezentacija":naziv})
                                        }).then(p=>{
                                            if(p.ok){
                                                fetch("https://localhost:44353/PrezentacijePoFirmama/DodajPrezentacijuZaFirmu/",{
                                                    method:"POST",
                                                    headers:{"Content-Type":"application/json"},
                                                    body:JSON.stringify({"pib":firma,"naziv_prezentacije":naziv})
                                                }).then(p=>{
                                                    if(p.ok){
                                                        window.location.reload(false);
                                                    }
                                                })
                                            }
                                        }).catch(ex=>{
                                            console.log(ex);
                                        })
                                    }
                                }
                            }).catch(ex=>{
                                console.log(ex);
                            })
                        }
                    }
                });
            }
        }).catch(ex=>{
            console.log(ex);
        });
        }
}

export default FormCreateEvent;