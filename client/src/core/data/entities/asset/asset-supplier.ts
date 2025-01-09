export interface AssetSupplier {
  id: string;
  name: string;
  website?: string;
}

export interface AssetSupplierForm {
  id?: string;
  name: string;
  website?: string;
}