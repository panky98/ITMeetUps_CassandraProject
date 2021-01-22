import React, {useState} from 'react'
import FormSignInForPresentation from "./FormSignInForPresentation"
import FormSignInForPresentationExisting from "./FormSignInForPresentationExisting"
import {useParams } from "react-router-dom";


function PreFormSignInForPresentation(){
    const[createAccount,setCreateAccount]=useState(false);
    const[existingAccount,setExistingAccount]=useState(false);
    const {naziv}=useParams();

    return(
        <div>
            <h2>  </h2>
            <button class="btn btn-info" onClick={()=>{setCreateAccount(true);setExistingAccount(false)}}>Create account</button> <button class="btn btn-info" onClick={()=>{setCreateAccount(false);setExistingAccount(true)}}>Already have account</button>
            {createAccount && <FormSignInForPresentation nazivPrezentacije={naziv}/>}
            {existingAccount && <FormSignInForPresentationExisting nazivPrezentacije={naziv}/>}
            <h2>  </h2>
        </div>
    )
}

export default PreFormSignInForPresentation;