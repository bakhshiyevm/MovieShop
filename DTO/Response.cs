using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	[Serializable]
	public class Response<T> 
	{
		public Guid Guid { get; set; }
		public T Data { get; set; }

		public Response(T data)
		{
			Guid = Guid.NewGuid();
			Data = data;
		}
	}



}
