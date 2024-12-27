import { Route, Routes } from 'react-router-dom';
import AssetsListScreen from "./assets-list/AssetsListScreen.tsx";
import AssetManageScreen from "./assets-manage/AssetsManageScreen.tsx";
import AssetCategoryManageScreen from './categories-manage/AssetCategoriesManageScreen.tsx';
import AssetCategoriesListScreen from './categories-list/AssetCategoriesListScreen.tsx';

export default function AssetsRoutes() {
  return (
    <Routes>
      <Route path="/" element={<AssetsListScreen />} />
      <Route path="manage" element={<AssetManageScreen />} />
      <Route path="categories" element={<AssetCategoriesListScreen />} />
      <Route path="categories/manage" element={<AssetCategoryManageScreen />} />
    </Routes>
  );
}