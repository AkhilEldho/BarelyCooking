import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

const cakeApi = createApi({
    reducerPath:"cakeApi",
    baseQuery: fetchBaseQuery({
        baseUrl:"https://barelycookingapi.azurewebsites.net/api/"
    }),
    tagTypes: ["Cake"],
    endpoints: (builder) => ({
        getCakes: builder.query({
            query: () => ({
                url:"cake"
            }),
            providesTags:["Cake"]
        }),
        getCake: builder.query({
            query: (cakeId) => ({
                url:`cake/${cakeId}`
            }),
            providesTags:["Cake"]
        })

    })
});

export const{useGetCakesQuery, useGetCakeQuery} = cakeApi;
export default cakeApi;