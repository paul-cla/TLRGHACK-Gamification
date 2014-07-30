using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Keywords.Domain;

namespace Keywords.API.SmokeTests
{
    [TestFixture]
    public class SmokeTest
    {
        [TestCase("http://{0}/keyword/1/Test", Schemas.Keywords, typeof(Keyword))]
        [TestCase("http://{0}/keyword/1", Schemas.Keywords, typeof(Keyword))]
        [TestCase("http://{0}/region/1/Test", Schemas.Regions, typeof(Region))]
        [TestCase("http://{0}/region/1", Schemas.Regions, typeof(Region))]
        public void KeywordUrls(string url, string schema, Type t)
        {
            var body = GetWebResponse(url, new List<object>());

            body.Should().BeValidJson(JsonSchema.Parse(schema));
            JsonConvert.DeserializeObject(body, t).Should().NotBeNull();
        }

        [TestCase("http://{0}/foo/baa", Schemas.Error, typeof(Keyword))]
        public void ErrorUrls(string url, string schema, Type t)
        {
            var body = GetWebResponse(url, new List<object>());

            body.Should().BeValidJson(JsonSchema.Parse(schema));
            JsonConvert.DeserializeObject(body, t).Should().NotBeNull();
        }

        private static string GetWebResponse(string url, IEnumerable<object> queryStringArgs)
        {
            HttpWebRequest request = CreateHttpRequest(url, queryStringArgs);

            string body;

            try
            {
                HttpWebResponse response;
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    body = response.GetBody();
                }
            }
            catch (WebException ex)
            {
                body = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }

            return body;
        }

        private static HttpWebRequest CreateHttpRequest(string url, IEnumerable<object> queryStringArgs = null)
        {
            var args = new List<object>();
            var hostname = ConfigurationManager.AppSettings["hostname"];
            args.Add(hostname);

            if (queryStringArgs != null)
                args.AddRange(queryStringArgs);

            var requestUri = string.Format(url, args.ToArray());

            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.Proxy = null;

            return request;
        }
    }

    public static class AssertionExtensions
    {
        public static string GetBody(this HttpWebResponse response)
        {
            using (var receiveStream = response.GetResponseStream())
            using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
            {

                return readStream.ReadToEnd();
            }
        }

        public static void BeValidJson(this FluentAssertions.Primitives.StringAssertions assertions, JsonSchema schema, string reason = "", params object[] reasonArgs)
        {
            IList<string> errors;
            Execute.Verification
               .ForCondition(JObject.Parse(assertions.Subject).IsValid(schema, out errors))
               .BecauseOf(reason, reasonArgs)
               .FailWith("Expected object to be valid json {0}{reason}", errors);
        }
    }

    public class Schemas
    {
        public const string Keywords = @"
			{
				'type':'object',
				'required':true,
				'properties':{
					'id': {
						'type':'number',
						'required':true
					},
                    'KeywordText': {
                        'type':'string',
						'required':true
                    },
                    'AreaName': {
                        'type':'string',
						'required':true
                    },
                    'Country': {
                        'type':'string',
						'required':true
                    },
                    'AreaID': {
                        'type':'number',
						'required':true
                    },
                    'FriendlyText': {
                        'type':'string',
						'required':true
                    }
				}
			}";
        
        public const string Regions = @"
			{
				'type':'object',
				'required':true,
				'properties':{
					'id': {
						'type':'number',
						'required':true
					},
                    'RegionText': {
                        'type':'string',
						'required':true
                    },
                    'FriendlyText': {
                        'type':'string',
						'required':true
                    }
				}
			}";
        public const string Error = @"
			{
				'type':'object',
				'required':true,
				'properties':{
					'error': {
						'type':'object',
						'required':true,
						'properties':{
							'code': {
								'type':'number',
								'required':true
							},
							'message': {
								'type':'string',
								'required':true
							}
						}
					}
				}
			}";
    }
}