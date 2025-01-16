import { Component, input } from '@angular/core';

@Component({
	selector: 'chat-svg-logo',
	standalone: true,
	imports: [],
	templateUrl: './chat-svg-logo.component.html',
})
export class ChatSvgLogoComponent {
	public readonly showBranding = input(false);
}
