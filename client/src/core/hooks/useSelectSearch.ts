import { useState } from 'react';
import useDebounce from "../../core/hooks/useDebounce.ts";

export function useSelectSearch(paramName: string) {
  const [searchQuery, setSearchQuery] = useState<string>('');
  const debouncedSearchQuery = useDebounce(searchQuery, 300);

  const result: Record<string, string> = {
    pageSize: '10',
    pageNumber: '1',
  };

  if (debouncedSearchQuery.length > 3) {
    result[paramName] = debouncedSearchQuery;
  }

  const onSearch = (query: string) => {
    setSearchQuery(query);
  };

  return {
    onSearch,
    params: result,
  };
}