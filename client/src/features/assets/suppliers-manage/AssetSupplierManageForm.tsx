import {
  Button,
  Form,
  Input
} from 'antd';
import { FormProps } from '../../../core/data/models/forms/FormProps.ts';
import { useParams } from 'react-router-dom';
import {
  useLazyGetAssetSupplierQuery
} from '../../../core/data/services/api/asset-api.ts';
import { useCallback } from 'react';
import { useManageForm } from '../../../core/hooks/useManageForm.ts';
import { AssetSupplier, AssetSupplierForm } from '../../../core/data/entities/asset/asset-supplier.ts';

export default function AssetSupplierManageForm({onSubmit, isLoading}: FormProps<AssetSupplierForm>) {
  const { supplierId } = useParams<{ supplierId: string }>();

  const [form] = Form.useForm<AssetSupplierForm>();
  const handleSuccess = useCallback((asset: AssetSupplier) => {
    form.setFieldValue('id', asset.id);
    form.setFieldValue('name', asset.name);
    form.setFieldValue('website', asset.website);
  }, [form]);

  const { formLoading } = useManageForm<AssetSupplier>({
    id: supplierId,
    queryHook: useLazyGetAssetSupplierQuery,
    onSuccess: handleSuccess
  });

  return (
    <Form<AssetSupplierForm>
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 14 }}
      layout="vertical"
      style={{ maxWidth: 600 }}
      form={form}
      disabled={formLoading || isLoading}
      onFinish={onSubmit}
    >
      <Form.Item hidden name='id' />

      <Form.Item
        rules={[{ required: true }]}
        label="Name"
        name='name'
        tooltip="This is a required field">
        <Input />
      </Form.Item>

      <Form.Item
        label="Website"
        name='website'>
        <Input />
      </Form.Item>

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}