import React from 'react';
import { useState } from 'react';

export const useNavBar = () => {
    //State
    const [navBarVisibility, setNavBarVisibility] = useState(false);

    const handleOnClick = () => {
        setNavBarVisibility(!navBarVisibility);
    }

    return {navBarVisibility, handleOnClick};
};