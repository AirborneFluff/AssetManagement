import { baseApi } from './base-api.ts';
import { Asset, AssetForm } from '../../entities/asset.ts';
import {
  PagedResponse,
  transformPagedResponse
} from '../../models/paged-response.ts';
import { Key } from 'react';

export const assetApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createAsset: builder.mutation<Asset, AssetForm>({
      query: (dto) => ({
        url: '/assets',
        method: 'POST',
        body: dto
      }),
    }),
    getAssets: builder.query<PagedResponse<Asset>, Record<string, Key>>({
      query: (params) => ({
        url: '/assets',
        params: params
      }),
      transformResponse: transformPagedResponse
    })
  }),
  overrideExisting: false,
});

export const {
  useCreateAssetMutation,
  useGetAssetsQuery
} = assetApi;