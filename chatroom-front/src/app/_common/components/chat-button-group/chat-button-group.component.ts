import { Component, input, model } from '@angular/core';
import { MHPButton } from './chat-button.interface';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'chat-button-group',
	standalone: true,
	imports: [CommonModule],
	styleUrl: './chat-button-group.component.scss',
	templateUrl: './chat-button-group.component.html',
})
export class ChatButtonGroupComponent<T> {
	public readonly buttons = input<MHPButton<T>[]>([]);
	public readonly choice = model<T>();

	public choose(choice: T): void {
		this.choice.set(choice);
	}
}
