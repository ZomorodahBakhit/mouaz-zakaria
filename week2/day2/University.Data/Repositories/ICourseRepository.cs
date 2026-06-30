using System.Collections.Generic;
using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface ICourseRepository
    {
        Course? GetById(int id);
        List<Course> GetAll();
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}
