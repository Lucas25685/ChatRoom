import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ThemeColorService } from './_common/services/theme-color/theme-color.service';

@Component({
	selector: 'app-root',
	standalone: true,
	imports: [RouterModule],
	templateUrl: './app.component.html',
	styleUrl: './app.component.scss',
})
export class AppComponent {
	private readonly _themeColorSvc = inject(ThemeColorService);
}
