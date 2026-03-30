namespace illiminatado_exam.Services
{
    public class Helpers
    {
        public decimal getHrsAmount(int value1, decimal value2, decimal percentage = 100m)
        {
            decimal result =
                (((value1 * 12m) / 261m) / 8m)
                * value2
                * (percentage / 100m);
            result = Math.Round(result, 2);
            return result;
        }

        public decimal getHrsAmount1(int value1, decimal value2, decimal percentage = 100m)
        {
            decimal result =
            (((value1 * 12m) / 261m) / 8m)
            * value2
            * percentage;

                return Math.Round(result, 2);
        }


        public decimal getAbsentAmount(int value1, decimal value2, decimal percentage = 100m)
        {
            decimal result =
                (((value1 * 12m) / 261m))
                * value2
                * (percentage / 100m);
            result = Math.Round(result, 2);
            return result;
        }
    }
}
