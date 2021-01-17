import React, {useState, useEffect, ChangeEvent, FormEvent, useContext } from 'react';
import { isArray } from 'util';
import { FoodItem } from '../Classes/FoodItem';
import { NutritionalContent } from '../Classes/NutritionalContent';

import Button from 'react-bootstrap/Button';
import './Food.css';
import { RecentItem, RecentItemsContext } from '../Contexts/RecentItemsContext';
import { AppContext } from '../Contexts/AppContext';


const Food: React.FC<{ nameParam: string, updatedFoodCallback: (i:number) => void }> = ({ nameParam, updatedFoodCallback }) =>
{

    const openButtonText: string = "Open";
    const saveButtonText: string = "Save";

    const [food, setFood] = React.useState(
        {
            name: '',
            fat: '',
            saturatedFat: '',
            sugar: '',
            salt: '',
            id: 0
        }
    );
    const [foodObj, setFoodObj] = React.useState(
        {
            currFood: new FoodItem(0, "", new NutritionalContent(0.0, 0.0, 0.0, 0.0))
        }
    );
    const [name, setName] = React.useState('');
    const [buttonText, setButtonText] = React.useState(openButtonText);

    const recentItems = useContext(RecentItemsContext);
    const appData = useContext(AppContext);


    const getFoodItem = async (newName: string) => {

        setName(newName);

        const defaultFoodItem = new FoodItem(0, "", new NutritionalContent(0.0, 0.0, 0.0, 0.0));

        if (newName.trimEnd().length == 0) {
            updateFood(defaultFoodItem);
            return;
        }

        let getUrl = appData.apiUrl + "Food/" + newName;
        const response = await fetch(
            getUrl
        );
        if (response.status === 200) {

            const jsonItem = await response.json();
            console.log(jsonItem);
            const foodItem: FoodItem = jsonItem;
            updateFood(foodItem);
            setButtonText('Save');

            recentItems.addFoodToHistory(newName);

        }
        else {
            updateFood(defaultFoodItem);
        }

    }

    const updateFood = (foodItem: FoodItem) =>
    {
        setFood({
            name: foodItem.name,
            fat: foodItem.nutrition.fat == 0 ? '' : String(foodItem.nutrition.fat),
            saturatedFat: foodItem.nutrition.saturatedFat == 0 ? '' : String(foodItem.nutrition.saturatedFat),
            sugar: foodItem.nutrition.sugar == 0 ? '' : String(foodItem.nutrition.sugar),
            salt: foodItem.nutrition.salt == 0 ? '' : String(foodItem.nutrition.salt),
            id: foodItem.id
        });
    }

    const postFoodItem = async (foodItem: FoodItem) => {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(foodItem)
        }
        let postUrl = appData.apiUrl + "Food/";
        const response = await fetch(postUrl, requestOptions);
        //const data = response.json();
        //foodItem.id = data.id;

    }

    useEffect(() => {
        getFoodItem(nameParam);
    }, [nameParam] );

    const handleNameChange = (event: ChangeEvent<HTMLInputElement>) => {
        const name = event.target.value;
        setName(name);
    }

    const handleNumericInputChange = (event: ChangeEvent<HTMLInputElement>) => {

        if (isNumeric(event.target.value) || (event.target.value === "")) {
            setFood({ ...food, [event.target.name]: event.target.value });
            //setFoodObj({ ...foodObj, event.target.name: event.target.value });
            let { currFood } = { ...foodObj };   //destructing inner property
            let newFoodObj = currFood.nutrition;
            const { name, value } = event.target;
            //newFoodObj[name] = event.target.value;
            //setFoodObj({ ...foodObj, { nutrition: { [event.target.name]: event.target.value } }});
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
        getFoodItem(name);
    }

    const handleSaveEvent = () => {
        const foodItem: FoodItem = new FoodItem(food.id, food.name,
            new NutritionalContent(Number(food.fat), Number(food.saturatedFat), Number(food.sugar), Number(food.salt)));
        postFoodItem(foodItem);
        updatedFoodCallback(food.id);
    }

    //function setStringField(key:string, value:String) {
    //    setFood(ev => ({
    //        ...ev,
    //        [key]: value,
    //    }))
    //}

    //function setNumberField(key: string, value: String) {
    //    const numValue = Number(value);
    //    setFood(ev => ({
    //        ...ev,
    //        [key]: numValue,
    //    }))
    //}

    return (

            <form onSubmit={ handleSubmit}>
                <div className="food-layout">
                    <label className="food-label">Name</label><input name="name" value={name} onChange={handleNameChange} />
                    <label className="food-label">Fat</label><input name="fat"  type="number" value={food.fat} onChange={e => handleNumericInputChange(e)} />
                    <label className="food-label">Saturated Fat</label><input name="saturatedFat" className=".food-label" type="number" value={food.saturatedFat} onChange={e => handleNumericInputChange(e)}/>
                    <label className="food-label">Sugar</label><input name="sugar" className=".food-label" type="number" value={food.sugar} onChange={e => handleNumericInputChange(e)}/>
                    <label className="food-label">Salt</label><input name="salt" className=".food-label" type="number" value={food.salt} onChange={e => handleNumericInputChange(e)} />
                    <label id="placeholder" /><Button type="submit" className="submit-button">{buttonText}</Button>
                </div>
            </form>

    );

    useEffect( () => console.log("xx"), []);
}

export default Food;