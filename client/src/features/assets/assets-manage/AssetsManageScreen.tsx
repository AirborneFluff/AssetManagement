import AssetManageForm from './AssetManageForm.tsx';

export default function AssetManageScreen() {

  return (
    <AssetManageForm onSubmit={(data) => console.log(data)} />
  );
}