using firstDoorBackEnd.Database;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace firstDoorBackEnd.Tests
{
    public class FirstDoorRepositoryTests
    {
        private FirstDoorRepository _repository;
        private FirstDoorContext _context;
        private SqliteConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<FirstDoorContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new FirstDoorContext(options);

            _context.Database.EnsureCreated();

            _repository = new FirstDoorRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            _connection.Dispose();
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnEmptyListOfJobs_WhenThereAreNoSavedJobsInTheDatabase()
        {
            if (_context.SavedJobs.Any())
            {
                _context.SavedJobs.RemoveRange();
                _context.SaveChanges();
            }

            var expectedJobs = new List<SavedJob>();

            var result = await _repository.GetAllSavedJobsAsync();

            result.Should().BeEquivalentTo(expectedJobs);
        }

        [Test]
        public async Task GetAllSavedJobsAsync_ShouldReturnListOfJobs_WhenThereAreSavedJobsInTheDatabase()
        {
            var expectedJobs = new List<SavedJob>
            {
                new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new SavedJob { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)}
            };

            _context.SavedJobs.AddRange(expectedJobs);
            _context.SaveChanges();

            var result = await _repository.GetAllSavedJobsAsync();

            result.Should().BeEquivalentTo(expectedJobs);
        }

    }
}
