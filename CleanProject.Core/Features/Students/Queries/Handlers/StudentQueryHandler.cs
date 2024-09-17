using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Students.Queries.Models;
using CleanProject.Core.Features.Students.Queries.Results;
using CleanProject.Core.Resources;
using CleanProject.Core.Wrappers;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : Result,
                                               IRequestHandler<GetStudentListQuery, Result<List<GetStudentListResponse>>>,
                                               IRequestHandler<GetStudentByIdQuery, Result<GetStudentByIdResponse>>,
                                               IRequestHandler<GetStudentPaginatedQuery, PaginatedResult<GetStudentPaginatedResponse>>
    {
        #region Feilds
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public StudentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion


        #region Costructors

        #endregion


        #region Handler
        public async Task<Result<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);

            return await Result<List<GetStudentListResponse>>.SuccessAsync(studentListMapper, "null Message", _stringLocalizer[SharedResourcesKeys.Found]);
        }


        public async Task<Result<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentById(request.Id);
            if (student == null) return await Result<GetStudentByIdResponse>.NotFound(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var studentMapper = _mapper.Map<GetStudentByIdResponse>(student);

            return await Result<GetStudentByIdResponse>.SuccessAsync(studentMapper, "null Message", _stringLocalizer[SharedResourcesKeys.Found]);
        }

        public async Task<PaginatedResult<GetStudentPaginatedResponse>> Handle(GetStudentPaginatedQuery request, CancellationToken cancellationToken)
        {
            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
            var paginatedList = await _mapper.ProjectTo<GetStudentPaginatedResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }

        #endregion
    }
}
