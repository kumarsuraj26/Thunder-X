using System.Data.SqlClient;
using System.Web.Mvc;

public class DietAndWorkoutController : Controller
{
    private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

    // Show Diet and Workout Plans
    public ActionResult DietAndWorkout()
    {
        int userId = 1; // Replace with session or logged-in user ID

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();

            // Get Diet Plan
            string dietQuery = "SELECT DietDescription FROM DietPlan WHERE ProfileId = @ProfileId";
            SqlCommand dietCmd = new SqlCommand(dietQuery, conn);
            dietCmd.Parameters.AddWithValue("@ProfileId", userId);
            string dietDescription = (string)dietCmd.ExecuteScalar();

            // Get Workout Plan
            string workoutQuery = "SELECT WorkoutDescription FROM WorkoutPlan WHERE ProfileId = @ProfileId";
            SqlCommand workoutCmd = new SqlCommand(workoutQuery, conn);
            workoutCmd.Parameters.AddWithValue("@ProfileId", userId);
            string workoutDescription = (string)workoutCmd.ExecuteScalar();

            ViewBag.DietDescription = dietDescription;
            ViewBag.WorkoutDescription = workoutDescription;
        }

        return View();
    }
}
