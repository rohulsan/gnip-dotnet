using System;
using Gnip.Client.Resource;
namespace Gnip.Client
{
    interface IGnipConnection
    {

        /// <summary>
        /// Create a Filter on a Publisher on the Gnip server.
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter to create</param>
        /// <exception cref="GnipException">if the Filter already exists, if there were problems authenticating with a Gnip
        /// server, or if another error occurred.</exception>
        Result Create(Publisher publisher, Filter filter);

        /// <summary>
        /// Create a new Publisher on the Gnip server.
        /// </summary>
        /// <param name="publisher">The Publisher to post to the Gnip server.</param>
        /// <returns>The Result</returns>
        /// <exception cref="GnipException">if the Publisher already exists, if there were problems authenticating with a Gnip 
        /// server, or if another error occurred.</exception>
        Result Create(Publisher publisher);

        /// <summary>
        /// Delete an existing Publisher.
        /// </summary>
        /// <param name="publisher">publisher to delete.</param>
        /// <returns>result message object from Gnip server.</returns>
        /// <exception cref="GnipException">if the Publisher does not exist, if there were problems authenticating with a Gnip
        /// server, or if another error occurred.  </exception>
        Result Delete(Publisher publisher);

        /// <summary>
        /// Delete an existing Publisher by publisher scope and publisher name.
        /// </summary>
        /// <param name="publisherType">the publisher scope of publisher to delete.</param>
        /// <param name="publisherName">name of the publisher to delete.</param>
        /// <returns>result message object from Gnip server.</returns>
        /// <exception cref="GnipException">if the Publisher does not exist, if there were problems authenticating with a Gnip
        /// server, or if another error occurred.  </exception>
        Result Delete(PublisherType type, string publisherName);

        /// <summary> Delete the Filter from the Publisher.</summary>
        /// <param name="publisher">the publisher from which to delete the filter]</param>
        /// <param name="filter">the filter to delete]</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">iif the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Delete(Publisher publisher, Filter filter);

        /// <summary> Delete the Filter from the Publisher.</summary>
        /// <param name="publisher">the publisher from which to delete the filter</param>
        /// <param name="filterName">the name of the filter to delete</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Delete(Publisher publisher, string filterName);

        /// <summary> Delete the rule from the Filter associated with the Publisher.</summary>
        /// <param name="publisher">the publisher from which to delete a filter's rule</param>
        /// <param name="filter">the filter from which to remove a rule]</param>
        /// <param name="rule">the rule to remove</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Delete(Publisher publisher, Filter filter, Rule rule);

        /// <summary> Delete the rule from the Filter associated with the Publisher.</summary>
        /// <param name="publisher">the publisher from which to delete a filter's rule</param>
        /// <param name="filterName">the name of the filter from which to remove a rule</param>
        /// <param name="rule">the rule to remove</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Delete(Publisher publisher, string filterName, Rule rule);

        /// <summary> 
        /// Retrieves the Activity data from the given Publisher for the current
        /// activity bucket.  This method expects that the publisher has a public timeline and makes 
        /// full activity data available to the credentials set in the Config instance used to 
        /// configure this GnipConnection. Note, not all Publisher publishers have a timeline 
        /// of activity data; for an up-to-date list of publishers that make such data available, 
        /// check the &lt;a href="https://prod.gnipcentral.com"&gt; Gnip Developer&lt;/a&gt; website.  
        /// Additionally, not all publishers provide access to complete
        /// activity data and instead typically just provide access to notifications.
        /// 
        /// Most Gnip users will need to use GetNotifications(Gnip.Client.Resource.Publisher)
        /// or GetNotifications(Gnip.Client.Resource.Publisher, DateTime) to
        /// get the notifications for a Publisher.
        /// </summary>
        /// <param name="publisher">the publisher whose activities to get</param>
        /// <returns> the Activities model, which contains a set of Activity activities.</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Activities GetActivities(Publisher publisher);

