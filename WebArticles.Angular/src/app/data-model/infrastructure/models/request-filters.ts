import { FilterLogicalOperators } from "./filters-logical-operators";
import { Filter } from "./filter";

export class RequestFilters {
    logicalOperator: FilterLogicalOperators;
    filters: Filter[] = [];
    tags: string[] = [];
}
