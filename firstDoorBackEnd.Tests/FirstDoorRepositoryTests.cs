using firstDoorBackEnd.Database;
using firstDoorBackEnd.Models;
using firstDoorBackEnd.Repositories;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;

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
<<<<<<< HEAD
        public async Task GetJobByIDAsync_ShouldReturnNull_WhenThereAreNoJobs()
=======
        public async Task GetAllSavedJobsAsync_ShouldReturnEmptyListOfJobs_WhenThereAreNoSavedJobsInTheDatabase()
>>>>>>> main
        {
            if (_context.SavedJobs.Any())
            {
                _context.SavedJobs.RemoveRange();
                _context.SaveChanges();
            }

<<<<<<< HEAD
            var result = await _repository.GetJobByIDAsync(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetJobByIDAsync_ShouldReturnNull_WhenIDDoesNotExist()
        {
            if (_context.SavedJobs.Any())
            {
                _context.SavedJobs.RemoveRange();
                _context.SaveChanges();
            }

            var jobs = new List<SavedJob>()
                {
                new() { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new() { Id = 2, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)},
                new() { Id = 3, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3)}
            };

            _context.SavedJobs.AddRange(jobs);
            _context.SaveChanges();

            var result = await _repository.GetJobByIDAsync(4);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetJobByIDAsync_ShouldReturnJob_WhenIDExists()

        {
            if (_context.SavedJobs.Any())
            {
                _context.SavedJobs.RemoveRange();
                _context.SaveChanges();
            }

            var theJob = new SavedJob { Id = 1, Title = "test", Description = "test", EmployerName = "test", Location = "test", Url = "test", TimeSaved = new DateTime(2025, 4, 3) };

            var jobs = new List<SavedJob>()
                {
                    theJob
                };

            _context.SavedJobs.Add(theJob);
            _context.SaveChanges();

            var result = await _repository.GetJobByIDAsync(1);

            Assert.That(result, Is.EqualTo(theJob));
=======
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
>>>>>>> main
        }

    }
}
