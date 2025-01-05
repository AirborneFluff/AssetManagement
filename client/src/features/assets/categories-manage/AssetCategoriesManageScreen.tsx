import {
  useCreateAssetCategoryMutation,
  useUpdateAssetCategoryMutation
} from '../../../core/data/services/api/asset-api.ts';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import AssetCategoryManageForm from './AssetCategoryManageForm.tsx';
import { AssetCategory, AssetCategoryForm } from '../../../core/data/entities/asset/asset-category.ts';
import { useCombinedAssetMutations } from '../../../core/hooks/useCombineFormMutations.ts';

export default function AssetCategoryManageScreen() {
  const {isLoading, onSubmit, isSuccess} =
    useCombinedAssetMutations<AssetCategoryForm, AssetCategory>(useCreateAssetCategoryMutation, useUpdateAssetCategoryMutation);
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      navigate(`/app/assets/categories`);
    }
  }, [isSuccess, navigate]);

  return (
    <AssetCategoryManageForm
      isLoading={isLoading}
      onSubmit={onSubmit}
    />
  );
}