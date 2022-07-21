import React from 'react';
import IPostDTO from '../../../assets/Interfaces/IPostDTO';
import { useNewsElement } from './utils';


//Endpoints
import { getImageByStorageName } from '../../../constants/ApiEndpoints';


//Style
import './style.scss';


const ListNewsElement: React.FunctionComponent<{post: IPostDTO}> = ({post}) => {

    const { formatDate } = useNewsElement();

    const imageSource = process.env.REACT_APP_API_URL + getImageByStorageName + post.imageStorageName


    return (
        <div className='NewsElement'>
            <div className='LeftPart'>
                <img className='NewsImage' src={imageSource}/>
            </div>

            <div className='RightPart'>
                <div className='Title'>{post.title}</div>
                <div className='TextContent'>{post.textContent}</div>
            </div>

            <div className='Date'>{formatDate(new Date(post.lastEditDate))}</div>
        </div>
    );
};

export default ListNewsElement;