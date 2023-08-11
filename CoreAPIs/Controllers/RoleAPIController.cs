using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAPIController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleAPIController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add([FromBody] RoleRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _roleRepository.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
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
        public async Task<BaseApiResponse> Update(RoleRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _roleRepository.UpdateAsync(request);
                response.Success = result.Status;
                response.Message = result.Message;
                response.Id = result.Id;
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
