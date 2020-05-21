import { Injectable } from "@angular/core";
import { Topic } from "../data-model/models/topic";
import { HttpClient } from "@angular/common/http";


@Injectable()
export class TopicService {

    constructor(private http: HttpClient) { }

    getTopics() {
        return this.http.get<Topic[]>("api/topics");
    }

    addTopic(topic: Topic) {
        return this.http.post<Topic>('api/topics',topic);
    }

    deleteTopic(id: number) {
        return this.http.delete(`api/topics/${id}`);
    }

    updateTopic(topic: Topic) {
        return this.http.put<Topic>('api/topics', topic);
    }
}
