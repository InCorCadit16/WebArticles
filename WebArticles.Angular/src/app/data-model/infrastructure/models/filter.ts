
export class Filter {
    path: string;
    value: any;
    action: string;

    constructor(path: string, value: any, action: string) {
        this.path = path;
        this.value = value;
        this.action = action;
    }
}
