import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SITEMAP } from '../_common/sitemap';
import { ChatSvgLogoComponent } from '../_common/components/chat-svg-logo/chat-svg-logo.component';

@Component({
	selector: 'app-auth',
	standalone: true,
	imports: [RouterModule, ChatSvgLogoComponent],
	styleUrl: './auth.component.scss',
	templateUrl: './auth.component.html',
})
export class AuthComponent {
	public readonly sitemap = SITEMAP;

	constructor() {}
}
