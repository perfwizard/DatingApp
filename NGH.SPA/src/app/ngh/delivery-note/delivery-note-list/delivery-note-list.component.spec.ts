import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryNoteListComponent } from './delivery-note-list.component';

describe('DeliveryNoteListComponent', () => {
  let component: DeliveryNoteListComponent;
  let fixture: ComponentFixture<DeliveryNoteListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryNoteListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryNoteListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
