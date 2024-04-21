using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection5.DB.Model
{
    [Table("Users")]
    public partial class User // partial нужен
    {
        [Key, Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        public virtual ICollection<Message> Messages { get; set; } // навигация из User к Message, нужны partial и virtual.
                                                                   // ICollection - при отношении одного ко многим,
                                                                   // во всех остальных случаях просто объект




    }
}
