import { useGetAssetQuery } from '../../../core/data/services/api/asset-api.ts';
import { useParams } from 'react-router-dom';
import AssetPreviewDescriptions from './components/AssetPreviewDescriptions.tsx';
import { Col, Row } from 'antd';
import AssetSupplerSourcesView from './components/AssetSupplerSourcesView.tsx';

export default function AssetPreviewScreen() {
  const { assetId } = useParams<{ assetId: string }>();
  const {data: asset, refetch} = useGetAssetQuery(assetId as string);

  return (
    <>
      <Row gutter={[12, 12]}>
        <Col span={24}>
          <AssetPreviewDescriptions asset={asset} />
        </Col>
        <AssetSupplerSourcesView asset={asset} onRefetch={refetch} />
      </Row>
    </>
  )
}