using AdoNetSamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace AdoNetSamples.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult ConnectDB()
        {

            SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=salesdb;Integrated Security=True;TrustServerCertificate=true");
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Select * from deptTable";

                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Console.WriteLine(reader[0]);
                    reader.NextResult();
                }
            }

            return View("Index");
        }

        //public IActionResult ExportToCsv()
        //{
        //    return View("Index");
        //}

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}