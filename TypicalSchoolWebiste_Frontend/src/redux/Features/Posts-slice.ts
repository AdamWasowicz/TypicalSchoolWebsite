import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import IPostDTO from '../../assets/Interfaces/IPostDTO';


interface Posts {
    value : Array<IPostDTO>
};


const initialState : Posts = {
    value : []
};


const postsSlice = createSlice({
    name : 'posts',
    initialState,
    reducers : {
        setPosts(state: Posts, action: PayloadAction<Array<IPostDTO>>) {
            state.value = action.payload
        }
    }
});


export const { setPosts } = postsSlice.actions;
export default postsSlice.reducer;

