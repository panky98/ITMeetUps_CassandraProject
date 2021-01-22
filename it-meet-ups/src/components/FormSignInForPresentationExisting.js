import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import {useParams } from "react-router-dom";

function FormSignInForPresentationExisting(){
    const {naziv}=useParams();
    const [username,setUsername]=useState("");
    const [unexistingUsername,setUnexistingUsername]=useState(false);

    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>    
            
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Username: </label> <input class="form-control" type="text" onChange={(event)=>setUsername(event.target.value)}/> {unexistingUsername&&<p style={{color:"red", display:"inline"}}>*Username ne postoji!</p>}<br/>
           </div>
           <div class="float-child" style={{width:"50%"}}>
               <h5 style={{color:"white"}}>_</h5>
            <button class="btn btn-dark " onClick={()=>SignIn(username)}>Prijavi se</button>
            </div> 
        </div>
    )
    
    async function SignIn(username)    {
        setUnexistingUsername(false);
        fetch("https://localhost:44353/User/User/"+username,{
            method:"GET"
        }).then(p=>{
            if(p.ok){
                console.log(p);
                p.json().then(data=>{
                    if(data==false){
                        setUnexistingUsername(true);
                    }
                    else{
                                fetch("https://localhost:44353/PrijavaPoUserima/PrijavaPoUserima",{
                                    method:"POST",
                                    headers:{"Content-Type":"application/json"},
                                    body:JSON.stringify({"username":username,"naziv_prezentacije":naziv})
                                }).then(p=>{
                                    if(p.ok){
                                        window.location.reload(false);
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

export default FormSignInForPresentationExisting;
