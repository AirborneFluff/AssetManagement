import { BaseEntity } from '../base-entity.ts';

export interface AssetSupplySource extends BaseEntity {
  supplierId: string;
  supplierName?: string;

  supplierReference: string;
  quantityUnit?: string;
  prices: Record<number, number>;
}

export interface AssetSupplySourceForm {
  id?: string;
  assetId: string;
  supplierId: string;
  supplierReference: string;
  quantityUnit?: string;
  prices: Record<number, number>;
}