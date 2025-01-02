import { Tag } from 'antd';
import { useGetAssetsQuery } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetTag } from '../../../core/data/entities/asset/asset.ts';
import useTable, { SearchableColumnProps } from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';

export default function AssetsListScreen() {
  const renderTable = useTable<Asset>(useGetAssetsQuery);

  const columns: SearchableColumnProps<Asset> = [
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
    }
  ]

  return (
    <ListScreenLayout
      table={renderTable(columns)}
    />
  )
}