using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.ServiceModel.Syndication;
using EventStoreNugetUsage;

namespace EventStoreSamples
{
    public partial class Form1 : Form
    {
        private Uri _last;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnPostMessage_Click(object sender, EventArgs e)
        {
            var message = "[{'eventType':'MyFirstEvent', 'eventId':'"
                          + Guid.NewGuid() + "', 'data':{'name':'hello world!', 'number':"
                          + new Random().Next() + "}}]";
            var request = WebRequest.Create("http://127.0.0.1:2113/streams/yourstream");
            request.Method = "POST";
            request.ContentType = "application/vnd.eventstore.events+json";
            request.ContentLength = message.Length;
            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(message);
            }
            using (var response = request.GetResponse())
            {
                response.Close();
            }
        }

        private void btnGetLast_Click(object sender, EventArgs e)
        {
            this._last = GetLast(new Uri("http://127.0.0.1:2113/streams/yourstream"));
        }

        private static SyndicationLink GetNamedLink(IEnumerable<SyndicationLink> links, string name)
        {
            return links.FirstOrDefault(link => link.RelationshipType == name);
        }

        private static void ProcessItem(SyndicationItem item)
        {
            Console.WriteLine(item.Title.Text);
            //get events
            var request = (HttpWebRequest)WebRequest.Create(GetNamedLink(item.Links, "alternate").Uri);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/json";
            using (var response = request.GetResponse())
            {
                var streamReader = new StreamReader(response.GetResponseStream());
                MessageBox.Show(streamReader.ReadToEnd());
                //Console.WriteLine(streamReader.ReadToEnd());
            }
        }

        private static Uri ReadPrevious(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/atom+xml";
            using (var response = request.GetResponse())
            {
                using (var xmlreader = XmlReader.Create(response.GetResponseStream()))
                {
                    var feed = SyndicationFeed.Load(xmlreader);
                    foreach (var item in feed.Items.Reverse())
                    {
                        ProcessItem(item);
                    }
                    var prev = GetNamedLink(feed.Links, "previous");
                    return prev == null ? uri : prev.Uri;
                }
            }
        }

        static Uri GetLast(Uri head)
        {
            var request = (HttpWebRequest)WebRequest.Create(head);
            request.Credentials = new NetworkCredential("admin", "changeit");
            request.Accept = "application/atom+xml";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        return null;
                    using (var xmlreader = XmlReader.Create(response.GetResponseStream()))
                    {
                        var feed = SyndicationFeed.Load(xmlreader);
                        var last = GetNamedLink(feed.Links, "last");
                        return (last != null) ? last.Uri : GetNamedLink(feed.Links, "self").Uri;
                    }
                }
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound) return null;
                throw;
            }
        }

        private void btnGetPrevious_Click(object sender, EventArgs e)
        {
            var current = ReadPrevious(this._last);
        }

        private void btnNugetWriteRead_Click(object sender, EventArgs e)
        {
            var samples = new EventStoreNugetSamples();
            samples.ConnectWriteRead();
        }
    }
}
