namespace FactoryAPI.Models.RequestBodies
{
    public class RequestCardOperation
    {
        public int CardId { get; set; }
        public int ServiceId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public double ResultCost { get; set; }
        public RequestCardOperation(int cardId, int serviceId, string date, string time, double resultCost)
        {
            CardId = cardId;
            ServiceId = serviceId;
            Date = date;
            Time = time;
            ResultCost = resultCost;
        }
    }
}
