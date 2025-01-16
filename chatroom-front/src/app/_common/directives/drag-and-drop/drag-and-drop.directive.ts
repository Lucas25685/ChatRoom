import { Directive, HostListener, output } from '@angular/core';

@Directive({
	selector: '[appDragAndDrop]',
	standalone: true,
})
export class DragAndDropDirective {
	public readonly fileDropped = output<FileList>();

	constructor() {}

	@HostListener('dragover', ['$event'])
	public onDragHover(e: Event): void {
		e.preventDefault();
		e.stopPropagation();
	}

	@HostListener('dragleave', ['$event'])
	public onDragLeave(e: Event): void {
		e.preventDefault();
		e.stopPropagation();
	}

	@HostListener('drop', ['$event'])
	public onDrop(e: Event): void {
		e.preventDefault();
		e.stopPropagation();

		const files: FileList | null = (e.target as HTMLInputElement).files;

		if (files && files.length > 0) {
			this.fileDropped.emit(files);
		}
	}
}