        /// <summary> Retrieves either the notifications or activities from the current bucket for the given Publisher
        /// and Filter based on whether the filter supports full data.  See the Filter class for more information
        /// about whether a Filter supports notifications or activities.Items.  If the Filter supports notifications, the
        /// Activities object returned here will just have activity notifications.
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter whose notifications or activities to retrieve</param>
        /// <returns> the notifications or activities in the current bucket or an empty Activities object if no
        /// notifications or activities were found in the current bucket.
        /// </returns>
        /// <exception cref="GnipException">if the publisher or filter don't exist, if there were problems 
        /// authenticating with the Gnip server, or if another error occurred.</exception>   
        Activities GetActivities(Publisher publisher, Filter filter);

        /// <summary> 
        /// Retrieves either the notifications or activities from the bucket with the given timestamp for the given Publisher
        /// and Filter} based on whether the filter supports full data. See the Filter class for more information
        /// about whether a Filter supports notifications or activities.Items. If the Filter supports notifications, the
        /// Activities} object returned here will just have activity notifications.
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter whose notifications or activities to retrieve</param>
        /// <param name="dateTime">timestamp - DateTime.MinValue insures that activities are
        /// read from the latest bucket. Refer to the TimeCorrection property for more 
        /// information about the dateTime parameter.</param>
        /// <returns> the notifications or activities in the current bucket or an empty Activities object if no
        /// notifications or activities were found in the current bucket.
        /// </returns>
        /// <exception cref="GnipException">if the publisher or filter don't exist, if there were problems 
        /// authenticating with the Gnip server, or if another error occurred.</exception>
        Activities GetActivities(Publisher publisher, Filter filter, DateTime dateTime);

        /// <summary> 
        /// Retrieves the Activity data from the given Publisher for an activity bucket at a given date.
        /// This method expects that publisher the has a public timeline and that the publisher makes
        /// full activity data available to the credentials set in the Config instance used to configure
        /// this GnipConnection.
        /// 
        /// Note, not all Publisher publishers have a timeline of activity data.
        /// 
        /// For an up-to-date list of publishers that have a public timeline, see the
        /// &lt;a href="https://prod.gnipcentral.com"&gt; Gnip Developer&lt;/a&gt; website.
        /// 
        /// Additionally, not all publishers provide access to complete
        /// activity data and instead typically just provide access to notifications.
        /// 
        /// Most Gnip users will need to use GetNotifications(Gnip.Client.Resource.Publisher)
        /// or GetNotifications(Gnip.Client.Resource.Publisher, System.DateTime) to
        /// get the notifications for a Publisher.
        /// 
        /// </summary>
        /// <param name="publisher">the publisher whose activities to get</param>
        /// <param name="dateTime">the timestamp of the activity bucket to retrieve </param>
        /// <returns> the Activities model, which contains a set of Activity activities or an empty
        /// Activities object if no notifications where found in the activity bucket</returns>
        /// <param name="dateTime">timestamp - DateTime.MinValue insures that activities are
        /// read from the latest bucket. Refer to the TimeCorrection property for more 
        /// information about the dateTime parameter.</param>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Activities GetActivities(Publisher publisher, DateTime dateTime);

        /// <summary> Retrieves the Filter named Gnip.Client.Resource.Filter.Name from the 
        /// Publisher named Gnip.Client.Resource.Publisher.Name
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter to retrieve</param>
        /// <returns>the Filter if it exists</returns>
        /// <exception cref="GnipException">if the Filter doesn't exist, if there were problems authenticating with the Gnip server,
        /// or if another error occurred.</exception>
        Filter GetFilter(Publisher publisher, Filter filter);

