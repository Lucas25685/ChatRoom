import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatThemeSwitchComponent } from './chat-theme-switch.component';

describe('ChatThemeSwitchComponent', () => {
  let component: ChatThemeSwitchComponent;
  let fixture: ComponentFixture<ChatThemeSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatThemeSwitchComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatThemeSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
