export class SimpleClass {
    protected _type: string = "__SIMPLE__";
    public name: string;

    constructor(name) {
        this.name = name;
    }
}

new SimpleClass("My Simple Class");