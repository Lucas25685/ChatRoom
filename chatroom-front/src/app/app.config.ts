import { ApplicationConfig, LOCALE_ID, provideZoneChangeDetection } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import localeFr from '@angular/common/locales/fr';
import { provideRouter } from '@angular/router';
import { provideNgIconsConfig, withExceptionLogger } from '@ng-icons/core';

import { routes } from './app.routes';
import { apiInterceptor } from './_common/interceptors/api/api.interceptor';

registerLocaleData(localeFr);

export const appConfig: ApplicationConfig = {
	providers: [
		provideHttpClient(withInterceptors([apiInterceptor])),
		provideNgIconsConfig({}, withExceptionLogger()),
		provideRouter(routes),
		provideZoneChangeDetection({ eventCoalescing: true }),
		{ provide: LOCALE_ID, useValue: navigator.language },
	],
};
