import { Navigate, Route, Routes } from 'react-router-dom';
import AssetsListScreen from "./assets-list/AssetsListScreen.tsx";
import AssetManageScreen from "./assets-manage/AssetsManageScreen.tsx";

export default function AssetsRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="list" />} />
      <Route path="list" element={<AssetsListScreen />} />
      <Route path="manage" element={<AssetManageScreen />} />
    </Routes>
  );
}