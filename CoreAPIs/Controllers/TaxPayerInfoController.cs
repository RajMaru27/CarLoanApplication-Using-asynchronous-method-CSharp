using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPayerInfoController : ControllerBase
    {
        private readonly ITaxPayerInfo _TaxPayerRepository;
        public TaxPayerInfoController(ITaxPayerInfo taxPayerRepository)
        {
            _TaxPayerRepository = taxPayerRepository ?? throw new ArgumentNullException(nameof(taxPayerRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add(TaxPayerInfoRequest request)
        {
            GeneralModel model = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    model = await _TaxPayerRepository.AddAsync(request);
                }
                else
                {
                    model.Message = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                model.Status = false;
                model.Message = ex.Message;
            }
            return model;
        }

        [HttpPut("Update")]
        public async Task<BaseApiResponse> UpdateAsync(TaxPayerUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _TaxPayerRepository.UpdateAsync(request);
                if (result != null)
                {
                    response.Data = result;
                    response.Id = result.Id;
                    response.Success = true;
                    response.Message = result.Message;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error Occured";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<TaxPayerInfoList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> GetList()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var list = await _TaxPayerRepository.GetList();
                if (list != null)
                {
                    response.taxPayerInfoList = list.ToList();
                    response.Success = true;
                    response.Message = "List Fetched";
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
        public async Task<BaseApiResponse> Delete(TaxPayerActivityStatus activityStatus)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _TaxPayerRepository.Delete(activityStatus);
                response.Id = result.Id;
                response.Success = true;
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
        public async Task<IActionResult> Restore(TaxPayerActivityStatus activityStatus)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _TaxPayerRepository.Restore(activityStatus);
                response.Id = result.Id;
                response.Success = true;
                response.Message = result.Message;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("ActivityFilter")]
        public async Task<BaseApiResponse> ActivityList([FromBody] TaxPayerActivityFilter filter)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var Actfilter = await _TaxPayerRepository.ActivityList(filter, false);
                var count = await _TaxPayerRepository.ActivityList(filter, true);

                response.Data = new { Actfilter = Actfilter, count = count };
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
