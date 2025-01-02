import { useGetAssetCategoriesQuery } from '../../../core/data/services/api/asset-api.ts';
import useTable, { SearchableColumnProps } from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';
import { AssetCategory } from '../../../core/data/entities/asset/asset-category.ts';


export default function AssetCategoriesListScreen() {
  const renderTable = useTable<AssetCategory>(useGetAssetCategoriesQuery);

  const columns: SearchableColumnProps<AssetCategory> = [
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      sorter: true,
      sortDirections: ['descend', 'ascend'],
      showSearch: true
    }
  ]

  return (
    <ListScreenLayout
      table={renderTable(columns)}
    />
  )
}