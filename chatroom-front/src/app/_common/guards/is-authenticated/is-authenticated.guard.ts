import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { User } from '../../models/user.model';
import { AccountService } from '../../services/account/account.service';
import { SITEMAP } from '../../sitemap';

export const isAuthenticatedGuard: CanActivateFn = async (route, state) => {
	const router: Router = inject(Router);
	const _account: AccountService = inject(AccountService);

	await _account.checkAuth();
	const user: User | null = _account.user();

	if (user) return true;

	router.navigate([SITEMAP.login.route]);
	return false;
};
