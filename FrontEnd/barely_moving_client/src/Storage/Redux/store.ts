import { configureStore } from "@reduxjs/toolkit";
import { cakeReducer } from "./cakeSlice";
import { cakeApi } from "../../API";

const store = configureStore({
    reducer:{
        cakeStore:cakeReducer,
        [cakeApi.reducerPath]: cakeApi.reducer
    },

    middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(cakeApi.middleware)
});

export type RootState = ReturnType<typeof store.getState>;
export default store;