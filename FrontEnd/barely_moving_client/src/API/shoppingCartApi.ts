import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";

const shoppingCartApi = createApi({
    reducerPath:"shoppingCartApi",
    baseQuery: fetchBaseQuery({
        baseUrl:"https://barelycookingapi.azurewebsites.net/api/"
    }),
    tagTypes: ["ShoppingCarts"],
    endpoints: (builder) => ({
        getShoppingCart: builder.query({
            query: (userId) => ({
                url:`shoppingCartApi`,
                params:{
                    userId:userId
                }
            }),
            providesTags:["ShoppingCarts"]
        }),
        updateShoppingCart: builder.mutation({
            query: ({
                userId, cakeId, quantity
            }) => ({
                url:"shoppingCartApi",
                method:"POST",
                params:{
                    userId, cakeId, quantity
                }
            }),
            invalidatesTags: ["ShoppingCarts"]
        })
    })
});

export const{useGetShoppingCartQuery, useUpdateShoppingCartMutation} = shoppingCartApi;
export default shoppingCartApi;