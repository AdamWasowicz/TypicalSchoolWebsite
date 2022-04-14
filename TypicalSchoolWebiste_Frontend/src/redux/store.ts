import { configureStore } from '@reduxjs/toolkit';

//Reducers
import counterReducer from './Features/counter-slice';


//Store
export const store = configureStore({
    reducer: {
        counter: counterReducer,

    },

})

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;