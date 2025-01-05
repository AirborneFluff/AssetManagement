import { BaseEntity } from '../data/entities/base-entity.ts';

interface UseCombinedAssetMutations<T> {
  onSubmit: (data: T) => void;
  isSuccess: boolean;
  isLoading: boolean;
}

type MutationHook<T, K> = () => readonly [
  (data: T) => void,
  { data?: K | undefined; isLoading: boolean; isError: boolean, isSuccess: boolean; }
];

export function useCombinedAssetMutations<T extends {
  id?: string
}, K extends BaseEntity>(createHook: MutationHook<T, K>, updateHook: MutationHook<T, K>): UseCombinedAssetMutations<T> {
  const [createMutation, {isLoading: isCreateLoading, isSuccess: isCreateSuccess}] =
    createHook();

  const [updateMutation, {isLoading: isUpdateLoading, isSuccess: isUpdateSuccess}] =
    updateHook();

  const isLoading = isCreateLoading || isUpdateLoading;
  const isSuccess = isCreateSuccess || isUpdateSuccess;

  const create = (data: T) => {
    createMutation(data);
  };

  const update = (data: T) => {
    updateMutation(data);
  };

  const handleOnFormSubmit = (data: T) => {
    if (data?.id) {
      update(data);
      return;
    }

    create(data);
  };

  return {
    onSubmit: handleOnFormSubmit,
    isLoading,
    isSuccess
  };
}