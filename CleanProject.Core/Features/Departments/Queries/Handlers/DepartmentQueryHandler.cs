using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Departments.Queries.Models;
using CleanProject.Core.Features.Departments.Queries.Results;
using CleanProject.Core.Resources;
using CleanProject.Core.Wrappers;
using CleanProject.Data.Entities;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;
using static CleanProject.Core.Features.Departments.Queries.Results.GetDepartmentByIdResponse;

namespace CleanProject.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>,
                                                           IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
                                                           IRequestHandler<GetDepartmentPaginatedListQuery, PaginatedResult<GetDepartmentPaginatedListResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, IDepartmentService departmentService, IStudentService studentService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            _studentService = studentService;
        }

        #endregion

        #region Constructors

        #endregion


        #region Handle Functions

        public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
        {
            var departmentList = await _departmentService.GetDepartmentListAsync();

            var departmentListMapper = _mapper.Map<List<GetDepartmentListResponse>>(departmentList);

            return Success(departmentListMapper, _stringLocalizer[SharedResourcesKeys.Found]);
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (department == null) return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var departmentMapper = _mapper.Map<GetDepartmentByIdResponse>(department);

            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.NameAr, e.NameEn);
            var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);

            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            departmentMapper.StudentList = PaginatedList;

            return Success(departmentMapper);
        }

        public async Task<PaginatedResult<GetDepartmentPaginatedListResponse>> Handle(GetDepartmentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            /*Expression<Func<Department, GetDepartmentPaginatedListResponse>> expression = e => new GetDepartmentPaginatedListResponse(e.DID, e.DNameEn, e.DNameAr);
            var FilterQuery = _departmentService.FilterDepartmentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);*/

            var FilterQuery = _departmentService.FilterDepartmentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await _mapper.ProjectTo<GetDepartmentPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        /*
                public async Task<Response<GetByIdDepartmentResponse>> Handle(GetByIdDepartmentQuery request, CancellationToken cancellationToken)
                {
                    //service Get By Id include St sub ins
                    var response = await _departmentService.GetDepartmentByIdAsync(request.Id);
                    //check Is Not exist
                    if (response == null) return NotFound<GetByIdDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                    //mapping 
                    var mapper = _mapper.Map<GetByIdDepartmentResponse>(response);

                    //pagination
                    Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.NameAr, e.NameEn);
                    var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
                    var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
                    mapper.StudentList = PaginatedList;

                    // Log.Information($"Get Department By Id {request.Id}!");
                    //return response
                    return Success(mapper);
                }*/


        #endregion
    }
}
