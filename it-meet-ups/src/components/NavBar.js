import React from 'react'

import { Link, NavLink } from "react-router-dom";

function NavBar() {
    return (

      <nav class="navbar navbar-light bg-light">
        <ul>
          <li class="navbar-brand">
            <Link to="/">
                ITMeetUps
            </Link>
          </li>
          <li class="navbar-brand">
            <NavLink to="/users">
              Users
            </NavLink>
          </li>
          <li class="navbar-brand">
            <NavLink to="/Prezentacije">
              Prezentacije
            </NavLink>
          </li>
          <li class="navbar-brand"> 
            <NavLink to="/companies">
              Firme
            </NavLink>
          </li>
        </ul>
      </nav>

    )
}

export default NavBar
