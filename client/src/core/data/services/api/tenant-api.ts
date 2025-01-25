import { baseApi } from './base-api.ts';
import { PagedResponse, transformPagedResponse } from '../../models/paged-response.ts';
import { Key } from 'react';
import { Tenant } from '../../entities/tenant/tenant.ts';

export const tenantApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getTenants: builder.query<PagedResponse<Tenant>, Record<string, Key>>({
      query: (params) => ({
        url: '/tenants',
        params: params
      }),
      transformResponse: transformPagedResponse
    })
  }),
  overrideExisting: false,
});

export const {
  useGetTenantsQuery
} = tenantApi;