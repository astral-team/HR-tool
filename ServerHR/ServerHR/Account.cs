using System;

public class Account
{
    public Account(string login, string password)
    {
        this.Hash = Guid.NewGuid().ToString(); // генерируем номер счета
        this.Login = login;
        this.Password = password;
    }
    public string Hash { get; private set; } // id - номер счета
    public string Login { get; private set; } // имя владельца
    public string Password { get; private set; } // сумма
}
