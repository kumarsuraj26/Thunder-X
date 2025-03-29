using System;
using System.Data.SqlClient;
using System.Web.Mvc;

public class AccountController : Controller
{
    private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // Register Action
    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(string username, string password, string email)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Users (Username, Password, Email) VALUES (@Username, @Password, @Email)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);  // In a real app, hash the password
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.ExecuteNonQuery();
        }
        return RedirectToAction("Login");
    }

    // Login Action
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string username, string password)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return RedirectToAction("UserProfile", "Profile");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }
    }
}
