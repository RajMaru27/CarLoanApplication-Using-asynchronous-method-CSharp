using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailAPIController : ControllerBase
    {
        private readonly IEmployeeDetails _employeeRepository;
        public EmployeeDetailAPIController(IEmployeeDetails employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpPost("Create")]
        public async Task<GeneralModel> Create(EmployeelDetailRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _employeeRepository.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
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
        public async Task<BaseApiResponse> Update(UpdateEmployeeDetails request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _employeeRepository.UpdateAsync(request);
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

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDetailList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> List()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var list = await _employeeRepository.GetAll();
                if (list != null)
                {
                    response.employeeDetailLists = list.ToList();
                    response.Success = true;
                    response.Message = "List fetched";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error while fetching list";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("Deactive")]
        public async Task<BaseApiResponse> Delete(EmployeeStatus empstatus)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _employeeRepository.DeleteAsync(empstatus);
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

        [HttpPut("Active")]
        public async Task<IActionResult> Restore(EmployeeStatus empstatus)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _employeeRepository.Restore(empstatus);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return Ok (response);
        }

        [HttpPost("ActivityList")]
        public async Task<BaseApiResponse> ActivityList([FromBody] EmployeeActivityFilter filter)
        {
            BaseApiResponse response = new BaseApiResponse();   
            try
            {
                var filterlist = await _employeeRepository.ActivityList(filter, false);
                var count = await _employeeRepository.ActivityList(filter, true);

                response.Data = new {filterlist = filterlist, count = count};
                response.Success = true;
                response.Message = "List Fetched";
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
