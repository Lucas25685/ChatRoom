import { Component, model } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
	selector: 'chat-switch',
	standalone: true,
	imports: [FormsModule],
	styleUrl: './chat-switch.component.scss',
	templateUrl: './chat-switch.component.html',
})
export class ChatSwitchComponent {
	public readonly value = model<boolean>(false);
}
