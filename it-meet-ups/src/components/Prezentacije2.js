import React, {useEffect, useState} from 'react'
import useFetch from '../services/useFetch'
import { Link, NavLink } from "react-router-dom";

import Spinner from './Spinner';
import FormCreateEvent from './FormCreateEvent';

function Prezentacije2() {

    const {data, loading, error}=useFetch("PrezentacijePoFirmama/SvePrezentacijePoFirmama");
    const [showForm,setShowForm]=useState(false);
    if (error) throw error;
    if (loading) return <Spinner />;

    if(data===null) return <div></div>
    return (
        <div>

        <div style={{marginTop:"20px", marginBottom:"20px"}} class="d-flex flex-column align-items-center">
        <button class="btn btn-primary" onClick={()=>setShowForm(!showForm)}>Kreiraj prezentaciju</button>
        {showForm && <FormCreateEvent />}
        </div>

            {data.map(prezentacija=>{
                return(
                   <div style={{textAlign:"center", marginTop:"20px"}}>
                        <div class=" card float-child"  style={{margin:"20px", width:"30%"}} key= {prezentacija.naziv_prezentacije}>
                            {prezentacija.naziv_prezentacije}
                            <Link to={`/prezentacija/${prezentacija.naziv_prezentacije}`} className="btn">Saznaj vise</Link>
                        </div>
                    </div>
                    
                )
            })}
        <br/>
        
            
    </div>
    )
}

export default Prezentacije2
