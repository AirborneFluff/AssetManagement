import { Key, useRef, useState } from 'react';
import { Input, InputRef, Space, Button, TableColumnType, TableProps } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { flattenTableFilters } from '../../helpers/convertFilterArray.ts';
import { BaseEntity } from '../../data/entities/base-entity.ts';
import { PagedResponse } from '../../data/models/paged-response.ts';

export interface BaseTableParams {
  pageNumber: number;
  pageSize: number;
  sortOrder?: 'descend' | 'ascend';
  sortField?: string | undefined;
}

export type SearchableColumnProps<T> = (TableColumnType<T> & {
  showSearch?: boolean;
})[];

export interface UseTableReturn<T> {
  columns: TableColumnType<T>[];
  dataSource: T[];
  loading: boolean;
  pagination: TableProps<T>['pagination'];
  onTableChange: TableProps<T>['onChange'];
  rowKey: (item: T) => string;
}

export default function useTable<T extends BaseEntity>(
  useQuery: (
    params: Record<string, Key>
  ) => {
    data?: PagedResponse<T>;
    isFetching: boolean;
    isError: boolean;
  }
): (columns: SearchableColumnProps<T>) => UseTableReturn<T> {
  const searchInput = useRef<InputRef>(null);
  const initialParams: BaseTableParams = {
    pageNumber: 1,
    pageSize: 10,
  };

  const [params, setParams] = useState<Record<string, Key>>({
    ...initialParams,
  });

  const { data, isFetching } = useQuery(params);

  const handleTableChange: TableProps<T>['onChange'] = (pagination, filters, sorter) => {
    const flatFilters = {
      pageNumber: pagination.current ?? 1,
      pageSize: pagination.pageSize ?? 10,
      sortOrder: Array.isArray(sorter) ? undefined : (sorter.order as string),
      sortField: Array.isArray(sorter) ? undefined : convertCamelToDot(sorter.columnKey as string),
      ...flattenTableFilters(filters),
    };
    setParams(flatFilters as Record<string, Key>);
  };

  function convertCamelToDot(camelCaseString: string): string {
    return camelCaseString?.replace(/([a-z])([A-Z])/g, '$1.$2').toLowerCase();
  }

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

  return (columns: SearchableColumnProps<T>): UseTableReturn<T> => {
    const enhancedColumns = columns.map((column) =>
      column.showSearch
        ? {
          ...column,
          ...getColumnSearchProps(),
        }
        : column
    );

    return {
      columns: enhancedColumns,
      dataSource: data?.items || [],
      loading: isFetching,
      pagination: {
        current: data?.pagination?.currentPage || 1,
        pageSize: data?.pagination?.pageSize || 10,
        total: data?.pagination?.totalCount || 0,
      },
      onTableChange: handleTableChange,
      rowKey: (item) => item.id
    };
  };
}