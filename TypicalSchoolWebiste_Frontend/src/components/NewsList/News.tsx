import React from 'react';

//Style
import './style.scss';

//Components
import ListNewsElement from './NewsListElement';
import Loading from '../Loading';

//Other
import { useNews } from './utils';

//Redux
import { useAppSelector } from '../../redux/hooks'

//Interfaces
import IPostDTO from '../../assets/Interfaces/IPostDTO';
import NewsList_NoContentError from './NewsList_NoContentError';



const News: React.FunctionComponent = () => {

    const { isLoading, handlePageClick, CreatePagesArray } = useNews();
    const maxPage = useAppSelector((state) => state.maxPage.value);
    const posts: Array<IPostDTO> = useAppSelector((state) => state.posts.value);
    const page = useAppSelector((state) => state.page.value);


    return (
        <div className='News'>
            <div className='NewsHeader'>
                Aktułalności:
            </div>

            {
                !isLoading && posts.length == 0
                    ? <NewsList_NoContentError />
                    : <>
                        <div className='NewsElementContainer'>
                            {
                                isLoading
                                    ? <Loading />
                                    : posts.map((element, index) => {
                                        return <ListNewsElement post={element} key={index} />
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
                    </>
            }

        </div>
    );
};

export default News;