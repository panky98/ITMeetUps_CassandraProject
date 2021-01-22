import React, {useEffect,useState} from 'react'
import useFetch from '../services/useFetch'

import {useParams } from "react-router-dom";

function ListaPrijavljenih()
{
    let prom=false;
    const {naziv}=useParams();
    const {data, loading, error}=useFetch("PrijavaPoPrezentacijama/PrijavaPoPrezentacijama/"+naziv);

    return(
        <div>
            <h3></h3>
        <ul>
            {(data!=null && data!=undefined) ? data.map((element)=>{
                return (<li>{element["username"]}</li>)
            })
        :<li></li>}
        </ul>
        <h3></h3>
        </div>
    )
}
export default ListaPrijavljenih;