export function guidToBase64(guid: string): string {
	const hex: string = guid.replace(/-/g, '');
	const bytes: Uint8Array = new Uint8Array(hex.match(/.{1,2}/g)!.map((byte: string) => parseInt(byte, 16)));
	const base64: string = btoa(String.fromCharCode.apply(null, bytes as unknown as number[]));

	return base64.replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
}

export function base64ToGuid(base64: string): string {
	base64 = base64.replace(/-/g, '+').replace(/_/g, '/');

	const paddedBase64: string = `${base64}===`.slice(0, (4 - (base64.length % 4)) % 4);
	const binaryString: string = atob(paddedBase64);
	const hex: string = Array.from(binaryString)
		.map((char: string) => ('00' + char.charCodeAt(0).toString(16)).slice(-2))
		.join('');

	return `${hex.slice(0, 8)}-${hex.slice(8, 12)}-${hex.slice(12, 16)}-${hex.slice(16, 20)}-${hex.slice(20)}`;
}
