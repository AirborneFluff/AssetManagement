import { EditableRow } from '../../../shared/forms/EditableRow.tsx';
import { AssetSupplySource } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { ColumnType } from 'antd/es/table';
import { Button, Popconfirm, Table } from 'antd';
import EditableCell from '../../../shared/forms/EditableCell.tsx';
import { Dispatch, SetStateAction } from 'react';
import { PlusCircleOutlined } from '@ant-design/icons';
import EmptyTable from '../../../shared/tables/EmptyTable.tsx';

interface AssetSupplySourcePricesTableProps {
  setDataSource: Dispatch<SetStateAction<AssetSupplySource[]>>;
  onUpdate: (source: AssetSupplySource) => void;
}

interface TableRow {
  quantity: number;
  price: number;
}

export default function useSupplySourcePriceTable({setDataSource, onUpdate}: AssetSupplySourcePricesTableProps) {
  const components = {
    body: {
      row: EditableRow,
      cell: EditableCell,
    },
  };

  const addPrice = (supplySourceId: string) => {
    setDataSource((prev) =>
      prev.map((source) => {
        if (source.id !== supplySourceId) return source;
        const updatedSource = {
          ...source,
          prices: {
            ...(source.prices || {}),
            [Math.max(0, ...Object.keys(source.prices || {}).map(Number)) + 1]: 0,
          },
        };
        onUpdate(updatedSource);
        return updatedSource;
      })
    );
  };

  const deletePrice = (supplySourceId: string, quantity: number) => {
    setDataSource((prev) =>
      prev.map((source) => {
        if (source.id !== supplySourceId) return source;
        const updatedSource = {
          ...source,
          prices: Object.fromEntries(
            Object.entries(source.prices || {}).filter(([key]) => Number(key) !== quantity)
          ),
        };
        onUpdate(updatedSource);
        return updatedSource;
      })
    );
  };

  const savePrice = (supplySourceId: string, updatedRow: TableRow, currentRow: TableRow) => {
    setDataSource((prev) =>
      prev.map((source) => {
        if (source.id !== supplySourceId) return source;

        const { quantity: updatedQuantity, price: updatedPrice } = updatedRow;
        const { quantity: originalQuantity } = currentRow;
        const oldPrices = { ...(source.prices || {}) };

        if (originalQuantity !== updatedQuantity) {
          delete oldPrices[originalQuantity];
        }

        oldPrices[updatedQuantity] = updatedPrice;
        onUpdate({ ...source, prices: oldPrices });
        return { ...source, prices: oldPrices };
      })
    );
  };

  return (record: AssetSupplySource) => {
    type EditableColumnType<T> = ColumnType<T> & { editable?: boolean };
    const priceColumns: EditableColumnType<TableRow>[] = [
      {
        title: 'Quantity',
        dataIndex: 'quantity',
        key: 'quantity',
        editable: true
      },
      {
        title: 'Price',
        dataIndex: 'price',
        key: 'price',
        editable: true
      },
      {
        title: 'Action',
        key: 'action',
        render: (_: unknown, row: TableRow) => (
          <Popconfirm
            title="Delete this price?"
            onConfirm={() => deletePrice(record.id, row.quantity)}
          >
            <a>Delete</a>
          </Popconfirm>
        ),
      },
    ];

    const editableColumns = priceColumns.map((col) =>
      !col.editable ? col : {
        ...col,
        onCell: (row: TableRow) => ({
          record: row,
          editable: col.editable,
          dataIndex: col.dataIndex,
          title: col.title,
          handleSave: (updatedRow: TableRow) =>
            savePrice(record.id, updatedRow, row),
        }),
      } as ColumnType<TableRow>
    );

    const data = Object.entries(record.prices || {}).map(([quantity, price]) => ({
      quantity: Number(quantity),
      price,
    }));

    return (
      <Table<TableRow>
        size='small'
        components={components}
        bordered
        locale={{emptyText: <EmptyTable text='No Prices' />}}
        dataSource={data}
        columns={editableColumns}
        rowKey={(row) => `${row.quantity}`}
        pagination={false}
        footer={() => (
          <Button
            icon={<PlusCircleOutlined />}
            onClick={() => addPrice(record.id)}
            type="text">
            Add Price
          </Button>
        )}
      />
    );
  };
}