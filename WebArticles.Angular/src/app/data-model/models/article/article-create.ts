
export class ArticleCreate {
    userId: number;
    title: string;
    topicId: number;
    overview: string;
    content: string;
    tags: string[];
    publishDate: Date;
}
