import { Component, input } from '@angular/core';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { matTrendingUpOutline } from '@ng-icons/material-icons/outline';

@Component({
	selector: 'chat-label',
	standalone: true,
	imports: [NgIconComponent],
	providers: [
		provideIcons({
			matTrendingUpOutline,
		}),
		provideNgIconsConfig({ size: '1rem' }),
	],
	styleUrl: './chat-label.component.scss',
	templateUrl: './chat-label.component.html',
})
export class ChatLabelComponent {
	public readonly label = input('');
}
