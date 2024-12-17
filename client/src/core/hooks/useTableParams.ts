import { useState } from "react";

export interface BaseTableParams {
  pageNumber: number;
  pageSize: number;
  sortOrder: 'descend' | 'ascend';
  sortField?: string | undefined;
}

/**
 * Hook for managing table parameters (pagination, filters, custom query params)
 * @param initialParams - Optional initial parameters to set
 * @returns An object containing the parameters, and handlers to update or reset them
 */
export default function useTableParams<T extends BaseTableParams>(
  initialParams: Partial<Record<keyof T, string | number>> = {}
) {
  // Default values for pageNumber and pageSize
  const defaultInitialParams: BaseTableParams = {
    pageNumber: 1,
    pageSize: 10,
    sortOrder: 'ascend'
  };

  // Merge defaults with user-provided initial params
  const [params, setParams] = useState<Record<keyof T, string | number>>({
    ...(defaultInitialParams as Record<keyof T, string | number>),
    ...initialParams,
  });

  /**
   * Update specific table parameters
   * @param updatedParams A partial record of updated parameters
   */
  const updateParams = (updatedParams: Partial<Record<keyof T, string | number>>) => {
    setParams((prev) => ({
      ...prev,
      ...updatedParams,
    }));
  };

  /**
   * Reset table parameters to the initial state
   */
  const resetParams = () => {
    setParams({
      ...(defaultInitialParams as Record<keyof T, string | number>),
      ...initialParams,
    });
  };

  return {
    params,
    updateParams,
    resetParams,
  };
}