import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatProgressbarComponent } from './chat-progressbar.component';

describe('ChatProgressbarComponent', () => {
  let component: ChatProgressbarComponent;
  let fixture: ComponentFixture<ChatProgressbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatProgressbarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatProgressbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
