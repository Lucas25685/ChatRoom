import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatSvgIconComponent } from './chat-svg-icon.component';

describe('ChatSvgIconComponent', () => {
  let component: ChatSvgIconComponent;
  let fixture: ComponentFixture<ChatSvgIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatSvgIconComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatSvgIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
