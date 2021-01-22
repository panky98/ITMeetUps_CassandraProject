import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import Spinner from './Spinner';

function PrijavaPoUserima({username}) {

    const {data, loading, error}=useFetch("PrijavaPoUserima/PrijavaPoUserima/"+username);

    if (error) throw error;
    if (loading) return <Spinner />;

    console.log(data);
    return (
        <div>
            {data.map(prez=>{
                return(
                    <div key={prez.naziv_prezentacije}>
                        <h5> </h5>
                    {prez.naziv_prezentacije}
                    <h5> </h5>
                    </div>
                )
            })}
        </div>
    )
}

export default PrijavaPoUserima
