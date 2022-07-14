import { useEffect, useState } from 'react';
import AxiosClient from '../Axios/AxiosClient';
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
    

    const createArrayInRange = (start: number, end: number, maxNumber: number) => {
        let array : Array<number> = [];

        for (let i = start ; i < end && i < maxNumber ; i++)
            array.push(i);

        return array;
    }


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
        console.log(dto)


        await axios({
            method: 'POST',
            url: `http://localhost:8000` + '/api/post/getPosts/query',
            data: dto
        }).then(result => {
            console.log(result)
        }).catch((e : AxiosError) => {
            console.log(e.toJSON());
            console.log(e.request.response);
        });
    }


    return {isLoading, createArrayInRange, 
            handlePageClick, CreatePagesArray,
            getPostsUsingQuery};
}