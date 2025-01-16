export interface User {
	id: string;
	isApproved: boolean;
	email: string;
	firstName: string;
	lastName: string;
	phoneNumber: string;
	roles: string[] | null;
}
