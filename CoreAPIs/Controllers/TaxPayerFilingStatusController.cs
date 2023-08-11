using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPayerFilingStatusController : ControllerBase
    {
        private readonly ITaxPayerFilingStatusRepository _repository;
        public TaxPayerFilingStatusController(ITaxPayerFilingStatusRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add(TPFillingStatusRequest request)
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
        [ProducesResponseType(typeof(List<TPFilingStatusList>),(int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> List()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var list = await _repository.GetListAsync();
                if (list != null)
                {
                    response.tPFilingStatusLists = list.ToList();
                    response.Success = true;
                    response.Message = "List Fetched";
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
