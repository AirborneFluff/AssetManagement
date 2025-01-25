import { ReactNode } from 'react';
import { MenuProps } from 'antd';
import { Layout, Menu } from 'antd';
import { useNavigate } from 'react-router-dom';
import AppHeader from './AppHeader.tsx';
import { AppModule } from '../data/entities/app-module.ts';
import { useSelector } from 'react-redux';
import { RootState } from '../data/store.ts';

const { Content, Sider } = Layout;

interface MenuSection {
  label: string;
  baseRoute: string;
  children: {label: string, route: string}[];
  requiredModule?: AppModule;
  requiresSystemAccess?: boolean;
}

export default function AppLayout({children}: {children: ReactNode}) {
  const user = useSelector((state: RootState) => state.user.user);
  const isSuperUser = user?.role === 'SuperUser';
  const navigate = useNavigate();

  const menuConfig: MenuSection[] = [
    {
      label: 'Assets',
      baseRoute: '/assets',
      children: [
        {label: 'Assets', route: '/'},
        {label: 'Categories', route: '/categories'},
        {label: 'Suppliers', route: '/supplier'},
      ],
      requiredModule: 'ASSET_MANAGEMENT'
    },
    {
      label: 'Purchase Orders',
      baseRoute: '/purchaseOrders',
      children: [
        {label: 'Test', route: '/'},
      ],
      requiredModule: 'PURCHASE_ORDERS'
    },
    {
      label: 'Sales Orders',
      baseRoute: '/salesOrders',
      children: [
        {label: 'Test', route: '/'},
      ],
      requiredModule: 'SALES_ORDERS'
    },
    {
      label: 'System',
      baseRoute: '/system',
      children: [
        {label: 'Test', route: '/'},
      ],
      requiresSystemAccess: true
    },
  ];

  function buildMenuItems(config: MenuSection[]): MenuProps['items'] {
    const menuItems: MenuProps['items'] = [];

    config.forEach((section) => {
      if (section.requiresSystemAccess && !isSuperUser) return;
      if (section.requiredModule && (!user || !user.modules.includes(section.requiredModule))) return;

      if (menuItems.length === 0) {
        menuItems.push({ type: 'divider' });
      }
      const group = {
        label: section.label,
        type: 'group',
        key: `${section.baseRoute}-group`,
        children: section.children.map((child) => ({
          label: child.label,
          key: `${section.baseRoute}${child.route}`,
        })),
      };

      menuItems.push(group as never);
      menuItems.push({ type: 'divider' });
    });

    return menuItems;
  }

  return (
    <Layout style={{ height: '100dvh' }}>
      <Sider width={250} className='bg-white border-r border-gray-200'>
        <div className='rounded-lg w-9/12 h-12 bg-gray-200 mx-auto my-8'/>
        <Menu
          mode="inline"
          defaultSelectedKeys={['/assets/']}
          items={buildMenuItems(menuConfig)}
          onClick={({key}) => navigate(`/app${key}`)}
        />
      </Sider>
      <Layout>
        <AppHeader />
        <Content className='p-6 m-2 mb-4 bg-white rounded-lg overflow-y-auto'>
          {children}
        </Content>
      </Layout>
    </Layout>
  )
};