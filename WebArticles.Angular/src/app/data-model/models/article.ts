import { Topic } from "./topic";

export class Article {
    id: number;
    title: string;
    rating: number;
    content: string;
    tags: string[];
    topic: Topic;
    userId: number;
    userName: string;
    publishDate: Date;
}
