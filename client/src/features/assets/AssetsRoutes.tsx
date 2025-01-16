import { Route, Routes } from 'react-router-dom';
import AssetsListScreen from "./assets-list/AssetsListScreen.tsx";
import AssetManageScreen from "./assets-manage/AssetsManageScreen.tsx";
import AssetCategoryManageScreen from './categories-manage/AssetCategoriesManageScreen.tsx';
import AssetCategoriesListScreen from './categories-list/AssetCategoriesListScreen.tsx';
import AssetSuppliersListScreen from './suppliers-list/AssetSuppliersListScreen.tsx';
import AssetSuppliersManageScreen from './suppliers-manage/AssetSuppliersManageScreen.tsx';
import AssetPreviewScreen from './asset-preview/AssetPreviewScreen.tsx';

export default function AssetsRoutes() {
  return (
    <Routes>
      <Route path="/" element={<AssetsListScreen />} />
      <Route path="/:assetId" element={<AssetPreviewScreen />} />
      <Route path="manage/:assetId" element={<AssetManageScreen />} />
      <Route path="manage/" element={<AssetManageScreen />} />
      <Route path="categories" element={<AssetCategoriesListScreen />} />
      <Route path="categories/manage" element={<AssetCategoryManageScreen />} />
      <Route path="categories/manage/:categoryId" element={<AssetCategoryManageScreen />} />
      <Route path="suppliers" element={<AssetSuppliersListScreen />} />
      <Route path="suppliers/manage" element={<AssetSuppliersManageScreen />} />
      <Route path="suppliers/manage/:supplierId" element={<AssetSuppliersManageScreen />} />
    </Routes>
  );
}