import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import Spinner from './Spinner';

function KomentariPoUserima({username}) {

    const {data, loading, error}=useFetch("Komentar/KomentarUsername/"+username);

    if (error) throw error;
    if (loading) return <Spinner />;

    console.log(data);
    
    return(
        
        <div className="divKomentar" class="  d-flex flex-column align-items-center">
            {data.map(komentar=>{
                return(
                    <div key={komentar.komentar}>
                        <h5> </h5>
                        <label style={{color:"#3399FF"}}>Komentar: </label> <label>{komentar.komentar}</label>
                        <p>Naziv prezentacije: {komentar.nazivPrezentacije}</p>
                        <p>Datum: {komentar.datum}</p>
                        <h5> </h5>
                    </div>
                )
            })}
        </div>
        
    )
}

export default KomentariPoUserima
