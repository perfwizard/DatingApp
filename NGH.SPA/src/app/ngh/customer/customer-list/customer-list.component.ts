import { Pagination } from './../../../_models/Pagination';
import { Component, OnInit, ViewChild } from '@angular/core';
import {DatatableComponent} from '@swimlane/ngx-datatable';
import { CustomerService } from '../customer.service';
import { PaginatedResult } from '../../../_models/Pagination';
import { Customer } from '../../../_models/Customer';

@Component({
  selector: 'app-customer',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customerListTitle: string;
  @ViewChild(DatatableComponent) table: DatatableComponent;
  columns = [
    { prop: 'code', name: 'Customer Code' },
    { prop: 'name', name: 'Customer Name' },
    { prop: 'telNo', name: 'Tel No.' },
    { prop: 'taxId', name: 'Tax ID' },
    { prop: 'contactName', name: 'Contact Name' },
    { prop: 'balance', name: 'Balance' }
  ];
  rows: Customer[];
  customers = {};
  rowFilter: string;
  reorderable = true;
  loadingIndicator = true;
  pagination = new Pagination();
  searchParams: any = {};

  constructor(private customerService: CustomerService) {}

  ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.searchParams.searchString = '';
    this.getCustomers();
  }
  searchCustomers() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.getCustomers();
  }
  getCustomers() {
    this.customerService.getCustomerList(this.pagination.currentPage,
      this.pagination.itemsPerPage, this.searchParams.searchString).subscribe(
        (cus: PaginatedResult<Customer[]>) => {
          this.rows = cus.result;
          this.pagination  = cus.pagination;
          setTimeout(() => { this.loadingIndicator = true; }, 1500);
        });
  }

}
