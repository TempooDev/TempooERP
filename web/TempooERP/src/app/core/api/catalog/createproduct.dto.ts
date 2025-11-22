export class CreateProductCommand {
  name!: string;
  price!: number;
  taxRate!: number;
  active!: boolean;

  constructor(init: Partial<CreateProductCommand>) {
    Object.assign(this, init);
  }

  static createProduct(name: string, price: number, taxRate: number, active: boolean): CreateProductCommand {
    return new CreateProductCommand({
      name,
      price,
      taxRate,
      active,
    });
  }
}
