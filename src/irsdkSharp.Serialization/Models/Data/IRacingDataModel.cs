using irsdkSharp.Enums;
using irsdkSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace irsdkSharp.Serialization.Models.Data
{
    public class IRacingDataModel
    {
        public static IRacingDataModel Serialize(Span<byte> toSerialize, Dictionary<string, VarHeader> headers)
        {

            var model = new DataModel();
            var missing = new List<VarHeader>();
            var cars = new CarModel[64];
            for (var i = 0; i < cars.Length; i++)
            {
                cars[i] = new CarModel { CarIdx = i };
            }

            foreach(var property in ExpressionAccessors.CarModelProperties)
            {

                var name = ExpressionAccessors.ModelSetters[$"CarModel::{property.Name}"];
                
                if (headers.ContainsKey(property.Name) && headers[property.Name].Count == 64)
                {
                    for (var i = 0; i < cars.Length; i++)
                    {
                        name.Invoke(cars[i], GetValue(toSerialize, headers[property.Name].Offset + ((headers[property.Name].Length / headers[property.Name].Count) * i), (headers[property.Name].Length / headers[property.Name].Count), headers[property.Name].Type));
                        //property.SetValue(cars[i], GetValue(toSerialize, headers[property.Name].Offset + ((headers[property.Name].Length / headers[property.Name].Count) * i), (headers[property.Name].Length / headers[property.Name].Count), headers[property.Name].Type));
                    }
                } 
            }

            foreach (var property in ExpressionAccessors.DataModelProperties)
            {
                var name = ExpressionAccessors.ModelSetters[$"DataModel::{property.Name}"];

                if (headers.ContainsKey(property.Name))
                {
                    if (headers[property.Name].Count == 1)
                    {
                        name.Invoke(model, GetValue(toSerialize, headers[property.Name].Offset, headers[property.Name].Length, headers[property.Name].Type));
                    }
                    else
                    {
                        var values = new object[headers[property.Name].Count];
                        for (int i = 0; i < headers[property.Name].Count; i++)
                        {
                            values[i] = GetValue(toSerialize, headers[property.Name].Offset + ((headers[property.Name].Length / headers[property.Name].Count) * i), (headers[property.Name].Length / headers[property.Name].Count), headers[property.Name].Type);

                        }
                    }

                }
            }
     
            model.Cars = cars;
            return new IRacingDataModel
            {
                Data = model,
                Missing = missing
            };
        }

        private static object GetValue(Span<byte> array, int start, int length, VarType type)
        {
            if (type == VarType.irChar)
            {
                return Encoding.Default.GetString(array.Slice(start, length)).TrimEnd(new char[] { '\0' });
            }
            else if (type == VarType.irBool)
            {
                return BitConverter.ToBoolean(array.Slice(start, 1));
            }
            else if (type == VarType.irInt || type == VarType.irBitField)
            {
                return BitConverter.ToInt32(array.Slice(start, 4));
            }
            else if (type == VarType.irFloat)
            {
                return BitConverter.ToSingle(array.Slice(start, 4));
            }
            else if (type == VarType.irDouble)
            {
                return BitConverter.ToDouble(array.Slice(start, 8));
            }

            return null;
        }

        public DataModel Data { get; set; }

        public List<VarHeader> Missing { get; set; } = new List<VarHeader>();
    }
}
