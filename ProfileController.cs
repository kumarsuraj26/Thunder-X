using System;
using System.Data.SqlClient;
using System.Web.Mvc;

public class ProfileController : Controller
{
    private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // Show Profile Page
    public ActionResult UserProfile()
    {
        return View();
    }

    // Save Profile Data
    [HttpPost]
    public ActionResult UserProfile(int age, decimal bodyWeight, string goal, string region)
    {
        int userId = 1; // Replace with session or logged-in user ID

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "INSERT INTO UserProfile (UserId, Age, BodyWeight, Goal, Region) VALUES (@UserId, @Age, @BodyWeight, @Goal, @Region)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@BodyWeight", bodyWeight);
            cmd.Parameters.AddWithValue("@Goal", goal);
            cmd.Parameters.AddWithValue("@Region", region);
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("DietAndWorkout", "DietAndWorkout");
    }
}
