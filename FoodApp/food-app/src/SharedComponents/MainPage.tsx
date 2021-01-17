import * as React from 'react';
import { Button, FormControl } from 'react-bootstrap';
import Form from 'react-bootstrap/esm/Form';
import Nav from 'react-bootstrap/esm/Nav';
import NavDropdown from 'react-bootstrap/esm/NavDropdown';
import Navbar from 'react-bootstrap/Navbar';
import Bag from '../ItemComponents/Bag';
import Food from '../ItemComponents/Food';

import { NavLink, Switch, Route, BrowserRouter as Router, useHistory, Link } from "react-router-dom";
import { RecentItem, RecentItemsContext } from '../Contexts/RecentItemsContext';
import MainMenu from './MainMenu';


const MainPage: React.FC = () => {

    const history = useHistory();

    const addFoodItemToHistory = (name: string) => {

        console.log("MainPage.ts: RecentItemsContext: addFoodItemToHistory");

        const newItem = new RecentItem( name, "/food/" + name );
        const newHist = itemHistState.itemHistory.concat( newItem );

        setItemHistState(prevState => {
            return {
                ...prevState,
                itemHistory: prevState.itemHistory.concat(newItem)
            }
        });

    }

    const [itemHistState, setItemHistState]  = React.useState({

        itemHistory: new Array<RecentItem>(),
        addFoodToHistory: addFoodItemToHistory,
        addBagToHistory: () => { }

    });


    return (

    <Router>
        <div>        

            <RecentItemsContext.Provider value={itemHistState}>

                <MainMenu></MainMenu>

                <Switch>
                        <Route path="/food/:foodName" render={(props) => <Food nameParam={props.match.params.foodName}
                            updatedFoodCallback={(i: number) => { }}></Food>} ></Route>

                        <Route exact path="/food" render={(props) => <Food nameParam="" updatedFoodCallback={(i: number) => { }}></Food>} ></Route>

                        <Route path="/bag/:bagName" render={(props) => <Bag nameParam={props.match.params.bagName}></Bag>} ></Route>

                        <Route exact path="/bag" render={(props) => <Bag nameParam={props.match.params.bagName}></Bag>} ></Route>

                        <Route exact path="/" render={(props) => <Food nameParam=""
                            updatedFoodCallback={(i: number) => { }}></Food>} ></Route>

                    </Switch>

             </RecentItemsContext.Provider>

         </div>
     </Router>
    );

}

export default MainPage;