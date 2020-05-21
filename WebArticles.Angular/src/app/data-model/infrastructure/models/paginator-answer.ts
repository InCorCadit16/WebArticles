import { ArticlePreview } from "../../models/article-preview";

export class PaginatorAnswer<T> {
    total: number;

    items: T[];
}
