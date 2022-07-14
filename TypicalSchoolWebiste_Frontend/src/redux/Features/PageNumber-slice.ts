import { createSlice, PayloadAction } from '@reduxjs/toolkit';


interface PageNumber {
    value : number;
}


const initialState : PageNumber = {
    value : 1,
};


const pageNumberSlice = createSlice({
    name : 'pageNumber',
    initialState,
    reducers: {
        setPageNumber(state: PageNumber, action: PayloadAction<number>) {
            state.value = action.payload
        }
    }
});


export const { setPageNumber } = pageNumberSlice.actions;
export default pageNumberSlice.reducer;
