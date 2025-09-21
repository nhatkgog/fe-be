using BusinessObjects;
using BusinessObjects.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class BooksControllerTests : IClassFixture<WebApplicationFactory<FPTShopLaptopMVC_API.Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public BooksControllerTests(WebApplicationFactory<FPTShopLaptopMVC_API.Program> factory, ITestOutputHelper output)
        {
            _output = output;
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    // Remove existing DbContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<FptshopDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    // Add in-memory DbContext
                    services.AddDbContext<FptshopDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestLaptopDb");
                    });

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<FptshopDbContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    var users = new List<User>
            {
                new User { Email = "alice@example.com", Password = "Password123" },
                new User { Email = "bob@example.com", Password = "Password123" }
            };
                    db.Users.AddRange(users);
                    db.SaveChanges();

                    var laptops = new List<Laptop>
            {
                new Laptop { Name = "MacBook Air M2", Brand = "Apple", Price = 1200, StockQuantity = 10, CreatedAt = DateTime.UtcNow, User = users[0] },
                new Laptop { Name = "XPS 15", Brand = "Dell", Price = 1500, StockQuantity = 5, CreatedAt = DateTime.UtcNow, User = users[1] },
                new Laptop { Name = "HP Spectre", Brand = "HP", Price = 1300, StockQuantity = 7, CreatedAt = DateTime.UtcNow, User = users[0] }
            };
                    db.Laptops.AddRange(laptops);
                    db.SaveChanges();

                    var orders = new List<Order>
            {
                new Order { Laptop = laptops[0], User = users[1], Quantity = 1, OrderDate = DateTime.UtcNow },
                new Order { Laptop = laptops[1], User = users[0], Quantity = 2, OrderDate = DateTime.UtcNow },
                new Order { Laptop = laptops[2], User = users[1], Quantity = 1, OrderDate = DateTime.UtcNow }
            };
                    db.Orders.AddRange(orders);
                    db.SaveChanges();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetAll_ReturnsBooks()
        {
            var response = await _client.GetAsync("/api/laptops");
            response.EnsureSuccessStatusCode();

            var books = await response.Content.ReadFromJsonAsync<Laptop[]>();
            Assert.NotNull(books);
            Assert.NotEmpty(books);

            foreach (var book in books)
                _output.WriteLine($"Id: {book.LaptopId}, Name: {book.Name}, Brand: {book.Brand}");
        }

        //[Fact]
        //public async Task GetById_ReturnsBook()
        //{
        //    var response = await _client.GetAsync("/api/laptops/1");
        //    response.EnsureSuccessStatusCode();

        //    var book = await response.Content.ReadFromJsonAsync<Laptop>();
        //    Assert.NotNull(book);
        //    Assert.Equal(1, book.LaptopId);
        //}

        //[Fact]
        //public async Task Post_AddsBook()
        //{
        //    var newBook = new Laptop { LaptopId = 3, Name = "Test Book", Brand = "Tester" };
        //    var response = await _client.PostAsJsonAsync("/api/laptops", newBook);

        //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //}

        //[Fact]
        //public async Task Delete_RemovesBook()
        //{
        //    var response = await _client.DeleteAsync("/api/laptops/2");
        //    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        //}
    }
}