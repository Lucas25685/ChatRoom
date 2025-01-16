import { Component, computed, input } from '@angular/core';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { saxArrowRight3Outline } from '@ng-icons/iconsax/outline';
import { ChatSvgIconComponent, ChatSvgIconType } from '../chat-svg-icon/chat-svg-icon.component';
import { NgClass } from '@angular/common';

@Component({
	selector: 'chat-suggestions-card',
	standalone: true,
	imports: [NgIconComponent, ChatSvgIconComponent, NgClass],
	providers: [
		provideIcons({
			saxArrowRight3Outline,
		}),
		provideNgIconsConfig({ size: '1.5rem' }),
	],
	templateUrl: './chat-suggestions-card.component.html',
	styleUrls: ['./chat-suggestions-card.component.scss'],
})
export class ChatSuggestionsCard {
	public readonly disabled = input<boolean>(false);

	public readonly icon = input<ChatSvgIconType>();
	public readonly iconBg = input<string>('bg-gray-200 dark:bg-gray-900');

	public readonly title = input.required<string>();
	public readonly text = input.required<string>();
}
