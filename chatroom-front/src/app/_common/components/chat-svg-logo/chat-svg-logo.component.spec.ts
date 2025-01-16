import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatSvgLogoComponent } from './chat-svg-logo.component';

describe('ChatSvgLogoComponent', () => {
  let component: ChatSvgLogoComponent;
  let fixture: ComponentFixture<ChatSvgLogoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatSvgLogoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatSvgLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
