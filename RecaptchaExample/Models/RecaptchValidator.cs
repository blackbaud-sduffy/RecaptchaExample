using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Configuration;

namespace RecaptchaExample.Models
{
    public class ReCaptchaValidator
    {
        public static ReCaptchaValidationResult IsValid(string captchaResponse)
        {
            if (string.IsNullOrWhiteSpace(captchaResponse))
            {
                return new ReCaptchaValidationResult() { Success = false };
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");

            var values = new List<KeyValuePair<string, string>>();
            var secret = ConfigurationManager.AppSettings["RecaptureSecret"];
            values.Add(new KeyValuePair<string, string>("secret", secret));
            values.Add(new KeyValuePair<string, string>("response", captchaResponse));

            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("/recaptcha/api/siteverify", content).Result;
            var verificationResponse = response.Content.ReadAsStringAsync().Result;
            var verificationResult = JsonConvert.DeserializeObject<ReCaptchaValidationResult>(verificationResponse);

            return verificationResult;
        }
    }
}