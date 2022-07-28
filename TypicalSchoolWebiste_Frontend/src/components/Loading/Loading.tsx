import React from 'react';

//Style
import './style.scss';


const Loading: React.FunctionComponent = () => {


    return (
        <div className='Loading'>
            <div className="lds-ring">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
    );
};

export default Loading;