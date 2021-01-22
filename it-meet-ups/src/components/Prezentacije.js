import React, {useEffect} from 'react'
import useFetch from '../services/useFetch'

import Spinner from './Spinner';

class Prez{
    constructor(naziv_prezentacije, datum, interesovanja, predavac)
    {
        this.naziv_prezentacije=naziv_prezentacije;
        this.datum=datum;
        this.interesovanja=interesovanja;
        this.predavac=predavac;
    }
}





function urediPrezentacije(data){
    
   
    let pom=new Array();

    console.log(pom);

    console.log(data);
    let postojecaPrezIndex;
    data.forEach(p=>{
        postojecaPrezIndex=pom.findIndex(x=>x.naziv_prezentacije===p.naziv_prezentacije);
       
        
        if(postojecaPrezIndex!==-1)
        {
            
            console.log("Postoji prez...sad je pom: ")
            pom[postojecaPrezIndex]={...pom[postojecaPrezIndex],
                 interesovanja:[...pom[postojecaPrezIndex].interesovanja,p.interesovanje ]};

            console.log(pom);
        }
        else{
           const novaPrez={
            naziv_prezentacije:p.naziv_prezentacije,
            datum:p.datum,
            interesovanja:p.interesovanje,
            predavac:p.predavac
            };

            console.log("Ne postoji prez...pom:")
        
            pom.push(novaPrez);
            console.log(pom);
        }
        
            
    });

    return pom;
}

function Prezentacije() {
    const {data, loading, error}=useFetch("Prezentacija/Prezentacije");

    

    

    if (error) throw error;
    if (loading) return <Spinner />;

    const prezentacije=urediPrezentacije(data);

    console.log(prezentacije);
    return (
        <div>
            {prezentacije.map(p=>{
                console.log(p);
                return(
                <div key={p.naziv_prezentacije}>
                    <p>{p.naziv_prezentacije}</p>
                    <p>{p.predavac}</p>
                    <p>{p.datum}</p>
                    <div>
                        {/*p.interesovanja.map(i=>{
                            console.log("int" + i);
                            return (
                                <div key={i}>
                                    <p>{i}</p>
                                </div>
                            )
                        })*/}
                    </div>
                </div>)
            })}
        </div>
    )
}

export default Prezentacije
