import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Depoimento } from './depoimento';

describe('Depoimento', () => {
  let component: Depoimento;
  let fixture: ComponentFixture<Depoimento>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Depoimento]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Depoimento);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
