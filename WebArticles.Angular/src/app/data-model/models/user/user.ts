import { Topic } from "../topic/topic";


export class User {
    id: number;
    profilePickLink: string;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    birthDate: Date;

    writerRating: number;
    writerDescription: string;
    writerTopics: Topic[];

    reviewerRating: number;
    reviewerDescription: string;
    reviewerTopics: Topic[];
}
