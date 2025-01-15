import { ReactNode, useContext, useEffect, useRef, useState } from 'react';
import { Form, InputNumber } from 'antd';
import EditableContext from '../../../core/context/EditableContext.ts';

interface EditableCellProps<T> {
  title: ReactNode;
  editable: boolean;
  dataIndex: keyof T;
  record: T;
  handleSave: (record: T) => void;
  children: ReactNode;
}

export default function EditableCell<T>(
  {title, editable, children, dataIndex, record, handleSave, ...restProps}: EditableCellProps<T>) {
  const form = useContext(EditableContext);
  const [editing, setEditing] = useState(false);
  const inputRef = useRef<HTMLInputElement | null>(null);

  useEffect(() => {
    if (editing) {
      inputRef.current?.focus();
    }
  }, [editing]);

  const toggleEdit = () => {
    if (form) {
      setEditing((prev) => !prev);
      form.setFieldsValue({ [dataIndex]: record[dataIndex] });
    }
  };

  const save = async () => {
    try {
      if (form) {
        const values = await form.validateFields();
        handleSave({ ...record, ...values });
      }
      toggleEdit();
    } catch (errInfo) {
      console.log('Save failed:', errInfo);
    }
  };

  let childNode = children;

  if (editable) {
    childNode = editing ? (
      <Form.Item
        style={{ margin: 0 }}
        name={dataIndex as string}
        rules={[
          {
            required: true,
            message: `${title} is required.`,
          },
        ]}
      >
        <InputNumber ref={inputRef} onPressEnter={save} onBlur={save} />
      </Form.Item>
    ) : (
      <div onClick={toggleEdit}>
        {children}
      </div>
    );
  }

  return <td {...restProps}>{childNode}</td>;
}