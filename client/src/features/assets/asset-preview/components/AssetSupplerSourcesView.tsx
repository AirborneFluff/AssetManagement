import AssetSupplySourceTable from './AssetSupplySourceTable.tsx';
import { Col, Drawer } from 'antd';
import AssetSupplySourceFormView from './AssetSupplySourceForm.tsx';
import {
  useCreateAssetSupplySourceMutation, useDeleteAssetSupplySourceMutation,
  useUpdateAssetSupplySourceMutation
} from '../../../../core/data/services/api/asset-api.ts';
import { AssetSupplySource, AssetSupplySourceForm } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { Asset } from '../../../../core/data/entities/asset/asset.ts';
import { useState } from 'react';

interface AssetSupplySourceFormProps {
  asset: Asset | undefined;
  onRefetch: () => void;
}

export default function AssetSupplerSourcesView({asset, onRefetch}: AssetSupplySourceFormProps) {
  const [showDrawer, setShowDrawer] = useState(false);
  const [updateSupplySource] = useUpdateAssetSupplySourceMutation();
  const [createSupplySource] = useCreateAssetSupplySourceMutation();
  const [deleteSupplySource] = useDeleteAssetSupplySourceMutation();

  const handleOnSupplySourceUpdate = (source: AssetSupplySource) => {
    if (!asset?.id) return;

    updateSupplySource({
      ...source,
      assetId: asset.id
    }).unwrap()
      .then(onRefetch)
  }

  const handleOnSupplySourceCreate = (source: AssetSupplySourceForm) => {
    if (!asset?.id) return;

    createSupplySource({
      ...source,
      assetId: asset.id
    }).unwrap()
      .then(() => {
        setShowDrawer(false);
        onRefetch();
      })
  }

  const handleOnSupplySourceDelete = (sourceId: string) => {
    if (!asset?.id) return;

    deleteSupplySource({
      id: sourceId,
      assetId: asset.id
    }).unwrap()
      .then(onRefetch)
  }

  return (
    <Col span={24} xl={12}>
      <AssetSupplySourceTable
        supplySources={asset?.supplySources}
        onUpdate={handleOnSupplySourceUpdate}
        onDelete={handleOnSupplySourceDelete}
        onShowAddDialog={() => setShowDrawer(true)}
      />
      <Drawer
        title="New Supply Source"
        onClose={() => setShowDrawer(false)}
        open={showDrawer}>
        <AssetSupplySourceFormView onSubmit={handleOnSupplySourceCreate} isLoading={false} />
      </Drawer>
    </Col>
  )
}