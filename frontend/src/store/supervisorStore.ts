import { defineStore } from 'pinia';
import { apiClient } from '@/services/api';
import { Supervisor } from '@/models/Supervisor';
import { InstallerSupervisor } from '@/models/InstallerSupervisor';

export const useSupervisorStore = defineStore('supervisor', {
  state: () => ({
    supervisors: [] as Supervisor[],
    installerSupervisors: [] as InstallerSupervisor[],
  }),
  actions: {
    async fetchSupervisors() {
      try {
        this.supervisors = await apiClient.getSupervisors();
        this.installerSupervisors = await apiClient.getInstallersWithSupervisors();
      } catch (error) {
        console.error('Error fetching supervisors:', error);
      }
    },

    async createSupervisor(supervisor: Supervisor) {
      try {
        const createdSupervisor = await apiClient.createSupervisor(supervisor);
        this.supervisors.push(createdSupervisor);
      } catch (error) {
        console.error('Error creating supervisor:', error);
      }
    },

    async updateSupervisor(supervisor: Supervisor) {
      try {
        const updatedSupervisor = await apiClient.updateSupervisor(supervisor);
        const index = this.supervisors.findIndex(s => s.supervisorId === updatedSupervisor.supervisorId);
        if (index !== -1) {
          this.supervisors[index] = updatedSupervisor;
        }
      } catch (error) {
        console.error('Error updating supervisor:', error);
      }
    },

    async deleteSupervisor(id: number) {
      try {
        await apiClient.deleteSupervisor(id);
        this.supervisors = this.supervisors.filter(s => s.supervisorId !== id);
      } catch (error) {
        console.error('Error deleting supervisor:', error);
      }
    },
    async removeInstallerFromSupervisor(supervisor: Supervisor, installerId: number) {
      try {
        await apiClient.removeInstallerFromSupervisor(supervisor.supervisorId, installerId);
        supervisor.installers = supervisor.installers.filter(i => i.Id !== installerId);
      } catch (error) {
        console.error('Error removing installer from supervisor:', error);
      }
    }
  },
}); 