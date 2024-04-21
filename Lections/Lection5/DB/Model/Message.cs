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
    [Table("messages")]
    public partial class Message
    {
        [Key]   // ключевой столбец
        [Column("id")]
        public int Id { get; set; } // должен быть паблик и быть свойством

        [Column("message")]
        public string MessageContent { get; set; }


        [ForeignKey("user_id")]  // внешний ключ на таблицу User
        public virtual User User { get; set; } // virtual нужен 
    }
}
