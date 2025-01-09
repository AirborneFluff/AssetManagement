import {
  Button,
  Form,
  Input, Select
} from 'antd';
import { CloseCircleTwoTone } from '@ant-design/icons';
import { Asset, AssetForm } from '../../../core/data/entities/asset/asset.ts';
import { useGetAssetCategoriesQuery, useLazyGetAssetQuery } from '../../../core/data/services/api/asset-api.ts';
import { useSelectSearch } from '../../../core/hooks/useSelectSearch.ts';
import { useParams } from 'react-router-dom';
import { useManageForm } from '../../../core/hooks/useManageForm.ts';
import { cleanFormValues } from '../../../core/helpers/cleanFormValues.ts';
import { useCallback } from 'react';
import { FormProps } from '../../../core/data/models/forms/FormProps.ts';

export default function AssetManageForm({onSubmit, isLoading}: FormProps<AssetForm>) {
  const { assetId } = useParams<{ assetId: string }>();
  const { onSearch, params: result } = useSelectSearch('name');
  const { data: categories, isFetching } = useGetAssetCategoriesQuery(result);

  const [form] = Form.useForm<AssetForm>();
  const handleSuccess = useCallback((asset: Asset) => {
    form.setFieldValue('id', asset.id);
    form.setFieldValue('description', asset.description);
    form.setFieldValue('categoryId', {
      value: asset.category?.id,
      label: asset.category?.name,
    });
    form.setFieldValue('tags', asset.tags.map(tag => tag.tag));
  }, [form]);

  const { formLoading } = useManageForm<Asset>({
    id: assetId,
    queryHook: useLazyGetAssetQuery,
    onSuccess: handleSuccess
  });

  return (
    <Form<AssetForm>
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 14 }}
      layout="vertical"
      style={{ maxWidth: 600 }}
      disabled={isLoading || formLoading}
      form={form}
      onFinish={(data) => onSubmit(cleanFormValues(data))}
    >
      <Form.Item hidden name='id' />

      <Form.Item
        rules={[{ required: true }]}
        label="Description"
        name='description'
        tooltip="This is a required field">
        <Input />
      </Form.Item>

      <Form.Item
        rules={[{ required: true }]}
        label="Category"
        name='categoryId'
        tooltip="This is a required field">
        <Select
          showSearch
          defaultActiveFirstOption={true}
          filterOption={false}
          onSearch={onSearch}
          loading={isFetching}
          options={(categories?.items ?? []).map((category) => ({
            value: category.id,
            label: category.name,
          }))}
        />
      </Form.Item>


      <Form.Item label="Tags">
        <Form.List name="tags">
          {(fields, { add, remove }) => (
            <>
              <div style={{ marginBottom: 8 }}>
                {fields.map(({ key, name, ...restField }) => (
                  <Input.Group key={key} compact>
                    <Form.Item noStyle
                      {...restField}
                      name={name}
                      rules={[{ required: true, message: 'Tag cannot be empty' }]}
                    >
                      <Input
                        style={{width: 'calc(100% - 32px)', marginBottom: 8}}
                        placeholder="Enter tag" />
                    </Form.Item>
                    <Button
                      onClick={() => remove(name)}
                      icon={<CloseCircleTwoTone twoToneColor="#ff4d4f" />}
                    />
                  </Input.Group>
                ))}
              </div>
              <Button type="dashed" onClick={() => add()} block>
                Add Tag
              </Button>
            </>
          )}
        </Form.List>
      </Form.Item>

      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}