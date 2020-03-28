using System.Collections;
using System.Collections.Generic;

public class User
{
    public string username;
    public string email;
    public string time;

    public User()
    {
    }

    public User(string username, string email,string time)
    {
        this.username = username;
        this.email = email;
        this.time = time;
    }
}
