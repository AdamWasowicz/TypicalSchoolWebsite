import React from 'react';

//Resources
import sealImage from './img/seal.jpg';


const Error404: React.FunctionComponent = () => {
    return (
        <div className='Error404'>
            Error404
            <img src={sealImage}/>
        </div>
    );
};

export default Error404;