import React, { ReactNode } from 'react';
import {
  LogoutOutlined,
  AuditOutlined,
  DashboardOutlined,
  PieChartOutlined,
  ArrowLeftOutlined
} from '@ant-design/icons';
import { Button, MenuProps } from 'antd';
import { Layout, Menu } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';
import { useLocation, useNavigate } from 'react-router-dom';

const { Content, Sider } = Layout;

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
        { label: 'Assets', key: '/' },
        {
          label: 'Categories',
          key: '/categories'
        },
        {
          label: 'Suppliers',
          key: '/suppliers'
        },
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
        { label: 'Summary', key: '/summary' },
        { label: 'Charts', key: '/charts' },
      ],
    },
  ];

  const buildMenuItems = (itemsConfig: typeof menuItemsConfig, parentKey?: string): MenuItem[] => {
    return itemsConfig.map((item) => {
      if (item.children) {
        return {
          label: item.label,
          key: (parentKey ?? '') + item.key,
          icon: item.icon,
          children: buildMenuItems(item.children, item.key)
        } as MenuItem;
      }

      return {
        label: item.label,
        key: (parentKey ?? '') + item.key,
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

  /*const findBreadcrumbTrail = (
    key: string | undefined,
    itemsConfig: MenuItemConfig[],
    trail: MenuItemConfig[] = []
  ): MenuItemConfig[] => {
    for (const item of itemsConfig) {
      const currentTrail = [...trail, item];

      if (item.key === key) {
        return currentTrail;
      }

      if (item.children) {
        const foundTrail = findBreadcrumbTrail(key, item.children, currentTrail);
        if (foundTrail.length) {
          return foundTrail;
        }
      }
    }

    return [];
  };*/

  //const breadcrumbTrail = findBreadcrumbTrail(selectedKey, menuItemsConfig);

  const navigateBack = () => navigate(-1);

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
        <div className={`m-4 py-4 px-2 bg-white rounded-lg flex items-center justify-start gap-4`}>
          <Button icon={<ArrowLeftOutlined />} type='text' onClick={navigateBack}>Back</Button>
          {/*<Breadcrumb>
            {breadcrumbTrail.map((item) => {
              return (
                <Breadcrumb.Item key={item.key}>
                  {item.label}
                </Breadcrumb.Item>
              );
            })}
          </Breadcrumb>*/}
        </div>
        <Content className={`p-6 mx-4 mb-4 bg-white rounded-lg overflow-y-auto`}>
          {children}
        </Content>
      </Layout>
    </Layout>
  );
};