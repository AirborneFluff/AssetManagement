import { Button, Table, Tag } from 'antd';
import { useGetAssetsQuery } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetTag } from '../../../core/data/entities/asset/asset.ts';
import useTable from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';
import { useNavigate } from 'react-router-dom';

export default function AssetsListScreen() {
  const navigate = useNavigate();

  const {
    columns,
    dataSource,
    loading,
    pagination,
    onTableChange,
    rowKey
  } = useTable(useGetAssetsQuery)([
    {
      title: 'Description',
      dataIndex: 'description',
      key: 'description',
      sorter: true,
      sortDirections: ['descend', 'ascend'],
      showSearch: true
    },
    {
      title: 'Category',
      render: (record: Asset) => record.category?.name || 'N/A',
      key: 'categoryName',
      sorter: true,
      sortDirections: ['descend', 'ascend'],
      showSearch: true
    },
    {
      title: 'Tags',
      dataIndex: 'tags',
      key: 'tags',
      render: (tags: AssetTag[]) => tags.map((tag) => {
        return (
          <Tag color='geekblue' key={tag.id}>
            {tag.tag}
          </Tag>
        )
      })
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
        onRow={(record) => {
          return {
            onClick: () => navigate(record.id),
          };
        }}
      />
    </ListScreenLayout>
  );
}