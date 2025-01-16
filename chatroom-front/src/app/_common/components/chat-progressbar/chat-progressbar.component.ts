import { Component, input } from '@angular/core';

@Component({
	selector: 'chat-progressbar',
	standalone: true,
	imports: [],
	styleUrl: './chat-progressbar.component.scss',
	templateUrl: './chat-progressbar.component.html',
})
export class ChatProgressbarComponent {
	public readonly progress = input<number>(0);
	public readonly showStats = input<boolean>(false);
}
