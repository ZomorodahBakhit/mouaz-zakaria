using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
 
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public StudentDto? GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) throw new NotFoundException($"Student with id {id} was not found");
            return MapToDto(student);
        }

        public List<StudentDto> GetAll()
        {
            return _repository.GetAll()
                .Select(MapToDto)
                .ToList();
        }

        public void Create(CreateStudentForm form)
        {
            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.Errors);
            }
            EnsureEmailIsUnique(form.Email);

            var student = new Student
            {
                Name = form.Name,
                Email = form.Email
            };
            _repository.Add(student);   
        }

        public void Update(int id, UpdateStudentForm form)
        {
            var validationResult = FormValidator.Validate(form);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.Errors);
            }

            var student = _repository.GetById(id);
            if (student == null) throw new NotFoundException($"Student with id {id} was not found");

            EnsureEmailIsUnique(form.Email, id);

            student.Name = form.Name;
            student.Email = form.Email;
            _repository.Update(student);
        }

        public void Delete(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) throw new NotFoundException($"Student with id {id} was not found");

            _repository.Delete(id);
        }

        private void EnsureEmailIsUnique(string email, int? currentStudentId = null)
        {
            var student = _repository.GetByEmail(email);
            if (student != null && student.Id != currentStudentId)
            {
                _logger.LogWarning("Duplicate student email submitted: {Email}", email);
                throw new BusinessException("Email address already exists");
            }
        }

        private static StudentDto MapToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
            };
        }
    }
}
