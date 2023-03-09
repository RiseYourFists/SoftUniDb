using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CarDealer
{
    public class DecimalConverter : JsonConverter
    {
        private readonly string format = "0.00";

        public DecimalConverter()
        {
           
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? (decimal?)null : Convert.ToDecimal(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var decimalValue = (decimal)value;
            var formattedValue = decimalValue.ToString(format, CultureInfo.InvariantCulture);
            writer.WriteValue(formattedValue);
        }
    }
}
