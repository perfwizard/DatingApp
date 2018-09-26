import { DeliveryNote } from './../../../_models/DeliveryNote';
import { Product } from './../../../_models/Product';
import { Customer } from './../../../_models/Customer';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { NgbCalendar } from '@ng-bootstrap/ng-bootstrap';

import { DeliveryNoteService } from './../../../_service/delivery-note.service';
import { CustomerService } from './../../customer/customer.service';
import { ProductService } from '../../../_service/product.service';


@Component({
  selector: 'app-delivery-note-edit',
  templateUrl: './delivery-note-edit.component.html',
  styleUrls: ['./delivery-note-edit.component.scss']
})
export class DeliveryNoteEditComponent implements OnInit {
  dnForm: FormGroup;
  calendar: NgbCalendar;
  customers: Customer[];
  products: Product[];
  product: Product;
  customerAddress: any[];
  billingAddress: string;
  shippingAddress: string;
  dn: DeliveryNote;

  // Property
  get DNLines(): FormArray {
    return <FormArray>this.dnForm.get('dnLines');
  }
  constructor(private fb: FormBuilder, private cusService: CustomerService,
    private dnService: DeliveryNoteService, private route: ActivatedRoute,
    private productService: ProductService) { }

  ngOnInit() {
    this.dnForm = this.fb.group({
      customer: ['0', Validators.required],
      dnDate: [Date.now, Validators.required],
      paidBy: ['', Validators.required],
      actualWeight: ['', Validators.required],
      stdPrice: ['', Validators.required],
      subtotal: ['', Validators.required],
      discount: ['', Validators.required],
      netAmount: ['', Validators.required],
      pricePerGram: ['', Validators.required],
      diff: ['', Validators.required],
      vat: ['', Validators.required],
      total: ['', Validators.required],
      dnLines: this.fb.array([this.createDnLine()])
    });

    const id = +this.route.snapshot.paramMap.get('id');

    this.getDeliveryNote(id);
    this.getCustomers();
    this.getProducts();
    if (this.dn != null) {
      this.populateDnLines();
    }
    /*const sources: any = [
      this.getCustomers(),
      this.getProducts(),
      this.getDeliveryNote(id)
    ];
    forkJoin<any[]>(sources).subscribe(data => {
      this.customers = data[0];
      this.products = data[1];
      this.dn = data[2];
    });*/
  }

  onSubmit() {

  }

  createDnLine(): FormGroup {
    return this.fb.group({
      productId: [0, Validators.required],
      productCode: ['', Validators.required],
      productName: ['', Validators.required],
      qty: [0, Validators.required],
      unitPrice: [0, Validators.required],
      discount: [0, Validators.required],
      lineTotal: [0, Validators.required]
    });
  }

  addLine() {
    this.DNLines.push(this.createDnLine());
  }

  removeLine(i: number) {
    this.DNLines.removeAt(i);
  }

  getCustomerAddress(customerVal) {
    // const customerVal = this.dnForm.controls['customer'].value;
    console.log(customerVal);
    this.getBillingAddress(customerVal);
    this.getShippingAddress(customerVal);
  }

  getBillingAddress(cusId: number) {
    this.cusService.getCustomerById(cusId).subscribe(
      res => {
        this.billingAddress = res.billingAddress;
      }
    );
    console.log(this.billingAddress);
  }

  getShippingAddress(cusId: number) {
    this.cusService.getCustomerById(cusId).subscribe(
      res => {
        this.shippingAddress = res.shippingAddress;
      }
    );
    console.log(this.shippingAddress);
  }

  getCustomers() {
    this.cusService.getAllCustomers().subscribe(
      (res: Customer[]) => this.customers = res);

    console.log(this.customers);
  }

  getDeliveryNote(id: number) {
    this.dnService.getDeliveryNote(id).subscribe(
      (res: DeliveryNote) => {
        this.dn = res;
        console.log('dn: ' + JSON.stringify(this.dn));
      },
      error => { console.log(error); });
  }

  getProducts() {
    this.productService.getAllProducts().subscribe(
      res => {
          this.products = res;
          console.log(this.products);
      }
    );
  }
  GetProductInfo(ctrl: HTMLInputElement, dnLine) {

    this.productService.getProductById(+ctrl.value)
      .subscribe((res: Product) => {
        this.product = res;
        console.log('id: ' + +ctrl.value);
        console.log(this.product);

        if (!this.product) {
          console.log('Product not found.');
          return;
        }
        const updateLine = this.DNLines.controls.find(l => l === dnLine);
        console.log('watch: ');
        console.log(dnLine);
        console.log(updateLine);

        if (updateLine != null) {
          console.log('update line');
          updateLine.get('productName').setValue(this.product.productName);
          updateLine.get('qty').patchValue((1).toLocaleString('en-US', {minimumFractionDigits: 2}));
          updateLine.get('unitPrice').patchValue(this.product.stdPrice.toLocaleString('en-US', {minimumFractionDigits: 2}));
          updateLine.get('discount').patchValue((0).toLocaleString('en-US', {minimumFractionDigits: 2}));
          updateLine.get('lineTotal').patchValue(this.product.stdPrice.toLocaleString('en-US', {minimumFractionDigits: 2}));

          this.calculateLineTotal(updateLine);
        }

      }, error => {
        console.log('error occured.');
      });
  }

  populateDnLines() {
    console.log('lines: ' + this.DNLines);
    this.dn.deliveryNoteLines.forEach(x => {
      this.DNLines.push(this.fb.group({
        productCode: x.productCode,
        productName: x.productName,
        unitPrice: x.unitPrice.toLocaleString('en-US', {minimumFractionDigits: 2}),
        qty: x.qty.toLocaleString('en-US', {minimumFractionDigits: 2}),
        discount: x.discount.toLocaleString('en-US', {minimumFractionDigits: 2}),
        lineTotal: x.lineTotal.toLocaleString('en-US', {minimumFractionDigits: 2})
      }));
    });
  }

  calculateLineTotal(ln: AbstractControl) {
    // qty * unitPrice - discount = lineTotal
    const qty = parseFloat(ln.get('qty').value);
    const unitPrice = parseFloat(ln.get('unitPrice').value);
    const discount = parseFloat(ln.get('discount').value);

    ln.get('lineTotal').patchValue((qty * unitPrice - discount).toLocaleString('en-US', {minimumFractionDigits: 2}));

    this.calculateTotal();
  }
  calculateTotal() {
    let subTotal = 0;
    if (this.DNLines) {
      this.DNLines.controls.forEach(e => {
        subTotal += parseFloat(e.get('lineTotal').value);
      });
    }
    this.dnForm.get('subtotal').setValue(subTotal.toLocaleString('en-US', {minimumFractionDigits: 2}));
    const discount = parseFloat(this.dnForm.get('discount').value || 0);
    this.dnForm.get('netAmount').setValue((subTotal - discount).toLocaleString('en-US', {minimumFractionDigits: 2}));
    const pricePerGram =  parseFloat(this.dnForm.get('redemptionPrice').value || 0);
    const diff = subTotal - discount - pricePerGram;
    this.dnForm.get('diff').setValue(diff.toLocaleString('en-US', {minimumFractionDigits: 2}));
    this.dnForm.get('vat').setValue((diff * .07).toLocaleString('en-US', {minimumFractionDigits: 2}));
    this.dnForm.get('total').setValue((subTotal - discount + (diff * .07)).toLocaleString('en-US', {minimumFractionDigits: 2}));
  }
}
