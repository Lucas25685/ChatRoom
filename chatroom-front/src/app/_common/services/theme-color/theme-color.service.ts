import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
})
export class ThemeColorService {
	private readonly themeColorKey: string = 'themeColor';

	private approvalThemeColor: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
		JSON.parse(localStorage.getItem(this.themeColorKey)!) || false
	);
	public currentThemeColor: Observable<boolean> = this.approvalThemeColor.asObservable();

	constructor(@Inject(DOCUMENT) private document: Document) {
		this.updateHTMLElement(this.getThemeColor());
	}

	private updateHTMLElement(state: boolean): void {
		state ? this.document.body.classList.add('dark') : this.document.body.classList.remove('dark');
	}

	public getThemeColor(): boolean {
		return this.approvalThemeColor.getValue();
	}

	public updateThemeColor(state: boolean): void {
		this.updateHTMLElement(state);
		this.approvalThemeColor.next(state);
		localStorage.setItem(this.themeColorKey, state.toString());
	}
}
