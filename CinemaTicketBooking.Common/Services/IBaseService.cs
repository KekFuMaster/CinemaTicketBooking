namespace CinemaTicketBooking.Common.Services;

public interface IBaseService<TEntityDto>
{
    Task<IEnumerable<TEntityDto>> GetAllAsync();
    Task<TEntityDto> GetAsync(int id);
    Task<TEntityDto> AddAsync(TEntityDto movieDto);
    Task<TEntityDto> UpdateAsync(TEntityDto movieDto);
    Task DeleteAsync(int id);
}