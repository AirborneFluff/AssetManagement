import { Key, useState, useRef } from 'react';
import { Button, Input, Space, TableColumnType, InputRef, TableProps } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { flattenTableFilters } from '../helpers/convertFilterArray.ts';

export interface BaseTableParams {
  pageNumber: number;
  pageSize: number;
  sortOrder?: 'descend' | 'ascend';
  sortField?: string | undefined;
}

export default function useTable<T>() {
  const searchInput = useRef<InputRef>(null);
  const initialParams: BaseTableParams = {
    pageNumber: 1,
    pageSize: 10
  };

  const [params, setParams] = useState<Record<string, Key>>({
    ...initialParams
  });

  const handleTableChange: TableProps<T>['onChange'] = (pagination, filters, sorter) => {
    const flatFilters = {
      pageNumber: pagination.current ?? 1,
      pageSize: pagination.pageSize ?? 10,
      sortOrder: Array.isArray(sorter) ? undefined : (sorter.order as string),
      sortField: Array.isArray(sorter) ? undefined : (sorter.field as string),
      ...flattenTableFilters(filters)
    }
    console.log(flatFilters);
    setParams(flatFilters as Record<string, Key>);
  };

  const getColumnSearchProps = (): Partial<TableColumnType<T>> => ({
    filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters, close }) => (
      <div style={{ padding: 8 }} onKeyDown={(e) => e.stopPropagation()}>
        <Input
          ref={searchInput}
          placeholder={`Search`}
          value={selectedKeys[0]}
          onChange={(e) => setSelectedKeys(e.target.value ? [e.target.value] : [])}
          onPressEnter={() => confirm()}
          style={{ marginBottom: 8, display: 'block' }}
        />
        <Space>
          <Button
            type="primary"
            onClick={() => confirm()}
            icon={<SearchOutlined />}
            size="small"
            style={{ width: 90 }}
          >
            Search
          </Button>
          <Button
            onClick={() => clearFilters && clearFilters()}
            size="small"
            style={{ width: 90 }}
          >
            Clear
          </Button>
          <Button type="link" size="small" onClick={close}>
            Close
          </Button>
        </Space>
      </div>
    ),
    filterIcon: (filtered: boolean) => (
      <SearchOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
    ),
    filterDropdownProps: {
      onOpenChange(open) {
        if (open) {
          setTimeout(() => searchInput.current?.select(), 100);
        }
      },
    },
  });

  return {
    params,
    getColumnSearchProps,
    handleTableChange,
  };
}