using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Students.Commands.Models;
using CleanProject.Data.Entities;
using CleanProject.Srevice.IServices;
using MediatR;

namespace CleanProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : IRequestHandler<AddStudentCommand, Result<Student>>,
                                         IRequestHandler<EditStudentCommand, Result<bool>>,
                                         IRequestHandler<DeleteStudentCommand, Result<bool>>
    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;
        #endregion

        #region Constructor
        public StudentCommandHandler(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }
        #endregion

        #region Handle Functions
        public async Task<Result<Student>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapper = _mapper.Map<Student>(request);
            var result = await _studentService.AddStudentAsync(studentMapper);
            if (result != null) return await Result<Student>.SuccessAsync(result, "Student Created Success");
            else return await Result<Student>.FailAsync("SomeThing Wrong");
        }

        public async Task<Result<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentById(request.StudID);
            if (student == null) return await Result<bool>.FailAsync("No Data Found");

            var result = await _studentService.DeleteStudentAsync(student);
            if (result == true) return await Result<bool>.SuccessAsync(true, "Student Deleted Success");
            else return await Result<bool>.FailAsync("SomeThing Wrong");
        }

        async Task<Result<bool>> IRequestHandler<EditStudentCommand, Result<bool>>.Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentById(request.StudID);
            if (student == null) return await Result<bool>.FailAsync("No Data Found");
            var studentMapper = _mapper.Map(request, student);
            var result = await _studentService.EditStudentAsync(studentMapper);
            if (result == true) return await Result<bool>.SuccessAsync(true, "Student Created Success");
            else return await Result<bool>.FailAsync("SomeThing Wrong");
        }
        #endregion
    }
}
