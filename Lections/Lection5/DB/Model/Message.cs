using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection5.DB.Model
{
    [Table("message")]
    public partial class Message
    {
        [Key]       // ключевой столбец
        [Column("Id")]
        public int Id {  get; set; } // должен быть public и должен быть свойством

        [Column("message")]
        public string MessageContent { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } // virtual нужен связан с UserId и классом User
     }
}
