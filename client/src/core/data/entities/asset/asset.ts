import { AssetCategory } from './asset-category.ts';
import { BaseEntity } from '../base-entity.ts';

export interface Asset extends BaseEntity {
  category?: AssetCategory;
  description: string;
  tags: AssetTag[];
}

export interface AssetTag {
  id: string;
  tag: string;
}

export interface AssetForm {
  id?: string;
  categoryId: string;
  description: string;
  tags?: string[];
}