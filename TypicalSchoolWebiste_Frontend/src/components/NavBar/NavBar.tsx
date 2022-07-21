import React from 'react';
import { useNavBar } from './utils';

//Style
import './style.scss';
import { faBars } from '@fortawesome/free-solid-svg-icons';

//Interfaces
import INavBarElement from '../../assets/Interfaces/INavBarElement';

//Components
import NavBarElement from './NavBarElement';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';


const NavBar: React.FunctionComponent<{elements: Array<INavBarElement>}> = ({elements}) => {
    const {navBarVisibility, handleOnClick} = useNavBar();

    return (
        <div className='NavBar'>
            <div className='barsContainer'>
                <FontAwesomeIcon icon={faBars} onClick={handleOnClick} />
            </div>
            
            <div 
                className={navBarVisibility ? 'NavBarElementContainer' : 'NavBarElementContainer-hidden'}>
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