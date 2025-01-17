using AutoFixture;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Dominio.Entidades;
using TaskManager.Infraestructura.Data;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace TaskManager.Test
{
    public class SuministradorServiceTest
    {
        private readonly ISuministradorRepositorio _suministradorRepositorio;
        private readonly ISuministradorService _suministradorService;
        private readonly Mock<ISuministradorRepositorio> _suministradorRepositorioMock;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public SuministradorServiceTest(ITestOutputHelper testOutputHelper)
        {
            _fixture = new Fixture();
            _suministradorRepositorioMock = new Mock<ISuministradorRepositorio>();
            _suministradorRepositorio = _suministradorRepositorioMock.Object;

            var suministradorInitialData = new List<SuministradorModel>();

            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
            ApplicationDbContext dbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp => temp.Suministradores, suministradorInitialData);

            _suministradorService = new SuministradorService(_suministradorRepositorio);
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task ObtenerListadoSuministradores()
        {
            // ARRANGE
            var suministradorModel = _fixture.Build<SuministradorModel>().Create();
            var suministradorList = new List<SuministradorModel> { suministradorModel };

            _suministradorRepositorioMock
                .Setup(repo => repo.ObtenerListadoSuministradorAsync())
                .ReturnsAsync(suministradorList);

            // ACT
            var result = await _suministradorService.ObtenerListadoDeLosSuministradoresAsync();

            // ASSERT
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Should().ContainSingle()
                   .Which.Should().BeEquivalentTo(suministradorModel);
            _suministradorRepositorioMock.Verify(repo => repo.ObtenerListadoSuministradorAsync(), Times.Once);
        }
    }
}
