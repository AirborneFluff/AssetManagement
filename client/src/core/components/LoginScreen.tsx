import React from 'react';
import { Card, Form, Input, Button, Typography, Flex } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import { useLoginMutation } from '../data/services/api/auth-api.ts';
import { LoginForm } from '../data/models/login-form.ts';
import { setUser } from '../data/slices/user-slice.ts';
import { useDispatch } from 'react-redux';

const { Title } = Typography;

export function LoginPage() {
  const [submit] = useLoginMutation();
  const dispatch = useDispatch();

  const handleSubmit = async (values: LoginForm) => {
    try {
      const result = await submit(values).unwrap();
      dispatch(setUser(result));
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <Flex align="center" justify="center" style={styles.container}>
      <Card style={styles.card} bordered={true}>
        <Title level={3} style={{ textAlign: 'center' }}>
          Login
        </Title>
        <Form
          name="loginForm"
          onFinish={handleSubmit}
          layout="vertical"
          initialValues={{ remember: true }}
        >
          <Form.Item
            name="email"
            label="Email"
            rules={[{ required: true, message: 'Please input your email' }]}
          >
            <Input
              prefix={<UserOutlined />}
              placeholder="Enter your email"
              size="large"
            />
          </Form.Item>

          <Form.Item
            name="password"
            label="Password"
            rules={[{ required: true, message: 'Please input your password' }]}
          >
            <Input.Password
              prefix={<LockOutlined />}
              placeholder="Enter your password"
              size="large"
            />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" size="large" block>
              Log In
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </Flex>
  );
}

const styles: { [key: string]: React.CSSProperties } = {
  container: {
    minHeight: '100vh'
  },
  card: {
    maxWidth: 600,
    borderRadius: 8,
    boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
    padding: '16px 24px',
  }
};

export default LoginPage;