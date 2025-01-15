import {
  Button,
  Form, Input,
  Select
} from 'antd';
import { AssetSupplySourceForm } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { useSelectSearch } from '../../../../core/hooks/useSelectSearch.ts';
import { FormProps } from '../../../../core/data/models/forms/FormProps.ts';
import { useGetAssetSuppliersQuery } from '../../../../core/data/services/api/asset-api.ts';

export default function AssetSupplySourceFormView({onSubmit, isLoading}: FormProps<AssetSupplySourceForm>) {
  const { onSearch, params: result } = useSelectSearch('name');
  const { data: suppliers, isFetching } = useGetAssetSuppliersQuery(result);

  const [form] = Form.useForm<AssetSupplySourceForm>();

  return (
    <Form<AssetSupplySourceForm>
      layout="vertical"
      style={{ maxWidth: 600 }}
      disabled={isLoading}
      form={form}
      onFinish={onSubmit}
    >
      <Form.Item hidden name='id' />

      <Form.Item
        rules={[{ required: true }]}
        label="Supplier"
        name='supplierId'
        tooltip="This is a required field">
        <Select
          showSearch
          defaultActiveFirstOption={true}
          filterOption={false}
          onSearch={onSearch}
          loading={isFetching}
          options={(suppliers?.items ?? []).map((supplier) => ({
            value: supplier.id,
            label: supplier.name,
          }))}
        />
      </Form.Item>

      <Form.Item
        rules={[{ required: true }]}
        label="Supplier Reference"
        name='supplierReference'
        tooltip="This is a required field">
        <Input />
      </Form.Item>

      <Form.Item label="Quantity Units" name='quantityUnit'>
        <Input />
      </Form.Item>

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}