import { Component, input } from '@angular/core';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { saxBagTickOutline, saxClockOutline, saxEdit2Outline } from '@ng-icons/iconsax/outline';
import { RequestStatusEnum } from '../../enums/request-status.enum';

@Component({
	selector: 'chat-status-label',
	standalone: true,
	imports: [NgIconComponent],
	providers: [
		provideIcons({
			saxBagTickOutline,
			saxClockOutline,
			saxEdit2Outline,
		}),
		provideNgIconsConfig({ size: '1rem' }),
	],
	styleUrl: './chat-status-label.component.scss',
	templateUrl: './chat-status-label.component.html',
})
export class ChatStatusLabelComponent {
	public readonly requestStatuses = RequestStatusEnum;

	public readonly status = input<number>(1);
}
