import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ThemeColorService } from '../../services/theme-color/theme-color.service';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { bootstrapSunFill, bootstrapMoonFill } from '@ng-icons/bootstrap-icons';

@Component({
	selector: 'chat-theme-switch',
	standalone: true,
	imports: [CommonModule, FormsModule, NgIconComponent],
	providers: [
		provideIcons({
			bootstrapMoonFill,
			bootstrapSunFill,
		}),
		provideNgIconsConfig({ size: '.75rem' }),
	],
	styleUrl: './chat-theme-switch.component.scss',
	templateUrl: './chat-theme-switch.component.html',
})
export class ChatThemeSwitchComponent {
	private readonly _themeColor = inject(ThemeColorService);

	public themeColor: boolean = false;

	constructor() {
		this._themeColor.currentThemeColor.subscribe((themeColor: boolean) => {
			this.themeColor = themeColor;
		});
	}

	public updateThemeColor(): void {
		this._themeColor.updateThemeColor(this.themeColor);
	}
}
