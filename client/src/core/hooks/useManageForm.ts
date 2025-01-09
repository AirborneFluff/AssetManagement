import { useEffect, useCallback } from 'react';

type LazyQueryHook<T> = () => [
  (id: string) => void,
  { data?: T; isFetching: boolean; isError: boolean },
  unknown
];

interface UseLoadFormProps<T> {
  id?: string;
  queryHook: LazyQueryHook<T>;
  onSuccess: (data: T) => void;
}

export function useManageForm<T>({ id, queryHook, onSuccess }: UseLoadFormProps<T>) {
  const [triggerQuery, { data, isError, isFetching }] = queryHook();

  const stableTriggerQuery = useCallback((id: string) => {
    triggerQuery(id);
  }, [triggerQuery]);

  useEffect(() => {
    if (id) {
      stableTriggerQuery(id);
    }
  }, [id, stableTriggerQuery]);

  const handleSuccess = useCallback(
    (data: T) => {
      if (!isError && data) {
        onSuccess(data);
      }
    },
    [isError, onSuccess]
  );

  useEffect(() => {
    if (data) {
      handleSuccess(data);
    }
  }, [data, handleSuccess]);

  return {
    formLoading: isFetching,
  };
}