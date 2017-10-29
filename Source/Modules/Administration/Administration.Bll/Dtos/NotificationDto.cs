namespace Administration.Bll.Dtos
{
    public class NotificationDto
    {
        public string Text { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }

        public bool HasUrl => Controller != null || Action != null;
    }
}
