import {Table, Tag} from "antd";
import React from "react";

const { Column } = Table;

interface DataType {
  key: React.Key;
  description: string;
  serialNumber: string;
  location: string;
  category: string;
  tags: string[];
}

export default function AssetsListScreen() {
  const dataSource: DataType[] = [
    {
      key: '1',
      description: 'Item description test',
      serialNumber: '1234',
      location: 'Office',
      category: 'Electronics',
      tags: ['tag1', 'tag2']
    },
    {
      key: '2',
      description: 'Second item description',
      serialNumber: '5678',
      location: 'Warehouse',
      category: 'Furniture',
      tags: ['tag4', 'tag2']
    },
    {
      key: '3',
      description: 'Third item description',
      serialNumber: '9101',
      location: 'Lab',
      category: 'Lab Equipment',
      tags: ['tag1', 'tag3']
    }
  ];

  return (
    <div className={`h-full`}>
      <Table<DataType> dataSource={dataSource} bordered>
        <Column title="Description" dataIndex="description" key="description" />
        <Column title="Serial Number" dataIndex="serialNumber" key="serialNumber" />
        <Column title="Location" dataIndex="location" key="location" />
        <Column title="Category" dataIndex="category" key="category" />
        <Column
          title="Tags"
          dataIndex="tags"
          key="tags"
          render={(tags: string[]) => (
            <>
              {tags.map((tag) => {
                let color = tag.length > 5 ? 'geekblue' : 'green';
                if (tag === 'loser') {
                  color = 'volcano';
                }
                return (
                  <Tag color={color} key={tag}>
                    {tag.toUpperCase()}
                  </Tag>
                );
              })}
            </>
          )}
        />
      </Table>
    </div>
  )
}