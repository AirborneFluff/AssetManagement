import { baseApi } from './base-api.ts';
import { User } from '../../entities/user.ts';
import { LoginForm } from '../../models/login-form.ts';

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    logout: builder.mutation<void, void>({
      query: () => ({
        url: '/auth/logout',
        method: 'POST',
      }),
    }),
    login: builder.mutation<User, LoginForm>({
      query: (dto) => ({
        url: '/auth/login',
        method: 'POST',
        body: dto
      }),
    })
  }),
  overrideExisting: false,
});

export const {
  useLoginMutation,
  useLogoutMutation,
} = authApi;