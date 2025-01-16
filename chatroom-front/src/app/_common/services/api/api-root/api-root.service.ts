import { inject, Injectable, Signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { toSignal } from '@angular/core/rxjs-interop';

import { environment } from 'src/environments/environment';

@Injectable({
	providedIn: 'root',
})
export class ApiRootService {
	private readonly http = inject(HttpClient);

	private readonly API_URL: string = environment.API_URL;

	constructor() {}

	public getStatus(): Signal<string> {
		return toSignal(
			this.http.get<string>(`${this.API_URL}/status`, { responseType: 'ancien_json' as 'json' }), // Ceux qui savent, savent ;D
			{ initialValue: '' }
		);
	}
}
