import React, { ReactNode } from 'react';
import {
  LogoutOutlined,
  AuditOutlined
} from '@ant-design/icons';
import { Button, MenuProps } from 'antd';
import { Layout, Menu } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';
import { useNavigate } from 'react-router-dom';

const { Header, Content, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

export default function AppLayout({children}: {children: ReactNode}) {
  const onLogout = useLogout();
  const navigate = useNavigate();

  function buildMenuItem(label: React.ReactNode, route?: string, icon?: React.ReactNode, children?: MenuItem[]): MenuItem {
    return {
      onClick: () => route ? navigate(`/app/${route}`) : undefined,
      key: route,
      icon,
      label,
      children: children
    } as MenuItem;
  }

  const items: MenuItem[] = [
    buildMenuItem('Assets', undefined, <AuditOutlined />, [
      buildMenuItem('List', 'assets/list'),
      buildMenuItem('Manage', 'assets/manage'),
    ]),
  ];

  return (
    <Layout style={{ height: '100dvh' }}>
      <Sider theme='light'>
        <div className={`flex flex-col justify-between h-full`}>
          <div className={`mt-12`}>
            <Menu defaultSelectedKeys={['assets']} mode="inline" items={items} />
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