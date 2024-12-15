import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from '../entities/user.ts'

const storedUser = localStorage.getItem('user');
const initialState: userSliceState = {
  user: storedUser !== null ? JSON.parse(storedUser) : null,
};

interface userSliceState {
  user: User | null
}

export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User>) => {
      state.user = action.payload;
      localStorage.setItem('user', JSON.stringify(action.payload));
    },
    logout: (state) => {
      state.user = null;
      localStorage.removeItem('user');
    },
  }
})

export const {
  setUser,
  logout
} = userSlice.actions;