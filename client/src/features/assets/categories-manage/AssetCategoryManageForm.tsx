import {
  Button,
  Form,
  Input
} from 'antd';
import { AssetCategoryForm } from '../../../core/data/entities/asset/asset-category.ts';

interface FormProps {
  onSubmit: (data: AssetCategoryForm) => void;
}

export default function AssetCategoryManageForm({onSubmit}: FormProps) {
  const [form] = Form.useForm<AssetCategoryForm>();

  return (
    <Form<AssetCategoryForm>
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 14 }}
      layout="vertical"
      style={{ maxWidth: 600 }}
      form={form}
      onFinish={onSubmit}
    >
      <Form.Item
        rules={[{ required: true }]}
        label="Name"
        name='name'
        tooltip="This is a required field">
        <Input />
      </Form.Item>

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}