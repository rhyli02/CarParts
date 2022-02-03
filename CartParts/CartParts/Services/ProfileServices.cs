using CartParts.Models;
using CartParts.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CartParts.Services
{
    public class ProfileServices : IProfileServices
    {
        private string Baseurl = "https://jsonplaceholder.typicode.com/";

      

        public async Task<IEnumerable<Profile>> GetAllProfiles()
        {
            List<Profile> profiles = new List<Profile>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("/users");
                if (Res.IsSuccessStatusCode)
                {
                    var profileResponse = Res.Content.ReadAsStringAsync().Result;
                    profiles = JsonConvert.DeserializeObject<List<Profile>>(profileResponse);
                }
            }
            return profiles;


        }

        public async Task<Profile> GetProfileById(int id)
        {
            var profiles = await GetAllProfiles();
            return (from item in profiles
                    where item.id.Equals(id)
                    select item) .SingleOrDefault();
        }
    }
}
