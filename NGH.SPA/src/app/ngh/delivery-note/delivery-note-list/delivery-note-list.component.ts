import { Pagination, PaginatedResult } from '../../../_models/Pagination';
import { DeliveryNote } from '../../../_models/DeliveryNote';
import { DeliveryNoteService } from './../../../_service/delivery-note.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-delivery-note-list',
  templateUrl: './delivery-note-list.component.html',
  styleUrls: ['./delivery-note-list.component.css']
})
export class DeliveryNoteListComponent implements OnInit {
  rows: any [];
  dns: DeliveryNote[];
  searchParams: any = {};
  pagination: Pagination = new Pagination();

  columns = [
    { prop: 'id'},
    { prop: 'dnNo', name: 'เลขที่' },
    { prop: 'dnDate', name: 'วันที่' },
    { prop: 'companyName', name: 'ลูกค้า' },
    { prop: 'grandTotal', name: 'จำนวนเงินรวม' },
    { prop: 'paidAmount', name: 'จ่ายแล้ว' },
    { prop: 'balance', name: 'คงเหลือ' },
    { prop: 'statusName', name: 'สถานะ' },
  ];
  rowFilter: string;
  reorderable = true;
  loadingIndicator = true;

  constructor(private dnService: DeliveryNoteService) { }

  ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.itemsPerPage = 10;
    this.getDNs();
  }

  getDNs() {
    return this.dnService
      .getDeliveryNotes(this.pagination.currentPage,
        this.pagination.itemsPerPage, this.searchParams)
      .subscribe(
        (res: PaginatedResult<DeliveryNote[]>) => {
          this.dns = res.result;
          this.pagination = res.pagination;
        },
        error => {
          console.log('Error: ' + error);
        }
      );
  }
  setPage(param) {
    this.pagination.currentPage = param.offset;
    this.pagination.itemsPerPage = param.limit;
    this.getDNs();
  }
  cancelDn() {
    console.log('cancel');
  }
  setStatusClass(status): string {
    return status === 'draft' ? 'label-warning' :
            (status === 'approved' ? 'label-success' :
              (status === 'cancelled' ? 'label-default' : 'label-secondary'));
  }
}
