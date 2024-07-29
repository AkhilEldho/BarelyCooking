import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    cake:[],
}

export const cakeSlice = createSlice({
    name:"Cake",
    initialState:initialState,
    reducers:{
        setCake: (state, action) => {
            state.cake = action.payload;
        },
    },
});

export const {setCake} = cakeSlice.actions;
export const cakeReducer = cakeSlice.reducer;