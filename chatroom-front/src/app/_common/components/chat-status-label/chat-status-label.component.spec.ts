import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatStatusLabelComponent } from './chat-status-label.component';

describe('ChatStatusLabelComponent', () => {
  let component: ChatStatusLabelComponent;
  let fixture: ComponentFixture<ChatStatusLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatStatusLabelComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatStatusLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
