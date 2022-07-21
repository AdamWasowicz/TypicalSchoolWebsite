import React from 'react';


//Style
import './style.scss';


//Components
import ListNewsElement from './NewsListElement';

//Other
import { useNews } from './utils';

//Redux
import { useAppSelector } from '../../redux/hooks'

//Interfaces
import IPostDTO from '../../assets/Interfaces/IPostDTO';


const News: React.FunctionComponent = () => {

    const { isLoading, handlePageClick, CreatePagesArray } = useNews();
    const maxPage = useAppSelector((state) => state.maxPage.value);
    const posts: Array<IPostDTO> = useAppSelector((state) => state.posts.value);
    const page =  useAppSelector((state) => state.page.value);


    return (
        <div className='News'>
            <div className='NewsHeader'>
                Aktułalności: {process.env.REACT_APP_API_URL}
            </div>
                    
            <div className='NewsElementContainer'>
            {
                isLoading
                ? <div>Loading...</div>
                : posts.map((element, index) => {
                    return <ListNewsElement post={element} key={index}/>
                })
            }
            </div>

            <div className='PageSelectorContainer'>
                {
                    [...CreatePagesArray(page, maxPage)].map((e, i) => {
                        return <a key={i} className={e == page ? 'page-current' : 'page'} onClick={handlePageClick}>{e}</a>
                    })
                }
            </div>
        </div>
    );
};

export default News;