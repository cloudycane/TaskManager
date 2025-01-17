using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;
using Xunit;
using FluentAssertions;
using AutoFixture;
using System.Data.Entity;
using EntityFrameworkCoreMock;
using Xunit.Abstractions;
using AutoMapper;

namespace TaskManager.Tests
{
    public class SuministradorServiceTest
    {
        private readonly ISuministradorService _suministradorService;
        private readonly ISuministradorRepositorio _suministradorRepositorio;
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<ISuministradorRepositorio> _suministradorRepositorioMock;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public SuministradorServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();

            // Mock the repository
            _suministradorRepositorioMock = new Mock<ISuministradorRepositorio>();
            _suministradorRepositorio = _suministradorRepositorioMock.Object;

            // Mock the AutoMapper
            var mapperMock = new Mock<IMapper>();

            // Mock DbContext and the DbSet
            var dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var suministradorInitialData = new List<SuministradorModel>()
             {
                new SuministradorModel() { Id = 1, RazonSocial = "Sum1", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo" },
                new SuministradorModel() { Id = 2, RazonSocial = "Sum2", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo"},
                new SuministradorModel() { Id = 3, RazonSocial = "Sum3", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo" }
            };

            var dbSetMock = new Mock<Microsoft.EntityFrameworkCore.DbSet<SuministradorModel>>();
            dbSetMock.As<IQueryable<SuministradorModel>>().Setup(m => m.Provider).Returns(suministradorInitialData.AsQueryable().Provider);
            dbSetMock.As<IQueryable<SuministradorModel>>().Setup(m => m.Expression).Returns(suministradorInitialData.AsQueryable().Expression);
            dbSetMock.As<IQueryable<SuministradorModel>>().Setup(m => m.ElementType).Returns(suministradorInitialData.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<SuministradorModel>>().Setup(m => m.GetEnumerator()).Returns(suministradorInitialData.GetEnumerator());

            _suministradorRepositorioMock
            .Setup(repo => repo.ObtenerListadoSuministradorAsync())
            .ReturnsAsync(suministradorInitialData);

            // Initialize the SuministradorService with both the mocked repository and mapper
            _suministradorService = new SuministradorService(mapperMock.Object, _suministradorRepositorio);

            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public async Task ObtenerListadoSuministradores_ShouldReturnListOfSuministradores()
        {
            // ACT
            var result = await _suministradorService.ObtenerListadoDeLosSuministradoresAsync();

            // Expected data used in your mock setup
            var expectedData = new List<SuministradorModel>
            {
                new SuministradorModel() { Id = 1, RazonSocial = "Sum1", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo" },
                new SuministradorModel() { Id = 2, RazonSocial = "Sum2", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo"},
                new SuministradorModel() { Id = 3, RazonSocial = "Sum3", DireccionLinea1 = "Algo", DireccionLinea2 = "Algo", Localidad = "Algo", Provincia = "Algo", Pais = "Algo" }
            };

            // ASSERT
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expectedData);

            _suministradorRepositorioMock.Verify(repo => repo.ObtenerListadoSuministradorAsync(), Times.Once);
        }
    }
}
