import { useCreateAssetCategoryMutation } from '../../../core/data/services/api/asset-api.ts';
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';
import AssetCategoryManageForm from './AssetCategoryManageForm.tsx';
import { AssetCategoryForm } from '../../../core/data/entities/asset/asset-category.ts';

export default function AssetCategoryManageScreen() {
  const [createCategory, {isSuccess: createSuccess}] = useCreateAssetCategoryMutation();
  const navigate = useNavigate();

  useEffect(() => {
    if (createSuccess) {
      navigate(`/app/assets/categories`);
    }
  }, [createSuccess, navigate]);

  const handleOnFormSubmit = (data: AssetCategoryForm) => {
    if (data.id) {
      return;
    }

    createCategory(data);
  }

  return (
    <AssetCategoryManageForm onSubmit={handleOnFormSubmit} />
  );
}