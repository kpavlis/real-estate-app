using Software_Technology.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Windows.Media.AppBroadcasting;
using Windows.System;

public abstract class User
{
    private string _usersID { get; }
    public string username { get; }
    public string name { get; private set;}
    public string surname { get; private set;}
	private string _password { get; set; }

    public User(string _usersID, string username, string name, string surname, string _password)
    {
        this._usersID = _usersID;
        this.username = username;
        this.name = name;
        this.surname = surname;
        this._password = _password;
    }
    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return _password;
    }

    public string GetUsersID()
    {
        return _usersID;
    }

    public static List<string> LogIn(string username, string password)
    {
        string encryptedPassword = ""; //To be retrieved from database
        bool flag = false;
        List<string> logInValues = new List<string>();
        logInValues = DatabaseController.LogIn(username);
        if (!logInValues.Count.Equals(0))
        {
            encryptedPassword = logInValues[3];
            flag = ValidatePassword(password, encryptedPassword);
            if (flag == true)
            {
                return logInValues;
            }
            else
            {
                logInValues.Clear();
                return logInValues;
            }
        }
        logInValues.Clear();
        return logInValues;
    }

    public void ChangePassword(String usersID, String newPassword)
    {
        string hashedPassword = HashPassword(newPassword);
        DatabaseController.ChangePassword(usersID, hashedPassword);
    }


    public void UpdateNameSurnameUsers(String usersID, String newName,String newSurname)
    {
        
        this.name = newName;
        this.surname = newSurname;
        DatabaseController.UpdateNameSurname(usersID, newName, newSurname);
    }


    //Encypt password 
    public static string HashPassword(String _password) 
	{
        byte[] salt; //Generate a 128-bit salt using a secure PRNG
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        var pbkdf2 = new Rfc2898DeriveBytes(_password, salt, 100000); //Derive a 256-bit subkey using PBKDF2 with HMACSHA1, along with the password and the salt
        byte[] hash = pbkdf2.GetBytes(20); //Get the 20-byte hash of the password
        byte[] hashBytes = new byte[36]; //Combine the salt and password bytes for storage
        Array.Copy(salt, 0, hashBytes, 0, 16); 
        Array.Copy(hash, 0, hashBytes, 16, 20); 
        string storedPassword = Convert.ToBase64String(hashBytes); //Convert the combined salt and hash to a Base64 string for storage
        /*Testing
        Debug.WriteLine(hashedPassword); 
        UnhashPassword(hashedPassword);*/
        return storedPassword; //return the hashed password
	}


    //Validate Password
    public static bool ValidatePassword(string enteredPassword, string storedPassword) 
	{
        byte[] hashBytes = Convert.FromBase64String(storedPassword); //Convert the stored Base64 string to byte array
        byte[] salt = new byte[16]; //Extract the salt from the stored hash
        Array.Copy(hashBytes, 0, salt, 0, 16);
        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000); //Derive the hash of the entered password using the same salt and PBKDF2 parameters
        byte[] hash = pbkdf2.GetBytes(20);
        for (int i = 0; i < 20; i++) //Compare the stored hash and the entered passwod's hash byte by byte
            if (hashBytes[i + 16] != hash[i])
            {
               return false;
            }

        return true;
    }

}
