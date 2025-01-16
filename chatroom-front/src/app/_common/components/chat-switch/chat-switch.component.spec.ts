import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatSwitchComponent } from './chat-switch.component';

describe('ChatSwitchComponent', () => {
  let component: ChatSwitchComponent;
  let fixture: ComponentFixture<ChatSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatSwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
