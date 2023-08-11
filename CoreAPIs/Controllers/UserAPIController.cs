using CoreAPIs.Entities;
using CoreAPIs.Models.Requests;
using CoreAPIs.Models.Response;
using CoreAPIs.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserAPIController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost("Add")]
        public async Task<GeneralModel> Add([FromBody] UserRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = await _userRepository.AddAsync(request);
                }
                else
                {
                    response.Message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
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
        public async Task<BaseApiResponse> Update(UserRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userRepository.UpdateAsync(request);
                response.Success = result.Status;
                response.Message = result.Message;
                response.Id = request.UserId;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("Filter")]
        public async Task<BaseApiResponse> Filter([FromBody] UserActivityFilter filter)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var activity = await _userRepository.GetActivity(filter, false);
                var count = await _userRepository.GetActivity(filter, true);

                response.Data = new { activity = activity, count = count };
                response.Success = true;
                response.Message = "Data Retrived Successfully";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost("InActive")]
        public async Task<BaseApiResponse> DeleteActivity(UserStatusUpdate request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userRepository.DeleteUser(request);
                response.Success = true;
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
        public async Task<IActionResult> Restore(UserStatusUpdate model)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = _userRepository.Restore(model).Result;
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

        [HttpPost("Update-Filter")]
        public async Task<GeneralModel> UpdateFilter([FromBody] SaveUserFilter model)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                if (ModelState.IsValid)
                {
                    await _userRepository.UpdateFilter(model);
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

        [HttpGet("List")]
        [ProducesResponseType(typeof(List<UserList>), (int)HttpStatusCode.OK)]
        public async Task<BaseApiResponse> List()
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userRepository.List();
                if (result != null)
                {
                    response.userList = result.ToList();
                    response.Success = true;
                    response.Message = "List Fetched Successfully";
                }
            }
            catch (Exception x)
            {
                response.Success = false;
                response.Message = x.Message;
            }
            return response;
        }

        [HttpPost("Details")]
        public async Task<BaseApiResponse> Details(string UserId)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userRepository.Details(UserId);
                response.Data = result;
                response.Id = UserId;
                response.Success = true;
                response.Message = "Details Fetched Successfully";
            }
            catch (Exception x)
            {
                response.Success = false;
                response.Message = x.Message;   
            }
            return response;
        }

        [HttpPost("SearchList")]
        public async Task<BaseApiResponse> SearchList(UserSearchData search)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userRepository.SearchList(search);
                response.Data = result.ToList();
                response.Success = true;
                response.Message = "Details Fetched Successfully";
            }
            catch(Exception x)
            {
                response.Success = false;
                response.Message = x.Message;
            }
            return response;
        }
    }
}
