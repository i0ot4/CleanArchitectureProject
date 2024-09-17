using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Departments.Commands.Models;
using CleanProject.Data.Entities;
using CleanProject.Srevice.IServices;
using MediatR;

namespace CleanProject.Core.Features.Departments.Commands.Handlers
{
    public class DepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, IResult<string>>,
                                            IRequestHandler<EditDepartmentCommand, IResult<string>>,
                                            IRequestHandler<DeleteDepartmentCommand, IResult<bool>>
    {
        #region Feilds
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        #endregion


        #region Constructors
        public DepartmentCommandHandler(IMapper mapper, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        #endregion



        #region Handle Functions
        public async Task<IResult<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentMapper = _mapper.Map<Department>(request);

            var result = await _departmentService.AddAsync(departmentMapper);

            if (result == "Success") return await Result<string>.SuccessAsync(result, "Created Async");
            else return await Result<string>.FailAsync("SomeThing Wrong");
        }

        public async Task<IResult<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null) return await Result<string>.FailAsync("No Data Fuond");
            var departmentMapper = _mapper.Map(request, department);

            var result = await _departmentService.EditAsync(departmentMapper);

            if (result == "Success") return await Result<string>.SuccessAsync(result, "Data Updated SuccessFully");
            else return await Result<string>.FailAsync("SomeThing Wrong");
        }

        public async Task<IResult<bool>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeletedAsync(request.Id);

            if (result == true) return await Result<bool>.SuccessAsync(true, "Data Updated SuccessFully");
            else if (result == false) return await Result<bool>.FailAsync("No Data Fuond");
            else return await Result<bool>.FailAsync("SomeThing Wrong");
        }
        #endregion
    }
}
