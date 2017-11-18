using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace EventStoreNugetUsage
{
    public class EventStoreNugetSamples
    {
        public void ConnectWriteRead()
        {
            var connection =
                EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            // Don’t forget to tell the connection to connect!
            connection.ConnectAsync().Wait();

            var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
                Encoding.UTF8.GetBytes("some data1"),
                Encoding.UTF8.GetBytes("some metadata1"));

            connection.AppendToStreamAsync("test-stream",
                ExpectedVersion.Any, myEvent).Wait();

            var streamEvents =
                connection.ReadStreamEventsForwardAsync("test-stream", 0, 2, false).Result;

            var returnedEvent = streamEvents.Events[0].Event;

            Debug.WriteLine("Read event with data: {0}, metadata: {1}",
                Encoding.UTF8.GetString(returnedEvent.Data),
                Encoding.UTF8.GetString(returnedEvent.Metadata));
        }
    }
}
