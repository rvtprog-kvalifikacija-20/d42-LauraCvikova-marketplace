using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace MarketplaceV1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "text")]
        public string Data { get; set; }
        public string Nickname { get; set; }

        public DateTime? DataTime { get; set; }

        public bool IsBlogger { get; set; }


        public string GetImage()
        {
            var inst = JsonConvert.DeserializeObject<dynamic>(this.Data);
            return "<img src=\"" + inst.SelectToken("graphql.user.profile_pic_url_hd").ToString() + "\">";
        }
        public string GetImageUrl()
        {
            var inst = JsonConvert.DeserializeObject<dynamic>(this.Data);
            return  inst.SelectToken("graphql.user.profile_pic_url_hd").ToString() ;
        }
        public string GetName()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.Data).SelectToken("graphql.user.full_name").ToString();
        }

        public string GetSubscribers()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.Data).SelectToken("graphql.user.edge_followed_by.count").ToString();
        }

        public string GetBio()
        {
            return JsonConvert.DeserializeObject<dynamic>(this.Data).SelectToken("graphql.user.biography").ToString();
        }


        public void BecameBlogger(string nickname)
        {
            this.Data = GetInstaData(nickname);
            if(this.Data.Length > 10)
            {
                this.Nickname = nickname; 
                this.IsBlogger = true;
                this.DataTime = DateTime.Now;
            }
            else
            {
                this.IsBlogger = false;
                this.DataTime = DateTime.Now;
            }
        }

        public string GetInstaData(string name)
        {
            string path = HostingEnvironment.MapPath("~/cache/" + name + ".txt"); ;
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
            

            return json;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    
}