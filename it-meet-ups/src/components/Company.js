import React, {useState} from 'react'

function Company({company}){
    const[showForm,setShowForm]=useState(false);
    return(
        <div class="float-child card" style={{width:"30%", marginLeft:"20px", marginTop:"20px"}}>
    <div style={{color:"#3399FF"}} class="card-body" className="divCompany">
        <h2 style={{color:"#3399FF"}}> Naziv: {company.naziv}</h2>
        <h4 style={{color:"black"}}>PIB: {company.pib}</h4>
        <h5 style={{color:"black"}}>Adresa: {company.adresa}</h5>
    </div>
        </div>
    );
}
export default Company;