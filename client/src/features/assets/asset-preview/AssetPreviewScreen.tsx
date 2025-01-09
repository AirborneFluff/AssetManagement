import { useGetAssetQuery } from '../../../core/data/services/api/asset-api.ts';
import { useParams } from 'react-router-dom';
import AssetPreviewDescriptions from './components/AssetPreviewDescriptions.tsx';

export default function AssetPreviewScreen() {
  const { assetId } = useParams<{ assetId: string }>();
  const {data: asset} = useGetAssetQuery(assetId as string);

  return (
    <>
      <AssetPreviewDescriptions asset={asset} />
    </>
  )
}