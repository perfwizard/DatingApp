import { DeliveryNote } from '../../../_models/DeliveryNote';
import { DeliveryNoteService } from './../../../_service/delivery-note.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-delivery-note-view',
  templateUrl: './delivery-note-view.component.html',
  styleUrls: ['./delivery-note-view.component.scss']
})
export class DeliveryNoteViewComponent implements OnInit {
  dnId = 0;
  dn: DeliveryNote;
  constructor(private dnService: DeliveryNoteService,
      private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.dnId = this.route.snapshot.params['id'];
    this.getDnById(this.dnId);
  }

  getDnById(id: number) {
    return this.dnService.getDeliveryNote(id).subscribe(
      (res: DeliveryNote) => {
        this.dn = res;
    });
  }

  edit() {
    console.log(this.dnId);
    this.router.navigate(['/deliverynote/edit', this.dnId]);
  }
}
