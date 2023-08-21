import { defineStore } from 'pinia';
import { apiClient } from '@/services/api';
import { Installer } from '@/models/Installer';
import { InstallerSupervisor } from '@/models/InstallerSupervisor';

export const useInstallerStore = defineStore('installer', {
  state: () => ({
    installers: [] as Installer[], 
    installerSupervisors: [] as InstallerSupervisor[], 
    installersWithSupervisors: [] as Installer[], 
  }),
  actions: {
    async fetchInstallers() {
      try { 
        this.installers = await apiClient.getInstallers();
        this.installerSupervisors = await apiClient.getInstallersWithSupervisors();;
        this.getInstallersWithSupervisors();
      } catch (error) {
        console.error('Error fetching installers:', error);
      }
    },

    async createInstaller(installer: Installer) {
      try {
        const createdInstaller = await apiClient.createInstaller(installer);
        this.installers.push(createdInstaller);
      } catch (error) {
        console.error('Error creating installer:', error);
      }
    },

    async updateInstaller(installer: Installer) {
      try {
        const updatedInstaller = await apiClient.updateInstaller(installer);
        const index = this.installers.findIndex(i => i.Id === updatedInstaller.Id);
        if (index !== -1) {
          this.installers[index] = updatedInstaller;
        }
      } catch (error) {
        console.error('Error updating installer:', error);
      }
    },
    
    async deleteInstaller(id: number) {
      try {
        await apiClient.deleteInstaller(id);
        this.installers = this.installers.filter(i => i.Id !== id);
      } catch (error) {
        console.error('Error deleting installer:', error);
      }
    },
    getInstallersWithSupervisors() {
      this.installersWithSupervisors = this.installers.map(installer => {
        const installerSupervisors = this.installerSupervisors
          .filter(is => is.installerID === installer.Id) 
          .map(is => is.supervisor);
    
        return { ...installer, supervisors: installerSupervisors };
      }); 
  
    },
  },
}); 