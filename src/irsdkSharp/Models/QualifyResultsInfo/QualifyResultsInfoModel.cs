using System.Collections.Generic;
using System.Linq;

namespace irsdkSharp.Models
{
    public class QualifyResultsInfoModel
    {
        public List<ResultModel> Results { get; set; }

        public override string ToString()
        {
            return $"QualifyResults: {string.Join("\n\t", Results.Select(r => r.ToString()))}";
        }
    }
}
