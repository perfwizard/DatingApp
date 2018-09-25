import { PaginatedResult } from './../../../_models/Pagination';
import { Pagination } from '../../../_models/Pagination';
import { Product } from '../../../_models/Product';
import { ProductService } from './../../../_service/product.service';
import { DatatableComponent } from '@swimlane/ngx-datatable';
import { Component, OnInit, ViewChild, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-product',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  @ViewChild(DatatableComponent) table: DatatableComponent;
  columns = [
    { prop: 'id'},
    { prop: 'code', name: 'Product Code' },
    { prop: 'name', name: 'Product Name' },
    { prop: 'weight', name: 'Weight' },
    { prop: 'price', name: 'Unit Price' },
    { prop: 'onHand', name: 'On Hand Qty' }
  ];
  products: Product[];
  searchParams: any = {};
  pagination = new Pagination();
  reorderable = true;
  loadingIndicator = true;
  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.getProducts();
  }

  searchProduct() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.getProducts();
  }

  getProducts() {
    this.productService.getProductList(this.pagination.currentPage,
      this.pagination.itemsPerPage, this.searchParams.searchString)
      .subscribe(
        (res: PaginatedResult<Product[]>) => {
          this.products = res.result;
          this.pagination = res.pagination;
          console.log(this.products);
          console.log(this.pagination);
      }, error => {
        console.log(error);
      });
  }

  setPage(param: any) {
    this.pagination.currentPage = param.offset;
    this.pagination.itemsPerPage = param.limit;
    this.getProducts();
  }

}
