using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace learningapp.Pages;

public class IndexModel : PageModel
{
     public List<Course> Courses=new List<Course>();
    private readonly ILogger<IndexModel> _logger;
    private IConfiguration _configuration;
    public IndexModel(ILogger<IndexModel> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration=configuration;
    }

    //public void OnGet()
    //{
       
    //    string connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")!;
    //    var sqlConnection = new SqlConnection(connectionString);
    //    sqlConnection.Open();

    //    var sqlcommand = new SqlCommand(
    //    "SELECT CourseID,CourseName,Rating FROM Course;",sqlConnection);
    //     using (SqlDataReader sqlDatareader = sqlcommand.ExecuteReader())
    //     {
    //         while (sqlDatareader.Read())
    //            {
    //                Courses.Add(new Course() {CourseID=Int32.Parse(sqlDatareader["CourseID"].ToString()),
    //                CourseName=sqlDatareader["CourseName"].ToString(),
    //                Rating=Decimal.Parse(sqlDatareader["Rating"].ToString())});
    //            }
    //     }
    //}
    public async Task<IActionResult> OnGet()
    {
        string? funcURL = Environment.GetEnvironmentVariable("CourseFunction");
        //string? funcURL = "https://funcapp0311-gaa9hfcqg4fmfcet.canadacentral-01.azurewebsites.net/api/appFunction";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(funcURL);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Courses = JsonConvert.DeserializeObject<List<Course>>(jsonResponse);
            }
        }
        return Page();
    }
}
