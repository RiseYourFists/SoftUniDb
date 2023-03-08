using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string format = "dd/MM/yyyy";

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(format));
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            // Not used in this example
            throw new NotImplementedException();
        }
    }
}
