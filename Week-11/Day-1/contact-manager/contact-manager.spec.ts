import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactManager } from './contact-manager';

describe('ContactManager', () => {
  let component: ContactManager;
  let fixture: ComponentFixture<ContactManager>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactManager],
    }).compileComponents();

    fixture = TestBed.createComponent(ContactManager);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
