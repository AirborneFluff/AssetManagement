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