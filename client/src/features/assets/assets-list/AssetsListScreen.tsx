import { Table, Tag } from 'antd';
import { useGetAssetsQuery } from '../../../core/data/services/api/asset-api.ts';
import { Asset, AssetTag } from '../../../core/data/entities/asset.ts';

const { Column } = Table;

export default function AssetsListScreen() {
  const {data} = useGetAssetsQuery({
    pageNumber: 1,
    pageSize: 10
  });

  return (
    <div className={`h-full`}>
      <Table<Asset>
        dataSource={(data?.items ?? [])}
        rowKey={(item) => item.id}
        bordered>
        <Column title="Description" dataIndex="description" key="description" />
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