using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json;
using System.Reflection.PortableExecutable;

namespace Software_Technology.Classes
{
    static class DatabaseController
    {
        static String connectionString = "Data source=realEstateApp.db;Version=3";
        static SQLiteConnection connection;


        public static bool SignUp(string email, string usersID, string username, string name, string surname, string phoneNumber, string hashedPassword)
        {
            try
            {
                using(var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertSQL = "INSERT INTO Members(email,usersID,username,name,surname,phoneNumber,hashedPassword) VALUES (@email,@usersID,@username,@name,@surname,@phoneNumber,@hashedPassword)";
                    using (var command = new SQLiteCommand(insertSQL, connection))
                    {

                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@usersID", usersID);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                        command.ExecuteNonQuery();
                    } 
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("SQLite error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("General error: " + ex.Message);
                return false;
            }
        }

        public static List<string> LogIn(string username)
        {
            List<string> logInValues = new List<string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select usersID,email,name,hashedPassword,surname,phoneNumber from Members  where @username=username";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            logInValues.Add(reader.GetString(0));
                            logInValues.Add(reader.GetString(1));
                            logInValues.Add(reader.GetString(2));
                            logInValues.Add(reader.GetString(3));
                            logInValues.Add(reader.GetString(4));
                            logInValues.Add(reader.GetString(5));
                            Debug.WriteLine("Welcome Member!!!!!");

                        }

                    }
                }
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select usersID,name,surname,hashedPassword from Admins where username=@username";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            logInValues.Add(reader.GetString(0));
                            logInValues.Add(reader.GetString(1));
                            logInValues.Add(reader.GetString(2));
                            logInValues.Add(reader.GetString(3));
                            Debug.WriteLine("Welcome Admin!!!!!");
                        }

                    }
                }
            }

            if(logInValues.Count == 0)
            {
                Debug.WriteLine("User not found!!!");
            }
            return logInValues;
        }

        

        public static bool DeleteRealEstateFromDatabase(int realEstateID)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Delete * from RealEstaes WHERE realEstateID =@realEstateID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@realEstateID", realEstateID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Deleted!");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Not Deleted!");
                        return false;
                    }
                }
            }
        }


        public static bool DeleteMemberFromDatabase(String usersID)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Delete * from RealEstaes WHERE usersID =@usersID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@usersID", usersID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Deleted!");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Not Deleted!");
                        return false;
                    }
                }
            }
        }

        public static bool UpdateRealEstateFromDatabase(int realEstateID, String buyer_tenantID, String seller_lessorID, int price, int size, int floor, int year, int bedrooms, bool availability, bool leaseSell, String area, String type, String details, List<String> images)
        {
            String myString = String.Join(",", images);
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Update * SET realEstateID = @realEstateID, buyer_tenantID = @buyer_tenantID, seller_lessorID = @seller_lessorID, price = @price, size = @size, floor = @floor,year = @year, bedrooms = @bedrooms, availability = @availability, leaseSell = @leaseSell, area = @area, type = @type, details = @details, image = @image WHERE realEstateID =@realEstateID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@realEstateID", realEstateID);
                    command.Parameters.AddWithValue("@buyer_tenantID", buyer_tenantID);
                    command.Parameters.AddWithValue("@seller_lessorID", seller_lessorID);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@size", size);
                    command.Parameters.AddWithValue("@floor", floor);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@bedrooms", bedrooms);
                    command.Parameters.AddWithValue("@availability", availability);
                    command.Parameters.AddWithValue("@leaseSell", leaseSell);
                    command.Parameters.AddWithValue("@area", area);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@details", details);
                    command.Parameters.AddWithValue("@image", myString);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Edited!");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Not Edited!");
                        return false;
                    }
                }
            }
        }



    }
}
