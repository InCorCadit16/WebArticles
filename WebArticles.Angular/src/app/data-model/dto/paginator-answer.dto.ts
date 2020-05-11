import { ArticlePreview } from "../models/article-preview.model";

export class PaginatorAnswer<T> {
    total: number;

    items: T[];
}