        /// <summary>
        /// Gets the Filer from the gnip server with publisher name = publisherName and
        /// filter name = filterName.
        /// </summary>
        /// <param name="type">The PublisherType</param>
        /// <param name="publisherName">the name of the publisher</param>
        /// <param name="filterName">the filter to retrieve</param>
        /// <returns>the Filter} if it exists</returns>
        /// <exception cref="GnipException">GnipException if the Filter doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Filter GetFilter(PublisherType type, string publisherName, string filterName);

        /// <summary>
        /// Retrieves the Filter named filterName from the 
        /// Publisher named Gnip.Client.Resource.Publisher.Name
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filterName">the name of the filter to retrieve</param>
        /// <returns>the Filter if it exists</returns>
        /// <exception cref="GnipException">if the Filter doesn't exist, if there were problems authenticating with the Gnip server,
        /// or if another error occurred.</exception>
        Filter GetFilter(Publisher publisher, string filterName);

        /// <summary> Retrieves the Activity notifications from the given Publisher for the current
        /// notification bucket.  This method can be invoked against all publishers that have a public timeline.
        /// For an up-to-date list of publishers that have a public timeline, see the
        /// &lt;a href="https://prod.gnipcentral.com"&t; Gnip Developer%lt;/a&gt; website.
        /// 
        /// Remember, notifications are just that -- notifications that an activity occurred on a Publisher.
        /// A notification does not contain an activity's complete data.  To obtain full activity data,
        /// use a Filter that has Filter.IsFullData = true}.
        /// </summary>
        /// <param name="publisher">the publisher whose notifications to get</param>
        /// <returns>the Activities model, which contains a set of Activity objects, or an empty
        /// Activities object if no notifications were found in the current notification bucket.</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Activities GetNotifications(Publisher publisher);

        /// <summary> Retrieves the Activity notifications from the given Publisher for the notification
        /// bucket at the given date. This method can be invoked against all publishers that have a public timeline.
        /// For an up-to-date list of publishers that have a public timeline, see the
        /// &lt;a href="https://prod.gnipcentral.com"&t; Gnip Developer%lt;/a&gt; website.
        /// 
        /// Remember, notifications are just that -- notifications that an activity occurred on a Publisher.
        /// A notification does not contain an activity's complete data.  To obtain full activity data,
        /// use a Filter that has Filter.IsFullData = true}.
        /// </summary>
        /// <param name="publisher">the publisher whose notifications to get</param>
        /// <param name="dateTime">timestamp -  DateTime.MinValue insures that notifications are
        /// read from the latest bucket. Refer to the TimeCorrection property for more 
        /// information about the dateTime parameter.</param>
        /// <returns> the Activities model, which contains a set of Activity objects, or an empty
        /// Activities object if no notifications were found in the current notification bucket.</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Activities GetNotifications(Publisher publisher, DateTime dateTime);

        /// <summary>
        /// Gets the Publisher with name = publisherName from the gnip server.
        /// </summary>
        /// <param name="type">The PublisherType</param>
        /// <param name="publisherName">name of the publisher to get</param>
        /// <returns> the Publisher if it exists</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were problems authentiating with the Gnip server,
        /// or if another error occurred.</exception>      
        Publisher GetPublisher(PublisherType type, string publisherName);

        /// <summary> 
        /// Retrieves the list of Publishers avaialble from Gnip.
        /// </summary>
        /// <returns>the list of Gnip.Client.Resource.Publishers</returns>
        /// <throws>GnipException if there were problems authenticating with the Gnip server 
        /// or if another error occurred.</throws>
        Publishers GetPublishers(PublisherType type);

        /// <summary>
        /// This method gets a TimeSpan that is the difference between the client time and the server time. 
        /// Adding this timespan to the local machine time should approximate the servers
        /// actual time. This value can then used to adjust times when getting time sensitive data such as
        /// getting activities from buckets. Please see the TimeCorrection property for more information.
        /// </summary>
        TimeSpan GetServerTimeDelta();

