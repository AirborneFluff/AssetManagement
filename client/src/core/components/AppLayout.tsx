import React, { ReactNode } from 'react';
import {
  LogoutOutlined,
  AuditOutlined,
  DashboardOutlined,
  PieChartOutlined
} from '@ant-design/icons';
import { Button, MenuProps } from 'antd';
import { Layout, Menu } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';
import { useLocation, useNavigate } from 'react-router-dom';

const { Header, Content, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

interface MenuItemConfig {
  label: string;
  key?: string;
  icon?: React.ReactNode;
  children?: MenuItemConfig[];
}

export default function AppLayout({children}: {children: ReactNode}) {
  const onLogout = useLogout();
  const navigate = useNavigate();
  const location = useLocation();

  const menuItemsConfig: MenuItemConfig[] = [
    {
      label: 'Assets',
      key: '/assets',
      icon: <AuditOutlined />,
      children: [
        { label: 'List', key: '/assets/list' },
        { label: 'Manage', key: '/assets/manage' },
      ],
    },
    {
      label: 'Dashboard',
      icon: <DashboardOutlined />,
      key: '/dashboard',
    },
    {
      label: 'Reports',
      key: '/reports',
      icon: <PieChartOutlined />,
      children: [
        { label: 'Summary', key: '/reports/summary' },
        { label: 'Charts', key: '/reports/charts' },
      ],
    },
  ];

  const buildMenuItems = (itemsConfig: typeof menuItemsConfig): MenuItem[] => {
    return itemsConfig.map((item) => {
      if (item.children) {
        return {
          label: item.label,
          key: item.key,
          icon: item.icon,
          children: buildMenuItems(item.children)
        } as MenuItem;
      }

      return {
        label: item.label,
        key: item.key,
        icon: item.icon,
      } as MenuItem;
    });
  };

  const menuItems = buildMenuItems(menuItemsConfig);

  const selectedKey = location.pathname.replace('/app', '');
  const openKeys = menuItemsConfig
    .filter((item) =>
      item.children?.some((child) => selectedKey.startsWith(child.key || ''))
    )
    .map((item) => item.key || '');

  return (
    <Layout style={{ height: '100dvh' }}>
      <Sider theme='light'>
        <div className={`flex flex-col justify-between h-full`}>
          <div className={`mt-12`}>
            <Menu
              mode="inline"
              items={menuItems}
              selectedKeys={[selectedKey]}
              defaultOpenKeys={openKeys}
              onClick={({ key }) => navigate(`/app${key}`)}
            />
          </div>
          <Button
            onClick={() => onLogout()}
            icon={<LogoutOutlined />}
            className={`m-2`}
          >Logout
          </Button>
        </div>
      </Sider>
      <Layout>
        <Header className={`m-4 bg-white rounded-lg`}>
          <p>Page Title</p>
        </Header>
        <Content className={`p-6 mx-4 mb-4 bg-white rounded-lg overflow-y-scroll`}>
          {children}
        </Content>
      </Layout>
    </Layout>
  );
};