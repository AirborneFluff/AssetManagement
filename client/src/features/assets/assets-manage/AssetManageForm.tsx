import {
  Button,
  Form,
  Input
} from 'antd';
import { CloseCircleTwoTone } from '@ant-design/icons';
import { AssetForm } from '../../../core/data/entities/asset.ts';

interface AssetManageFormProps {
  onSubmit: (data: AssetForm) => void;
}

export default function AssetManageForm({onSubmit}: AssetManageFormProps) {
  const [form] = Form.useForm<AssetForm>();

  return (
    <Form<AssetForm>
      labelCol={{ span: 4 }}
      wrapperCol={{ span: 14 }}
      layout="vertical"
      style={{ maxWidth: 600 }}
      form={form}
      onFinish={onSubmit}
    >
      <Form.Item
        rules={[{ required: true }]}
        label="Description"
        name='description'
        tooltip="This is a required field">
        <Input />
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