        /// <summary>
        /// Publish Activity data in an Activities model to a Publisher. In order to publish
        /// activities, the credentials set in the Config instance associated with this GnipConnection
        /// must own the publisher.
        /// </summary>
        /// <param name="publisher">the publisher to publish activities to</param>
        /// <param name="activities">the activities to publish</param>
        /// <returns>Result, null of there were no activities to publish, otherwise the results of the publish.</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Publish(Publisher publisher, Activities activities);

        /// <summary>
        /// Gets/Sets a TimeSpan that is added to the DateTime passed to 
        /// GetActivities(..., DateTime) abd GetNotifications(..., DateTime). Typically this is 
        /// either set to TimeSpan.Zero (default) or GetServerTimeDelta(). 
        /// 
        /// When activities are published to date buckets, they are published accoring to
        /// the gnip server GMT time. Thus, when passing a client generated dateTime as a parameter to
        /// the methods mentioned above, you may not get expected results if your client time is 
        /// different than that of the server, which it likely is. For instance, say you want all the
        /// activities published one minute ago. you would get the current time and subtract one minute.
        /// However, that time is likely to be, at the very least, a little different than the server 
        /// time. You have two options to adjust that time. you can add the results of GetServerTimeDelta()
        /// to the local time, or you can set TimeCorrection to GetServerTimeDelta() and the GnipConnection
        /// will automatically add that TimeSpan to the the dateTime passed to the GetActivities and 
        /// GetNotifications methods.
        /// </summary>
        TimeSpan TimeCorrection { get; set; }

        /// <summary> Update the Filter by adding a single rule to it. </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filterName">the filter to update</param>
        /// <param name="rule">the rule to add to the filter</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">GnipException if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Update(Publisher publisher, string filterName, Rule rule);

        /// <summary> Update the Filter by adding a single rule to it. </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter to update</param>
        /// <param name="rule">the rule to add to the filter</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Update(Publisher publisher, Filter filter, Rule rule);

        /// <summary>
        /// Update the Filter by bulk adding a Rules document containing many Rules.
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter to update]</param>
        /// <param name="rules">the set of rules to add to the filter</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Update(Publisher publisher, Filter filter, Rules rules);

        /// <summary> 
        /// Update the Filter associated with the Publisher. As practiced in the
        /// REST style, this call updates the represetntation of the given Filter 
        /// by replacing the one that already exists. It does not do a merge of the two 
        /// Filter documents.
        /// 
        /// To do incremental updates of a Filter, use
        /// Update(com.Gnip.Client.Resource.Publisher, com.Gnip.Client.Resource.Filter, com.Gnip.Client.Resource.Rule)} or
        /// Update(com.Gnip.Client.Resource.Publisher, com.Gnip.Client.Resource.Filter, com.Gnip.Client.Resource.Rules)} to
        /// add rules to an existing Filter. 
        /// </summary>
        /// 
        /// <param name="publisher">the publisher that owns the filter</param>
        /// <param name="filter">the filter to update</param>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Update(Publisher publisher, Filter filter);

        /// <summary>
        /// Update a Publisher on the Gnip server.
        /// </summary>
        /// <param name="publisher">The Publisher to update.</param>
        /// <returns>The Result</returns>
        /// <exception cref="GnipException">GnipException if the Publisher does not already exists, if there were problems authenticating with a Gnip 
        /// server, or if another error occurred.</exception>
        Result Update(Publisher publisher);

        /// <summary>
        /// Update the Filter by bulk adding a Rules document containing many Rules.
        /// </summary>
        /// <param name="publisher">the publisher that owns the filter]</param>
        /// <param name="filterName">the filter to update]</param>
        /// <param name="rules">the set of rules to add to the filter]</param>
        /// <returns>The Result of the Update</returns>
        /// <exception cref="GnipException">if the publisher doesn't exist, if there were 
        /// problems authenticating with the Gnip server, or if another error occurred.</exception>
        Result Update(Publisher publisher, string filterName, Rules rules);
    }
}
