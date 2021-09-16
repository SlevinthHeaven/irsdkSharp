using irsdkSharp.Models;
using System.Collections.Generic;
using System.Linq;

namespace irsdkSharp.Serialization.Models.Data
{
    public class IRacingDataModel
    {
        public static List<CarModel> SerializeCars(byte[] toSerialize, Dictionary<string, VarHeader> headers)
        {
            var cars = new CarModel[64];
            for (var i = 0; i < cars.Length; i++)
            {
                cars[i] = new CarModel(i, toSerialize, headers);
            }
            return cars.ToList();
        }

        public static IRacingDataModel Serialize(byte[] toSerialize, Dictionary<string, VarHeader> headers)
        {

            var model = new DataModel(toSerialize, headers);
           
            return new IRacingDataModel
            {
                Data = model
            };
        }
        
        public DataModel Data { get; set; }

        public List<VarHeader> Missing { get; set; } = new List<VarHeader>();
    }
}
