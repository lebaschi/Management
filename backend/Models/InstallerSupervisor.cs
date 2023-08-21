using System.ComponentModel.DataAnnotations.Schema;

namespace InstallerManagement.Models
{
    [Table("InstallerSupervisor")]
    public class InstallerSupervisor
    {
        [ForeignKey("InstallerID")]
        public int InstallerID { get; set; }
        public Installer Installer { get; set; }

        [ForeignKey("SupervisorID")]
        public int SupervisorID { get; set; }
        public Supervisor Supervisor { get; set; } 
    }
}