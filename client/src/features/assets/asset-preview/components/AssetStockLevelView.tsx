import { Button, Card, Col, Drawer, Flex } from 'antd';
import { Asset } from '../../../../core/data/entities/asset/asset.ts';
import { CartesianGrid, Line, LineChart, ResponsiveContainer, XAxis, YAxis } from 'recharts';
import { humanizeDate } from '../../../../core/utils/humanizers/date-humanizer.ts';
import { Typography } from 'antd';
import AssetStockLevelFormView from './AssetStockLevelFormView.tsx';
import { AssetStockLevelForm } from '../../../../core/data/entities/asset/asset-stock-level.ts';
import { useUpdateAssetStockLevelMutation } from '../../../../core/data/services/api/asset-api.ts';
import { useState } from 'react';
const { Text } = Typography;

interface AssetStockLevelViewProps {
  asset: Asset | undefined;
  onRefetch: () => void;
}
export default function AssetStockLevelView({asset, onRefetch}: AssetStockLevelViewProps) {
  const [updateStockLevel] = useUpdateAssetStockLevelMutation();
  const [showDrawer, setShowDrawer] = useState(false);
  const historicData = asset?.historicStockLevels?.map(s => ({
    name: humanizeDate(s.createdOn),
    value: s.stockLevel
  })) ?? [];

  // todo Sort the data client side my date.
  const chartData = [...historicData, {
    name: humanizeDate(new Date().toString()),
    value: asset?.stockLevel
  }];

  const handleOnStockLevelUpdate = (data: AssetStockLevelForm) => {
    if (!asset?.id) return;

    updateStockLevel({
      ...data,
      assetId: asset.id
    }).unwrap()
      .then(() => {
        setShowDrawer(false);
        onRefetch();
      })
  }

  return (
    <Col span={24} xl={12}>
      <Card
        size="small"
        styles={{header: {backgroundColor: 'rgb(250, 250, 250)'}}}
        title={(
          <Flex justify='space-between' align='center'>
            <Text>Stock Level: {asset?.stockLevel}</Text>
            <Button type='text' onClick={() => setShowDrawer(true)}>Update</Button>
          </Flex>
        )}
      >
        <ResponsiveContainer width="100%" height={390}>
          <LineChart data={chartData}>
            <XAxis dataKey="name"/>
            <YAxis/>
            <CartesianGrid stroke="#eee" strokeDasharray="5 5"/>
            <Line type="monotone" dataKey="value" stroke="#8884d8" />
          </LineChart>
        </ResponsiveContainer>
      </Card>
      <Drawer
        title="Update Stock Level"
        onClose={() => setShowDrawer(false)}
        open={showDrawer}>
        <AssetStockLevelFormView
          initialValue={asset?.stockLevel ?? 0}
          onSubmit={handleOnStockLevelUpdate}
          isLoading={false}
        />
      </Drawer>
    </Col>
  )
}