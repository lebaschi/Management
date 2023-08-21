
<template>
  <v-container class="fill-height">
    <v-responsive class="d-flex text-center fill-height">
      <h1 class="text-h2">Employee List</h1>

      <v-row class="d-flex align-center justify-center">
        <v-expansion-panels class="w-50 listContainer pa-4 elevation-8">
          <v-expansion-panel v-for="installer in installersWithSupervisors" :key="installer.Id">
            <v-expansion-panel-title>{{ installer.name }}</v-expansion-panel-title>
            <v-expansion-panel-text>
              <v-row class="text-start my-2">
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>Phone Number:</p>
                  </v-sheet>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>{{ installer.phoneNumber }}</p>
                  </v-sheet>
                </v-col>
              </v-row>
              <v-divider></v-divider>
              <v-row class="text-start my-2">
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>Supervisors:</p>
                  </v-sheet>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-sheet class="d-flex mb-4" v-for="supervisor in installer.supervisors" :key="supervisor.supervisorId">
                    <p v-if="supervisor">{{ supervisor.name }}</p>

                  </v-sheet>
                </v-col>
              </v-row>
            </v-expansion-panel-text>
          </v-expansion-panel>
          <v-expansion-panel v-for="supervisor in supervisors" :key="supervisor.supervisorId">
            <v-expansion-panel-title>{{ supervisor.name }}</v-expansion-panel-title>
            <v-expansion-panel-text>
              <!-- Display Phone Number -->
              <v-row class="text-start my-2">
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>Phone Number:</p>
                  </v-sheet>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>{{ supervisor.phoneNumber }}</p>
                  </v-sheet>
                </v-col>
              </v-row>
              <v-divider></v-divider>
              <v-row class="text-start my-2">
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <p>Installers:</p>
                  </v-sheet>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-sheet class="d-flex mb-4">
                    <p v-for="(installer, index) in supervisor.installers" :key="index" class="pr-3">
                      {{ installer.name }}</p>
                    <v-btn density="compact" size="medium" color="red" icon="mdi-minus-circle"
                      @click="removeInstaller(supervisor, installer)"></v-btn>
                  </v-sheet>
                </v-col>
                <v-col cols="12" sm="4">
                  <v-sheet>
                    <v-autocomplete clearable label="Add Installer" :items="getAvailableInstallers(supervisor)" multiple
                      v-model="selectedInstallers" variant="solo"></v-autocomplete>
                  </v-sheet>
                </v-col>
              </v-row>
            </v-expansion-panel-text>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-row>
    </v-responsive>
  </v-container>
</template>

<style>
h1::first-letter {
  color: #ff7f50;
}

h1 {
  margin: 50px 0;
}

.listContainer {
  background-color: #f8f8ff;
  border-radius: 10px;
}
</style>

<script setup lang="ts">
import { Supervisor } from '@/models/Supervisor';
import { useInstallerStore, } from '@/store/installerStore';
import { useSupervisorStore } from '@/store/supervisorStore';
import { onMounted, computed } from 'vue';

const installerStore = useInstallerStore();
const supervisorStore = useSupervisorStore();

// Fetch installers and supervisors on component mount
onMounted(async () => {
  await installerStore.fetchInstallers();
  await supervisorStore.fetchSupervisors();
});

const removeInstaller = async (supervisor: Supervisor, installerId: number) => {
  try {
    await supervisorStore.removeInstallerFromSupervisor(supervisor, installerId);
    await supervisorStore.fetchSupervisors();
  } catch (error) {
    console.error('Error removing installer from supervisor:', error);
  }
};

const getAvailableInstallers = (supervisor: Supervisor) => {
  return installerStore.installers.filter(installer => !supervisor.installers.some(i => i.Id === installer.Id));
};

const installers = computed(() => installerStore.installers);
const supervisors = computed(() => supervisorStore.supervisors);

const installersWithSupervisors = computed(() => {
  return installers.value.map(installer => {
    const associatedSupervisors = supervisorStore.installerSupervisors
      .filter(is => is.installerID === installer.Id)
      .map(is => is.supervisor);

    return {
      ...installer,
      supervisors: associatedSupervisors
    };
  });

});

</script>