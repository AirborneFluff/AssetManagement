import { baseApi } from './base-api.ts';
import { Asset, AssetForm } from '../../entities/asset/asset.ts';
import {
  PagedResponse,
  transformPagedResponse
} from '../../models/paged-response.ts';
import { Key } from 'react';
import { AssetCategory, AssetCategoryForm } from '../../entities/asset/asset-category.ts';
import { AssetSupplier, AssetSupplierForm } from '../../entities/asset/asset-supplier.ts';
import { AssetSupplySource, AssetSupplySourceForm } from '../../entities/asset/asset-supply-source.ts';
import { AssetStockLevelForm } from '../../entities/asset/asset-stock-level.ts';

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
    updateAssetCategory: builder.mutation<AssetCategory, AssetCategoryForm>({
      query: (dto) => ({
        url: `/assets/categories/${dto.id}`,
        method: 'PUT',
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
    }),
    getAssetCategory: builder.query<AssetCategory, string>({
      query: (categoryId) => ({
        url: `/assets/categories/${categoryId}`
      })
    }),
    getAssetSuppliers: builder.query<PagedResponse<AssetSupplier>, Record<string, Key>>({
      query: (params) => ({
        url: '/assets/suppliers',
        params: params
      }),
      transformResponse: transformPagedResponse
    }),
    getAssetSupplier: builder.query<AssetSupplier, string>({
      query: (supplierId) => ({
        url: `/assets/suppliers/${supplierId}`
      })
    }),
    createAssetSupplier: builder.mutation<AssetSupplier, AssetSupplierForm>({
      query: (dto) => ({
        url: '/assets/suppliers',
        method: 'POST',
        body: dto
      }),
    }),
    updateAssetSupplier: builder.mutation<AssetSupplier, AssetSupplierForm>({
      query: (dto) => ({
        url: `/assets/suppliers/${dto.id}`,
        method: 'PUT',
        body: dto
      }),
    }),
    updateAssetSupplySource: builder.mutation<AssetSupplySource, AssetSupplySourceForm>({
      query: (dto) => ({
        url: `/assets/${dto.assetId}/supplySources/${dto.id}`,
        method: 'PUT',
        body: dto
      }),
    }),
    createAssetSupplySource: builder.mutation<AssetSupplySource, AssetSupplySourceForm>({
      query: (dto) => ({
        url: `/assets/${dto.assetId}/supplySources`,
        method: 'POST',
        body: dto
      }),
    }),
    deleteAssetSupplySource: builder.mutation<AssetSupplySource, { id: string, assetId: string }>({
      query: (dto) => ({
        url: `/assets/${dto.assetId}/supplySources/${dto.id}`,
        method: 'DELETE'
      }),
    }),
    updateAssetStockLevel: builder.mutation<AssetSupplySource, AssetStockLevelForm>({
      query: (dto) => ({
        url: `/assets/${dto.assetId}/stockLevels`,
        method: 'POST',
        body: dto
      }),
    }),
  }),
  overrideExisting: false,
});

export const {
  useCreateAssetMutation,
  useCreateAssetCategoryMutation,
  useUpdateAssetMutation,
  useUpdateAssetCategoryMutation,
  useGetAssetsQuery,
  useGetAssetQuery,
  useLazyGetAssetQuery,
  useLazyGetAssetCategoryQuery,
  useGetAssetCategoriesQuery,
  useGetAssetSuppliersQuery,
  useLazyGetAssetSupplierQuery,
  useCreateAssetSupplierMutation,
  useUpdateAssetSupplierMutation,
  useUpdateAssetSupplySourceMutation,
  useCreateAssetSupplySourceMutation,
  useDeleteAssetSupplySourceMutation,
  useUpdateAssetStockLevelMutation
} = assetApi;