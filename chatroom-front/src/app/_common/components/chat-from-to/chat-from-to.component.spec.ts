import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatFromToComponent } from './chat-from-to.component';

describe('ChatFromToComponent', () => {
  let component: ChatFromToComponent;
  let fixture: ComponentFixture<ChatFromToComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatFromToComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatFromToComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
