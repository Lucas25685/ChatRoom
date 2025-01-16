import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

import { User } from 'src/app/_common/models/user.model';
import { UserDto } from 'src/app/_common/dto/user.dto';

import { environment } from 'src/environments/environment';
import { ApiServiceBase } from '../api-service-base';

@Injectable({
	providedIn: 'root',
})
export class ApiUserService extends ApiServiceBase {
	private readonly USER_ROUTE: string = 'user';

	constructor() {
		super();
	}

	public getUser(): Promise<HttpResponse<User>> {
		return firstValueFrom(this.http.get<User>(`${this.API_URL}/${this.USER_ROUTE}/self`, { observe: 'response' }));
	}
}
