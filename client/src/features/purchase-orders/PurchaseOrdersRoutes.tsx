import { Route, Routes } from 'react-router-dom';
import PurchaseOrderTestScreen from './test-route/PurchaseOrderTestScreen.tsx';

export default function PurchaseOrdersRoutes() {
  return (
    <Routes>
      <Route path="/" element={<PurchaseOrderTestScreen />} />
    </Routes>
  );
}