export function cleanFormValues<T>(data: T): T {
  if (data !== null && typeof data === 'object') {
    if ('value' in data && typeof data.value !== 'undefined') {
      return data.value as T;
    }

    if (Array.isArray(data)) {
      return data.map(cleanFormValues) as T;
    }

    return Object.keys(data).reduce((acc, key) => {
      (acc as Record<string, unknown>)[key] = cleanFormValues((data as Record<string, unknown>)[key]);
      return acc;
    }, {} as T);
  }

  return data;
}