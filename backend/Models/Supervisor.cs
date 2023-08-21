using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstallerManagement.Models
{
    public class Supervisor
    {
        [Column("SupervisorID")]
        public int SupervisorId { get; set; }
        [Column("Name")]
        public string Name { get; set; } 
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<InstallerSupervisor> InstallerSupervisors { get; set; }
        public ICollection<InstallerSupervisor> Installers { get; set; }
    }
}