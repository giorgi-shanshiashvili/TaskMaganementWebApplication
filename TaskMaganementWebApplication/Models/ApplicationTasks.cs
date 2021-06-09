using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaganementWebApplication.Models
{
    public class  ApplicationTasks
    {
        public int Id { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Priority")]
        public string Priority { get; set; }
        [DisplayName("Status")]
        public StatusType Status { get; set; }
    }

    public enum StatusType
    {
        New,
        InProgress,
        Done
    }
}
