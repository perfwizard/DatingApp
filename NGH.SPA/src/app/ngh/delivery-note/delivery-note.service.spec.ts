/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DeliveryNoteService } from '../../_service/delivery-note.service';

describe('Service: DeliveryNote', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DeliveryNoteService]
    });
  });

  it('should ...', inject([DeliveryNoteService], (service: DeliveryNoteService) => {
    expect(service).toBeTruthy();
  }));
});
