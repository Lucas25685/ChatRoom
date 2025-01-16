import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatSuggestionsCard } from './chat-suggestions-card.component';

describe('NewRequestButtonComponent', () => {
	let component: ChatSuggestionsCard;
	let fixture: ComponentFixture<ChatSuggestionsCard>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			imports: [ChatSuggestionsCard],
		}).compileComponents();

		fixture = TestBed.createComponent(ChatSuggestionsCard);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
