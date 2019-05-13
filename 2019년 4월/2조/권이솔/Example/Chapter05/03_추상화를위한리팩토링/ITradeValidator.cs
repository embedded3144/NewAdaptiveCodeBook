namespace Chapter05._3_추상화를위한리팩토링
{
    public interface ITradeValidator
    {
        bool Validate(string[] fields);
    }
}