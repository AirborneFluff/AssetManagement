export interface FormProps<T> {
  onSubmit: (data: T) => void;
  isLoading: boolean;
}