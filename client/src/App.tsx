import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { RootState } from './core/data/store.ts';
import { useSelector } from 'react-redux';
import ContentLoading from './core/components/ContentLoading.tsx';

const LoginScreen = React.lazy(() => import('./core/components/LoginScreen.tsx'));
const AppRoutes = React.lazy(() => import('./core/components/AppRoutes.tsx'));

const App: React.FC = () => {
  const user = useSelector((state: RootState) => state.user.user);
  const isAuth = !!user;

  return (
    <Router>
      <React.Suspense fallback={<ContentLoading />}>
        <Routes>
          <Route path="/" element={<Navigate to={isAuth ? '/app' : '/login'} />} />
          <Route
            path="login"
            element={isAuth ? <Navigate to="/app" replace /> : <LoginScreen />}
          />
          <Route
            path="app/*"
            element={!isAuth ? <Navigate to="/login" replace /> : <AppRoutes />}
          />
        </Routes>
      </React.Suspense>
    </Router>
  );
};

export default App;