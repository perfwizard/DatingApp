import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductResolver } from '../../_resolver/product.resolver';


const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    data: {
      title: 'Products',
      status: false
    },
    children: [
      {
        path: '',
        component: ProductListComponent
      },
      {
        path: 'create',
        component: ProductEditComponent
      },
      {
        path: 'edit/:id',
        component: ProductEditComponent,
        resolve: { product: ProductResolver }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
