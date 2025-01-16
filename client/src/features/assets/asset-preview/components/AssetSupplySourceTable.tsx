import { Button, Popconfirm, Table } from 'antd';
import useSupplySourcePriceTable from '../hooks/useSupplySourcePriceTable.tsx';
import { AssetSupplySource } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { useEffect, useState } from 'react';
import { PlusCircleOutlined } from '@ant-design/icons';

interface AssetSupplySourceTableProps {
  supplySources: AssetSupplySource[] | undefined;
  onUpdate: (source: AssetSupplySource) => void;
  onDelete: (sourceId: string) => void;
  onShowAddDialog: () => void;
}

export default function AssetSupplySourceTable({ supplySources, onUpdate, onDelete, onShowAddDialog }: AssetSupplySourceTableProps) {
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
    {
      title: 'Action',
      key: 'action',
      render: (_: unknown, row: AssetSupplySource) => (
        <Popconfirm
          title="Delete this supply source?"
          onConfirm={() => onDelete(row.id)}
        >
          <a>Delete</a>
        </Popconfirm>
      ),
    },
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
      scroll={{ y: 39 * 10 }}
      pagination={false}
      footer={() => (
        <Button
          icon={<PlusCircleOutlined />}
          onClick={onShowAddDialog}
          type="text">
          Add Supply Source
        </Button>
      )}
    />
  );
}