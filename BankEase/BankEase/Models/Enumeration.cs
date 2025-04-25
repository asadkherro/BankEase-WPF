namespace BankEase.Models
{
    [Flags]
    public enum AccountType
    {
        Saving = 1,
        Current = 2,
        Salary = 4
    }
    [Flags]
    public enum AccountServices
    {
        CreditCard = 1,
        DebitCard = 2,
        ChequeBook = 4,
        Insurance = 8,
        CarLoan = 16,
        HouseLoan = 32,
    }
    [Flags]
    public enum AccountStatus
    {
        Open = 1,
        Closed = 2,
        Pending = 4,
    }
    [Flags]
    public enum TransactionType
    {
        Deposit = 1,
        Withdrawl = 2,
        FundsSent = 4,
        FundsReceive = 8,
    }
    [Flags]
    public enum UserRole 
    {
        User = 1 , 
        Admin = 2 ,
    }
    [Flags]
    public enum RequestType
    {
        AccountClose = 1,
        AccountUpgrade = 2,
    }
    [Flags]
    public enum RequestStatus
    {
        Approved = 1,
        Pending = 2,
        Rejected = 4
    }
}
