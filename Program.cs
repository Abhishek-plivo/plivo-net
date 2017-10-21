using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Plivo.Authentication;
using Plivo.Client;
using Plivo.Resource.Account;
using Plivo.Resource.Subaccount;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plivo.XML;

namespace Plivo
{
    internal class Program
    {   
        public static void Main(string[] args)
        {   
            var api = new PlivoApi("MAODYYYWRMNGZMYTA2YZ", "MjEyOWU5MGVlM2NjZDY1ZTNmZTU2NjZhZGNjMTc5");
            
//            var response = api.Account.Get();
//            response = api.Account.Update(address: "plivo");
//            response = api.Subaccount.Create("naya subaccount 2 Jun");
//            response = api.Subaccount.Get("SAMMU3YJJMNMFLYMJJZT");
//            response = api.Subaccount.List();
//            response = api.Subaccount.Delete("SAM2U2NZM0ZDDKNJUXNZ");
//            var response = api.Subaccount.Update("SAMMU3YJJMNMFLYMJJZT", "subacc 2 Jun", enabled: true);
//            Console.WriteLine(response);
//            var response1 = api.Subaccount.List(offset:1);
//            Console.WriteLine(response1);
//            return;

//            string str = "abcAbc_Abc";
//            Console.WriteLine(string.Concat(str.Select(
//                (x, i) => i > 0 && char.IsUpper(x) ?
//                    "_" + x.ToString().ToLower() : x.ToString())));
//
//            DateTime dt = new DateTime();
//            Console.WriteLine(dt);
//            Console.WriteLine(dt.ToString("yyyy-MM-dd HH':'mm'[:'ss'[.'ffffff']]'"));
//            
//            return;
            
            // application test
//            string appName = RandomString(8);
//            var applicationCreateResponse = api.Application.Create(appName, "http://example.com", "POST");
//            Console.WriteLine("Application created");
//            Console.WriteLine(applicationCreateResponse);
//            Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+");
//            var application = api.Application.Get(applicationCreateResponse.AppId);
//            Console.WriteLine("Application retrieved");
//            Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+");
//            var applicationList = api.Application.List();
//            Console.WriteLine("All Applications retrieved");
//            Console.WriteLine(applicationList);
//            Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+");
//            var applicationUpdateResponse = api.Application.Update(applicationCreateResponse.AppId, "http://example.com", fallbackAnswerUrl:"http://fallbackhere.com");
//            Console.WriteLine("Application updated");
//            Console.WriteLine(applicationUpdateResponse);
//            Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+");
//            var applicationDeleteResponse = api.Application.Delete(applicationCreateResponse.AppId);
//            Console.WriteLine("Application deleted");
//            Console.WriteLine(applicationDeleteResponse);
//            Console.WriteLine("_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+");
//            
//            call test
//            var callCreateResponse = api.Call.Create("+918630879474", new List<string>() {"+919717016918"}, "http://answer.url", "POST");
//            Console.WriteLine("Call created");
//            Console.WriteLine(callCreateResponse);

//            var liveCalls = api.Call.List(limit:12);
//            Console.WriteLine(string.Join(",", liveCalls));
//
//            var liveCall = api.Call.GetLive("a79ffb98-2f3d-4f34-85dc-ae48f500ab7e");
//            Console.WriteLine(liveCall);
            
            
            //endpoint test

//            Console.WriteLine(api.Endpoint.Get("60217047238604").Application);
//            var endpointCreateResponse = api.Endpoint.Create("lllll", "lllll", "lllll");
            var endpointUpdateResponse = api.Endpoint.Update("17127625368177", alias: "aSASDADS");
            Console.WriteLine(endpointUpdateResponse);
//            Console.WriteLine(api.Endpoint.Get("60217047238604").Application);

//            Console.WriteLine(api.Number.Get("60217047238604").Application);

//            var xml = new PlivoXML(new Response());
//            xml.Response.AddDial(new Dictionary<string, string>()
//            {
//                {"method", "POST"},
//                {"action", "http://asada.asdada"}
//            });
//            Console.WriteLine(xml.Response.ToString());

            Console.WriteLine(((SystemHttpClient) api.Client._client).RuntimeVersion);

        }
        
        
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static void CalculateDifference(dynamic obj)
        {
            Console.WriteLine("Calculating difference");
            var diff =
                string.Join("\n",
                    Util.DoCompare(
                        obj.Content,
                        JsonConvert.SerializeObject(obj.Object)));

            if (diff == "")
            {
                Console.WriteLine("No difference :D");
            }
            else
            {
                Console.WriteLine("Differences");
                Console.WriteLine(diff);
            }
        }
        
        public static void Compare(dynamic a)
        {
            if (a.Content == "")
            {
                Console.WriteLine("Empty response content");
                return;
            }
            Console.WriteLine( 
                Util.CompareObjects(
                    JObject.Parse(a.Content),
                    JObject.Parse(
                        JsonConvert.SerializeObject(a.Object))).ToString());
        }
    }
}
