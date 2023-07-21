using Newtonsoft.Json;
using Pro131_Nhom4.Data;
using System.Security.Principal;

namespace App_client.Services
{
    public class SessionServices
    {
        public static User GetAccountFromSession(ISession session, string key)
        {
            //Bước 1: lấy string data từ session ở dạng json
            string jsonData = session.GetString(key);
            if (jsonData == null) return new User() { Status = 404 };
            //Nếu dữ liệu null tạo mới 1 list rỗng
            //Bước 2: Convert về list
            var user = JsonConvert.DeserializeObject<User>(jsonData);
            return user;
        }
        //Ghi dữ liệu từ 1 list vào session
        public static void SetAccountToSession(ISession session, string key, object values)
        {
            var jsonData = JsonConvert.SerializeObject(values);
            session.SetString(key, jsonData);
        }
    }
}
