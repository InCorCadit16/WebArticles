import { Filters } from "./filters.dto";

export class PaginatorQuery {
    filters: Filters = null;
    search:string = "";
    page: number = 1;
    sortBy: string = "id";
    sortDirection: string = "asc";
    pageSize: number = 10;
}
