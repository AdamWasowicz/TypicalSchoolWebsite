import React from 'react';

//Style
import './style.scss';


const NewsList_NoContentError = () => {


    return (
        <div className='NewsList_NoContentError'>
            <div className='NewsList_NoContentError_Container'>
                <h4>Nie udało się pobrać zawartości</h4>
                <p>Spróbuj ponownie za jakiś czas...</p>
            </div>
        </div>
    );
}

export default NewsList_NoContentError;