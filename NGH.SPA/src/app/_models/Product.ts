import { Discount } from './Discount';

export class Product {
    id: number;
    productCode: string;
    productName: string;
    stdWeight: number;
    estWeight: number;
    uom: string;
    wage: number;
    stdPrice: number;
    onHandQty: number;
    custProductCode: string;
    productDiscounts?: Discount[];
    // delted: boolean;
}
