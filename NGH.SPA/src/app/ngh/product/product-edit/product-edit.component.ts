import { Discount } from './../../../_models/Discount';
import { CustomerService } from './../../customer/customer.service';
import { AlertifyService } from './../../../_service/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from './../../../_service/product.service';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormArray
} from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Product } from '../../../_models/product';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
  productForm: FormGroup;
  product: Product = new Product();
  customers: any[];

  // property
  get productDiscounts(): FormArray {
    return <FormArray>this.productForm.get('productDiscounts');
  }

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private customerService: CustomerService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.productForm = this.fb.group({
      productId: null,
      productCode: ['', Validators.required],
      productName: ['', Validators.required],
      stdWeight: [0, Validators.required],
      estWeight: 0,
      imageId: null,
      wage: 0,
      stdPrice: 0,
      onHand: 0,
      cusProdCode: null,
      productDiscounts: this.fb.array([])
    });

    this.route.data.subscribe(data => {
      this.product = data['product'];
      console.log('Loading product...');
      console.log(this.product);
      if (this.product) {
        this.alertify.success('Get product data sucessfully.');
        this.productForm.patchValue({
          productId: this.product.id,
          productCode: this.product.productCode,
          productName: this.product.productName,
          stdWeight: this.product.stdWeight,
          estWeight: this.product.estWeight,
          imageId: null,
          wage: this.product.wage,
          stdPrice: this.product.stdPrice,
          onHand: this.product.onHandQty,
          cusProdCode: this.product.custProductCode,
          productDiscounts: this.populateDiscounts()

        });
        // this.populateDiscounts();
      }
    });

    this.customerService.getAllCustomers()
      .subscribe((data: any[]) => (this.customers = data));
  }

  buildDiscount() {
    return this.fb.group({
      customerId: null,
      discPrice: 0,
      discount: 0,
      isPercent: false
    });
  }

  addDiscount() {
    return this.productDiscounts.push(this.buildDiscount());
  }

  removeDiscount(index: number) {
    this.productDiscounts.removeAt(index);
  }

  populateDiscounts() {
    console.log(this.product.productDiscounts);
    if (this.product.productDiscounts) {
      this.product.productDiscounts.forEach(disc => {
        const discForm = this.buildDiscount();
        discForm.patchValue(disc);
        this.productDiscounts.push(discForm);
      });
    }
  }

  onSubmit() {
    if (this.productForm.valid) {
      this.product = Object.assign({}, this.product, this.productForm.value);
      this.product.productDiscounts = Object.assign([], this.product.productDiscounts);

      const result$ = this.product.id
        ? this.productService.updateProduct(this.product.id, this.product)
        : this.productService.addProduct(this.product);

      result$.subscribe(
        next => {
          this.alertify.success('Update successfully.');
          this.productForm.reset();
          this.router.navigate(['/product']);
        },
        error => {
          this.alertify.error('Error occurred. ' + error);
        }
      );
    }
  }
}
