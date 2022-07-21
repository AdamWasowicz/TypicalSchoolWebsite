import { createSlice, PayloadAction } from '@reduxjs/toolkit';


interface MaxPageNumber {
    value : number;
}


const initialState : MaxPageNumber = {
    value : 1, 
};


const maxPageNumberSlice = createSlice({
    name : 'pageNumber',
    initialState,
    reducers: {
        setMaxPageNumber(state: MaxPageNumber, action: PayloadAction<number>) {
            state.value = action.payload
        }
    }
});


export const { setMaxPageNumber } = maxPageNumberSlice.actions;
export default maxPageNumberSlice.reducer;