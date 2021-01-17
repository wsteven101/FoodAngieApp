import React from "react";

export const AppContext = React.createContext({

    apiUrl: window.location.origin + "/api/"

});