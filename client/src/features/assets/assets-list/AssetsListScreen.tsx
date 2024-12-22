import { Table, Tag } from 'antd';
import { useGetAssetsQuery } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetTag } from '../../../core/data/entities/asset.ts';
import useTable from '../../../core/hooks/useTable.tsx';
import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';

const { Column } = Table;

export default function AssetsListScreen() {
  const { params, handleTableChange, getColumnSearchProps } = useTable<Asset>();
  const {data} = useGetAssetsQuery(params);

  return (
    <ListScreenLayout>
      <Table<Asset>
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
          title="Description"
          dataIndex="description"
          key="description"
          sorter={true}
          sortDirections={['descend', 'ascend']}
          {...getColumnSearchProps()}
        />
        <Column
          title="Tags"
          dataIndex="tags"
          key="tags"
          render={(tags: AssetTag[]) => tags.map((tag) => {
            return (
              <Tag color='geekblue' key={tag.id}>
                {tag.tag}
              </Tag>
            )})
          }
        />
      </Table>
    </ListScreenLayout>
  )
}