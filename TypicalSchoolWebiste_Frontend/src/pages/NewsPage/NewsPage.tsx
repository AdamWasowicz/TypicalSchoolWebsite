import React from 'react';
import { useParams } from 'react-router';

//Style
import './style.scss';


const NewsPage: React.FunctionComponent = () => {

    const { newsName } = useParams();
    console.log(newsName);


    return (
        <div className='NewsPage'>
            NewsPage
        </div>
    );
};

export default NewsPage;