import crypto from 'crypto-js';

export function getGravatarUrl(email?: string, size: number = 64): string {
	if (!email) return 'https://www.gravatar.com/avatar/00000000000000000000000000000000?d=mp&f=y';

	const trimmedEmail: string = email.trim().toLowerCase();
	const hash: string = crypto.SHA256(trimmedEmail).toString();

	return `https://www.gravatar.com/avatar/${hash}?s=${size}&d=identicon`;
}
