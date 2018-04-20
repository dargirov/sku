namespace Request.Entities
{
    public enum RequestStatusEnum
    {
        New = 1,
        Sent,
        PartiallyCompleted,
        Completed,
        Cancelled
    }
}
