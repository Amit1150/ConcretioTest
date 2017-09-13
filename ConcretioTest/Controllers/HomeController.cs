using ConcretioTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ConcretioTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GitLogin()
        {

            var rootUrl = (Request.Url.GetLeftPart(UriPartial.Authority) + (Request.ApplicationPath == "/" ? Request.ApplicationPath : Request.ApplicationPath + "/") + "home/gitlogin").ToLower();

            GitRequest gitRequest = new GitRequest
            {
                Code = Request.QueryString["code"],
                RedirectUri = rootUrl,
                ClientId = ConfigurationManager.AppSettings["ClientID"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"]
            };
            var data = JsonConvert.SerializeObject(gitRequest);

            var response = ExecutePost("https://github.com/login/oauth/access_token", data);

            if (response != null)
            {
                var gitResponse = JsonConvert.DeserializeObject<GitResponse>(response);
                HttpCookie accessTokenCookie = new HttpCookie("git_access_token");
                accessTokenCookie.Value = gitResponse.AccessToken;
                accessTokenCookie.Expires = DateTime.Now.AddDays(2);
                HttpContext.Response.Cookies.Add(accessTokenCookie);
                ViewBag.message = "You have been logged in.";
                return RedirectToAction("MyProfile");
            }

            ViewBag.message = "Login with github failed.";
            return RedirectToAction("Index");
        }

        public ActionResult MyProfile()
        {
            var gitToken = Request.Cookies.Get("git_access_token");
            if (gitToken != null)
            {
                var gists = GetUserGist();
                return View(gists);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        private List<Gist> GetUserGist()
        {
            List<Gist> gists = new List<Gist>();
            try
            {
                var token = Request.Cookies.Get("git_access_token").Value;
                Dictionary<string,string> headers = new Dictionary<string,string>();
                headers.Add("Authorization","bearer "+token);
                var getGists = ExecuteGet("https://api.github.com/gists", headers);
                gists = JsonConvert.DeserializeObject<List<Gist>>(getGists);
                return gists;
            }
            catch
            {
             
            }
            return gists;
        }

        private string ExecutePost(string url, string data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
            if (data != null)
            {
                using (var sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(data);
                    sw.Flush();
                }
            }
            var response = request.GetResponse();
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                string responseText = sr.ReadToEnd();
                return responseText;
            }
        }

        public string ExecuteGet(string url, Dictionary<string, string> headers = null)
        {

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = ConfigurationManager.AppSettings["GitApp"];
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            request.ContentType = "application/json";
            request.Accept = "application/json";

            var response = request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                string responseText = sr.ReadToEnd();
                return responseText;
            }

        }
    }
}