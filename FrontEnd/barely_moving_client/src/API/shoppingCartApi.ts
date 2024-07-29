import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const shoppingCartApi = createApi({
    reducerPath: "shoppingCartApi",
    baseQuery: fetchBaseQuery({
        baseUrl: "https://barelycookingapi.azurewebsites.net/api/"
    }),
    tagTypes: ["ShoppingCart"],
    endpoints: (builder) => ({
        getShoppingCart: builder.query({
            query: (userId) => ({
                url: `shoppingCart`,
                params: {
                    userId: userId
                }
            }),
            providesTags: ["ShoppingCart"]
        }),
        updateShoppingCart: builder.mutation({
            query: ({ userId, cakeId, quantity }) => ({
                url: "shoppingCart",
                method: "POST",
                params: {
                    userId, 
                    cakeId, 
                    quantity,
                },
            }),
            invalidatesTags: ["ShoppingCart"]
        })
    })
});

export const { useGetShoppingCartQuery, useUpdateShoppingCartMutation } = shoppingCartApi;
export default shoppingCartApi;
