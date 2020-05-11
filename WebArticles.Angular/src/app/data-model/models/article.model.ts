import { Topic } from "./topic.model";

export class Article {
    id: number;
    title: string;
    rating: number;
    content: string;
    tags: string[];
    topic: Topic;
    userId: number;
    userName: string;
    publichDate: Date;
}
