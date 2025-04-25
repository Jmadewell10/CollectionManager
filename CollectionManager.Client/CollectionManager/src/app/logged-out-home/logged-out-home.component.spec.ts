import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggedOutHomeComponent } from './logged-out-home.component';

describe('LoggedOutHomeComponent', () => {
  let component: LoggedOutHomeComponent;
  let fixture: ComponentFixture<LoggedOutHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoggedOutHomeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoggedOutHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
