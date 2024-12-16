import AssetManageForm from './AssetManageForm.tsx';
import { useCreateAssetMutation } from '../../../core/data/services/api/asset-api.ts';
import { AssetForm } from '../../../core/data/entities/asset.ts';

export default function AssetManageScreen() {
  const [createAsset] = useCreateAssetMutation();

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