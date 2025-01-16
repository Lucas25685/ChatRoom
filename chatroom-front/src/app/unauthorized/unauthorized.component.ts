import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SITEMAP } from '../_common/sitemap';
import { ChatButtonComponent } from '../_common/components/chat-button/chat-button.component';

@Component({
	selector: 'app-unauthorized',
	standalone: true,
	imports: [RouterModule, ChatButtonComponent],
	templateUrl: './unauthorized.component.html',
})
export class UnauthorizedComponent {
	public readonly sitemap = SITEMAP;
}
