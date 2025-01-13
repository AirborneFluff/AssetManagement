import { useGetAssetQuery, useUpdateAssetSupplySourceMutation } from '../../../core/data/services/api/asset-api.ts';
import { useParams } from 'react-router-dom';
import AssetPreviewDescriptions from './components/AssetPreviewDescriptions.tsx';
import { Col, Row } from 'antd';
import AssetSupplySourceTable from './components/AssetSupplySourceTable.tsx';
import { AssetSupplySource } from '../../../core/data/entities/asset/asset-supply-source.ts';

export default function AssetPreviewScreen() {
  const { assetId } = useParams<{ assetId: string }>();
  const {data: asset} = useGetAssetQuery(assetId as string);
  const [updateSupplySource] = useUpdateAssetSupplySourceMutation();

  const handleOnSupplySourceUpdate = (source: AssetSupplySource) => {
    if (!assetId) return;

    updateSupplySource({
      ...source,
      assetId
    })
  }

  return (
    <>
      <Row>
        <Col span={24}>
          <AssetPreviewDescriptions asset={asset} />
        </Col>
      </Row>
      <Row>
        <Col span={24}>
          <AssetSupplySourceTable asset={asset} onUpdate={handleOnSupplySourceUpdate} />
        </Col>
      </Row>
    </>
  )
}