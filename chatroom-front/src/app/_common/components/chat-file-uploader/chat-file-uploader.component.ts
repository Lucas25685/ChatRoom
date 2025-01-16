import { Component, input, model, output, signal } from '@angular/core';
import { NgIcon, provideIcons } from "@ng-icons/core";
import { ChatSvgIconComponent } from "../chat-svg-icon/chat-svg-icon.component";
import { DragAndDropDirective } from "../../directives/drag-and-drop/drag-and-drop.directive";
import { I18nPluralPipe } from "@angular/common";
import { saxImportOutline, saxInfoCircleOutline, saxTrashOutline } from "@ng-icons/iconsax/outline";
import { saxTickCircleBold } from "@ng-icons/iconsax/bold";

@Component({
  selector: 'chat-file-uploader',
  standalone: true,
	imports: [
		NgIcon,
		ChatSvgIconComponent,
		DragAndDropDirective,
		I18nPluralPipe,
	],
	providers: [
		provideIcons({
			saxImportOutline,
			saxInfoCircleOutline,
			saxTickCircleBold,
			saxTrashOutline,
		})
	],
  templateUrl: './chat-file-uploader.component.html',
  styleUrl: './chat-file-uploader.component.scss'
})
export class ChatFileUploaderComponent {
	public readonly acceptedFiles = input<string[]>();

	public readonly file = model<File | null>(null);

	protected readonly isFileInputFilled = signal<boolean>(false);
	public readonly isLoading = signal<boolean>(false);

	public readonly onClear = output<void>();


	private processingFiles(files: FileList) {
		if (files && files.length) {
			const file = files.item(0);

			if (file) {
				this.file.set(file);
				return;
			}
		}

		this.file.set(null);
	}

	protected inputDroppedFileProcess(files: FileList) {
		this.isFileInputFilled.set(true);

		this.processingFiles(files);
		console.log(files);
	}

	protected inputFileProcess(eventTarget: EventTarget | null) {
		this.isFileInputFilled.set(true);

		if (eventTarget) {
			const files = (<HTMLInputElement>eventTarget).files;

			if (files) {
				this.processingFiles(files);
			}
		}
	}

	protected clearFile() {
		this.file.set(null);
		this.isFileInputFilled.set(false);
		this.onClear.emit();
	}
}
