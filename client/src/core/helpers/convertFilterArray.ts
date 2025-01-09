import { Key } from 'react';

type FilterValue = (Key | boolean)[];

export function flattenTableFilters(filters: Record<string, FilterValue | null>): Record<string, Key> {
  const result: Record<string, Key> = {};
  for (const key in filters) {
    const filterValue = filters[key];
    if (filterValue && filterValue.length > 0) {
      const firstValue = filterValue[0];
      if (typeof firstValue !== 'boolean') {
        result[key] = firstValue;
      }
    }
  }
  return result;
}