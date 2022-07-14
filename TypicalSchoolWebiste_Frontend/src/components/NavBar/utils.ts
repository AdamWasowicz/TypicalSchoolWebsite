import { useState} from 'react';



export const useNavBar = () => {
    //State
    const [navBarVisibility, setNavBarVisibility] = useState(false);


    //Handlers
    const handleOnClick = () => {
        setNavBarVisibility(!navBarVisibility);
    }


    return {navBarVisibility, handleOnClick};
};