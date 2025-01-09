import {
  Button,
  Form,
  Input
} from 'antd';
import { AssetCategory, AssetCategoryForm } from '../../../core/data/entities/asset/asset-category.ts';
import { FormProps } from '../../../core/data/models/forms/FormProps.ts';
import { useParams } from 'react-router-dom';
import {
  useLazyGetAssetCategoryQuery
} from '../../../core/data/services/api/asset-api.ts';
import { useCallback } from 'react';
import { useManageForm } from '../../../core/hooks/useManageForm.ts';

export default function AssetCategoryManageForm({onSubmit, isLoading}: FormProps<AssetCategoryForm>) {
  const { categoryId } = useParams<{ categoryId: string }>();

  const [form] = Form.useForm<AssetCategoryForm>();
  const handleSuccess = useCallback((asset: AssetCategory) => {
    form.setFieldValue('id', asset.id);
    form.setFieldValue('name', asset.name);
  }, [form]);

  const { formLoading } = useManageForm<AssetCategory>({
    id: categoryId,
    queryHook: useLazyGetAssetCategoryQuery,
    onSuccess: handleSuccess
  });

  return (
    <Form<AssetCategoryForm>
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

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}