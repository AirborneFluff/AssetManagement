import { ListScreenLayout } from '../../shared/layouts/ListScreenLayout.tsx';
import { Button, Table } from 'antd';
import useTable from '../../../core/hooks/table/useTable.tsx';
import { useGetTenantsQuery } from '../../../core/data/services/api/tenant-api.ts';
import {EyeOutlined} from '@ant-design/icons';
import { useSwitchTenantMutation } from '../../../core/data/services/api/auth-api.ts';
import { useDispatch } from 'react-redux';
import { setUser } from '../../../core/data/slices/user-slice.ts';

export default function TenantsListScreen() {
  const [switchTenant] = useSwitchTenantMutation();
  const dispatch = useDispatch();

  const handleOnSwitchTenant = (id: string) => {
    switchTenant(id)
      .unwrap()
      .then((user) => dispatch(setUser(user)));
  }

  const {
    columns,
    dataSource,
    loading,
    pagination,
    onTableChange,
    rowKey
  } = useTable(useGetTenantsQuery)([
    {
      title: 'Id',
      dataIndex: 'id',
      key: 'id'
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      sorter: true,
      sortDirections: ['descend', 'ascend']
    },
    {
      title: 'Licences',
      dataIndex: 'licences',
      key: 'licences'
    },
    {
      title: 'Action',
      key: 'operation',
      fixed: 'right',
      width: 100,
      render: (tenant) => (
        <Button
          icon={<EyeOutlined />}
          type='text'
          onClick={() => handleOnSwitchTenant(tenant.id)}
        >
          View
        </Button>
      )
    },
  ]);

  return (
    <ListScreenLayout>
      <Table
        columns={columns}
        dataSource={dataSource}
        loading={loading}
        pagination={pagination}
        onChange={onTableChange}
        rowKey={rowKey}
        bordered
      />
    </ListScreenLayout>
  )
}