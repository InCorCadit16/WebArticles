import { ArticlePreview } from "../../models/article/article-preview";

export class PaginatorAnswer<T> {
    total: number;

    items: T[];
}
