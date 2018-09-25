import { AlertifyService } from './../../../_service/alertify.service';
import { Customer } from '../../../_models/Customer';
import { CustomerService } from '../../customer/customer.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-customer-edit',
  templateUrl: './customer-edit.component.html',
  styleUrls: ['./customer-edit.component.scss']
})
export class CustomerEditComponent implements OnInit {

  id: number;
  customerForm: FormGroup;
  customer: Customer;

  constructor(private route: ActivatedRoute,
    private fb: FormBuilder,
    private customerService: CustomerService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
    this.id = +this.route.snapshot.params['id'] || 0;

    this.customerForm = this.fb.group({
      code: ['', Validators.required],
      name: ['', Validators.required],
      taxId: ['', Validators.required],
      branchCode: ['', Validators.required],
      telNo: '',
      faxNo: '',
      customerGroup: null,
      contactName: null,
      billingAddress1: ['', Validators.required],
      billingAddress2: null,
      billingTambon: null,
      billingAmphur: null,
      billingProvince: null,
      billingPostalCode: null,
      shippingAddress1: ['', Validators.required],
      shippingAddress2: null,
      shippingTambon: null,
      shippingAmphur: null,
      shippingProvince: null,
      shippingPostalCode: null,
    });

    this.getCustomer(this.id);
    this.setCustomerForm();
  }
  getCustomer(id: number) {
    this.customerService.getCustomerById(id).subscribe(cus => this.customer = cus);
  }
  setCustomerForm() {
    this.customerForm.patchValue({
      code: this.customer.customerCode,
      name: this.customer.companyName,
      taxid: this.customer.taxId,
      branch: this.customer.branchCode,
      telNo: this.customer.telNo,
      faxNo: this.customer.faxNo,
      customerGroup: this.customer.customerGroupName,
      contactName: this.customer.contactName,
      billingAddress1: this.customer.billingAddress,
      /*billingAddress2: this.customer,
      billingTambon: null,
      billingAmphur: null,
      billingProvince: null,
      billingPostalCode: null,*/
      shippingAddress1: this.customer.shippingAddress
      /*shippingAddress2: null,
      shippingTambon: null,
      shippingAmphur: null,
      shippingProvince: null,
      shippingPostalCode: null*/
    });
  }

  onSubmit(customer: Customer) {
    if (this.customer.id) {
      this.customerService.updateCustomer(customer);
    } else {
      this.customerService.addCustomer(customer);
    }
    if (this.customerForm.valid) {
      this.customer = Object.assign({}, this.customer, this.customerForm.value);

      const result$ = this.customer.id
        ? this.customerService.updateCustomer(this.customer)
        : this.customerService.addCustomer(this.customer);

      result$.subscribe(
        next => {
          this.alertify.success('Update successfully.');
          this.customerForm.reset();
          this.router.navigate(['/product']);
        },
        error => {
          this.alertify.error('Error occurred. ' + error);
        }
      );
    }
  }

}
