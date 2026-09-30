namespace MarketHub.BLL.GeneralResponse
{
    public class GenResponse
    {
        //public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public dynamic? DataObject { get; set; }

    }
}
