import { Navigate, Route, Routes } from 'react-router-dom';
import AppLayout from './AppLayout.tsx';
import React, { ReactNode } from 'react';
import ContentLoading from './ContentLoading.tsx';
import { useSelector } from 'react-redux';
import { RootState } from '../data/store.ts';
import { AppModule } from '../data/entities/app-module.ts';

const AssetsHome = React.lazy(() => import('../../features/assets/AssetsRoutes.tsx'));
const SalesOrdersHome = React.lazy(() => import('../../features/sales-orders/SalesOrdersRoutes.tsx'));
const PurchaseOrdersHome = React.lazy(() => import('../../features/purchase-orders/PurchaseOrdersRoutes.tsx'));
const SystemHome = React.lazy(() => import('../../features/system/SystemRoutes.tsx'));

interface ProtectedRoute {
  requiredModule?: AppModule;
  element: ReactNode;
}

export default function AppRoutes() {
  const user = useSelector((state: RootState) => state.user.user);
  const isSuperUser = user?.role === 'SuperUser';

  const routes: ProtectedRoute[] = [
    {
      requiredModule: 'ASSET_MANAGEMENT',
      element: <Route path="assets/*" key='ASSET_MANAGEMENT' element={<AssetsHome />} />
    },
    {
      requiredModule: 'PURCHASE_ORDERS',
      element: <Route path="purchaseOrders/*" key='PURCHASE_ORDERS' element={<PurchaseOrdersHome />} />
    },
    {
      requiredModule: 'SALES_ORDERS',
      element: <Route path="salesOrders/*" key='SALES_ORDERS' element={<SalesOrdersHome />} />
    }
  ]

  const renderRoutes = () => {
    return routes.map((route) => {
      if (!route.requiredModule || (user && user.modules.includes(route.requiredModule))) {
        return route.element;
      } else {
        return null;
      }
    });
  }

  return (
    <AppLayout>
      <React.Suspense fallback={<ContentLoading />}>
        <Routes>
          <Route path="/" element={<Navigate to='assets' />} />
          {renderRoutes()}
          {isSuperUser && <Route path="system/*" element={<SystemHome />} />}
        </Routes>
      </React.Suspense>
    </AppLayout>
  )
}