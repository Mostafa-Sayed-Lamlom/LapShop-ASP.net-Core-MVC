namespace LapShop.Api.Base
{
    public class ApiBase
    {
        /// <summary>
        /// data field to carry all data form api
        /// </summary>
        public object Data { get; set; } = new object();
        /// <summary>
        /// errors field to carry all data form api
        /// </summary>
        public object Errors { get; set; } = new object();
        /// <summary>
        /// status code field to know if the request is success or not 
        /// </summary>
        public string StatusCode { get; set; } = string.Empty;
    }
}
