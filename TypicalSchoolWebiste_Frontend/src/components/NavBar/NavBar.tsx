import React from 'react';
import { useState } from 'react';


//Style
import './style.scss';


//Interfaces
import INavBarElement from '../../assets/Interfaces/INavBarElement';

//Components
import NavBarElement from './NavBarElement';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { useNavBar } from './utils';



const NavBar: React.FunctionComponent<{elements: Array<INavBarElement>}> = ({elements}) => {
    const {navBarVisibility, handleOnClick} = useNavBar();



    return (
        <div className='NavBar'>
            <div className='barsContainer'>
                <FontAwesomeIcon icon={faBars} onClick={handleOnClick} />
            </div>
            
            <div className={!navBarVisibility ? 'NavBarElementContainer' : 'NavBarElementContainer-hidden'}>
                {
                    elements.map((element, index) => {
                        return <NavBarElement key={index} element={element}/>
                    })
                }
            </div>
        </div>
    );
};

export default NavBar;