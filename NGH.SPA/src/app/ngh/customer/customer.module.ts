import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { CustomerRoutingModule } from './customer-routing.module';
import { RouterModule } from '@angular/router/src/router_module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CustomerRoutingModule,
    NgxDatatableModule,
    SharedModule,
  ],
  declarations: [CustomerListComponent, CustomerEditComponent]
})
export class CustomerModule {
}
