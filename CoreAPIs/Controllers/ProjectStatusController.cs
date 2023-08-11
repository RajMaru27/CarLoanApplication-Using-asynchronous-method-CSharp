using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks.Dataflow;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectStatusController : ControllerBase
    {
        private readonly IProjectStatusRepository _repository;
        public ProjectStatusController(IProjectStatusRepository statusRepository)
        {
            _repository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add([FromBody] StatusRequest request)
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
                    response.Message = string.Join(" | ",ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<StatusRequest>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> GetList()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var getlist = await _repository.GetListAsync();
                if (getlist != null)
                {
                    response.projectStatuses = getlist.ToList();
                    response.Success = true;
                    response.Message = "Data Fetched Successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Data Fetching Failed";
                }
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
