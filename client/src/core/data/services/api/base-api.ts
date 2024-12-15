import {
  createApi, fetchBaseQuery
} from '@reduxjs/toolkit/query/react';

export const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: `/api/`
  }),
  keepUnusedDataFor: 0,
  refetchOnMountOrArgChange: true,
  tagTypes: ['User'],
  endpoints: () => ({}),
});