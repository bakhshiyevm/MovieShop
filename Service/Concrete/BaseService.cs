using AutoMapper;
using DataAccess;
using DataAccess.Entites;
using DTO;
using Helper.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
	public abstract class BaseService<TRqDTO, TEntity, TRsDTO> 
		: IBaseService<TRqDTO, TEntity, TRsDTO>
		where TEntity : BaseEntity 
		where TRsDTO : class
		where TRqDTO : class
	{
		//protected readonly IMemoryCache _memoryCache;
		protected readonly IMapper _mapper;
		protected readonly ILogger _logger;
		protected readonly DataContext _dbContext;
		protected readonly DbSet<TEntity> _dbSet;


		protected BaseService(IMapper mapper, ILogger logger, DataContext dbContext)
		{
			_logger = logger;
			_mapper = mapper;
			_dbContext = dbContext;
			_dbSet = dbContext.Set<TEntity>();
		}

		public TRsDTO Create(TRqDTO dto, int userId)
		{
			ArgumentNullException.ThrowIfNull(dto, nameof(dto));
			
			var ent = _mapper.Map<TEntity>(dto);
			
			_dbSet.AddAsync(ent);
			
			_dbContext.SaveChanges(userId);

			var res = _mapper.Map<TRsDTO>(ent);

			return res;
		}

		public int Delete(int id, int userId)
		{
			var ent = _dbSet.Find(id);

			ent.ThrowIfNull();

			_dbContext.Remove(ent);

			_dbContext.SaveChanges(userId);
			return ent.Id;
		}

		public TRsDTO Get(int id)
		{
			var ent = _dbSet.Find(id);

			var res = _mapper.Map<TRsDTO>(ent);

			return res;
		}

		public IEnumerable<TRsDTO> Get()
		{
			var ents = _dbSet.ToList();

			var res = _mapper.Map<IEnumerable<TRsDTO>>(ents);

			return res;
		}

		public IEnumerable<TRsDTO> Get(int page, int pageSize)
		{
			var ents = _dbSet.Skip((page - 1) * pageSize)
				.Take(pageSize).ToList();
			
			var res = _mapper.Map<IEnumerable<TRsDTO>>(ents);

			return res;
		}

		public void Update(TRqDTO dto, int userId)
		{
			throw new NotImplementedException();
		}
	}
}
