import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SITEMAP } from '../_common/sitemap';
import { ChatButtonComponent } from '../_common/components/chat-button/chat-button.component';

@Component({
	selector: 'app-not-found',
	standalone: true,
	imports: [RouterModule, ChatButtonComponent],
	templateUrl: './not-found.component.html',
})
export class NotFoundComponent {
	public readonly sitemap = SITEMAP;
}
