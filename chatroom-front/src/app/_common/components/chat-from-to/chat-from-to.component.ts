import { Component, input } from '@angular/core';

import type { Company } from '../../models/company.model';
import type { User } from '../../models/user.model';
import { getGravatarUrl } from '../../utils/get-gravatar-url.util';
import { ChatSvgIconComponent } from '../chat-svg-icon/chat-svg-icon.component';

@Component({
	selector: 'chat-from-to',
	standalone: true,
	imports: [ChatSvgIconComponent],
	templateUrl: './chat-from-to.component.html',
	styleUrl: './chat-from-to.component.scss',
})
export class ChatFromToComponent {
	public readonly from = input.required<User | Company>();
	public readonly to = input<Company[]>();

	public getIconUri(party?: User | Company | null): string {
		if (!party) {
			return 'https://www.gravatar.com/avatar/00000000000000000000000000000000?d=mp&f=y';
		}

		return getGravatarUrl((party as User).email ?? (party as Company).displayName, 64);
	}

	public getDisplayName(party?: User | Company | null): string {
		if (!party) {
			return 'Unknown';
		}

		return (party as User).firstName ?? (party as Company).displayName;
	}

	public getDisplayNames(parties: Company[]): string {
		return parties.map(p => p.displayName).join(', ');
	}
}
