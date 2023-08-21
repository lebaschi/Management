import { Installer } from './Installer'; 

export interface Supervisor {
  supervisorId: number; 
  name: string;
  phoneNumber: string;
  installers: Installer[];
  }