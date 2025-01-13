import React, { useState, useContext, useRef, useEffect, ReactNode } from 'react';
import { Button, Form, InputNumber, Popconfirm, Table } from 'antd';
import type { FormInstance } from 'antd';
import { Asset } from '../../../../core/data/entities/asset/asset.ts';
import { AssetSupplySource } from '../../../../core/data/entities/asset/asset-supply-source.ts';
import { ColumnType } from 'antd/es/table';

interface EditableCellProps {
  title: ReactNode;
  editable: boolean;
  dataIndex: keyof TableRow;
  record: TableRow;
  handleSave: (record: TableRow) => void;
  children: ReactNode;
}

interface TableRow {
  quantity: number;
  price: number;
}

const EditableContext = React.createContext<FormInstance | null>(null);

function EditableRow({children}: {children: ReactNode}) {
  const [form] = Form.useForm();
  return (
    <Form form={form} component={false}>
      <EditableContext.Provider value={form}>
        <tr>{children}</tr>
      </EditableContext.Provider>
    </Form>
  );
}

function EditableCell({title, editable, children, dataIndex, record, handleSave, ...restProps}: EditableCellProps) {
  const [editing, setEditing] = useState(false);
  const inputRef = useRef<HTMLInputElement>(null);
  const form = useContext(EditableContext);

  useEffect(() => {
    if (editing) {
      inputRef.current?.focus();
    }
  }, [editing]);

  const toggleEdit = () => {
    setEditing(!editing);
    if (form) {
      form.setFieldsValue({ [dataIndex]: record[dataIndex] });
    }
  };

  const save = async () => {
    try {
      if (form) {
        const values = await form.validateFields();
        toggleEdit();
        handleSave({ ...record, ...values });
      }
    } catch (errInfo) {
      console.log('Save failed:', errInfo);
    }
  };

  let childNode = children;

  if (editable) {
    childNode = editing ? (
      <Form.Item
        style={{ margin: 0 }}
        name={dataIndex}
        rules={[
          {
            required: true,
            message: `${title} is required.`,
          },
        ]}
      >
        <InputNumber ref={inputRef} onPressEnter={save} onBlur={save} />
      </Form.Item>
    ) : (
      <div
        className='pr-6'
        onClick={toggleEdit}
      >
        {children}
      </div>
    );
  }

  return <td {...restProps}>{childNode}</td>;
}

interface AssetSupplySourceTableProps {
  asset: Asset | undefined;
  onUpdate: (supplySource: AssetSupplySource) => void;
}

export default function AssetSupplySourceTable({asset, onUpdate}: AssetSupplySourceTableProps) {
  const [dataSource, setDataSource] = useState<AssetSupplySource[]>([]);

  useEffect(() => {
    if (asset?.supplySources) {
      setDataSource([
        ...(asset?.supplySources || []),
      ])
    }

  }, [asset?.supplySources]);

  const handleAddPrice = (supplySourceId: string) => {
    setDataSource((prev) =>
      prev.map((source) => {
        if (source.id !== supplySourceId) return source;
        const updatedSource = {
          ...source,
          prices: {
            ...(source.prices || {}),
            [Math.max(0, ...Object.keys(source.prices || {}).map(Number)) + 1]: 0,
          },
        }
        onUpdate(updatedSource);
        return updatedSource;
      })
    );
  };

  const handleDeletePrice = (supplySourceId: string, quantity: number) => {
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

  const handleSavePrice = (
    supplySourceId: string,
    updatedRow: TableRow,
    currentRow: TableRow
  ) => {
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

  const mainColumns = [
    {
      title: 'Supplier',
      dataIndex: 'supplierName',
    },
    {
      title: 'Supplier Reference',
      dataIndex: 'supplierReference',
    },
  ];

  const components = {
    body: {
      row: EditableRow,
      cell: EditableCell,
    },
  };

  const expandedRowRender = (record: AssetSupplySource) => {
    type EditableColumnType<T> = ColumnType<T> & {editable?: boolean};
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
            onConfirm={() => handleDeletePrice(record.id, row.quantity)}
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
            handleSavePrice(record.id, updatedRow, row),
        }),
      } as ColumnType<TableRow>
    );

    const data = Object.entries(record.prices || {}).map(([quantity, price]) => ({
      quantity: Number(quantity),
      price,
    }));

    return (
      <div>
        <Button
          onClick={() => handleAddPrice(record.id)}
          type="primary"
          style={{ marginBottom: 16 }}
        >
          Add Price
        </Button>
        <Table<TableRow>
          components={components}
          bordered
          dataSource={data}
          columns={editableColumns}
          rowKey={(row) => `${row.quantity}`}
          pagination={false}
        />
      </div>
    );
  };

  return (
    <Table<AssetSupplySource>
      bordered
      rowKey="id"
      dataSource={dataSource}
      columns={mainColumns}
      expandable={{ expandedRowRender }}
      pagination={false}
    />
  );
}