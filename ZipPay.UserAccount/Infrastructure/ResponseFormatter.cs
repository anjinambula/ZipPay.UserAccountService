using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Infrastructure
{
    public static class ResponseFormatter
    {
        public static ResponseModel CreateResponse(bool success, string message)
        {
            return new ResponseModel { Created = success, Message = message };
        }
    }
}
