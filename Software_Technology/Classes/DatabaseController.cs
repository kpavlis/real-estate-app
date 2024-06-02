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

        public static void UpdateRealEstatesList(string username, string realEstatesList, string listType)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                Debug.WriteLine("Hello " + username);
                Debug.WriteLine("Hello " + realEstatesList);
                string columnName;
                switch (listType.ToLower())
                {
                    case "sold":
                        columnName = "soldRealEstates";
                        break;
                    case "bought":
                        columnName = "boughtRealEstates";
                        break;
                    case "leased":
                        columnName = "leasedRealEstates";
                        break;
                    case "rented":
                        columnName = "rentedRealEstates";
                        break;
                    default:
                        throw new ArgumentException("Invalid list type!");
                }
                String updateSQL = $"UPDATE Members SET {columnName} = @realEstatesList WHERE username = @username";
                using( var command = new SQLiteCommand(updateSQL, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("realEstatesList", realEstatesList);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Debug.WriteLine("Success Update!");
                    }
                    else
                    {
                        Debug.WriteLine("Failed Update!");
                    }
                }
            }
        }

        public static bool DeleteRealEstateFromDatabase(int realEstateID)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Delete from RealEstaes WHERE realEstateID =@realEstateID";
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



        public static List<RealEstate> LoadRealEstatesForBuyerTenant(bool leaseSell,string buyer_tenantID)
        {
            List<RealEstate> bought_rentedRealEsatets = new List<RealEstate>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image from Members where @leaseSell=leaseSell and @buyer_tenantID=buyer_tenantID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@leaseSell", leaseSell);
                    command.Parameters.AddWithValue("@buyer_tenantID", buyer_tenantID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int realEstateID = (int)reader.GetInt32(0);
                            string seller_lessorID = reader.GetString(2);
                            int price = (int)reader.GetInt32(3);
                            int size = (int)reader.GetInt32(4);
                            int floor = (int)reader.GetInt32(5);
                            int year = (int)reader.GetInt32(6);
                            int bedrooms = (int)reader.GetInt32(7);
                            bool availability = reader.GetBoolean(8);
                            string area = reader.GetString(10);
                            string type = reader.GetString(11);
                            string details = reader.GetString(12);
                            string listimage = reader.GetString(13);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            List<string> list = images.ToList();


                            Debug.WriteLine("!!!!!");

                            RealEstate boughtRealEstate = new RealEstate(realEstateID, buyer_tenantID, seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,list);

                            bought_rentedRealEsatets.Add(boughtRealEstate);
                        }

                    }
                }
            }

            if (bought_rentedRealEsatets.Count == 0)
            {
                Debug.WriteLine("No bought Real Estates!!!");
            }
            return bought_rentedRealEsatets;
        }


        public static List<RealEstate> LoadRealEstateForBuyerLessor(bool leaseSell, string seller_lessorID)
        {
            List<RealEstate> sold_leasedRealEsatets = new List<RealEstate>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image from Members where @leaseSell=leaseSell and @seller_lessorID=seller_lessorID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@username", leaseSell);
                    command.Parameters.AddWithValue("@seller_lessorID", seller_lessorID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int realEstateID = (int)reader.GetInt32(0);
                            string buyer_tenantID = reader.GetString(1);
                            int price = (int)reader.GetInt32(3);
                            int size = (int)reader.GetInt32(4);
                            int floor = (int)reader.GetInt32(5);
                            int year = (int)reader.GetInt32(6);
                            int bedrooms = (int)reader.GetInt32(7);
                            bool availability = reader.GetBoolean(8);
                            string area = reader.GetString(10);
                            string type = reader.GetString(11);
                            string details = reader.GetString(12);
                            string listimage = reader.GetString(13);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            List<string> list = images.ToList();


                            Debug.WriteLine("!!!!!");

                            RealEstate boughtRealEstate = new RealEstate(realEstateID, buyer_tenantID, seller_lessorID, price, size, floor, year, bedrooms, availability, leaseSell, area, type, details, list);

                            sold_leasedRealEsatets.Add(boughtRealEstate);
                        }

                    }
                }
            }

            if (sold_leasedRealEsatets.Count == 0)
            {
                Debug.WriteLine("No bought Real Estates!!!");
            }
            return sold_leasedRealEsatets;
        }



    }
}
