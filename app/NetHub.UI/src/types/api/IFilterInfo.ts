export interface IFilterInfo {
  filters: string | undefined;
  sorts: string | undefined;
  page: number | undefined;
  pageSize: number | undefined;
}

export class FilterInfo implements IFilterInfo{
  filters: string | undefined;
  page: number | undefined;
  pageSize: number | undefined;
  sorts: string | undefined;

  constructor(filters?: string, page?: number, pageSize?: number, sorts?: string) {
    this.filters = filters;
    this.page = page;
    this.pageSize = pageSize;
    this.sorts = sorts;
  }

}