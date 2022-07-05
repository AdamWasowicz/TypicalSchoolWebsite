import React from 'react';


//Style
import './style.scss';


//Components



const NewsElement: React.FunctionComponent = () => {


    return (
        <div className='NewsElement'>
            <div className='LeftPart'>
                <img className='NewsImage' src='https://c.tenor.com/n4tkp-dw89IAAAAC/walk-polar-bear.gif'/>
            </div>

            <div className='RightPart'>
                <div className='Title'>Very Very Very Very Very Very Long Title</div>
                <div className='TextContent'>TextContent TextContentText ContentTextContent TextCont entTex ContentTextCont entTextC ontent</div>
            </div>

            <div className='Date'>
                05.07.2022
            </div>
        </div>
    );
};

export default NewsElement;