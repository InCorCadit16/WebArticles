import { Topic } from "../models/topic.model";

export class ArticleCreate {
    userId: number;
    title: string;
    topicId: number;
    overview: string;
    content: string;
    tags: string[];
}
