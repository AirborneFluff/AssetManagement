import React, { ReactNode } from 'react';
import { Form } from 'antd';
import EditableContext from '../../../core/context/EditableContext';

interface EditableRowProps {
  children: ReactNode;
}

export const EditableRow: React.FC<EditableRowProps> = ({ children }) => {
  const [form] = Form.useForm();

  return (
    <Form form={form} component={false}>
      <EditableContext.Provider value={form}>
        <tr>{children}</tr>
      </EditableContext.Provider>
    </Form>
  );
};