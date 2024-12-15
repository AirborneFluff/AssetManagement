import React, { ReactNode } from 'react';
import {
  LogoutOutlined,
  AuditOutlined
} from '@ant-design/icons';
import { Button, MenuProps } from 'antd';
import { Layout, Menu, theme } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';
import { useNavigate } from 'react-router-dom';

const { Header, Content, Footer, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

export default function AppLayout({children}: {children: ReactNode}) {
  const onLogout = useLogout();
  const navigate = useNavigate();

  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  function buildMenuItem(label: React.ReactNode, route: string, icon: React.ReactNode): MenuItem {
    return {
      onClick: () => navigate(`/app/${route}`),
      key: route,
      icon,
      label,
    } as MenuItem;
  }

  const items: MenuItem[] = [
    buildMenuItem('Assets', 'assets', <AuditOutlined />),
  ];

  return (
    <Layout style={{ minHeight: '100vh' }}>
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
        <Header style={{ padding: 0, background: colorBgContainer, margin: '0 16px' }} />
        <Content style={{ margin: '0 16px' }}>
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            {children}
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};