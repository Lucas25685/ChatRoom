import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatFileUploaderComponent } from './chat-file-uploader.component';

describe('ChatFileUploaderComponent', () => {
  let component: ChatFileUploaderComponent;
  let fixture: ComponentFixture<ChatFileUploaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatFileUploaderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatFileUploaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
