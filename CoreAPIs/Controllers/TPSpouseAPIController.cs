using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TPSpouseAPIController : ControllerBase
    {
        private readonly ITPSpouseInfo _repository;
        public TPSpouseAPIController(ITPSpouseInfo repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add(TPSpouseInfoRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _repository.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | " ,ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
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
        public async Task<BaseApiResponse> Update(TPSpouseInfoUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.UpdateAsync(request);
                response.Id = result.Id;
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
