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
using Windows.Devices.Printers;

namespace Software_Technology.Classes
{
    static class DatabaseController
    {
        static String connectionString = "Data source=realEstateApp.db;Version=3";
        static SQLiteConnection connection;


        public static void CreateTables()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createSQLMembers = "Create table if not exists Members(email Text,usersID Text primary key,username Text,name Text,surname Text,phoneNumber Text,hashedPassword Text,soldRealEstates Text,boughtRealEstates Text,leasedRealEstates Text,rentedRealEstates Text)";
            SQLiteCommand commandMembers = new SQLiteCommand(createSQLMembers, connection);
            commandMembers.ExecuteNonQuery();

            String createSQLAdmins = "Create table if not exists Admins(usersID Text primary key,username Text,name Text,surname Text,hashedPassword Text)";
            SQLiteCommand commandAdmins = new SQLiteCommand(createSQLAdmins, connection);
            commandAdmins.ExecuteNonQuery();

            String createSQLRealEstates = "Create table if not exists RealEstates(realEstateID Int primary key,buyer_tenantID Text,seller_lessorID Text,price Int,size Int,floor Int,year Int,bedrooms Int,availability Boolean,leaseSell Boolean,area Text,type Text,details Text,image Text)";
            SQLiteCommand commandRealEstates = new SQLiteCommand(createSQLRealEstates, connection);

