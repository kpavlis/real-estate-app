using Microsoft.WindowsAppSDK.Runtime.Packages;
using Software_Technology.Classes;
using System;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using Windows.Media.AppBroadcasting;
using Windows.System;

public abstract class Users
{
    private  string _usersID { get; }
    public string username { get; }
    public string name { get; private set;}
    public string surname { get; private set;}
	private string _password { get; set; }

    //_encryptedPassword = HashPassword(_password); //Call method to encrypt password
    //ValidatePassword(_password, _encryptedPassword); //Call method to validate password
    public Users(string _usersID, string username, string name, string surname, string _password)
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

    public static bool LogIn(string username, string password)
    {
        string usersIDLogin = null; //To be retrieved from database
        string nameLogin = null; //To be retrieved from database
        string surnameLogin = null; //To be retrieved from database
        string encryptedPassword = ""; //To be retrieved from database
        bool flag = false;
        if (usersIDLogin.Equals("A")){
            Admins admin = new Admins(usersIDLogin, username, nameLogin, surnameLogin, password);    
        }else if(usersIDLogin.Equals("M")){
            flag = ValidatePassword(password,encryptedPassword);
            if (flag == true)
            {
                string email = null; //To be retrieved from database
                Members member = new Members(email, usersIDLogin, username, nameLogin, surnameLogin, password);
            }
            else
            {
                return false;
            }
        }
        
        return true;
    }

    public string ChangePassword(String newPassword)
	{
        _password = newPassword;
        return _password;
    }
    public static string ShowAreas()
    {
        return "";
    }

    
    //Encypt password 
    public string HashPassword(String _password) 
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
                throw new UnauthorizedAccessException("Password is incorrect!"); //If any byte does not match, the password is invalid
                return false;
            }
        Debug.WriteLine("Success"); //If all bytes match, the password is valid

        return true;
    }


}
