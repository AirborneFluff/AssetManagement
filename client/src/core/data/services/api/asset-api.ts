import { baseApi } from './base-api.ts';
import { Asset, AssetForm } from '../../entities/asset.ts';

export const assetApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createAsset: builder.mutation<Asset, AssetForm>({
      query: (dto) => ({
        url: '/assets',
        method: 'POST',
        body: dto
      }),
    })
  }),
  overrideExisting: false,
});

export const {
  useCreateAssetMutation
} = assetApi;