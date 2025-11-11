export interface IResult<T> {
    success: boolean;
    data?: T;
    message?: string,
    errors?: Record<string, string[]>;

    isSuccess(): boolean;
    getData(): T | undefined;
    getMessage(): string | undefined;
    getErrors(): Record<string, string[]> | undefined;
}
export class Result<T> implements IResult<T> {
    success: boolean = false;
    data?: T;
    message?: string;
    errors?: Record<string, string[]>;

    constructor(init: Partial<IResult<T>>) {
        Object.assign(this, init);
    }

    isSuccess(): boolean {
        return this.success;
    }

    getData(): T | undefined {
        return this.data;
    }

    getMessage(): string | undefined {
        return this.message;
    }

    getErrors(): Record<string, string[]> | undefined {
        return this.errors;
    }
}