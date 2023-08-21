import { Supervisor } from './Supervisor'; 
import { Installer } from './Installer'; 

export interface InstallerSupervisor {
    installerID: number;
    installer: Installer; 
  
    SupervisorID: number;
    supervisor: Supervisor;  
  }