using ADO.HELP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADO.HELP.Controllers
{
    public class HomeController : Controller
    {
        private string connect = "Data Source=DESKTOP-NEVLUCA;Initial Catalog=ADOdb;Integrated Security=True;Application Name=EntityFramework;";

        [HttpPost]
        public ActionResult AddInimene(string nimi, int vanus, string maakond)
        {
            InsertInimeneToDatabase(nimi, vanus, maakond);

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<Inimene> inimesed = new List<Inimene>();

            string query = "SELECT Id, Nimi, Vanus, Maakond FROM Inimesed";

            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inimene inimene = new Inimene
                        {
                            Id = (int)reader["Id"],
                            Nimi = reader["Nimi"].ToString(),
                            Vanus = reader["Vanus"] as int?,
                            Maakond = reader["Maakond"].ToString()
                        };
                        inimesed.Add(inimene);
                    }
                }
            }

            return View(inimesed);
        }
        private void InsertInimeneToDatabase(string nimi, int vanus, string maakond)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                string query = "INSERT INTO Inimesed (Nimi, Vanus, Maakond) VALUES (@Nimi, @Vanus, @Maakond)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nimi", nimi);
                command.Parameters.AddWithValue("@Vanus", vanus);
                command.Parameters.AddWithValue("@Maakond", maakond);
                command.ExecuteNonQuery();
            }
        }

    }
}