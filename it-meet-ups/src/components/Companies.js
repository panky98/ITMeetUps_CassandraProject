import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import Company from "./Company.js"
import Spinner from './Spinner';
import FormCreateCompany from './FormCreateCompany';



function Companies() {
    const {data, loading, error}=useFetch("Firma/Firme");
    const[showForm,setShowForm]=useState(false);


    if (error) throw error;
    if (loading) return <Spinner />;

    return(
        
        <div style={{textAlign:"center", marginTop:"20px"}}>
            <div style={{marginBottom:"20px"}}>
                <button class="btn btn-primary" onClick={()=>setShowForm(!showForm)}>Create new company!</button><br/>
                {showForm && <FormCreateCompany />}
            </div>

            {data.map(company=>{               
                return(
                    <Company company={company}/>
                )
            })}
            
            
        </div>
        
    );
}

export default Companies;