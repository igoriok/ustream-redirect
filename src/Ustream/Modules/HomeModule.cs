using Nancy;

namespace Ustream.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["oauth2/authorize"] = _ => View["Authorize"];
            Get["oauth2/redirect-target"] = RedirectTarget;
            Get["users/self/channels.xml"] = Channels;
            Get["users/self/channels.json"] = ChannelsJson;
            Post["oauth2/authorize"] = Authorize;
        }

        private dynamic Authorize(dynamic ctx)
        {
            return Response.AsRedirect("http://secure-www.ustream.tv/oauth2/redirect-target?access_token=1a446888dfaa921e189479409d638d680dfdbf77&token_type=mac&mac_key=dfc337d39b0941650b67051a622885cb0eb67a51&mac_algorithm=hmac-sha-1&created_at=1310000546&");
        }

        public dynamic RedirectTarget(dynamic ctx)
        {
            return string.Empty;
        }

        public dynamic Channels(dynamic ctx)
        {
            return Response.AsText("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<xml>\r\n<channels>\r\n<array key=\"12345678\">\r\n<id>12345678</id>\r\n<title><![CDATA[mycamera's show]]></title>\r\n<url><![CDATA[http://www.ustream.tv/channel/mycamera-s-show]]></url>\r\n<tiny_url><![CDATA[http://ustre.am/12345]]></tiny_url>\r\n<broadcast_urls>\r\n<array key=\"0\"><![CDATA[rtmp://my-defender-web.tv/ustreamVideo/12345678]]></array>\r\n<array key=\"1\"><![CDATA[rtmp://my-defender-web.tv/ustreamVideo/12345678]]></array>\r\n<array key=\"2\"><![CDATA[rtmp://my-defender-web.tv/ustreamVideo/12345678]]></array>\r\n</broadcast_urls>\r\n<status><![CDATA[offline]]></status>\r\n<description><![CDATA[]]></description>\r\n<owner>\r\n<id>87654321</id>\r\n<username><![CDATA[mycamera]]></username>\r\n<picture><![CDATA[http://my-defender-web.tv]]></picture>\r\n</owner>\r\n<authority>\r\n<reason><![CDATA[own]]></reason>\r\n</authority>\r\n<default>TRUE</default>\r\n</array>\r\n</channels>\r\n<paging>\r\n<actual>\r\n<href><![CDATA[https://api.ustream.tv/users/self/channels.xml?p=1]]></href>\r\n</actual>\r\n</paging>\r\n</xml>", "application/xml");
        }

        public dynamic ChannelsJson(dynamic ctx)
        {
            return Response.AsText("{\"channels\":{\"12345678\":{\"id\":\"12345678\",\"title\":\"mycamera's show\",\"url\":\"http://www.ustream.tv/channel/mycamera-s-show\",\"tiny_url\":\"http://ustre.am/12345\",\"broadcast_urls\":[\"rtmp://my-defender-web.tv/live/my-camera\",\"rtmp://my-defender-web.tv/live/my-camera\",\"rtmp://my-defender-web.tv/live/my-camera\"],\"status\":\"offline\",\"description\":null,\"owner\":{\"id\":\"87654321\",\"username\":\"mycamera\",\"picture\":\"http://my-defender-web.tv\"},\"authority\":{\"reason\":\"own\"},\"default\":true}},\"paging\":{\"actual\":{\"href\":\"http://api.ustream.tv/users/self/channels.json?detail_level=broadcaster&p=1\"}}}", "application/json");
        }
    }
}