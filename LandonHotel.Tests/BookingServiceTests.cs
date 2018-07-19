using LandonHotel.Data;
using LandonHotel.Repositories;
using LandonHotel.Services;
using Moq;
using Xunit;

namespace LandonHotel.Tests
{
    public class BookingServiceTests
    {
        private Mock<IRoomsRepository> _roomsRepository;

        public BookingServiceTests()
        {
            _roomsRepository = new Mock<IRoomsRepository>();
        }

        private BookingService Subject()
        {
            return new BookingService(_roomsRepository.Object);
        }

        [Fact]
        public void IsBookingValid_NonSmoker_Valid()
        {
            var service = Subject();
            var isValid = service.IsBookingValid(1, new Booking { IsSmoking = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_NonSmoker_Invalid()
        {
            var service = Subject();
            var isValid = service.IsBookingValid(1, new Booking { IsSmoking = true });

            Assert.False(isValid);
        }

        [Fact]
        public void IsBookingValid_PetsNotAllowed_Invalid()
        {
            var service = Subject();
            _roomsRepository.Setup(r => r.GetRoom(1)).Returns(new Room() { ArePetsAllowed = false });

            var isValid = service.IsBookingValid(1, new Booking { HasPets = true });

            Assert.False(isValid);
        }

        [Fact]
        public void IsBookingValid_PetsAllowed_IsValid()
        {
            var service = Subject();
            _roomsRepository.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

            var isValid = service.IsBookingValid(1, new Booking { HasPets = true });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_NoPetsAllowed_IsValid()
        {
            var service = Subject();
            _roomsRepository.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = true });

            var isValid = service.IsBookingValid(1, new Booking { HasPets = false });

            Assert.True(isValid);
        }

        [Fact]
        public void IsBookingValid_NoPetsNotAllowed_IsValid()
        {
            var service = Subject();
            _roomsRepository.Setup(r => r.GetRoom(1)).Returns(new Room { ArePetsAllowed = false });

            var isValid = service.IsBookingValid(1, new Booking { HasPets = false });

            Assert.True(isValid);
        }

    }
}
