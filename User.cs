public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public User(int userId, string username, string password, bool isAdmin)
    {
        UserId = userId;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }
}
