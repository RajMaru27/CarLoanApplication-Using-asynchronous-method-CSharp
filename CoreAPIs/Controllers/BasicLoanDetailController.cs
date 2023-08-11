using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicLoanDetailController : ControllerBase
    {
        private readonly IBasicLoanDetailsRep _basicLoanDetailsRep;
        public BasicLoanDetailController(IBasicLoanDetailsRep basicLoanDetailsRep)
        {
            _basicLoanDetailsRep = basicLoanDetailsRep ?? throw new ArgumentNullException(nameof(basicLoanDetailsRep));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add(LoanDetailRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _basicLoanDetailsRep.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(v => v.ErrorMessage));
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
        public async Task<BaseApiResponse> Update(UpdateLoanDetails request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _basicLoanDetailsRep.UpdateAsync(request);
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

        [HttpPost("InActive")]
        public async Task<BaseApiResponse> DeleteActivity(LoanStatusUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _basicLoanDetailsRep.DeleteAsync(request);
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
        public async Task<IActionResult> RestoreActivity(LoanStatusUpdate request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = _basicLoanDetailsRep.Restore(request).Result;

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
        [ProducesResponseType(typeof(IEnumerable<LoanDetailsList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> List()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var list = await _basicLoanDetailsRep.GetAll();
                if (list != null)
                {
                    response.LoanDetailsLists = list.ToList();
                    response.Success = true;
                    response.Message = "List Fetched";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error Occured While Fecthing List";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("Detail")]
        public async Task<BaseApiResponse> Details(string id)
        {
            BaseApiResponse response = new BaseApiResponse();

            try
            {
                var detail = await _basicLoanDetailsRep.DetailAsync(id);
                if (detail != null)
                {
                    response.Data = detail;
                    response.Id = id;
                    response.Success = true;
                    response.Message = "Details fetched";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error Occured While Fecthing Details";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("ActivityList")]
        public async Task<BaseApiResponse> ActiveList([FromBody] LoanActivityFilter filter)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var Activity = await _basicLoanDetailsRep.GetActivity(filter, false);
                var Count = await _basicLoanDetailsRep.GetActivity(filter, true);

                response.Data = new {Activity = Activity,Count = Count };
                response.Success = true;
                response.Message = "ActivityList Fetched";
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
