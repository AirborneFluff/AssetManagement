import { Table } from 'antd';
import { useGetAssetCategoriesQuery } from '../../../core/data/services/api/asset-api.ts';
import useTable from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';
import { AssetCategory } from '../../../core/data/entities/asset/asset-category.ts';

const { Column } = Table;

export default function AssetCategoriesListScreen() {
  const { params, handleTableChange, getColumnSearchProps } = useTable<AssetCategory>();
  const {data} = useGetAssetCategoriesQuery(params);

  return (
    <ListScreenLayout>
      <Table<AssetCategory>
        pagination={{
          current: data?.pagination?.currentPage ?? 1,
          pageSize: data?.pagination?.pageSize ?? 10,
          total: data?.pagination?.totalCount ?? 0
        }}
        onChange={handleTableChange}
        dataSource={(data?.items ?? [])}
        rowKey={(item) => item.id}
        bordered>
        <Column
          title="Name"
          dataIndex="name"
          key="name"
          sorter={true}
          sortDirections={['descend', 'ascend']}
          {...getColumnSearchProps()}
        />
      </Table>
    </ListScreenLayout>
  )
}