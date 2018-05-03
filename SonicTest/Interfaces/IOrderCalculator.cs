namespace Exam
{
    public interface IOrderCalculator
    {
        decimal CalculateTotal(Order order, decimal taxRate);
    }
}