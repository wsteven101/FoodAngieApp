import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BagCardComponent } from './bag-card.component';

describe('BagCardComponent', () => {
  let component: BagCardComponent;
  let fixture: ComponentFixture<BagCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BagCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BagCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
