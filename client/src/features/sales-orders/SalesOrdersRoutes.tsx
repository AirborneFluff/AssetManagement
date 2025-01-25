import { Route, Routes } from 'react-router-dom';
import SalesOrderTestScreen from './test-route/SalesOrderTestScreen.tsx';

export default function SalesOrdersRoutes() {
  return (
    <Routes>
      <Route path="/" element={<SalesOrderTestScreen />} />
    </Routes>
  );
}