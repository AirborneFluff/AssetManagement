import { Button } from 'antd';
import { useLogout } from '../hooks/useLogout.ts';

export default function AppScreen() {
  const onLogout = useLogout();
  return (
    <>
      <Button onClick={() => onLogout()}>Logout</Button>
    </>
  )
}