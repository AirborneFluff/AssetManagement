import { BaseEntity } from '../base-entity.ts';

export interface AssetStockLevel extends BaseEntity {
  stockLevel: number;
  createdOn: string;
}

export interface AssetStockLevelForm {
  assetId: string;
  stockLevel: number;
}