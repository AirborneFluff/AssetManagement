import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';
import { ReactElement } from 'react';

interface ListScreenLayoutProps {
  table: ReactElement;
  addItemText?: string;
}

export function ListScreenLayout({table, addItemText}: ListScreenLayoutProps) {
  const navigate = useNavigate();

  const navigateToManage = () => navigate('manage');

  return (
    <>
      <Button className='mb-4' onClick={navigateToManage}>{addItemText ?? 'Add'}</Button>
      {table}
    </>
  )
}