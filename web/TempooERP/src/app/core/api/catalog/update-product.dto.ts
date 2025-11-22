export class UpdateProductCommand {
  id!: string;
  name!: string;
  price!: number;
  taxRate!: number;
  isActive!: boolean;

  constructor(init: Partial<UpdateProductCommand>) {
    Object.assign(this, init);
  }

  static updateProduct(
    id: string,
    name: string,
    price: number,
    taxRate: number,
    isActive: boolean,
  ): UpdateProductCommand {
    return new UpdateProductCommand({
      id,
      name,
      price,
      taxRate,
      isActive,
    });
  }
}
