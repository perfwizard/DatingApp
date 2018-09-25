import { CustomerEditComponent } from './customer-edit/customer-edit.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Customers',
      icon: 'ti-user',
      caption: 'Manage customer information.',
      status: false
    },
    children: [
      {
        path: '',
        component: CustomerListComponent
      },
      {
        path: 'create',
        component: CustomerEditComponent
      },
      {
        path: 'edit/:id',
        component: CustomerEditComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
