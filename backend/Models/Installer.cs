using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InstallerManagement.Models
{
    public class Installer
    {
        [Key]
        [Column("InstallerID")]
        public int Id { get; set; } 

        [Column("Name")]
        public string Name { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [ForeignKey("SupervisorId")]
        public int SupervisorId { get; set; }

        [JsonIgnore]
        public ICollection<InstallerSupervisor> InstallerSupervisors { get; set; }
    }
}