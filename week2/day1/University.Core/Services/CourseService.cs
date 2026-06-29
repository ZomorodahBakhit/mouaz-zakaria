using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;
using University.Core.Validations;
using University.Core.Exceptions;

namespace University.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository repository, ILogger<CourseService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public CourseDto? GetById(int id)
        {
            var course = _repository.GetById(id);
            if (course == null) throw new NotFoundException($"Course with id {id} was not found");
            return MapToDto(course);
        }

        public List<CourseDto> GetAll()
        {
            return _repository.GetAll()
                .Select(MapToDto)
                .ToList();
        }

        public void Create(CreateCourseForm form)
        {
            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.Errors);
            }

            var course = new Course
            {
                Name = form.Name,
                Weight = form.Weight
            };
            _repository.Add(course);
        }

        public void Update(int id, UpdateCourseForm form)
        {
            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.Errors);
            }

            var course = _repository.GetById(id);
            if (course == null) throw new NotFoundException($"Course with id {id} was not found");

            course.Name = form.Name;
            course.Weight = form.Weight;
            _repository.Update(course);
        }

        public void Delete(int id)
        {
            var course = _repository.GetById(id);
            if (course == null) throw new NotFoundException($"Course with id {id} was not found");
            _repository.Delete(id);
        }

        private static CourseDto MapToDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Weight = course.Weight
            };
        }
    }
}
