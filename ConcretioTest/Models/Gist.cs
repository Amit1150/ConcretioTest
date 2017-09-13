using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcretioTest.Models
{
    public class Gist
    {
        public string url { get; set; }
        public string forks_url { get; set; }
        public string commits_url { get; set; }
        public string id { get; set; }
        public string git_pull_url { get; set; }
        public string git_push_url { get; set; }
        public string html_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string description { get; set; }
        public int comments { get; set; }
        public string comments_url { get; set; }
        public Owner owner { get; set; }
        public bool truncated { get; set; }
        [JsonProperty("public")]
        public bool IsPublic{get;set;}
    }

    public class Owner
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
}