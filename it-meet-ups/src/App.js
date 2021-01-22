import React from 'react';
// react router
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';

import Home from './components/Home.js'
import Prezentacije from './components/Prezentacije.js';
import Prezentacije2 from './components/Prezentacije2.js';
import Error from './components/Error.js';
import Users from './components/Users.js';
import NavBar from './components/NavBar'
import Prezentacija from './components/Prezentacija';
import Companies from './components/Companies';
function App() {
  return (
    <Router>
        <NavBar/>
        <Switch>
          <Route exact path="/">
            <Home/>
          </Route>

          <Route path="/prezentacije">
            <Prezentacije2/>
          </Route>

          <Route exact path="/prezentacija/:naziv" component={Prezentacija}></Route>

          <Route path="/users">
            <Users/>
          </Route>

          <Route path="/companies">
            <Companies/>
          </Route>

          <Route  path="*">
            <Error />
          </Route>
        </Switch>
    </Router>
  );
}

export default App;
