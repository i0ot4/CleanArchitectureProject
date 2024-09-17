using CleanProject.Core.Features.Departments.Commands.Models;
using CleanProject.Srevice.IServices;
using FluentValidation;

namespace CleanProject.Core.Features.Departments.Commands.Validations
{
    public class EditDepartmentValidator : AbstractValidator<EditDepartmentCommand>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public EditDepartmentValidator(IDepartmentService departmentService)
        {
            _departmentService = departmentService;

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Action
        public void ApplyValidationRules()
        {
            RuleFor(x => x.DNameAr)
                .NotEmpty().WithMessage("Name EN Must not be empty")
                .NotNull().WithMessage("Name En Is Required");
            RuleFor(x => x.DNameEn)
                .NotEmpty().WithMessage("Name AR Must not be empty")
                .NotNull().WithMessage("Name AR Is Required");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(x => x.DNameAr)
                .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsDepartmentArExist(Key))
                .WithMessage("Name Ar Is Exist");
            RuleFor(x => x.DNameEn)
                .MustAsync(async (Key, CancellationToken) => !await _departmentService.IsDepartmentEnExist(Key))
                .WithMessage("Name En Is Exist");
        }
        #endregion
    }
}
