import { useLogoutMutation } from '../data/services/api/auth-api.ts';
import { useDispatch } from 'react-redux';
import { logout } from '../data/slices/user-slice.ts';

export const useLogout = () => {
  const [logoutUser] = useLogoutMutation();
  const dispatch = useDispatch();

  return async (onSuccess?: () => void, onError?: (error: unknown) => void) => {
    try {
      await logoutUser().unwrap();
      dispatch(logout());
      onSuccess?.();
    } catch (error) {
      onError?.(error);
    }
  };
};