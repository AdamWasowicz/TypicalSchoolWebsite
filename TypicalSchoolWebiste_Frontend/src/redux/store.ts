import { configureStore } from '@reduxjs/toolkit';


//Reducers
import counterReducer from './Features/counter-slice';
import pageReducer from './Features/PageNumber-slice';
import postsReducer from './Features/Posts-slice';
import maxPageReducer from './Features/MaxPage-slice';


//Store
export const store = configureStore({
    reducer: {
        counter: counterReducer,
        page: pageReducer,
        posts: postsReducer,
        maxPage: maxPageReducer
    }
})


export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;