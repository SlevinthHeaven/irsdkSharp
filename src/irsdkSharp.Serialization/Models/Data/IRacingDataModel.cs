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
        public static IRacingDataModel Serialize(Span<byte> toSerialize, List<VarHeader> headers)
        {

            var dataModelProperties = typeof(DataModel).GetProperties().ToList();
            var carModelProperties = typeof(CarModel).GetProperties().ToList();

            var model = new DataModel();
            var missing = new List<VarHeader>();
            var cars = new CarModel[64];
            for (var i = 0; i < cars.Length; i++)
            {
                cars[i] = new CarModel();
            }

            foreach (var header in headers)
            {
                var dataModelProperty = dataModelProperties.FirstOrDefault(x => x.Name.ToLower() == header.Name.ToLower());
                var carModelProperty = carModelProperties.FirstOrDefault(x => x.Name.ToLower() == header.Name.ToLower());
                if (dataModelProperty != null)
                {
                    if (header.Count == 1)
                    {
                        dataModelProperty.SetValue(model, GetValue(toSerialize, header.Offset, header.Length, header.Type));
                    }
                    else
                    {
                        var values = new object[header.Count];
                        for (int i = 0; i < header.Count; i++)
                        {
                            values[i] = GetValue(toSerialize, header.Offset + ((header.Length / header.Count) * i), (header.Length / header.Count), header.Type);

                        }
                        // dataModelProperty.SetValue(model, values);
                    }
                }
                else if (carModelProperty != null)
                {
                    for (int i = 0; i < header.Count; i++)
                    {
                        carModelProperty.SetValue(cars[i], GetValue(toSerialize, header.Offset + ((header.Length / header.Count) * i), (header.Length / header.Count), header.Type));
                    }
                }
                else
                {
                    missing.Add(header);
                }
            }
            model.Cars = cars.ToList();
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
