import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import {useParams } from "react-router-dom";

function FormLeaveCommentExisting(){
    const {naziv}=useParams();
    const [username,setUsername]=useState("");
    const [unexistingUsername,setUnexistingUsername]=useState(false);
    const [textComment,setTextComment]=useState("");
    const [numberOfStarts,setNumberOfStars]=useState(1);

    return(
        <div className={"formCreate"} class="float-container" style={{textAlign:"left"}}>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Username: </label> <input type="text" class="form-control" onChange={(event)=>setUsername(event.target.value)}/> {unexistingUsername&&<p style={{color:"red", display:"inline"}}>*Username ne postoji!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Komentar: </label> <input type="text" class="form-control" onChange={(event)=>setTextComment(event.target.value)}/>{(textComment.length<6)&&<p style={{color:"red", display:"inline"}}>*Ovo polje je obavezno! Minumum 6 karaktera!</p>}<br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
            <label style={{color:"#3399FF"}}>Broj zvezdica:</label> <input type="number" class="form-control" maxLength="1" onChange={(event)=>{if(parseInt(event.target.value)<=5 && parseInt(event.target.value)>=1){setNumberOfStars(event.target.value)}}}/><br/>
            </div>
            <div class="float-child" style={{width:"50%"}}>
                <h5 style={{color:"white"}}>_</h5>
            <button class="btn btn-dark " disabled={textComment.length>6?false:true} onClick={()=>SignIn(username)}>Prijavi se</button>   
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
                        var today = new Date();
                        var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
                                fetch("https://localhost:44353/Komentar/DodajKomentar",{
                                    method:"POST",
                                    headers:{"Content-Type":"application/json"},
                                    body:JSON.stringify({"username":username,"nazivPrezentacije":naziv,"komentar":textComment,"brojZvezdica":parseInt(numberOfStarts),"datum":date})
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

export default FormLeaveCommentExisting;
