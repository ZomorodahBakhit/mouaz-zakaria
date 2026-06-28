using System.Collections.Generic;
using System.Linq;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
 
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public StudentDto? GetById(int id)
        {
            var student = _repository.GetById(id);
            if (student == null) return null;
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
            };
        }

        public List<StudentDto> GetAll()
        {
            return _repository.GetAll()
                .Select(s => new StudentDto { Id = s.Id, Name = s.Name, Email = s.Email })
                .ToList();
        }

        public void Create(CreateStudentForm form)
        {
            var student = new Student
            {
                Name = form.Name,
                Email = form.Email
            };
            _repository.Add(student);
        }

        public void Update(int id, UpdateStudentForm form)
        {
            var student = _repository.GetById(id);
            if (student == null) return;
            student.Name = form.Name;
            student.Email = form.Email;
            _repository.Update(student);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
