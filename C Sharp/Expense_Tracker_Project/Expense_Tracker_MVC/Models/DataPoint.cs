using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Expense_Tracker_MVC.Models
{
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, decimal? y)
        {
            this.Label = label;
            this.Y = y;
        }


        [DataMember(Name = "label")]
        public string Label = "";

   
        [DataMember(Name = "y")]
        public Nullable<decimal> Y = null;
    }

	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class DataPointList
	{
		[DataMember(Name = "type")]
		public string Type { get; set; } = "line";
		[DataMember(Name = "name")]
		public string Name { get; set; } = "";
		[DataMember(Name = "showInLegend")]
		public bool ShowInLegend { get; set; } = true;
		[DataMember(Name = "dataPoints")]
		public List<DataPoint> DataPoints { get; set; }
	}
}
