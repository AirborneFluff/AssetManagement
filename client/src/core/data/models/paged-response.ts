import { FetchBaseQueryMeta } from '@reduxjs/toolkit/query';

export interface Pagination {
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;
}

export interface PagedResponse<T> {
  items: T[];
  pagination: Pagination;
}

export const transformPagedResponse = <T>(
  baseQueryReturnValue: T[],
  meta: FetchBaseQueryMeta
): PagedResponse<T> => {
  const items = baseQueryReturnValue;

  const paginationHeader: Pagination = JSON.parse(meta?.response?.headers.get('X-Pagination') || '{}');
  const pagination: Pagination = {
    totalCount: paginationHeader.totalCount,
    pageSize: paginationHeader.pageSize,
    currentPage: paginationHeader.currentPage,
    totalPages: paginationHeader.totalPages,
  };

  return { items, pagination };
};