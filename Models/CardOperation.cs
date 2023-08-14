namespace FactoryAPI.Models
{
    public class CardOperation
    {
        public int Id { get; set; }
        public int Card_id { get; set; }
        public int Object_id { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public decimal Result_cost { get; set; }

        public CardOperation(int card_id, int object_id, DateOnly date, TimeOnly time, decimal result_cost)
        {
            Card_id = card_id;
            Object_id = object_id;
            Date = date;
            Time = time;
            Result_cost = result_cost;
        }
    }
}
