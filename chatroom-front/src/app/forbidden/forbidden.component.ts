import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SITEMAP } from '../_common/sitemap';
import { ChatButtonComponent } from '../_common/components/chat-button/chat-button.component';

@Component({
	selector: 'app-forbidden',
	standalone: true,
	imports: [RouterModule, ChatButtonComponent],
	templateUrl: './forbidden.component.html',
})
export class ForbiddenComponent {
	public readonly sitemap = SITEMAP;

	constructor() {}
}
