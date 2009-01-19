Welcome to the Gnip .NET convenience library!

== Overview ==

This library provides a .NET API for accessing Gnip web services. This 
library supports activities related to publishing and subscribing to data.

== Dependencies == 
Dependency Urls coppied into the Gnip.Lib folder.

= Required Dependencies =
  - .NET 3.0
  - A user account on Gnip http://gnipcentral.com/
  - log4net - Copy the log4net.dll and log4net.xml to the 
    Gnip.Lib/log4net-<version> folder. If you are using a 
    different version of log4net, you may want to create a 
    new folder using the version number and change your 
    dependency on the Gnip.Client and Gnip.Test project to 
    point to the appropriate version.

= Test Dependencies =
  - NUnit - Copy the nunit.framework.dll to the Gnip.Lib/NUnit-<version>
    folder. If you are using a different version of NUnit, you may
    want to create a new folder using the version number and change
    your dependency on the Gnip.Test project to point to the 
    appropriate version.

Resource links for the above dependencies can be found here:
  http://logging.apache.org/log4net/
  http://www.nunit.org/

== Quick Start ==
Gnip has a test publisher "gnip-test-publisher":
https://prod.gnipcentral.com/publishers/gnip-test-publisher/notification/

The following example get the twitter publisher:

    Config config = new Config("<username>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "twitter");
    Console.WriteLine("Got Publisher: " + publisher.Name);

The following example retrieves notification data from the current bucket for 
twitteryour own publisher. Please note that the current bucket is not static and 
therefore will contain a variable amount of data, but you'll get quick feedback 
to know if you can connect and access the public notification data.

    Config config = new Config("<username>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.My, "valid_publisher");
    Activities activities = gnip.GetActivities(publisher);
    foreach(Activity activity in activities.Items)
    {
        Console.WriteLine("Twitter Activity Actors for activity at " + activity.At + ": ");
        foreach(Actor actor in activity.Actors)
        {
            Console.WriteLine("Actor: " + actor.Value);
        }
    }
	
You should see an array of objects printed.

== Installing ==

= WINDOWS w/ VisualStudio

Open the Gnip.Client.sln in VisualStudio and perform a build. The dll will be in 
Gnip.Client/bin/[Debug | Release].

= Debugging =
The Gnip .NET library uses the log4net Logger to send messages to the logs. By default
the library does not log. However, the logger is configured to run with the Gnip.ClientTest
NUnit test. To config the logger in your app include the line:

      XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo("[path to log config file]"));

A sample log config file exists in Gnip.ClientTest/gnip.log4net.xml. 
see http://loggin.apache.org/log4net for more.

= Unit Tests Tips =
NUnit tests are set up for these libraries. There are unit tests and 
integration tests. They live in Gnip.ClientTest. The parameters for the
tests are setup in Gnip.ClientTest/App.config. You will want to create
a producer at http://prod.gnipcentral.com/. Under 'your publishers' select
'create a new one'. Then use the pubisher name in the app.config file.
an file names Gnip.ClientTest/gnip.nuit exists for the nunit client application.

The tests in Gnip.ClientTest/Resources and Gnip.ClientTest/Util are unit tests for
the object model objects and utilities. They do not connect to a server, nor do they 
require the App.config. The tests in Gnip.ClientTest/*Test.cs are integration
tests.

==== Subscriber Actions ====

=== Notification vs. Activity ===

As a subscriber you can retrieve notification data or activity data. The main 
difference between these two types of data buckets are:

*** Notifications contain a reduced meta-data subset of attributes for an activity.
*** Activities contain full data, including the raw payload. There are some 
    restrictions on activity data. You can only request unfiltered activities 
    on publishers that you own (have authorization access to). You can create 
    filters on any publisher and request activity data.

Both notification data and activity data are delivered in the Activity object.

=== Example 1: Retrieve notifications from a publisher ===

As a consumer one thing you might be interested in immediately is
grabbing data from a publisher. To do this you must create a connection to Gnip 
using your username and password.  Once the connection is established you can 
get the publisher and request the stream. These examples uses the publisher 
"gnip-test-publisher".

*** Notification data stream request ***

    Config config = new Config("<username>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Console.WriteLine("Got Publisher: " + publisher.Name);
    Activities activities = gnip.GetNotifications(publisher);
    foreach(Activity activity in activities.Items)
    {
        Console.WriteLine("Twitter Activity Actors for activity at " + activity.At + ": ");
        foreach(Actor actor in activity.Actors)
        {
            Console.WriteLine("Actor: " + actor.Value);
        }
    }

You can also view the current notifications bucket via web on the Gnip site:
    https://prod.gnipcentral.com/publishers/gnip-test-publisher/notification/current.xml
	
	
*** Notification data stream request with optional date param ***

    Config config = new Config("<user>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Console.WriteLine("Got Publisher: " + publisher.Name);
    Activities activities = gnip.GetNotifications(publisher, DateTime.Now);
    foreach(Activity activity in activities.Items)
    {
        Console.WriteLine("Twitter Activity Actors for activity at " + activity.At + ": ");
        foreach(Actor actor in activity.Actors)
        {
            Console.WriteLine("Actor: " + actor.Value);
        }
    }

You can see the running list of notification buckets on the Gnip site:
    https://prod.gnipcentral.com/gnip/publishers/gnip-test-publisher/notification/
	
=== Example 2: Filter notifications or activities by a set of users ===

You can create a filter to stream activity data for the users you care about. 
Posts from the users that have already occurred will not be included in a 
filter. Therefore any new filter you create will be empty until the users you 
specify perform an action (make a tweet, digg a story, create a bookmark in 
delicious, etc.). 

You can only retrieve activity data (full data) from publishers that you don't own 
by creating a filter.

The test actor for "gnip-test-publisher" is "joeblow". To test your filter, be sure 
"joeblow"appears in your rule set.

The following examples illustrate creating filters for both notification and activity 
data. Additionally, the two examples show how to use/not use the post URL parameter.

*** Notificiation Filter without POST URL ***

Note that the full data (second parameter) of the filter object must be set to 
false. This example does not include a POST Url, meaning you'll have to poll 
Gnip for the results when you need them. The following snippet creates (and 
retrieves) a notification filter called "myNotificationFilter" on the publisher 
gnip-test-publisher.

    Config config = new Config("<user>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Console.WriteLine("Got Publisher: " + publisher.Name);

    Filter filter = new Filter("myNotificationFilter");
    filter.Rules.Add(new Rule(RuleType.Actor, "joeblow"));
    filter.Rules.Add(new Rule(RuleType.Actor, "janeblow"));

    gnip.Create(publisher, filter);

    Activities activities = gnip.GetActivities(publisher, filter);
    foreach(Activity activity in activities.Items)
    {
        Console.WriteLine("Activity Actors for activity at " + activity.At + ": ");
        foreach(Actor actor in activity.Actors)
        {
            Console.WriteLine("Actor: " + actor.Value);
        }
    }

You can viewget your filters by running:
    Filter filter = gnip.GetFilter(publisher, "myNotificationFilter");

Your actors list should be (not necessarily in this order): joeblow, janeblow

You can also see your filters list for each publisher by going to the Gnip site:
    https://prod.gnipcentral.com/gnip/publishers/gnip-test-publisher/filters
	
You can view notification buckets on the Gnip site by going to:
    https://prod.gnipcentral.com/gnip/publishers/gnip-test-publisher/filters/myNotificationFilter/notification
	
*** Activity Filter with POST URL ***

Note that the full data of the filter object must be set to 
true to view activity data. This example includes the optional POST Url, 
meaning Gnip will POST via an HTTP HEAD request to this URL. The following 
snippet creates (and gets) a notification filter called "myActivityFilter" on 
the publisher gnip-test-publisher. 

If you want notifications to be sent to a script on your server for processing, 
you must ensure that the PostUrl Property you set responds successfully to an 
HTTP HEAD request. (note that this example will throw an error because the POST 
url is invalid).

    Config config = new Config("<username>", "<password>");
    GnipConnection gnip = new GnipConnection(config);
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");

    Filter filter = new Filter("myActivityFilter", "http://mysite.com/processingscript.php", true);
    filter.Rules.Add(new Rule(RuleType.Actor, "joeblow"));
    filter.Rules.Add(new Rule(RuleType.Actor, "janeblow"));

    gnip.Create(publisher, filter);

You can view your filters by running:
      Filter filter = gnip.GetFilter(publisher, "myActivityFilter");

You can see your filters by going to the Gnip site:
	https://prod.gnipcentral.com/gnip/publishers/gnip-test-publisher/filters
Your actors list should be (not necessarily in this order): joeblow, janeblow

Once data is available, you can see it here:
	https://review.gnipcentral.com/gnip/publishers/gnip-test-publisher/activity
	
=== Example 3: Add rules to an existing filter ===

You can add rules later to an existing filter. The following code snippet adds 
two new rules to the filter we created above, myNotificationFilter:

    Config config = new Config("<username>", "<password>");   
    GnipConnection gnip = new GnipConnection(config); 
    Filter filter = gnip.GetFilter(publisher, "myActivityFilter");
    filter.Rules.Add(new Rule(RuleType.Actor, "spotblow"));
    gnip.Update(publisher, filter);
    filter = gnip.GetFilter(publisher, "myActivityFilter");

You should see the following actors in filter: joeblow, janblow, spotblow

=== Example 4: Delete a filter ===

Filters can be easily deleted. The following code sample deletes the filter 
that was created above:

    Config config = new Config("<username>", "<password>");   
    GnipConnection gnip = new GnipConnection(config); 
    Filter filter = gnip.GetFilter(publisher, "myActivityFilter");
    Result result = gnip.Delete(publisher, filter);
	
You should get a responseresult.Message message of "Success".
	
=== Example 5: Retrieve activities from a publisher ===

*** Activity Data Stream Request ***

NOTE: You must create a filter (see Example 2 above) before you can view 
activities for a publisher that you do not own.

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Activities activities = gnip.GetActivities(publisher);

You can also view the current activity bucket via web on the Gnip site:
    https://prod.gnipcentral.com/gnip/publishers/gnip-test-publisher/activity/current.xml

*** Activity Data Stream Request with Date Param ***

NOTE: You must create a filter (see Example 3 below) before you can view 
activities for a publisher that you do not own.

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Activities activities = gnip.GetActivities(publisher, DateTime.Now);

You can see the running list of activity buckets on the Gnip site:
    https://prod.gnipcentral.com/publishers/gnip-test-publisher/activity/

=== Example 6: Add rules in large batches ===

Adding rules in large batches is the fastest way to augment an existing Filter, and 
for Filters that already contain large rule sets, batch additions must be used to 
change the Filter.  Here's an example of a batch add:

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher publisher = gnip.GetPublisher(PublisherType.Gnip, "gnip-test-publisher");
    Rules rules = new Rules();
    rules.Items.Add(new Rule(RuleType.Actor, "fluffyblow"));
    rules.Items.Add(new Rule(RuleType.Actor, "roverblow"));
    rules.Items.Add(new Rule(RuleType.Actor, "goldieblow"));
    Result result = gnip.Update(publisher, "myActivityFilter", rules);

If the server receives the message successfully, you should receive an HTTP response code 
of 200 and a result.Message of "Success".  Note, Gnip processes rule addition asynchronously, so 
there may be a delay beteween completion of the request and Gnip's finishing adding rules
to the Filter.  
	
If you like, you can delete the rule:
    // poor fluffy.
    Result result = gnip.Delete(publisher, "myActivityFilter", new Rule(RuleType.Actor, "fluffyblow"));
	
You should see a response message of "Success".

==== Publisher Actions ====

In order to utilize the publisher API, you must first create a publisher. The 
publisher name should be descriptive to you, especially if you want it to be 
publicly available. For instance, the publisher name for Digg is "digg". For 
now, publishers cannot be deleted once they are created, so be mindful when 
naming and testing your publishers.

Publishers must have one or more rule types specified so that filters can be 
created based on the rule types. The following rule types are supported by 
Gnip:
	Actor 
	To
	Regarding
	Source
	Tag
	
=== Example 1: Create a publisher

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher pub = new Publisher(
                PublisherType.My,
                "myPublisher",
                RuleType.Actor,
                RuleType.Regarding,
                RuleType.Source,
                RuleType.Tag,
                RuleType.To);

    gnip.Create(publisher);	
	
You should see a response message of "Success".

=== Example 2: Updating a publisher

The following example takes an existing publisher and updates it with a new set 
of supported rule types.

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher publisher = gnipConnection.GetPublisher(PublisherType.My, "myPublisher");
    publisher.SupportedRuleTypes = new List<RuleType>() { RuleType.Actor };
    Result result = gnipConnection.Update(publisher);
	
    You should see a response result.Message of "Success".

=== Example 3: Publishing activities

Here is how you can publish activities to the activity stream:

    GnipConnection gnip = new GnipConnection(new Config("<username>", "<password>"));
    Publisher publisher = gnipConnection.GetPublisher(PublisherType.My, "myPublisher");

    Activities activities = new Activities();
    activities.Items.Add(new Activity(new Actor("joeblow"), "update1"));
    activities.Items.Add(new Activity(new Actor("tomblow"), "update2"));
    activities.Items.Add(new Activity(new Actor("janeblow"), "update3"));

    Result result = gnipConnection.Publish(publisher, activities);

You should see a result.Message of "Success".