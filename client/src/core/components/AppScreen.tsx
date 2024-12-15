import { Navigate, Route, Routes } from 'react-router-dom';
import AppLayout from './AppLayout.tsx';
import React from 'react';
import ContentLoading from './ContentLoading.tsx';

const AssetsHome = React.lazy(() => import('../../features/assets/AssetsHome.tsx'));

export default function AppScreen() {
  return (
    <AppLayout>
      <React.Suspense fallback={<ContentLoading />}>
        <Routes>
          <Route path="/" element={<Navigate to='assets' />} />
          <Route path="/assets" element={<AssetsHome />} />
        </Routes>
      </React.Suspense>
    </AppLayout>
  )
}