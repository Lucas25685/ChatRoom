import { CommonModule } from '@angular/common';
import { Component, output } from '@angular/core';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { saxElement3Bold, saxRowVerticalBold } from '@ng-icons/iconsax/bold';

@Component({
	selector: 'chat-view-switch',
	standalone: true,
	imports: [CommonModule, NgIconComponent],
	providers: [
		provideIcons({
			saxElement3Bold,
			saxRowVerticalBold,
		}),
		provideNgIconsConfig({ size: '1rem' }),
	],
	styleUrl: './chat-view-switch.component.scss',
	templateUrl: './chat-view-switch.component.html',
})
export class ChatViewSwitchComponent {
	public readonly choice = output<number>();

	public readonly buttons: number[] = [1, 2];
	public selected: number = this.buttons[0];

	public choose(value: number): void {
		this.selected = value;
		this.choice.emit(value);
	}
}
