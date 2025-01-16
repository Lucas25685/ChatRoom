import { Component, input } from '@angular/core';

@Component({
	selector: 'chat-svg-icon',
	standalone: true,
	imports: [],
	templateUrl: './chat-svg-icon.component.html',
})
export class ChatSvgIconComponent {
	public readonly class = input<string>();
	public readonly icon = input.required<ChatSvgIconType>();
	public readonly size = input<number>(16);
}

export type ChatSvgIconType =
	| 'close'
	| 'jar-of-pills'
	| 'from-to'
	| 'spinner'
	| 'pill'
	| 'void-circle'
	| 'file'
	| 'search'
	| 'cardboardBox'
	| 'pieChart'
	| 'empty-chat'
	| 'empty-request'
	| 'empty-offer'
	| 'empty-product';
