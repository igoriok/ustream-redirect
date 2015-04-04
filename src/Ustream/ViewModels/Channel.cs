using System;

namespace Ustream.ViewModels
{
    public class Channel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public Uri Url { get; set; }

        public Uri TiniUrl { get; set; }

        public Uri[] BroadcastUrls { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public User Owner { get; set; }

        public Authority Authority { get; set; }

        public bool IsDefault { get; set; }
    }
}