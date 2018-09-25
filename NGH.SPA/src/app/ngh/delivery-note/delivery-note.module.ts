import { ProductService } from './../../_service/product.service';
import { DeliveryNoteService } from './../../_service/delivery-note.service';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeliveryNoteRoutingModule } from './delivery-note-routing.module';
import { DeliveryNoteListComponent } from './delivery-note-list/delivery-note-list.component';
import { DeliveryNoteViewComponent } from './delivery-note-view/delivery-note-view.component';
import { DeliveryNoteEditComponent } from './delivery-note-edit/delivery-note-edit.component';
import { SharedModule } from '../../shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { FormPickerModule } from '../../theme/forms/form-picker/form-picker.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    NgxDatatableModule,
    DeliveryNoteRoutingModule,
    FormPickerModule
  ],
  declarations: [DeliveryNoteListComponent, DeliveryNoteViewComponent, DeliveryNoteEditComponent],
  providers: [DeliveryNoteService, ProductService]
})
export class DeliveryNoteModule { }
