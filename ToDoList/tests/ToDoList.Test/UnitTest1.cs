namespace ToDoList.Test;

public class UnitTest1
{
    [Fact]
    public void Divide_WithoutRemainder_ReturnCorrectNumbe_Parametrized(int value1, int value2)
    {
        var calc = new Calculator();

        var result = calc.Divide(10,5);

        var expectedResult = value1 / value2;

        Assert.Equal(2,result);
    }

    [Fact]
    public void Divide_ByZero_ThrowDivideByZeroExceptions()
    {
        var calc = new Calculator();

        Assert.Throws<DivideByZeroException>(() => calc.Divide(10,0));
    }
}


public class Calculator
{
    public int Divide(int divident, int divisor)
    {
        return divident / divisor;
    }
}

