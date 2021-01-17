import React, { useState, useEffect, ChangeEvent, FormEvent, useContext } from 'react';
import { isArray } from 'util';
import { FoodItem } from '../Classes/FoodItem';
import { NutritionalContent } from '../Classes/NutritionalContent';


import Button from 'react-bootstrap/Button';
import './Bag.css';
import 'bootstrap-css-only/css/bootstrap.min.css';

import { BagItem } from '../Classes/BagItem';
import { BagItemNode } from '../Classes/BagItemNode';
import { FoodItemNode } from '../Classes/FoodItemNode';
import Food from './Food';
import { Console } from 'console';
import { RecentItem, RecentItemsContext } from '../Contexts/RecentItemsContext';
import { AppContext } from '../Contexts/AppContext';

const Bag: React.FC<{nameParam: string}> = ({ nameParam }) => {

    const openButtonText: string = "Open";
    const saveButtonText: string = "Save";

    const [bag, setBag] = React.useState(
        {
            id: '0',
            name: '',
            item: new BagItem(0, "", [], []),
            foodNodes: new Array<FoodItemNode>(),
            bagNodes: new Array<BagItemNode>()
        }
    );
    const [modified, setModified] = React.useState(false);
    const [name, setName] = React.useState('');
    const [buttonText, setButtonText] = React.useState(openButtonText);
    const [selectedFood, setSelectedFood] = React.useState('');

    const appData = useContext(AppContext);

    const getItem = async (name: string) => {

        const defaultItem = new BagItem(0, bag.name, new Array<BagItemNode>(), new Array<FoodItemNode>());

        if ((name == null) || (name.trimEnd().length == 0)) {
            updateItem(defaultItem);
            return;
        }

        let getUrl = appData.apiUrl + "Bag/" + name;
        const response = await fetch(
            getUrl
        );
        if (response.status === 200) {

            const jsonItem = await response.json();
            console.log(jsonItem);
            const item: BagItem = jsonItem;
            if (item.foods[0].food == null) {
                console.log(item);
            }
            if (item.foods[1].food == null) {
                console.log(item);
            }
            updateItem(item);
            setButtonText('Save');

        }
        else {
            updateItem(defaultItem);
        }

    }

    const updateItem = (updateItem: BagItem) => {
        setBag({
            id: updateItem.id.toString(),
            name: updateItem.name,
            item: updateItem,
            foodNodes: updateItem.foods,
            bagNodes: updateItem.bags
        });
    }

    const postItem = async (item: BagItem) => {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(item)
        }
        let postUrl = appData.apiUrl + "Bag/";
        const response = await fetch(postUrl, requestOptions);
        //const data = response.json();
        //foodItem.id = data.id;

    }

    useEffect(() => {
        getItem(nameParam);
    }, [nameParam]);

    const handleNameChange = (event: ChangeEvent<HTMLInputElement>) => {
        const name = event.target.value;
        setName(name);
    }

    const handleNumericInputChange = (event: ChangeEvent<HTMLInputElement>) => {

        if (isNumeric(event.target.value) || (event.target.value === "")) {
            setBag({ ...bag, [event.target.name]: event.target.value });
        }

    }

    function isNumeric(val: any): val is number | string {
        // parseFloat NaNs numeric-cast false positives (null|true|false|"")
        // ...but misinterprets leading-number strings, particularly hex literals ("0x...")
        // subtraction forces infinities to NaN
        // adding 1 corrects loss of precision from parseFloat (#15100)
        return !isArray(val) && (val - parseFloat(val) + 1) >= 0;
    }

    const handleSubmit = (event: FormEvent) => {

        event.preventDefault();

        if (buttonText === openButtonText) {
            handleOpenEvent();
        } else {
            handleSaveEvent();
        }
    }

    const handleOpenEvent = () => {
        getItem(name);
    }

    const handleSaveEvent = () => {
        const bagItem: BagItem = new BagItem(Number(bag.id), bag.name, bag.bagNodes, bag.foodNodes);
        postItem(bagItem);
    }

    const handleFoodUpdate = (foodId: number) => {
        setModified( true );
    }

    const handleFoodSelected = (foodName: string) => {
        setSelectedFood(foodName);
    }

    const handleFoodQtyChanged = (node: FoodItemNode, e: React.ChangeEvent<HTMLSelectElement>) => {

        let newQty: number = Number(e.target.value);
        setBag({
            id: bag.id,
            name: bag.name,
            item: bag.item,
            bagNodes: bag.bagNodes,
            foodNodes: bag.foodNodes.map((currNode) => {

                if (currNode.food.id == node.food.id) {
                    currNode.quantity = newQty;
                }
                return currNode;
            })
        });
    }

    const handleBagQtyChanged = (node: BagItemNode, e: React.ChangeEvent<HTMLSelectElement>) => {

        let newQty: number = Number(e.target.value);
        setBag({
            id: bag.id,
            name: bag.name,
            item: bag.item,
            foodNodes: bag.foodNodes,
            bagNodes: bag.bagNodes.map((currNode) => {

                if (currNode.bag.id == node.bag.id) {
                    currNode.quantity = newQty;
                }
                return currNode;
            })
        });
    }

    return (
        <div>

            <form onSubmit={handleSubmit}>
                <div className="bag-layout">
                    <label className="bag-label">Name</label>
                    <input name="name" value={name} onChange={handleNameChange} />

                    <label />
                    <label>Bag Items</label>

                    {bag.bagNodes.map((i) => (
                        <React.Fragment key={i.bag.id}>
                            <label  />
                            <div  className="node-list">
                                <select 
                                    value={i.quantity}
                                    onChange={(e: React.ChangeEvent<HTMLSelectElement>) => {
                                        console.log("handleFoodQtyChanged");
                                        handleBagQtyChanged(i, e)
                                    }
                                    }>
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                                <Button variant="outline-secondary" size="sm" active > {i.bag.name}</Button>
                            </div>
                        </React.Fragment>))}

                    <label />
                    <label>Food Items</label>

                    {bag.foodNodes.map((i) => (
                        <React.Fragment key={bag.id + "_" + i.food.id.toString()}>
                            <label  />
                            <div className="node-list">
                                <select
                                    value={i.quantity}
                                    onChange={(e: React.ChangeEvent<HTMLSelectElement>) => {
                                            console.log("handleFoodQtyChanged");
                                            handleFoodQtyChanged(i, e)
                                        }
                                    }>
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                                <Button variant="outline-secondary" size="sm"
                                    active onClick={() => handleFoodSelected(i.food.name)}> {i.food.name}</Button>
                            </div>
                        </React.Fragment>))}
                    <label>{modified == true ? "modified" : ""}</label>
                    <label id="placeholder" /><Button type="submit" className="submit-button">{buttonText}</Button>
                </div>
            </form>

            <div className="selected-food">
                <div className="item1"/>
                <div className="item2">
                    {selectedFood.trim() != "" &&
                        <Food nameParam={selectedFood}
                            updatedFoodCallback={(id: number) => { handleFoodUpdate(id) }}>
                        </Food>}
                </div>
            </div>

        </div>
    );

    //useEffect(() => console.log("xx"), []);
}

export default Bag;