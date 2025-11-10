export interface PagedList<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export function emptyPagedList<T>(): PagedList<T> {
  return {
    items: [],
    totalCount: 0,
    page: 1,
    pageSize: 0,
    totalPages: 0,
    hasNextPage: false,
    hasPreviousPage: false
  };
}
