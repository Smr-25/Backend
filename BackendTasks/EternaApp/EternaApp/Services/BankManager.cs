namespace EternaApp.Services;

public class BankManager(BankService bankService)
{
    public int BankManagerBalance => bankService.Balance;  
    public void AddBalance()
    {
        bankService.Add();
    }
}