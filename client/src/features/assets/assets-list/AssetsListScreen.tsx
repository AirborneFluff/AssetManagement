import { Table, TableProps, Tag } from 'antd';
import { useGetAssetsQuery } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetTag } from '../../../core/data/entities/asset.ts';
import useTableParams, { BaseTableParams } from '../../../core/hooks/useTableParams.ts';

const { Column } = Table;

export default function AssetsListScreen() {
  const { params, updateParams } = useTableParams<BaseTableParams>();
  const {data} = useGetAssetsQuery({...params});

  const handleTableChange: TableProps<Asset>['onChange'] = (pagination, _, sorter) => {
    updateParams({
      pageNumber: pagination.current,
      pageSize: pagination.pageSize,
      sortOrder: Array.isArray(sorter) ? undefined : sorter.order as string,
      sortField: Array.isArray(sorter) ? undefined : sorter.field as string,
    });
  };

  return (
    <div className={`h-full`}>
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
    </div>
  )
}