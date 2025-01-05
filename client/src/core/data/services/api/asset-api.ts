import { baseApi } from './base-api.ts';
import { Asset, AssetForm } from '../../entities/asset/asset.ts';
import {
  PagedResponse,
  transformPagedResponse
} from '../../models/paged-response.ts';
import { Key } from 'react';
import { AssetCategory, AssetCategoryForm } from '../../entities/asset/asset-category.ts';

export const assetApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createAsset: builder.mutation<Asset, AssetForm>({
      query: (dto) => ({
        url: '/assets',
        method: 'POST',
        body: dto
      }),
    }),
    updateAsset: builder.mutation<Asset, AssetForm>({
      query: (dto) => ({
        url: `/assets/${dto.id}`,
        method: 'PUT',
        body: dto
      }),
    }),
    createAssetCategory: builder.mutation<AssetCategory, AssetCategoryForm>({
      query: (dto) => ({
        url: '/assets/categories',
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
    }),
    getAsset: builder.query<Asset, string>({
      query: (assetId) => ({
        url: `/assets/${assetId}`
      })
    }),
    getAssetCategories: builder.query<PagedResponse<AssetCategory>, Record<string, Key>>({
      query: (params) => ({
        url: '/assets/categories',
        params: params
      }),
      transformResponse: transformPagedResponse
    })
  }),
  overrideExisting: false,
});

export const {
  useCreateAssetMutation,
  useCreateAssetCategoryMutation,
  useUpdateAssetMutation,
  useGetAssetsQuery,
  useLazyGetAssetQuery,
  useGetAssetCategoriesQuery
} = assetApi;