import { Menu, Layout, Dropdown, MenuProps, Button } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';
const { Header } = Layout;

export default function AppHeader() {
  const onLogout = useLogout();

  const items: MenuProps['items'] = [
    {
      type: 'divider',
    },
    {
      onClick: () => onLogout(),
      label: 'Logout',
      key: '3',
    },
  ];
  return (
    <Header className="bg-white border-b border-gray-200">
      <div className='h-full justify-between items-center flex bg-white'>
        <Menu
          theme="light"
          mode="horizontal"
        />

        <Dropdown menu={{ items }} trigger={['click']}>
          <Button type="primary" shape="circle">
            A
          </Button>
        </Dropdown>
      </div>
    </Header>
  )
}