import {
  Button,
  Form,
  Input
} from 'antd';
import { AssetForm } from '../../../core/data/entities/asset.ts';
import { useState } from 'react';

interface AssetManageFormProps {
  onSubmit: (data: AssetForm) => void;
}

export default function AssetManageForm({onSubmit}: AssetManageFormProps) {
  const [form] = Form.useForm<AssetForm>();
  const [tagInput, setTagInput] = useState('');
  const tags = Form.useWatch('tags', form);
  const setTags = (items: string[]) => form.setFieldValue('tags', items);

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
        <Input.Group compact>
          <Form.Item noStyle>
            <Input
              style={{width: 'calc(100% - 60px)', marginBottom: 8}}
              placeholder="Enter tag"
              value={tagInput}
              onChange={(e) => setTagInput(e.target.value)}
            />
          </Form.Item>
          <Button
            type="primary"
            onClick={() => {
              if (tagInput && !(tags ?? []).includes(tagInput)) {
                setTags([...(tags ?? []), tagInput]);
                setTagInput('');
              }
            }}>
            Add
          </Button>
        </Input.Group>

        <div style={{marginTop: 8}}>
          {(tags ?? []).map((tag, index) => (
            <Button
              key={index}
              size="small"
              type="default"
              style={{marginRight: 5, marginBottom: 5}}
              onClick={() => setTags((tags ?? []).filter((t) => t !== tag))}>
              {tag} ✖
            </Button>
          ))}
        </div>
      </Form.Item>
      
      <Button type="primary" htmlType="submit">
        Submit
      </Button>
    </Form>
  )
}