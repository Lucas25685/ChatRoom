import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatPackageComponent } from './chat-package.component';

describe('ChatPackageComponent', () => {
  let component: ChatPackageComponent;
  let fixture: ComponentFixture<ChatPackageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatPackageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatPackageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
