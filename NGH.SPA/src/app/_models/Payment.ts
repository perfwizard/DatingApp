export class Payment {
    ID: number;
    ReceiptNo: string;
    PaymentDate: Date;
    PaidBy: string;
    PIC: string;
    Bank: string;
    Branch: string;
    AccountNo: string;
    ChequeNo: string;
    CreditCardNo: string;
    Remark: string;
    InternalNote: string;
    TotalAmount: number;
    TotalVat: number;
    WHTax: number;
}

export class PaymentLine {
    ID: number;
    PaymentID: number;
    DNNo: string;
    DNLineNo: number;
    WHTaxRate: number;
    WHTaxAmount: number;
    DNAmount: number;
    DNVat: number;
    Remark: string;
}