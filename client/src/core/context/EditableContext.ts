import React from 'react';
import { FormInstance } from 'antd';

const EditableContext = React.createContext<FormInstance | null>(null);
export default EditableContext;