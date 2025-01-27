

export interface ApiResponse<T> {
  succeeded : boolean,
  message   : string,
  data      : T,
  errors    : string
}


export interface PagedList<T>
{
  items: T[],
  totalCount: number,
  pageNumber: number,
  pageSize: number,
}

export interface Pagination<T>
{
  PageNumber : number,
  PageSize   : number,
  Search     : T,
}
