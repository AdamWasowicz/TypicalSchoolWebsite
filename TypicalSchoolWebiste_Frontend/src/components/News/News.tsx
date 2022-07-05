import React from 'react';


//Style
import './style.scss';


//Components
import NewsElement from './NewsElement';


const News: React.FunctionComponent = () => {


    return (
        <div className='News'>
            <div className='NewsHeader'>
                Aktułalności:
            </div>
                    
             <div className='NewsElementContainer'>
                <NewsElement/>
                <NewsElement/>
                <NewsElement/>
                <NewsElement/>
            </div>

            <div className='PageSelectorContainer'>
                <a>1</a>
                <a>2</a>
                <a>3</a>
            </div>
        </div>
    );
};

export default News;