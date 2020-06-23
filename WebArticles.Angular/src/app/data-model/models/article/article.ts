import { Topic } from "../topic/topic";

export class Article {
    id: number;
    title: string;
    rating: number;
    content: string;
    overview: string;
    tags: string[];
    topic: Topic;
    userId: number;
    userName: string;
    publishDate: Date;
    lastEditDate: Date;
}
