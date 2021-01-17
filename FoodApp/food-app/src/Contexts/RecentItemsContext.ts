import React from "react";

export class RecentItem {
    constructor(public name: string, public link: string) { }
} 

export const RecentItemsContext = React.createContext({

    itemHistory: new Array<RecentItem>(),
    addFoodToHistory: (name: string) => { console.log("Default called. Define RecentItemsContext: addFoodToHistory");  },
    addBagToHistory: () => { }

});