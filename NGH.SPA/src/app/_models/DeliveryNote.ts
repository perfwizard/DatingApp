export interface DeliveryNote {
    id: number;
    dnNo: number;
    dnDate: Date;
    dueDate: Date;
    customerId: number;
    customerCode: string;
    customerName: string;
    billingAddress: string;
    shippingAddress: string;
    salesPIC: string;
    discount: number;
    actualWeight: number;
    stdPrice: number;
    tradePrice: number;
    goldPercent: number;
    remark: string;
    internalMemo: string;
    subtotal: number;
    vatRate: number;
    vatAmount: number;
    grandTotal: number;
    paidAmount: number;
    balance: number;
    issueInvoice: boolean;
    statusName: string;
    deliveryNoteLines: DeliveryNoteLine[];
}

export interface DeliveryNoteLine {
    id: number;
    productId: number;
    productCode: string;
    productName: string;
    qty: number;
    weight: number;
    uom: string;
    unitPrice: number;
    discount: number;
    lineTotal: number;
}
