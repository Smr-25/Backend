namespace EternaApp.Services;

public class BankService
{
    public int Balance { get; set; }

    public void Add()
    {
        Balance += 100;
    }
}