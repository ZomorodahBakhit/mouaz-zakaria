using System.Collections.Generic;
using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
{
    public interface ICourseService
    {
        CourseDto? GetById(int id);
        List<CourseDto> GetAll();
        void Create(CreateCourseForm form);
        void Update(int id, UpdateCourseForm form);
        void Delete(int id);
    }
}
