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

    public AuthorizedUser(string login, string password, string hash)
    {
        if (hash == "")
        {
            this.Hash = Guid.NewGuid().ToString(); // генерируем номер счета
        }
        else
        {
            this.Hash = hash;
        }
        this.Login = login;
        this.Password = password;


    }

    public UserDB ToUserDB()
    {
        var user = new UserDB();
        user.Login = this.Login;
        user.Password = this.Password;
        user.Hash = this.Hash;
        return user;
    }
}
