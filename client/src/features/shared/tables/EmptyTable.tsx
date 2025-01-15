import { UnorderedListOutlined } from '@ant-design/icons';

interface EmptyTableProps {
  text?: string;
}

export default function EmptyTable({text}: EmptyTableProps) {
  return (
    <div style={{ textAlign: 'center' }}>
      <UnorderedListOutlined style={{ fontSize: 24 }} />
      <p>{text ?? 'Data Not Found'}</p>
    </div>
  )
}