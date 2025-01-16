import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ApiServiceBase } from '../api-service-base';

@Injectable({
	providedIn: 'root',
})
export class ApiAuthService extends ApiServiceBase {
	private readonly AUTH_ROUTE: string = 'auth';

	constructor() {
		super();
	}

	public getLoginProvider(provider: 'Microsoft' | 'Google', redirectUri: string = window.location.origin): void {
		window.location.href = `${this.API_URL}/${this.AUTH_ROUTE}/login/${provider}?redirectUri=${encodeURIComponent(
			redirectUri
		)}`;
	}

	/**
	 * @description Sign out the user from the api server
	 * @returns 200 OK if successful
	 */
	public signOut(): Promise<HttpResponse<string>> {
		return firstValueFrom(
			this.http.post<string>(`${this.API_URL}/${this.AUTH_ROUTE}/logout`, {}, { observe: 'response' })
		);
	}
}
