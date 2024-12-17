import AssetManageForm from './AssetManageForm.tsx';
import { useCreateAssetMutation } from '../../../core/data/services/api/asset-api.ts';
import { AssetForm } from '../../../core/data/entities/asset.ts';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

export default function AssetManageScreen() {
  const [createAsset, {isSuccess: createSuccess}] = useCreateAssetMutation();
  const navigate = useNavigate();

  useEffect(() => {
    if (createSuccess) {
      navigate(`/app/assets/list`);
    }
  }, [createSuccess]);

  const handleOnFormSubmit = (data: AssetForm) => {
    if (data.id) {
      return;
    }

    createAsset(data);
  }

  return (
    <AssetManageForm onSubmit={handleOnFormSubmit} />
  );
}