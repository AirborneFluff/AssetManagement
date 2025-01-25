import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from '../entities/user.ts'

const initialState: userSliceState = {
  user: null,
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
    },
    logout: (state) => {
      state.user = null;
    },
  }
})

export const {
  setUser,
  logout
} = userSlice.actions;