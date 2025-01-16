import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatButtonGroupComponent } from './chat-button-group.component';

describe('ChatButtonGroupComponent', () => {
  let component: ChatButtonGroupComponent;
  let fixture: ComponentFixture<ChatButtonGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatButtonGroupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatButtonGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
