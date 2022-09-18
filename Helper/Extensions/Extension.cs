using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Extensions
{
	public static class Extension
	{
		public static void ThrowIfNull(this object? obj)
		{
			if (obj == null)
				throw new NullReferenceException();
		}

		public static string SerializeJson(this object? obj, bool isCamelCase = false)
		{
			obj.ThrowIfNull();

			var serializerSettings = new JsonSerializerSettings();
			
			if(isCamelCase)
			serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			
			serializerSettings.NullValueHandling = NullValueHandling.Ignore;

			return JsonConvert.SerializeObject(obj, serializerSettings);

		}
	}
}
