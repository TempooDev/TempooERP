export interface CreateProductCommand {
    name: string;
    price: number;
    taxRate: number;
    isActive: boolean;
};