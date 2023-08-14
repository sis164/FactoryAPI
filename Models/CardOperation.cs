using Npgsql.Internal.TypeHandlers.DateTimeHandlers;

namespace FactoryAPI.Models
{
    public class CardOperation
    {
        public int Id { get; set; }
        public int Card_id { get; set; }
        public int Service_id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public double Result_cost { get; set; }

        public CardOperation(int id, int card_id, int service_id, DateTime date, DateTime time, double result_cost)
        {
            Id = id;
            Card_id = card_id;
            Service_id = service_id;
            Date = date;
            Time = time;
            Result_cost = result_cost;
        }

        public CardOperation(int card_id, int service_id, string date, string time, double result_cost)
        {
            Card_id = card_id;
            Service_id = service_id;
            Date = DateTime.Parse(date);
            Time = DateTime.Parse(time);
            Result_cost = result_cost;
        }
    }
}
