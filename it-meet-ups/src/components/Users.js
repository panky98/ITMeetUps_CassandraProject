import React, {useState} from 'react'
import useFetch from '../services/useFetch'
import Spinner from './Spinner';
import User from './User.js'

import KomentariPoUserima from './KomentariPoUserima'
function Users() {


    

    const {data, loading, error}=useFetch("User/User");

    if (error) throw error;
    if (loading) return <Spinner />;

    
    return (
        <div>
            {data.map(user=>{               
                return(
                    <User key={user.username} user={user}/>
                )
            })}
        </div>
    )
}

export default Users
