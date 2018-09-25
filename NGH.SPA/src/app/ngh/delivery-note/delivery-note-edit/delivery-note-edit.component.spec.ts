import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryNoteEditComponent } from './delivery-note-edit.component';

describe('DeliveryNoteEditComponent', () => {
  let component: DeliveryNoteEditComponent;
  let fixture: ComponentFixture<DeliveryNoteEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryNoteEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryNoteEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
