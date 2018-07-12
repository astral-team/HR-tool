using System;
using ServerHR;

public class AuthorizedUser : UserDB
{
    public AuthorizedUser(UserDB user)
    {
        this.Hash = Guid.NewGuid().ToString(); // генерируем номер счета
        user.Hash = this.Hash;
        this.Login = user.Login;
        this.Password = user.Password;
        this.Root = user.Root;
    }

    public AuthorizedUser(string login, string password)
    {
        this.Hash = Guid.NewGuid().ToString(); // генерируем номер счета
        this.Login = login;
        this.Password = password;
    }
}
