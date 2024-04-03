using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.HomeWorks.HomeWork2
{
    public class MessageHW2
    {
        public string? Text { get; set; }
        public DateTime? DateTime { get; set; }
        public string? NickNameFrom { get; set; }
        public string? NickNameTo { get; set; }

        public string SerializeMessageToJson() => JsonSerializer.Serialize(this); 
        
        public static MessageHW2? DeserializeFromJson(string message) => JsonSerializer.Deserialize<MessageHW2>(message);

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public override string? ToString()
        {
            return $"{DateTime}. Получено сообщение '{Text}' от '{NickNameFrom}'";
        }
    }

    
}
