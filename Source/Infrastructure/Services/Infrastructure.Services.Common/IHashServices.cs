namespace Infrastructure.Services.Common
{
    public interface IHashServices
    {
        string Base36Encode(long input);
        long Base36Decode(string input);
    }
}
