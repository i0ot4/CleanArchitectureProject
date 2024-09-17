namespace CleanProject.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentPaginatedListResponse
    {
        //This if we use The Expression in Handler
        /*   public GetDepartmentPaginatedListResponse(int id, string NameAr, string NameEn)
           {
               DID = id;
               DNameAr = NameAr;
               DNameEn = NameEn;
           }*/

        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
    }
}
