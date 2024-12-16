export interface Asset {
  id: string;
  description: string;
  tags: AssetTag[];
}

export interface AssetTag {
  id: string;
  tag: string;
}

export interface AssetForm {
  id?: string;
  description: string;
  tags?: string[];
}