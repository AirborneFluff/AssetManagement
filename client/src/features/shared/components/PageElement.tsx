import { Card, CardProps } from 'antd';
import { ReactNode } from 'react';

interface PageElementProps {
  title?: ReactNode;
  children?: ReactNode;
  styles?: CardProps['styles'];
}

export default function PageElement({title, styles, children}: PageElementProps) {
  const cardStyles: CardProps['styles'] = {
    header: {backgroundColor: 'rgb(250, 250, 250)'}
  }

  return (
    <Card
      size="small"
      styles={{...cardStyles, ...styles}}
      title={title}
    >
      {children}
    </Card>
  )
}