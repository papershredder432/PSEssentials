using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;

namespace papershredder432.PSEssentials
{
    public class WebhookManager
    {

        public class Embed
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string Color { get; set; }
            public bool SendTimestamp { get; set; }
            public Footer Footer { get; set; }
            public Author Author { get; set; }
            public Field[] Fields { get; set; }
            public string Thumbnail { get; set; }
            public string Image { get; set; }
        }
        public class Footer
        {
            public string Text { get; set; }
            public string Icon_url { get; set; }
        }
        public class Author
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Icon_url { get; set; }
        }
        public class Field
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public bool Inline { get; set; }
        }

        [JsonObject]
        public class WebhookMessage
        {
            public string username;

            public string avatar_url;

            public Embed[] embeds;
        }

        public class Webhook
        {
            public void SendEmbed(Embed embed, string name, string webhookurl)
            {
                WebhookMessage webhookMessage = new WebhookMessage();
                webhookMessage.username = name;
                webhookMessage.embeds = new Embed[1]
                {
                    embed
                };
                SendHook(webhookMessage, webhookurl);
            }

            public void SendHook(WebhookMessage embed, string webhookUrl)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(embed));
                using (WebClient webClient = new WebClient())
                {
                    WebHeaderCollection headers = webClient.Headers;
                    headers.Set(HttpRequestHeader.ContentType, "application/json");
                    webClient.UploadData(webhookUrl, bytes);
                }
            }
        }
    }
}
