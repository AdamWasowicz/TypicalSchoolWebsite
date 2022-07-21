import { useEffect, useState } from 'react';
import axios, { AxiosError } from 'axios';

//Interfaces
import IPostQueryDTO from '../../assets/Interfaces/IPostQueryDTO';
import IPostQueryResult from '../../assets/Interfaces/IPostQueryResult';

//Redux
import { useAppDispatch, useAppSelector } from '../../redux/hooks'
import { setPageNumber } from '../../redux/Features/PageNumber-slice';
import { setPosts } from '../../redux/Features/Posts-slice';
import { setMaxPageNumber } from '../../redux/Features/MaxPage-slice';

//Other
import { getPostsUsingQueryEndpoint } from '../../constants/ApiEndpoints';


export const useNews = () => {
    
    const [isLoading, setIsLoading] = useState(false);
    const dispatch = useAppDispatch();
    const currentPage = useAppSelector(state => state.page.value);


    const CreatePagesArray = (page: number, maxPage: number) =>
    {

        let array : Array<number> = [];
        let totalAdded = 0;
        
        for (let i = 1 ; page - i > 1 && totalAdded <= 4 ; i++)
        {
            array.push(page - i);
            totalAdded++;
        }

        for (let i = 1 ; page + i < maxPage && totalAdded <= 4 ; i++)
        {
            array.push(page + i);
            totalAdded++;
        }

        array.includes(maxPage) ? null : array.push(maxPage);
        array.includes(1) ? null : array.push(1);
        array.includes(page) ? null : array.push(page);
        array.sort();


        return array;
    }


    //Handlers
    const handlePageClick = (event: React.MouseEvent) => {
        event.preventDefault();
        dispatch(setPageNumber(+event.currentTarget.innerHTML));
    }


    //useEffect
    useEffect(() => {
        const dto : IPostQueryDTO = {
            DesiredPage: currentPage,
            DesiredNumberOfItems: 4
        }

        getPostsUsingQuery(dto);
    }, []);

    useEffect(() => {
        const dto : IPostQueryDTO = {
            DesiredPage: currentPage,
            DesiredNumberOfItems: 4
        }

        getPostsUsingQuery(dto);
    }, [currentPage]);



    //Api calls
    const getPostsUsingQuery = async (dto : IPostQueryDTO) => {

        setIsLoading(true);
        let resultDTO : IPostQueryResult;
        let url = process.env.REACT_APP_API_URL + getPostsUsingQueryEndpoint
        console.log(url)


        await axios({
            method: 'POST',
            url: url,
            data: dto
        })
        .then(result => {

            resultDTO = result.data;

            dispatch(setPosts(resultDTO.posts));
            dispatch(setPageNumber(resultDTO.desiredPage));
            dispatch(setMaxPageNumber(resultDTO.maxPages));
        })
        .catch((e : AxiosError) => {

            console.error(e.toJSON());
            console.error(e.request.response);
        })
        .finally(() => {

            setIsLoading(false);
        })
    }


    return { isLoading, handlePageClick, CreatePagesArray, getPostsUsingQuery };
}