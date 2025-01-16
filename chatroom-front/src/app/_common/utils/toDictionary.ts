/**
 * Converts an array to a dictionary where each element is added to an array under a key determined
 * by the keySelector function.
 * dico[key] = [item1, item2, ...]
 */
export function toDictionarySet<T, K extends string | number | symbol>(
	array: T[],
	keySelector: (item: T) => K
): Record<K, T[]> {
	return array.reduce((acc, item: T) => {
		if (!acc[keySelector(item)]) acc[keySelector(item)] = [];
		acc[keySelector(item)].push(item);
		return acc;
	}, {} as Record<K, T[]>);
}

/**
 * Converts an array to a dictionary where each element matches the key determined
 * by the keySelector function.
 * If key is duplicated in array, only the first one will be kept.
 * dico[key] = item1
 */
export function toDictionary<T, K extends string | number | symbol>(
	array: T[],
	keySelector: (item: T) => K
): Record<K, T> {
	return array.reduce((acc, item: T) => {
		if (!acc[keySelector(item)]) acc[keySelector(item)] = item;
		return acc;
	}, {} as Record<K, T>);
}
