import { RequestFilters } from "./request-filters";

export class PaginatorQuery {
    filters: RequestFilters = new RequestFilters();
    searchString: string= "";
    page: number = 1;
    sortBy: string = "id";
    sortDirection: string = "asc";
    pageSize: number = 10;
}
