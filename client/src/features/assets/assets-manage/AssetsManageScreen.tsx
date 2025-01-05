import AssetManageForm from './AssetManageForm.tsx';
import { useCreateAssetMutation, useUpdateAssetMutation } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetForm } from '../../../core/data/entities/asset/asset.ts';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import { useCombinedAssetMutations } from '../../../core/hooks/useCombineFormMutations.ts';

export default function AssetManageScreen() {
  const {isLoading, onSubmit, isSuccess} =
    useCombinedAssetMutations<AssetForm, Asset>(useCreateAssetMutation, useUpdateAssetMutation);
  const navigate = useNavigate();

  useEffect(() => {
    if (isSuccess) {
      navigate(`/app/assets`);
    }
  }, [isSuccess, navigate]);

  return (
    <AssetManageForm
      isLoading={isLoading}
      onSubmit={onSubmit}
    />
  );
}