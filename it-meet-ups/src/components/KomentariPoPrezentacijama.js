import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import Spinner from './Spinner';
function KomentariPoPrezentacijama({nazivPrezentacije}) {

    const {data, loading, error}=useFetch("Komentar/KomentarPrezentacija/"+nazivPrezentacije);

    
    if (error) throw error;
    if (loading) return <Spinner />;

    console.log(data);


    
    return (
        <div className="divKomentariPoPrez">
            {data.map(komentar=>{
                return(
                    <div key={komentar.komentar + " " + komentar.datum+" " + komentar.username}>
                        <p>{komentar.username}: {komentar.komentar}</p>
                        <p>Ocena: {komentar.brojZvezdica}/5</p>
                        <p>Datum komentara: {komentar.datum}</p>
                    </div>
                )
            })}
        </div>
    )
}

export default KomentariPoPrezentacijama
