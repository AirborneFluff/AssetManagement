import { Button } from 'antd';
import { ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';

interface ListScreenLayoutProps {
  children: ReactNode;
  addItemText?: string;
}

export function ListScreenLayout({children, addItemText}: ListScreenLayoutProps) {
  const navigate = useNavigate();

  const navigateToManage = () => navigate('../manage');

  return (
    <>
      <Button className='mb-4' onClick={navigateToManage}>{addItemText ?? 'Add'}</Button>
      {children}
    </>
  )
}