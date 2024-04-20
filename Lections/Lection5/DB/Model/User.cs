using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection5.DB.Model
{
    [Table("users")]
    public partial class User // partial нужен
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; } // ICollection - когда одно ко многим (User к Messages)
    }
}
