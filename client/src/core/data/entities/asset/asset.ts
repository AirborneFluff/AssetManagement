import { AssetCategory } from './asset-category.ts';

export interface Asset {
  id: string;
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