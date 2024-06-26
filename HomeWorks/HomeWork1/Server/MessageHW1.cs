﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.HomeWorks.HomeWork1
{
    public class MessageHW1
    {
        public string? Text { get; set; }
        public DateTime? DateTime { get; set; }
        public string? NickNameFrom { get; set; }
        public string? NickNameTo { get; set; }

        public string SerializeMessageToJson() => JsonSerializer.Serialize(this); 
        
        public static MessageHW1? DeserializeFromJson(string message) => JsonSerializer.Deserialize<MessageHW1>(message);

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
