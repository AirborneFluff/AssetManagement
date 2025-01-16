import { BaseEntity } from '../base-entity.ts';

export interface AssetStockLevel extends BaseEntity {
  stockLevel: number;
}