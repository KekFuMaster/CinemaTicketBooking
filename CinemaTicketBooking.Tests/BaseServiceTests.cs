using AutoMapper;
using CinemaTicketBooking.Common.Repositories;
using CinemaTicketBooking.Common.Services;
using CinemaTicketBooking.Core.Dtos;
using CinemaTicketBooking.Core.Entities;
using Moq;
using Xunit;

namespace CinemaTicketBooking.Tests;

public class BaseServiceTests
{
    private readonly Mock<IRepository<Movie>> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly BaseService<Movie, MovieDto, IRepository<Movie>> _service;

    public BaseServiceTests()
    {
        _mockRepository = new Mock<IRepository<Movie>>();
        _mockMapper = new Mock<IMapper>();
        _service = new BaseService<Movie, MovieDto, IRepository<Movie>>(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnDtos()
    {
        // Arrange
        var movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie 1" },
            new Movie { Id = 2, Title = "Movie 2" }
        };
        var movieDtos = new List<MovieDto>
        {
            new MovieDto { Id = 1, Title = "Movie 1" },
            new MovieDto { Id = 2, Title = "Movie 2" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(movies);
        _mockMapper.Setup(mapper => mapper.Map<IEnumerable<MovieDto>>(movies)).Returns(movieDtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(movieDtos, result);
    }
    
    [Fact]
    public async Task GetAsync_ShouldReturnDto()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Movie 1" };
        var movieDto = new MovieDto { Id = 1, Title = "Movie 1" };
        _mockRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(movie);
        _mockMapper.Setup(mapper => mapper.Map<MovieDto>(movie)).Returns(movieDto);

        // Act
        var result = await _service.GetAsync(1);

        // Assert
        Assert.Equal(movieDto, result);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnDto()
    {
        // Arrange
        var movieDto = new MovieDto { Title = "New Movie" };
        var movie = new Movie { Title = "New Movie" };
        _mockMapper.Setup(mapper => mapper.Map<Movie>(movieDto)).Returns(movie);
        _mockRepository.Setup(repo => repo.AddAsync(movie)).ReturnsAsync(new Movie { Id = 1, Title = "New Movie" });
        _mockMapper.Setup(mapper => mapper.Map<MovieDto>(It.IsAny<Movie>())).Returns(new MovieDto { Id = 1, Title = "New Movie" });

        // Act
        var result = await _service.AddAsync(movieDto);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("New Movie", result.Title);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedDto()
    {
        // Arrange
        var movieDto = new MovieDto { Id = 1, Title = "Updated Movie" };
        var movie = new Movie { Id = 1, Title = "Movie 1" };
        _mockRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(movie);
        _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Movie>())).ReturnsAsync(new Movie { Id = 1, Title = "Updated Movie" });
        _mockMapper.Setup(mapper => mapper.Map<MovieDto>(It.IsAny<Movie>())).Returns(new MovieDto { Id = 1, Title = "Updated Movie" });

        // Act
        var result = await _service.UpdateAsync(movieDto);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Updated Movie", result.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotThrowException()
    {
        // Arrange
        var movie = new Movie { Id = 1, Title = "Movie 1" };
        _mockRepository.Setup(repo => repo.GetAsync(1)).ReturnsAsync(movie);

        // Act & Assert
        await _service.DeleteAsync(1);  // This should not throw an exception
    }
    
    
}