namespace MarketHub.BLL.GeneralResponse
{
    public class GenResponse
    {
        //public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public dynamic? DataObject { get; set; }
        //public GenResponse CreateResponse(bool IsSuccess, string Message, dynamic? DataObject)
        //{
        //    GenResponse response = new GenResponse();
        //    response.IsSuccess = IsSuccess;
        //    response.Message = Message;
        //    response.DataObject = DataObject;
        //    return response;
        //}
    }
}
