import crypto from 'crypto-js';
import { Component, computed, inject } from '@angular/core';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { saxEditOutline } from '@ng-icons/iconsax/outline';

import { User } from 'src/app/_common/models/user.model';
import { AccountService } from 'src/app/_common/services/account/account.service';
import { ChatInputComponent } from 'src/app/_common/components/chat-input/chat-input.component';

@Component({
	selector: 'app-main-account',
	standalone: true,
	imports: [NgIconComponent, ChatInputComponent],
	providers: [
		provideIcons({
			saxEditOutline,
		}),
		provideNgIconsConfig({ size: '1.25rem' }),
	],
	styleUrl: './main-account.component.scss',
	templateUrl: './main-account.component.html',
})
export class MainAccountComponent {
	private readonly _accountSvc = inject(AccountService);

	public readonly user = computed<User | null>(this._accountSvc.user);

	constructor() {}

	public getGravatarUrl(email: string, size: number = 64): string {
		const trimmedEmail: string = email.trim().toLowerCase();
		const hash: string = crypto.SHA256(trimmedEmail).toString();

		return `https://www.gravatar.com/avatar/${hash}?s=${size}&d=identicon`;
	}
}
