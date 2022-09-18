using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	[Serializable]
	public abstract class BaseDTO
	{
		public int Id { get; set; }
		public Guid Guid { get; set; }
		public int UserId { get; set; }
	}
}
