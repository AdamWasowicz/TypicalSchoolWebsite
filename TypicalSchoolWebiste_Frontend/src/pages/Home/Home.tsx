import React from 'react';

//Style
import './style.scss';

//Components
import NewsList from '../../components/NewsList';


const Home: React.FunctionComponent = () => {


    return (
        <div className='Home'>
            <div className='HomeContent'>
                <NewsList/>
            </div>
        </div>
    );
};

export default Home;