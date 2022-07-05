import React from 'react';


//Style
import './style.scss';


//Components
import News from '../../components/News';


const Home: React.FunctionComponent = () => {


    return (
        <div className='Home'>
            <div className='HomeContent'>
                
                <News/>
            </div>
        </div>
    );
};

export default Home;