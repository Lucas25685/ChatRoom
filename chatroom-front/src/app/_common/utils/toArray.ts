/**
 * Converts a dictionary to array
 */
export function toArray<T, K extends string | number | symbol>(dictionary: Record<K, T[]>): Array<T> {
	return <T[]>Object.values(dictionary).reduce((acc: T[], items) => (<T[]>acc).concat(<T[]>items), []);
}
