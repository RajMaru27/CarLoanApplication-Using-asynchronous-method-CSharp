using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailsController : ControllerBase
    {
        private readonly IProjectDetailsRepository _projectDetailsRepository;
        public ProjectDetailsController(IProjectDetailsRepository projectDetailsRepository)
        {
            _projectDetailsRepository = projectDetailsRepository ?? throw new ArgumentNullException(nameof(projectDetailsRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add([FromBody] ProjectDetailRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _projectDetailsRepository.AddAsync(request);
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
        public async Task<BaseApiResponse> Update(ProjectDetailRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _projectDetailsRepository.UpdateAsync(request);
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

        [HttpPost("InActive")]
        public async Task<BaseApiResponse> DeleteActivity(ProDetailStatusUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _projectDetailsRepository.DeleteAsync(request);
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

        [HttpPut("Restore")]
        public async Task<IActionResult> RestoreActivity(ProDetailStatusUpdate request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = _projectDetailsRepository.Restore(request).Result;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<ProDetailStatus>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> List()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var getlist = await _projectDetailsRepository.List();
                if (getlist != null)
                {
                    response.ProDetails = getlist.ToList();
                    response.Success = true;
                    response.Message = "";
                }
                else
                {
                    response.Success = false;
                    response.Message = "";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
