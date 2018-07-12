using System;
using ServerHR;

public class AuthorizedUser
{
    public AuthorizedUser(UserDB user)
    {
        this.Hash = Guid.NewGuid().ToString(); // генерируем номер счета
        user.Hash = this.Hash;
        this.Login = user.Login;
        this.Password = user.Password;
    }
    public string Hash { get; private set; } // id - номер счета
    public string Login { get; private set; } // имя владельца
    public string Password { get; private set; } // сумма
}
