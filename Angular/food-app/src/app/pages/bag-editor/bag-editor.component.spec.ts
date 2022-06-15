import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BagEditorComponent } from './bag-editor.component';

describe('BagEditorComponent', () => {
  let component: BagEditorComponent;
  let fixture: ComponentFixture<BagEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BagEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BagEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
