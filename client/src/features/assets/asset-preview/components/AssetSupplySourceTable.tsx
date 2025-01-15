import { Button, Table } from 'antd';
import useSupplySourcePriceTable from './useSupplySourcePriceTable.tsx';
import { AssetSupplySource } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { useEffect, useState } from 'react';
import { PlusCircleOutlined } from '@ant-design/icons';

interface AssetSupplySourceTableProps {
  supplySources: AssetSupplySource[] | undefined;
  onUpdate: (source: AssetSupplySource) => void;
}

export default function AssetSupplySourceTable({ supplySources, onUpdate }: AssetSupplySourceTableProps) {
  const [dataSource, setDataSource] = useState<AssetSupplySource[]>([]);

  useEffect(() => {
    if (supplySources) {
      setDataSource(supplySources);
    }

  }, [supplySources]);

  const expandedRowRender = useSupplySourcePriceTable({
    setDataSource,
    onUpdate
  });

  const mainColumns = [
    { title: 'Supplier', dataIndex: 'supplierName' },
    { title: 'Supplier Reference', dataIndex: 'supplierReference' },
  ];

  return (
    <Table
      size='small'
      bordered
      dataSource={dataSource}
      columns={mainColumns}
      rowKey="id"
      expandable={{
        expandedRowRender
      }}
      scroll={{ y: 55 * 5 }}
      pagination={false}
      footer={() => (
        <Button
          icon={<PlusCircleOutlined />}
          onClick={() => null}
          type="text">
          Add Supply Source
        </Button>
      )}
    />
  );
}