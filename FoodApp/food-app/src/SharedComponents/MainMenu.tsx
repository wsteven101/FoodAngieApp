import * as React from 'react';
import { Button, FormControl } from 'react-bootstrap';
import Form from 'react-bootstrap/esm/Form';
import Nav from 'react-bootstrap/esm/Nav';
import NavDropdown from 'react-bootstrap/esm/NavDropdown';
import Navbar from 'react-bootstrap/Navbar';
import Bag from '../ItemComponents/Bag';
import Food from '../ItemComponents/Food';

import { NavLink, Route, useHistory } from "react-router-dom";
import { RecentItem, RecentItemsContext } from '../Contexts/RecentItemsContext';

const MainMenu: React.FC = () => {

    const history = useHistory();

    const recentItems = React.useContext(RecentItemsContext);

    return (
        <div>

                <Navbar bg="light" expand="lg">
                <Navbar.Brand href="#home">Food Helper App</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="mr-auto">

                        <Nav.Link href="/food">Home</Nav.Link>

                        <NavDropdown title="Recent Items" id="basic-nav-dropdown">

                                {recentItems.itemHistory.map((x, idx) => (
                                    <React.Fragment>
                                        <NavDropdown.Item key={idx} onClick={
                                            () => {
                                                history.push(x.link)
                                            }
                                        } > {x.name + " " + idx.toString()}</NavDropdown.Item>
                                    </React.Fragment>
                                ))}

                        </NavDropdown>

                        <NavDropdown title="Items" id="basic-nav-dropdown">
                            <NavDropdown.Item onClick={()=>history.push("/bag")}>Bag</NavDropdown.Item>
                            <NavDropdown.Item onClick={() => history.push("/food")}>Food</NavDropdown.Item>
                            <NavDropdown.Item href="/food">Food (href)</NavDropdown.Item>
                        </NavDropdown>

                    </Nav>

                        <Form>
                            <Form.Group controlId="exampleForm.SelectCustom">
                                <Form.Label>Custom select</Form.Label>
                                <Form.Control as="select" custom>
                                    <option>abc</option>
                                    <option>acbd</option>
                                    <option>acad</option>
                                    <option>4</option>
                                    <option>5</option>
                                </Form.Control>
                            </Form.Group>
                            <Button variant="outline-success">Search</Button>
                        </Form>
                    </Navbar.Collapse>
                </Navbar>

        </div>
    );

}

export default MainMenu;