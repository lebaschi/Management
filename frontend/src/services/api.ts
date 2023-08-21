import axios from 'axios';
import { Installer } from '@/models/Installer';
import { Supervisor } from '@/models/Supervisor';
import { InstallerSupervisor } from '@/models/InstallerSupervisor';

const api = axios.create({
  baseURL: process.env.VUE_APP_API_URL || 'http://localhost:5055', 
});

export const apiClient = {
  async getInstallers(): Promise<Installer[]> {
    const response = await api.get<{ $values: Installer[] }>('/api/installer');
    return response.data.$values;
  },

  async createInstaller(installer: Installer): Promise<Installer> {
    const response = await api.post<Installer>('api/installer', installer); 
    return response.data;
  },

  async updateInstaller(installer: Installer): Promise<Installer> {
    const response = await api.put<Installer>(`api/installer/${installer.Id}`, installer); 
    return response.data;
  },

  async deleteInstaller(id: number): Promise<void> {
    await api.delete(`api/installer/${id}`); 
  },

  async getSupervisors(): Promise<Supervisor[]> {
    const response = await api.get<{ $values: Supervisor[] }>('api/supervisor');
    return response.data.$values;
  },

  async createSupervisor(supervisor: Supervisor): Promise<Supervisor> {
    const response = await api.post<Supervisor>('api/supervisor', supervisor); 
    return response.data;
  },

  async updateSupervisor(supervisor: Supervisor): Promise<Supervisor> {
    const response = await api.put<Supervisor>(`api/supervisor/${supervisor.supervisorId}`, supervisor); 
    return response.data;
  },

  async deleteSupervisor(id: number): Promise<void> {
    await api.delete(`api/supervisor/${id}`); 
  },

  async getInstallersWithSupervisors(): Promise<InstallerSupervisor[]> {
    const response = await api.get<{ $values: InstallerSupervisor[] }>('/api/installer/installers-with-supervisors');
    return response.data.$values;
  },

  async removeInstallerFromSupervisor(supervisorId: number, installerId: number): Promise<void> {
    await api.delete(`api/supervisor/${supervisorId}/installer/${installerId}`);
  },


}; 