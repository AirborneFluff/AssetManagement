import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { RootState } from './core/data/store.ts';
import { useSelector } from 'react-redux';

const LoginScreen = React.lazy(() => import('./core/components/LoginScreen.tsx'));
const AppScreen = React.lazy(() => import('./core/components/AppScreen.tsx'));

const App: React.FC = () => {
  const user = useSelector((state: RootState) => state.user.user);
  const isAuth = !!user;

  return (
    <Router>
      <React.Suspense fallback={<div>Loading...</div>}>
        <Routes>
          <Route path="/" element={<Navigate to={isAuth ? '/app' : '/login'} />} />

          <Route
            path="/login"
            element={isAuth ? <Navigate to="/app" replace /> : <LoginScreen />}
          />

          <Route
            path="/app"
            element={!isAuth ? <Navigate to="/login" replace /> : <AppScreen />}
          />
        </Routes>
      </React.Suspense>
    </Router>
  );
};

export default App;