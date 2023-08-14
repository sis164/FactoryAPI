namespace FactoryAPI.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int Client_id { get; set; }
        public string Code { get; set; }

        public Card(int client_id, string code)
        {
            Client_id = client_id;
            Code = code;
        }
    }
}
