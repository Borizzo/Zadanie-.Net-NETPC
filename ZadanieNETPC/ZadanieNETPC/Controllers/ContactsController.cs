using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using ZadanieNETPC.Models;
using Newtonsoft.Json.Serialization;

// klasa kontrolera API

namespace ZadanieNETPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ContactsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //metoda odpowiedzialna za przekazywanie informacji z bazy danych do aplikacji frontednowej w formacie JSON
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select @Email,@FirstName,@LastName,@Type,@PhoneNumb,@DateOfBirth,@Password from
                            dbo.Contact
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        //metoda odpowiedzialna za dodawanie nowych rekordow do bazy danych
        [HttpPost]
        public JsonResult Post(Contact par)
        {
            string query = @"
                           insert into dbo.Contact
                           values (@Email,@FirstName,@LastName,@Type,@PhoneNumb,@DateOfBirth,@Password)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Email", par.Email);
                    myCommand.Parameters.AddWithValue("@FirstName", par.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", par.LastName);
                    myCommand.Parameters.AddWithValue("@Type", par.Type);
                    myCommand.Parameters.AddWithValue("@PhoneNumb", par.PhoneNumb);
                    myCommand.Parameters.AddWithValue("@Password", par.Password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        //metoda odpowiedzialna za edycje rekordow w bazie danych
        [HttpPut]
        public JsonResult Put(Contact par)
        {
            string query = @"
                            update dbo.Contact
                           set FirstName=@FirstName,
                            LastName=@LastName,
                            Type=@Type,
                            PhoneNumb=@PhoneNumb,
                            Password=@Password
                            where Email=@Email
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Email", par.Email);
                    myCommand.Parameters.AddWithValue("@FirstName", par.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", par.LastName);
                    myCommand.Parameters.AddWithValue("@Type", par.Type);
                    myCommand.Parameters.AddWithValue("@PhoneNumb", par.PhoneNumb);
                    myCommand.Parameters.AddWithValue("@Password", par.Password);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        //metoda odpowiedzialna za kasowanie rekordow z bazy danych
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete dbo.Contact
                            where Email=@Email
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ContactConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Email", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
