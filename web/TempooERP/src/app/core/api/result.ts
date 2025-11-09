export interface IResult<T> {
    success: boolean;
    data?: T;
    error?: string;

    isSuccess(): boolean;
    getData(): T | undefined;
    getError(): string | undefined;
}
export class Result<T> implements IResult<T> {
    success: boolean = false;
    data?: T;
    error?: string;

    constructor(init: Partial<IResult<T>>) {
        Object.assign(this, init);
    }

    isSuccess(): boolean {
        return this.success;
    }

    getData(): T | undefined {
        return this.data;
    }

    getError(): string | undefined {
        return this.error;
    }
}