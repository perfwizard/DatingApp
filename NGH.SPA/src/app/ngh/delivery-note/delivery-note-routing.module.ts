import { DeliveryNoteListComponent } from './delivery-note-list/delivery-note-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeliveryNoteEditComponent } from './delivery-note-edit/delivery-note-edit.component';
import { DeliveryNoteViewComponent } from './delivery-note-view/delivery-note-view.component';

const routes: Routes = [{
  path: '',
  data: {
    title: 'Delivery Notes',
    icon: 'ti-receipt',
    caption: 'Manage Delivery Note  information',
    status: false
  },
  children: [
    {
      path: '',
      component: DeliveryNoteListComponent
    },
    {
      path: 'view/:id',
      component: DeliveryNoteViewComponent
    },
    {
      path: 'edit/:id',
      component: DeliveryNoteEditComponent
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeliveryNoteRoutingModule { }
