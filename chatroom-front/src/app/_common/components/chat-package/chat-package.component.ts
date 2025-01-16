import { Component, input } from '@angular/core';
import { ChatSvgIconComponent } from '../chat-svg-icon/chat-svg-icon.component';
import { ToCipPipe } from "../../pipes/to-cip/to-cip.pipe";

@Component({
	selector: 'chat-package',
	standalone: true,
	imports: [
		ChatSvgIconComponent,
		ToCipPipe
	],
	templateUrl: './chat-package.component.html',
	styleUrl: './chat-package.component.scss'
})
export class ChatPackageComponent {
	public readonly package = input.required<{ cip13: number, shortName: string | null }>()
}
