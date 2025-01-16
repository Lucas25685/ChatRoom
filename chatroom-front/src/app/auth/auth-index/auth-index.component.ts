import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { SITEMAP } from 'src/app/_common/sitemap';

@Component({
	selector: 'app-auth-index',
	standalone: true,
	imports: [],
	templateUrl: './auth-index.component.html',
})
export class AuthIndexComponent {
	private readonly router = inject(Router);

	public readonly sitemap = SITEMAP;

	constructor() {
		if (this.router.url === this.sitemap.auth.route) this.router.navigate([this.sitemap.login.route]);
	}
}
