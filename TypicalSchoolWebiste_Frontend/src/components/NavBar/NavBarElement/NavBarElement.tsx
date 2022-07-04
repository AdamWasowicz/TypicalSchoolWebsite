import React from 'react';

//Style
import './style.scss';

//Components
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

//Interfaces
import INavBarElement from '../../../assets/Interfaces/INavBarElement';


const NavBarElement: React.FunctionComponent<{element: INavBarElement}> = ( {element} ) => {

    
    return (
        <div className='NavBarElement'>
            <div className='NavBarElementContainer' onClick={element.onClickHandler}>
                <div className='NavBarElementIcon'>
                    <FontAwesomeIcon icon={element.iconName} />
                </div>

                <div className='NavBarElementText'>
                    {element.textContent}
                </div>
            </div> 
        </div>
    );
};

export default NavBarElement;