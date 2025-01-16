import crypto from 'crypto-js';
import { Component, HostListener, inject, input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import {
	saxSmsOutline,
	saxNotificationOutline,
	saxArrowDown1Outline,
	saxLogout1Outline,
	saxProfileCircleOutline,
} from '@ng-icons/iconsax/outline';

import { SITEMAP } from 'src/app/_common/sitemap';
import { User } from 'src/app/_common/models/user.model';
import { AccountService } from 'src/app/_common/services/account/account.service';

@Component({
	selector: 'app-main-header-account',
	standalone: true,
	imports: [RouterModule, NgIconComponent],
	providers: [
		provideIcons({
			saxArrowDown1Outline,
			saxLogout1Outline,
			saxNotificationOutline,
			saxProfileCircleOutline,
			saxSmsOutline,
		}),
		provideNgIconsConfig({ size: '1.5rem' }),
	],
	styleUrl: './main-header-account.component.scss',
	templateUrl: './main-header-account.component.html',
})
export class MainHeaderAccountComponent {
	private readonly _accountSvc = inject(AccountService);

	public readonly sitemap = SITEMAP;

	public readonly user = input.required<User>();

	public clickInside: boolean = false;
	public isSubMenuOpen: boolean = false;
	public newMessage: boolean = false;
	public newNotification: boolean = true;

	constructor() {}

	public signOut(): void {
		this._accountSvc.signOut();
	}

	public toggleMenu(): void {
		this.isSubMenuOpen = !this.isSubMenuOpen;
		setTimeout(() => (this.clickInside = false), 100);
	}

	@HostListener('document:click')
	public closeSubMenu(): void {
		if (!this.clickInside) this.isSubMenuOpen = false;
	}

	public getGravatarUrl(email: string, size: number = 64): string {
		const trimmedEmail: string = email.trim().toLowerCase();
		const hash: string = crypto.SHA256(trimmedEmail).toString();

		return `https://www.gravatar.com/avatar/${hash}?s=${size}&d=identicon`;
	}
}
