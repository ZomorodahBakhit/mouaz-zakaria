using System.Collections.Generic;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface IStudentRepository
    {
        Student? GetById(int id);
        List<Student> GetAll();
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}
