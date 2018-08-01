using System;

namespace Benday.WebCalculator.Api
{
    public class CalculatorService : ICalculatorService
    {
        public double Add(double value1, double value2)
        {
            return value1 + value2;
        }

        public double Subtract(double value1, double value2)
        {
            return value1 - value2;
        }

        public double Multiply(double value1, double value2)
        {
            return value1 * value2;
        }

        public double Divide(double value1, double value2)
        {
            return value1 / value2;
        }
    }
}
