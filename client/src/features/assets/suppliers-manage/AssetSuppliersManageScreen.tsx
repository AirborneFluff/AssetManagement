import {
  useCreateAssetSupplierMutation,
  useUpdateAssetSupplierMutation
} from '../../../core/data/services/api/asset-api.ts';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import AssetSupplierManageForm from './AssetSupplierManageForm.tsx';
import { AssetCategory, AssetCategoryForm } from '../../../core/data/entities/asset/asset-category.ts';
import { useCombinedAssetMutations } from '../../../core/hooks/useCombineFormMutations.ts';

export default function AssetSuppliersManageScreen() {
  const {isLoading, onSubmit, isSuccess} =
    useCombinedAssetMutations<AssetCategoryForm, AssetCategory>(useCreateAssetSupplierMutation, useUpdateAssetSupplierMutation);
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      navigate(`/app/assets/suppliers`);
    }
  }, [isSuccess, navigate]);

  return (
    <AssetSupplierManageForm
      isLoading={isLoading}
      onSubmit={onSubmit}
    />
  );
}