import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import {useParams } from "react-router-dom";



function FormSignInForPresentation(){
    const [newUsername, setNewUsername]=useState("");
    const [newName, setNewName]=useState("");
    const [newSurname, setNewSurname]=useState("");
    const [newInterests,setNewInterest]=useState("");
    const [existingUsername,setExistingUsername]=useState(false);
    const {naziv}=useParams();

    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>    
            
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Username: </label> <input type="text" class="form-control" onChange={(event)=>setNewUsername(event.target.value)}/> {(newUsername.length<5)&&<p style={{color:"red", display:"inline"}}>*Minimalna duzina username je 5 karaktera!</p>}{existingUsername&&<p style={{color:"red", display:"inline"}}>*Username je zauzet!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Ime: </label> <input type="text" class="form-control" onChange={(event)=>setNewName(event.target.value)}/>{(newName.length==0)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Prezime: </label> <input type="text" class="form-control" onChange={(event)=>setNewSurname(event.target.value)}/> {(newSurname.length==0)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Interesovanja: </label> <input type="text" class="form-control" onChange={(event)=>setNewInterest(event.target.value)}/><p style={{color:"black", display:"inline"}}>Unesite pojmove sa razmakom izmedju!</p><br/> <br/>
           </div>
           <div class="float-child" style={{widht:"50%"}}>
            <button class="btn btn-dark " disabled={((newUsername.length>5) && (newName.length>0) && (newSurname.length>0)) ? false : true} onClick={async ()=>{await CreateUserAndSignIn(newUsername,newName,newSurname,newInterests);}}>Prijavi se</button>
        </div>
        </div>
    );

    async function CreateUserAndSignIn(newUsername,newName,newSurname,newInterests)
    {
    setExistingUsername(false);
    fetch("https://localhost:44353/User/User/"+newUsername,{
        method:"GET"
    }).then(p=>{
        if(p.ok){
            console.log(p);
            p.json().then(data=>{
                if(data!=false){
                    setExistingUsername(true);
                }
                else{
                    fetch("https://localhost:44353/User/User/",{
                        method:"POST",
                        headers:{"Content-Type":"application/json"},
                        body: JSON.stringify({"username":newUsername,"ime":newName,"prezime":newSurname,"interesovanja":newInterests.split(" ")})
                    }).then(p=>{
                        if(p.ok){
                            fetch("https://localhost:44353/PrijavaPoUserima/PrijavaPoUserima",{
                                method:"POST",
                                headers:{"Content-Type":"application/json"},
                                body:JSON.stringify({"username":newUsername,"naziv_prezentacije":naziv})
                            }).then(p=>{
                                if(p.ok){
                                    window.location.reload(false);
                                }
                            }).catch(ex=>{
                                console.log(ex);
                            })
                        }
                    }).catch(ex=>{
                        console.log(ex);
                    })
                }
            });
        }
    }).catch(ex=>{
        console.log(ex);
    });
    }
}



export default FormSignInForPresentation;