import { Supervisor } from './Supervisor'; 

export interface Installer {
    Id: number; 
    name: string;
    phoneNumber: string;
    supervisors: Supervisor[]; 
  }