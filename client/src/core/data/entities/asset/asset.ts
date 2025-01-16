import { AssetCategory } from './asset-category.ts';
import { BaseEntity } from '../base-entity.ts';
import { AssetSupplySource } from './asset-supply-source.ts';
import { AssetStockLevel } from './asset-stock-level.ts';

export interface Asset extends BaseEntity {
  category?: AssetCategory;
  description: string;
  tags: AssetTag[];
  stockLevel: number;
  supplySources: AssetSupplySource[];
  historicStockLevels: AssetStockLevel[];
}

export interface AssetTag {
  id: string;
  tag: string;
}

export interface AssetForm {
  id?: string;
  categoryId: string;
  description: string;
  stockLevel: number;
  tags?: string[];
}