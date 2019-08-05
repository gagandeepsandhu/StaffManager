using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace StaffManager.Controllers
{
    public class StaffController : Controller
    {
        public List<string[]> AllStaff = new List<string[]> { };
        [HttpPost]
        public ActionResult Add()
        {


            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=staffdb.db;Version=3;New=False;Compress=True;");
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "CREATE TABLE STAFF (SID varchar, NAME varchar, EMAIL varchar)";
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write("TABLE ALREAY CREATED");
            }


            string sid = Request.Form["sid"];
            string name = Request.Form["fname"];
            string email = Request.Form["email"];


            try
            {
                SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=staffdb.db;Version=3;New=False;Compress=True;");
                sQLiteConnection.Open();
                SQLiteCommand command = sQLiteConnection.CreateCommand();
                command.CommandText = "INSERT INTO STAFF (SID, NAME, EMAIL) VALUES(@param1, @param2, @param3)";
                command.Parameters.Add(new SQLiteParameter("@param1", sid));
                command.Parameters.Add(new SQLiteParameter("@param2", name));
                command.Parameters.Add(new SQLiteParameter("@param3", email));
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write("Failed to execute commands");
            }
           

            Response.Redirect("/Home/Panel");

            return View();
        }

        [HttpPost]
        public ActionResult Remove()
        {

            string sid = Request.Form["sid"];


            try
            {
                SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=staffdb.db;Version=3;New=False;Compress=True;");
                sQLiteConnection.Open();
                SQLiteCommand command = sQLiteConnection.CreateCommand();
                command.CommandText = "DELETE FROM STAFF WHERE SID = @param1;";
                command.Parameters.Add(new SQLiteParameter("@param1", sid));
                command.ExecuteNonQuery();
                Response.Redirect("/Home/Panel");
            }catch(Exception e)
            {
                Console.Write("Failed to execute commands");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Update()
        {

            string sid = Request.Form["sid"];
            string name = Request.Form["fname"];
            string email = Request.Form["email"];


            try
            {
                SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=staffdb.db;Version=3;New=False;Compress=True;");
                sQLiteConnection.Open();
                SQLiteCommand command = sQLiteConnection.CreateCommand();
                command.CommandText = "UPDATE STAFF SET NAME = @param1, EMAIL = @param2 Where SID = @param3;";
                command.Parameters.Add(new SQLiteParameter("@param1", sid));
                command.Parameters.Add(new SQLiteParameter("@param2", name));
                command.Parameters.Add(new SQLiteParameter("@param3", email));
                command.ExecuteNonQuery();
            }catch(Exception e)
            {
                Console.Write("Failed to execute commands");
            }

            Response.Redirect("/Home/Panel");

            return View();
        }


        [HttpGet]
        public ActionResult List()
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=staffdb.db;Version=3;New=False;Compress=True;");
            sQLiteConnection.Open();
            SQLiteCommand command = sQLiteConnection.CreateCommand();

            command.CommandText = "SELECT * FROM STAFF;";
            command.ExecuteNonQuery();
            SQLiteDataReader sqliteReader = command.ExecuteReader();
            while (sqliteReader.Read())
            {
                String[] staffInfo = new string[3];
                staffInfo[0] = sqliteReader.GetString(0);
                staffInfo[1] = sqliteReader.GetString(1);
                staffInfo[2] = sqliteReader.GetString(2);
                AllStaff.Add(staffInfo);
            }

            ViewData["Display"] = AllStaff;
            return View();
        }


    }
}
