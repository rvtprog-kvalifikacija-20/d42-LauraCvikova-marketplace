using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace MarketplaceV1.Models
{
    public class IGUser
    {
        public string data;

        public IGUser(string name)
        {
            string path = @"C:\Users\User\source\repos\MarketplaceV1\MarketplaceV1\cache\" + name+".txt";
            string json = string.Empty;
            try
            {
                json = System.IO.File.ReadAllText(path);
                if (json.Length < 2)
                {
                    throw new Exception("Empty file");
                }
            }
            catch
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.instagram.com/" + name + "/?__a=1");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            System.IO.File.WriteAllText(path, json);
            this.data = json;
            
        }
        public string GetImage()
        {
            var inst = JsonConvert.DeserializeObject<dynamic>(this.data);
            return "<img src=\"" + inst.SelectToken("graphql.user.profile_pic_url_hd").ToString() + "\">";
        }

        public string GetName()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.data).SelectToken("graphql.user.full_name").ToString();
        }

        public string GetSubscribers()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.data).SelectToken("graphql.user.edge_followed_by.count").ToString();
        }

        public string GetBio()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.data).SelectToken("graphql.user.biography").ToString();
        }

    }
}