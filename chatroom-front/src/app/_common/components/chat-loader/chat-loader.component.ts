import { Component, input } from '@angular/core';

@Component({
	selector: 'chat-loader',
	standalone: true,
	imports: [],
	templateUrl: './chat-loader.component.html',
})
export class ChatLoaderComponent {
	public readonly text = input('Loading...');
}
