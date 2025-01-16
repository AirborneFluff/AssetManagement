import {
  Button,
  Form,
  InputNumber
} from 'antd';
import { FormProps } from '../../../../core/data/models/forms/FormProps.ts';
import { AssetStockLevelForm } from '../../../../core/data/entities/asset/asset-stock-level.ts';

interface AssetStockLevelFormProps extends FormProps<AssetStockLevelForm> {
  initialValue: number;
}

export default function AssetStockLevelFormView({onSubmit, isLoading, initialValue}: AssetStockLevelFormProps) {
  const [form] = Form.useForm<AssetStockLevelForm>();

  return (
    <Form<AssetStockLevelForm>
      layout="vertical"
      style={{ maxWidth: 600 }}
      disabled={isLoading}
      form={form}
      onFinish={onSubmit}
    >
      <Form.Item
        initialValue={initialValue}
        rules={[{ required: true }]}
        label="Stock Level"
        name='stockLevel'
        tooltip="This is a required field">
        <InputNumber />
      </Form.Item>

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}