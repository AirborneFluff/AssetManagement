import { Descriptions, Skeleton } from 'antd';
import { Asset } from '../../../../core/data/entities/asset/asset.ts';

export default function AssetPreviewDescriptions({asset}: {asset: Asset | undefined}) {
  return (
    <Descriptions title={asset?.description ?? 'Asset'} bordered layout='vertical' size='small'>
      <Descriptions.Item label="Description">{asset?.description ?? <Skeleton active />}</Descriptions.Item>
      <Descriptions.Item label="Telephone">1810000000</Descriptions.Item>
      <Descriptions.Item label="Live">Hangzhou, Zhejiang</Descriptions.Item>
      <Descriptions.Item label="Remark">empty</Descriptions.Item>
      <Descriptions.Item label="Address">
        No. 18, Wantang Road, Xihu District, Hangzhou, Zhejiang, China
      </Descriptions.Item>
    </Descriptions>
  )
}