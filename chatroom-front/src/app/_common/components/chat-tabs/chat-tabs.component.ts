import { Component, input, model } from '@angular/core';
import { MHPTab } from './chat-tab.interface';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'chat-tabs',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './chat-tabs.component.html',
	styleUrl: './chat-tabs.component.scss',
})
export class ChatTabsComponent<T = string | number> {
	public readonly tabs = input<MHPTab<T>[]>([]);
	public readonly choice = model<MHPTab<T>>();

	public choose(tab: MHPTab<T>): void {
		this.choice.set(tab);
	}
}
