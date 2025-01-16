import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatViewSwitchComponent } from './chat-view-switch.component';

describe('ChatViewSwitchComponent', () => {
  let component: ChatViewSwitchComponent;
  let fixture: ComponentFixture<ChatViewSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatViewSwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatViewSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
