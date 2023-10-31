using AutoMapper;
using CinemaTicketBooking.Common.Dtos;
using CinemaTicketBooking.Common.Entitiea;
using CinemaTicketBooking.Common.Exceptions;
using CinemaTicketBooking.Common.Repositories;

namespace CinemaTicketBooking.Common.Services;

public class BaseService<TEntity, TEntityDto, TRepository> : IBaseService<TEntityDto>
    where TEntity : BaseEntity
    where TEntityDto : BaseDto
    where TRepository : IRepository<TEntity>
{
    private readonly TRepository _repository;
    private readonly IMapper _mapper;

    public BaseService(TRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntityDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TEntityDto>>(entities);
    }

    public async Task<TEntityDto> GetAsync(int id)
    {
        var entity = await _repository.GetAsync(id) ?? throw new NotFoundException(id, typeof(TEntity).Name);
        return _mapper.Map<TEntityDto>(entity);
    }

    public async Task<TEntityDto> AddAsync(TEntityDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        if (entity.Id > 0)
        {
            throw new ValidationException(
                $"Trying to Create {typeof(TEntity).Name} with id != 0. Id should be 0 on creation.");
        }

        await this.OnSaving(entity);
        var createdEntity = await _repository.AddAsync(entity);
        return _mapper.Map<TEntityDto>(createdEntity);
    }

    public async Task<TEntityDto> UpdateAsync(TEntityDto entityDto)
    {
        if (entityDto.Id <= 0)
        {
            throw new ValidationException($"Invalid {typeof(TEntity).Name} id.");
        }

        var entity = await _repository.GetAsync(entityDto.Id) ??
                     throw new NotFoundException(entityDto.Id, typeof(TEntity).Name);
        _mapper.Map(entityDto, entity);

        await this.OnSaving(entity);
        var updatedEntity = await _repository.UpdateAsync(entity);
        return _mapper.Map<TEntityDto>(updatedEntity);
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException($"Invalid {typeof(TEntity).Name} id.");
        }

        var entity = await _repository.GetAsync(id) ?? throw new NotFoundException(id, typeof(TEntity).Name);
        await _repository.DeleteAsync(entity);
    }

    protected virtual async Task OnSaving(TEntity entity)
    {
    }
}