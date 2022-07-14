import React from 'react';


//Style
import './style.scss';


//Components
import NewsElement from './NewsElement';


//Other
import { useNews } from './utils';


//Redux
import { useSelector } from 'react-redux';
import { useAppDispatch, useAppSelector } from '../../redux/hooks'


const News: React.FunctionComponent = () => {

    //Const
    const maxDisplayedAmountOfPages = 1;


    const { isLoading, createArrayInRange, handlePageClick, CreatePagesArray } = useNews();
    const maxPage = useAppSelector((state) => state.maxPage.value);
    const posts = useAppSelector((state) => state.posts.value);
    const page =  useAppSelector((state) => state.page.value);


    return (
        <div className='News'>
            <div className='NewsHeader'>
                Aktułalności: {process.env.REACT_APP_API_URL}
            </div>
                    
             <div className='NewsElementContainer'>
                <NewsElement/>
                <NewsElement/>
                <NewsElement/>
                <NewsElement/>
            </div>

            <div className='PageSelectorContainer'>
                {
                    maxPage <= maxDisplayedAmountOfPages 

                    ? [1, ...createArrayInRange(1, maxPage, maxPage)].map((e, i) => {
                        return <a key={i} className={e == page ? 'page-current' : 'page'} onClick={handlePageClick}>{e}</a>
                    })

                    : [...CreatePagesArray(page, maxPage)].map((e, i) => {
                        return <a key={i} className={e == page ? 'page-current' : 'page'} onClick={handlePageClick}>{e}</a>
                    })
                }
            </div>
        </div>
    );
};

export default News;