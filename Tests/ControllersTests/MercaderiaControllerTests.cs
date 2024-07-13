

namespace Tests.ControllersTests
{
    public class MercaderiaControllerTests
    {
        [Fact]
        public async Task SearchMercaderia_ShouldReturnListOfMercaderiaGetResponse()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task SearchMercaderia_ShouldReturnEmptyList()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_MercaderiaIsValid_ShouldReturnMercaderiaResponseWithStatusCode201()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_TipoMercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_NombreMercaderiaDoesExist_ShouldReturnBadRequestWithStatusCode409()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById_IdIsNegative_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById_IdDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById__ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_TipoMercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_NombreMercaderiaAlreadyInUse_ShouldReturnBadRequestWithStatusCode409()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task DeleteMercaderia_MercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task DeleteMercaderia_MercaderiaIsOnComanda_ShouldReturnBadRequestWithStatusCode409()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task DeleteMercaderia_ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }
    }
}
