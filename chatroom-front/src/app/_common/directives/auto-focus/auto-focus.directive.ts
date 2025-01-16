import { Directive, ElementRef } from '@angular/core';

@Directive({
	selector: '[appAutoFocus]',
	standalone: true,
})
export class AutoFocusDirective {
	constructor(private elementRef: ElementRef) {
		window.setTimeout(() => this.elementRef.nativeElement.focus(), 50);
	}
}
