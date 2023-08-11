using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchAPIController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        public BranchAPIController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> AddAsync(BranchRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _branchRepository.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPut("Update")]
        public async Task<BaseApiResponse> Update(BranchRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _branchRepository.UpdateAsync(request);
                response.Id = result.Id;
                response.Success = result.Status;
                response.Message = result.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
