import React from 'react';
import logo from './logo.svg';
import './App.css';
import Food from './ItemComponents/Food';
import MainPage from './SharedComponents/MainPage';
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import Bag from './ItemComponents/Bag';

function App() {

    const [test] = React.useState(
        {
            init: ""
        });

    return (

            <div className="App">

                <MainPage></MainPage>

            </div>

  );
}

export default App;
