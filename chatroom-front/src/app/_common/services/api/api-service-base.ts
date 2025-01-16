import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { environment } from 'src/environments/environment';

export class ApiServiceBase {
	protected readonly http = inject(HttpClient);
	protected readonly API_URL: string = environment.API_URL;
}
