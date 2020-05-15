import { Injectable } from "@angular/core";
import { Topic } from "../data-model/models/topic.model";
import { HttpClient } from "@angular/common/http";
import { CreateAnswer } from "../data-model/dto/create-answer.dto";
import { UpdateAnswer } from "../data-model/dto/update-answer.dto";


@Injectable()
export class TopicService {

    constructor(private http: HttpClient) { }

    getTopics() {
        return this.http.get<Topic[]>("api/topics");
    }

    addTopic(topic: Topic) {
        return this.http.post<CreateAnswer>('api/topics',topic);
    }

    deleteTopic(id: number) {
        return this.http.delete<UpdateAnswer>(`api/topics/${id}`);
    }

    updateTopic(topic: Topic) {
        return this.http.put<UpdateAnswer>('api/topics', topic);
    }
}
