import { AppModule } from './app-module.ts';

export interface User {
  id: string;
  email: string;
  role?: string | 'SuperUser';
  tenantId?: string;
  modules: AppModule[];
}