import { Component, computed, HostListener, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { NgIconComponent, provideIcons, provideNgIconsConfig } from '@ng-icons/core';
import { saxSearchNormal1Outline, saxHome2Outline, saxBook1Outline } from '@ng-icons/iconsax/outline';
import { saxSearchNormal1Bulk, saxHome2Bulk, saxBookBulk } from '@ng-icons/iconsax/bulk';

import { SITEMAP } from 'src/app/_common/sitemap';
import { User } from 'src/app/_common/models/user.model';
import { AccountService } from 'src/app/_common/services/account/account.service';

import { ChatThemeSwitchComponent } from '../../_common/components/chat-theme-switch/chat-theme-switch.component';
import { ChatSvgLogoComponent } from '../../_common/components/chat-svg-logo/chat-svg-logo.component';
import { MainHeaderAccountComponent } from './main-header-account/main-header-account.component';
import { ChatLabelComponent } from '../../_common/components/chat-label/chat-label.component';

@Component({
	selector: 'app-main-header',
	standalone: true,
	imports: [
		CommonModule,
		RouterModule,
		NgIconComponent,
		ChatSvgLogoComponent,
		ChatThemeSwitchComponent,
		MainHeaderAccountComponent,
		ChatLabelComponent,
	],
	providers: [
		provideIcons({
			saxBookBulk,
			saxHome2Bulk,
			saxSearchNormal1Bulk,
			saxBook1Outline,
			saxHome2Outline,
			saxSearchNormal1Outline,
		}),
		provideNgIconsConfig({ size: '1.5rem' }),
	],
	styleUrl: './main-header.component.scss',
	templateUrl: './main-header.component.html',
})
export class MainHeaderComponent {
	public readonly router = inject(Router);
	private readonly _accountSvc = inject(AccountService);

	public readonly sitemap = SITEMAP;

	public readonly user = computed<User | null>(this._accountSvc.user);

	public isMenuOpened: boolean = false;
	public isSubMenuOpened: boolean[] = [];
	public currentURL: string = '';
	public searchModal: boolean = false;

	constructor() {}

	@HostListener('document:keydown', ['$event'])
	public triggerOpenSearchModal(e: any): void {
		if (e.ctrlKey && e.key === 'k') {
			e.preventDefault();
			this.openSearchModal();
		}
	}

	public openSearchModal(): void {
		if (!this.user()) {
			this.router.navigate([this.sitemap.login.route]);
			return;
		}

		this.searchModal = true;
	}
}
