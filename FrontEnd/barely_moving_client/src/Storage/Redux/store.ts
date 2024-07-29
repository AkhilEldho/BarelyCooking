import { configureStore } from "@reduxjs/toolkit";
import { cakeReducer } from "./cakeSlice";
import { cakeApi, shoppingCartApi } from "../../API";

const store = configureStore({
    reducer:{
        cakeStore:cakeReducer,
        [cakeApi.reducerPath]: cakeApi.reducer,
        [shoppingCartApi.reducerPath]: shoppingCartApi.reducer
    },

    middleware: (getDefaultMiddleware) => getDefaultMiddleware()
    .concat(cakeApi.middleware)
    .concat(shoppingCartApi.middleware)
});

export type RootState = ReturnType<typeof store.getState>;
export default store;