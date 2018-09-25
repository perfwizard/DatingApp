import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertifyService } from './../../_service/alertify.service';
import { ProductResolver } from '../../_resolver/product.resolver';
import { ProductService } from './../../_service/product.service';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { SharedModule } from '../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ProductRoutingModule,
    NgxDatatableModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [ProductListComponent, ProductEditComponent],
  providers: [ProductService, ProductResolver, AlertifyService]
})
export class ProductModule { }
