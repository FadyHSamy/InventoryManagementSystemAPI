using InventoryManagementSystem.Core.Entities.Shared;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Utilities.Helpers
{
    public static class Helpers
    {
        public static bool IsValidJson(this string text)
        {
            text = text.Trim();
            if ((text.StartsWith("{") && text.EndsWith("}")) || //For object
                (text.StartsWith("[") && text.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(text);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        public static bool IsEmptyString(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static string HashPassword(string plainPassword)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return passwordHash;
        }
        public static bool VerifyHashPassword(string userPassword , string databaseUserPassword)
        {
            var valid = BCrypt.Net.BCrypt.Verify(userPassword,databaseUserPassword);
            return valid;
        }

    }
}
