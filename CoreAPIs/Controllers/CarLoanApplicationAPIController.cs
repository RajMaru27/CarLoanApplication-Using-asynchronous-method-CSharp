using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarLoanApplicationAPIController : ControllerBase
    {
        private readonly ICarLoanApplicationRep _repository;
        public CarLoanApplicationAPIController(ICarLoanApplicationRep repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add(CarLoanAppRequest request)
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
        public async Task<BaseApiResponse> Update(CarLoanAppUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.UpdateAsync(request);
                response.Id = result.Id;
                response.Data = result;
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

        [HttpGet("ApplicantList")]
        [ProducesResponseType(typeof(IEnumerable<LoanApplicantList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> GetAll()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.GetAll();
                if (result != null)
                {
                    response.loanApplicantLists = result.ToList();
                    response.Success = true;
                    response.Message = "Applicant list fetched successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error occured while fetching list";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("InActive")]
        public async Task<BaseApiResponse> Delete(LoanApplicantStatus request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.Delete(request);
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
        public async Task<IActionResult> Restore(LoanApplicantStatus request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _repository.Restore(request);
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
            return Ok(response);
        }

        [HttpPost("ActivityList")]
        public async Task<BaseApiResponse> Activity([FromBody] CarLoanSearchList filter)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var filterData = await _repository.FilterActivity(filter);

                response.Data = filterData.ToList();
                response.Success = true;
                response.Message = "Details fetched successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        [HttpPost("Details")]
        public async Task<BaseApiResponse> Details(string LoanId)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.Details(LoanId);
                response.Data = new { result = result };
                response.Id = LoanId;
                response.Success = true;
                response.Message = "Details fetched";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("InActiveListOnly")]
        [ProducesResponseType(typeof(List<LoanApplicantList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> InActiveList()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _repository.InActiveListOnly();
                if (result != null)
                {
                    response.loanApplicantLists = result.ToList();
                    response.Success = true;
                    response.Message = "List Fetched Successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error occured while fetching list";
                }
            }
            catch (Exception x)
            {
                response.Success = false;
                response.Message = x.Message;
            }
            return response;
        }
    }
}