            commandRealEstates.ExecuteNonQuery();
            connection.Close();
        }


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
                return false;
            }
            catch (Exception ex)
            {
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
                            
                        }

                    }
                }
            }

            return logInValues;
        }

        

        public static bool DeleteRealEstateFromDatabase(int realEstateID)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Delete from RealEstates WHERE realEstateID=@realEstateID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@realEstateID", realEstateID);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        
                        return true;
                    }
                    else
                    {
                        
                        return false;
                    }
                }
            }
        }


        public static void DeleteMemberFromDatabase(String usersID,String username)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String deleteSQL = "Delete from Members WHERE usersID=@usersID and username=@username";
                using (var command = new SQLiteCommand(deleteSQL, connection))
                {
                    command.Parameters.AddWithValue("@usersID", usersID);
                    command.Parameters.AddWithValue("@username", username);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        
                        //return true;
                    }
                    else
                    {
                        
                        //return false;
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
                String selectSQL = "Update RealEstates SET realEstateID = @realEstateID, buyer_tenantID = @buyer_tenantID, seller_lessorID = @seller_lessorID, price = @price, size = @size, floor = @floor,year = @year, bedrooms = @bedrooms, availability = @availability, leaseSell = @leaseSell, area = @area, type = @type, details = @details, image = @image WHERE realEstateID =@realEstateID";
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
                        
                        return true;
                    }
                    else
                    {
                        
                        return false;
                    }
                }
            }
        }



        public static void SignUpAdmins(string usersID, string username, string name, string surname, string hashedPassword)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertSQL = "INSERT INTO Admins(usersID,username,name,surname,hashedPassword) VALUES (@usersID,@username,@name,@surname,@hashedPassword)";
                    using (var command = new SQLiteCommand(insertSQL, connection))
                    {


                        command.Parameters.AddWithValue("@usersID", usersID);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@surname", surname);

                        command.Parameters.AddWithValue("@hashedPassword", hashedPassword);
                        command.ExecuteNonQuery();
                    }
                }
                //return true;
            }
            catch (SQLiteException ex)
            {
                
                //return false;
            }
            catch (Exception ex)
            {
                
                //return false;
            }
        }



        public static void ChangePassword(string usersID, string hashedPassword)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                if (usersID.StartsWith("A"))
                {
                    String updateSQL = $"UPDATE Admins SET hashedPassword = @hashedPassword WHERE usersID = @usersID";
                    using (var command = new SQLiteCommand(updateSQL, connection))
                    {
                        command.Parameters.AddWithValue("hashedPassword", hashedPassword);
                        command.Parameters.AddWithValue("usersID", usersID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //Debug.WriteLine("Password Changed Successfully!");
                        }
                        else
                        {
                            //Debug.WriteLine("Password Change Failed!");
                        }
                    }
                }
                else if (usersID.StartsWith("M"))
                {
                    String updateSQL = $"UPDATE Members SET hashedPassword = @hashedPassword WHERE usersID = @usersID";
                    using (var command = new SQLiteCommand(updateSQL, connection))
                    {
                        command.Parameters.AddWithValue("hashedPassword", hashedPassword);
                        command.Parameters.AddWithValue("usersID", usersID);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //Debug.WriteLine("Password Changed Successfully!");
                        }
                        else
                        {
                            //Debug.WriteLine("Password Change Failed!");
                        }
                    }
                }
                
                
            }
        }




        public static void UpdateNameSurname(string usersID, string name, string surname)
        {
            
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append("UPDATE ");

                if (usersID.StartsWith("A"))
                {
                    sb.Append("Admins SET ");
                }
                else if (usersID.StartsWith("M"))
                {
                    sb.Append("Members SET ");
                }


                if (!string.IsNullOrEmpty(name))
                {
                    sb.Append("name = @name");
                }

                if (!string.IsNullOrEmpty(surname))
                {

                    if (!string.IsNullOrEmpty(name))
                    {

                        sb.Append(", ");
                    }
                    sb.Append("surname = @surname");
                }

                

                sb.Append(" WHERE usersID = @usersID ");

                String updateSQL = sb.ToString();


                using (var command = new SQLiteCommand(updateSQL, connection))
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        command.Parameters.AddWithValue("name", name);
                    }

                    if (!string.IsNullOrEmpty(surname))
                    {
                        command.Parameters.AddWithValue("surname", surname);
                    }

                    

                    command.Parameters.AddWithValue("usersID", usersID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //Debug.WriteLine("User Details Changed Successfully");
                    }
                    else
                    {
                        //Debug.WriteLine("User Details Change Failed!");
                    }
                }

                
            }
        }





        public static void AddRealEstate(RealEstate realEstate)
        {
            
            String stringImages = String.Join(",", realEstate.images);
            

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertSQL = "INSERT INTO RealEstates (realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image) VALUES (@realEstateID,@buyer_tenantID,@seller_lessorID,@price,@size,@floor,@year,@bedrooms,@availability,@leaseSell,@area,@type,@details,@image)";
                using (var command = new SQLiteCommand(insertSQL, connection))
                {
                    command.Parameters.AddWithValue("realEstateID", realEstate.realEstateID);
                    command.Parameters.AddWithValue("buyer_tenantID", realEstate.buyer_tenantID);
                    command.Parameters.AddWithValue("seller_lessorID", realEstate.seller_lessorID);
                    command.Parameters.AddWithValue("price", realEstate.price);
                    command.Parameters.AddWithValue("size", realEstate.size);
                    command.Parameters.AddWithValue("floor", realEstate.floor);
                    command.Parameters.AddWithValue("year", realEstate.year);
                    command.Parameters.AddWithValue("bedrooms", realEstate.bedrooms);
                    command.Parameters.AddWithValue("availability", realEstate.availability);
                    command.Parameters.AddWithValue("leaseSell", realEstate.leaseSell);
                    command.Parameters.AddWithValue("area", realEstate.area);
                    command.Parameters.AddWithValue("type", realEstate.type);
                    command.Parameters.AddWithValue("details", realEstate.details);
                    command.Parameters.AddWithValue("image", stringImages);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //Debug.WriteLine("Success Insert of RealEstate!");
                    }
                    else
                    {
                        //Debug.WriteLine("Failed Insert of RealEstate!");
                    }
                }
            }
        }



        public static List<RealEstate> RetrieveUsersRealEstates(string buyer_tenantID, string seller_lessorID)
        {
            List<RealEstate> foundRealEstates = new List<RealEstate>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image from RealEstates WHERE @buyer_tenantID = buyer_tenantID OR @seller_lessorID = seller_lessorID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@buyer_tenantID", buyer_tenantID);
                    command.Parameters.AddWithValue("@seller_lessorID", seller_lessorID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int realEstateID = (int)reader.GetInt32(0);
                            buyer_tenantID = reader.GetString(1);
                            seller_lessorID = reader.GetString(2);
                            int price = (int)reader.GetInt32(3);
                            int size = (int)reader.GetInt32(4);
                            int floor = (int)reader.GetInt32(5);
                            int year = (int)reader.GetInt32(6);
                            int bedrooms = (int)reader.GetInt32(7);
                            bool availability = reader.GetBoolean(8);
                            bool leaseSell = reader.GetBoolean(9);
                            string area = reader.GetString(10);
                            string type = reader.GetString(11);
                            string details = reader.GetString(12);
                            string listimage = reader.GetString(13);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            List<string> listImages = images.ToList();
                            RealEstate real = new RealEstate(realEstateID, buyer_tenantID, seller_lessorID, price, size, floor, year, bedrooms, availability, leaseSell, area, type, details, listImages);
                            foundRealEstates.Add(real);
                        }
                    }
                }
            }

            if (foundRealEstates.Count == 0)
            {
                //Debug.WriteLine("The user has no real estates yet!!!");
            }
            return foundRealEstates;
        }




        public static List<RealEstate> ShowRealEstateToBuy_Rent(bool leaseSell)
        {
            List<RealEstate> foundRealEstates = new List<RealEstate>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image from RealEstates WHERE @leaseSell = leaseSell and availability=@availability";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@leaseSell", leaseSell);
                    command.Parameters.AddWithValue("@availability", true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int realEstateID = (int)reader.GetInt32(0);
                            string buyer_tenantID = reader.GetString(1);
                            string seller_lessorID = reader.GetString(2);
                            int price = (int)reader.GetInt32(3);
                            int size = (int)reader.GetInt32(4);
                            int floor = (int)reader.GetInt32(5);
                            int year = (int)reader.GetInt32(6);
                            int bedrooms = (int)reader.GetInt32(7);
                            bool availability = reader.GetBoolean(8);
                            leaseSell = reader.GetBoolean(9);
                            string area = reader.GetString(10);
                            string type = reader.GetString(11);
                            string details = reader.GetString(12);
                            string listimage = reader.GetString(13);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            List<string> listImages = images.ToList();
                            RealEstate real = new RealEstate(realEstateID, buyer_tenantID, seller_lessorID, price, size, floor, year, bedrooms, availability, leaseSell, area, type, details, listImages);
                            foundRealEstates.Add(real);
                        }
                    }
                }
            }
            if (foundRealEstates.Count == 0)
            {
                //Debug.WriteLine("No real estates found!!!");
            }
            return foundRealEstates;
        }




        public static void Buy_Sell_Rent_LeaseRealEstate(RealEstate realEstate, string buyer_tenantID)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String updateSQL = $"UPDATE RealEstates SET buyer_tenantID = @buyer_tenantID, availability = @availability WHERE realEstateID = @realEstateID";
                using (var command = new SQLiteCommand(updateSQL, connection))
                {
                    command.Parameters.AddWithValue("buyer_tenantID", buyer_tenantID);
                    command.Parameters.AddWithValue("realEstateID", realEstate.realEstateID);
                    command.Parameters.AddWithValue("availability", false);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //Debug.WriteLine("RealEstate sold/bought Successfully!");
                    }
                    else
                    {
                        //Debug.WriteLine("RealEstate sold/bought Change Failed!");
                    }
                }
            }
        }


        public static void UpdateContactDetails(string usersID, string email, string phoneNumber)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append("UPDATE Members SET ");

                if (!string.IsNullOrEmpty(email))
                {
                    sb.Append("email = @email");
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {

                    if (!string.IsNullOrEmpty(email))
                    {

                        sb.Append(", ");
                    }
                    sb.Append("phoneNumber = @phoneNumber");
                }

                sb.Append(" WHERE usersID = @usersID ");

                String updateSQL = sb.ToString();


                using (var command = new SQLiteCommand(updateSQL, connection))
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        command.Parameters.AddWithValue("email", email);
                    }

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        command.Parameters.AddWithValue("phoneNumber", phoneNumber);
                    }

                    command.Parameters.AddWithValue("usersID", usersID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //Debug.WriteLine("Contact Details Changed Successfully");
                    }
                    else
                    {
                        //Debug.WriteLine("Contact Details Change Failed!");
                    }
                }


            }
        }


        public static List<String> GetMembersID()
        {
            List<String> memberlist = new List<String>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select usersID from Members where usersID NOT IN (Select buyer_tenantID FROM RealEstates UNION Select seller_lessorID FROM RealEstates) ORDER BY usersID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            memberlist.Add(reader.GetString(0));

                        }

                    }
                }
            }

            return memberlist;
        }


        public static List<String> GetMembersUsername()
        {
            List<String> memberlist = new List<String>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select username from Members where usersID NOT IN (Select buyer_tenantID FROM RealEstates UNION Select seller_lessorID FROM RealEstates) ORDER BY usersID";
                using (var command = new SQLiteCommand(selectSQL, connection))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            memberlist.Add(reader.GetString(0));
                            

                        }

                    }
                }
            }

            return memberlist;
        }


        public static List<int> GetRealEstates()
        {
            List<int> realEstateslist = new List<int>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID from RealEstates where availability=@availability";
                
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("availability", true);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            realEstateslist.Add((int)reader.GetInt32(0));
                            

                        }

                    }
                }
            }

            return realEstateslist;
        }


        public static List<int> GetMyRealEstatesForDelete(String seller_lessorID,bool leaseSell)
        {
            List<int> realEstateslist = new List<int>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID from RealEstates where availability=@availability and seller_lessorID=@seller_lessorID and leaseSell=@leaseSell";

                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("availability", true);
                    command.Parameters.AddWithValue("seller_lessorID", seller_lessorID);
                    command.Parameters.AddWithValue("leaseSell", leaseSell);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            realEstateslist.Add((int)reader.GetInt32(0));
                            

                        }

                    }
                }
            }

            return realEstateslist;
        }


        public static List<String> GetRealEstatesFrorDeleteImages(int realEstateID)
        {
            List<String> listImages = new List<String>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select image from RealEstates where realEstateID=@realEstateID";

                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("realEstateID", realEstateID);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read() == true)
                        {
                            
                            string listimage = reader.GetString(0);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            listImages = images.ToList();
                            

                        }

                    }
                }
            }
            
            foreach (String item in listImages)
            {
                //Debug.WriteLine(item);
            }
            return listImages;
        }


        public static List<RealEstate> GetMyRealEstates(String seller_lessorID, bool leaseSell)
        {
            List<RealEstate> realEstateslist = new List<RealEstate>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select realEstateID,buyer_tenantID,seller_lessorID,price,size,floor,year,bedrooms,availability,leaseSell,area,type,details,image from RealEstates where availability=@availability and seller_lessorID=@seller_lessorID and leaseSell=@leaseSell";

                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("availability", true);
                    command.Parameters.AddWithValue("seller_lessorID", seller_lessorID);
                    command.Parameters.AddWithValue("leaseSell", leaseSell);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int realEstateID = (int)reader.GetInt32(0);
                            string buyer_tenantID = reader.GetString(1);
                            seller_lessorID = reader.GetString(2);
                            int price = (int)reader.GetInt32(3);
                            int size = (int)reader.GetInt32(4);
                            int floor = (int)reader.GetInt32(5);
                            int year = (int)reader.GetInt32(6);
                            int bedrooms = (int)reader.GetInt32(7);
                            bool availability = reader.GetBoolean(8);
                            leaseSell = reader.GetBoolean(9);
                            string area = reader.GetString(10);
                            string type = reader.GetString(11);
                            string details = reader.GetString(12);
                            string listimage = reader.GetString(13);


                            string[] images = listimage.Split(",", StringSplitOptions.None);

                            List<string> listImages = images.ToList();
                            RealEstate real = new RealEstate(realEstateID, buyer_tenantID, seller_lessorID, price, size, floor, year, bedrooms, availability, leaseSell, area, type, details, listImages);
                            realEstateslist.Add(real);

                        }

                    }
                }
            }
            if (realEstateslist.Count == 0)
            {
                //Debug.WriteLine("No real estates found!!!");
            }
            
            return realEstateslist;
        }


        public static List<String> GetMemberContactDetails(String seller_lessorID)
        {
            List<String> sellerLessorContactDetails = new List<String>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                String selectSQL = "Select email,phoneNumber from Members where usersID=@usersID";
                
                using (var command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("usersID", seller_lessorID);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            sellerLessorContactDetails.Add(reader.GetString(0));
                            sellerLessorContactDetails.Add(reader.GetString(1));
                            

                        }

                    }
                }
            }

            return sellerLessorContactDetails;
        }


    }
}
