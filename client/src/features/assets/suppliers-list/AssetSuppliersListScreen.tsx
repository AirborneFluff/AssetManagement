import { useGetAssetSuppliersQuery } from '../../../core/data/services/api/asset-api.ts';
import useTable from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';
import { Button, Table } from 'antd';
import { useNavigate } from 'react-router-dom';

export default function AssetSuppliersListScreen() {
  const navigate = useNavigate();

  const {
    columns,
    dataSource,
    loading,
    pagination,
    onTableChange,
    rowKey
  } = useTable(useGetAssetSuppliersQuery)([
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      sorter: true,
      sortDirections: ['descend', 'ascend'],
      showSearch: true
    },
    {
      title: 'Website',
      dataIndex: 'website',
      key: 'website',
      sorter: true,
      sortDirections: ['descend', 'ascend'],
      showSearch: true
    },
    {
      title: 'Action',
      key: 'operation',
      fixed: 'right',
      width: 100,
      render: (asset) => (
        <Button
          type='text'
          onClick={() => navigate(`manage/${asset.id}`)}
        >
          Edit
        </Button>
      )
    },
  ]);

  return (
    <ListScreenLayout>
      <Table
        columns={columns}
        dataSource={dataSource}
        loading={loading}
        pagination={pagination}
        onChange={onTableChange}
        rowKey={rowKey}
        bordered
      />
    </ListScreenLayout>
  )
}