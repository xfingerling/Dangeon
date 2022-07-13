using System;

public static class Bank
{
    public static event Action OnBankInitializedEvent;

    private static BankInteractor _bankInteractor;

    public static int Coins { get { CheckClass(); return _bankInteractor.Coins; } }
    public static bool isInitialized { get; private set; }

    public static void Initialize(BankInteractor interactor)
    {
        _bankInteractor = interactor;
        isInitialized = true;

        OnBankInitializedEvent?.Invoke();
    }

    public static bool IsEnoughtCoins(int value)
    {
        CheckClass();
        return _bankInteractor.IsEnoughtCoins(value);
    }

    public static void AddCoins(object sender, int value)
    {
        CheckClass();
        _bankInteractor.AddCoins(sender, value);
    }

    public static void Spend(object sender, int value)
    {
        CheckClass();
        _bankInteractor.Spend(sender, value);
    }

    private static void CheckClass()
    {
        if (!isInitialized)
        {
            throw new Exception("Bank is not initialize yet");
        }
    }
}
