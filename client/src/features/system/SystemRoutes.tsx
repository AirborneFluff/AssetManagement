import { Route, Routes } from 'react-router-dom';
import TenantsListScreen from './tenants-list/TenantsListScreen.tsx';

export default function SystemRoutes() {
  return (
    <Routes>
      <Route path="/" element={<TenantsListScreen />} />
    </Routes>
  );
}