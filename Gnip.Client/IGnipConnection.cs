using System;
namespace Gnip.Client
{
    interface IGnipConnection
    {
        Gnip.Client.Resource.Result Create(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter);
        Gnip.Client.Resource.Result Create(Gnip.Client.Resource.Publisher publisher);
        Gnip.Client.Resource.Result Delete(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter);
        Gnip.Client.Resource.Result Delete(Gnip.Client.Resource.Publisher publisher, string filterName);
        Gnip.Client.Resource.Result Delete(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter, Gnip.Client.Resource.Rule rule);
        Gnip.Client.Resource.Result Delete(Gnip.Client.Resource.Publisher publisher, string filterName, Gnip.Client.Resource.Rule rule);
        Gnip.Client.Resource.Activities GetActivities(Gnip.Client.Resource.Publisher publisher);
        Gnip.Client.Resource.Activities GetActivities(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter);
        Gnip.Client.Resource.Activities GetActivities(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter, DateTime dateTime);
        Gnip.Client.Resource.Activities GetActivities(Gnip.Client.Resource.Publisher publisher, DateTime dateTime);
        Gnip.Client.Resource.Filter GetFilter(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter);
        Gnip.Client.Resource.Filter GetFilter(Gnip.Client.Resource.PublisherType type, string publisherName, string filterName);
        Gnip.Client.Resource.Filter GetFilter(Gnip.Client.Resource.Publisher publisher, string filterName);
        Gnip.Client.Resource.Activities GetNotifications(Gnip.Client.Resource.Publisher publisher);
        Gnip.Client.Resource.Activities GetNotifications(Gnip.Client.Resource.Publisher publisher, DateTime dateTime);
        Gnip.Client.Resource.Publisher GetPublisher(Gnip.Client.Resource.PublisherType type, string publisherName);
        Gnip.Client.Resource.Publishers GetPublishers(Gnip.Client.Resource.PublisherType type);
        TimeSpan GetServerTimeDelta();
        Gnip.Client.Resource.Result Publish(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Activities activities);
        TimeSpan TimeCorrection { get; set; }
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher, string filterName, Gnip.Client.Resource.Rule rule);
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter, Gnip.Client.Resource.Rule rule);
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter, Gnip.Client.Resource.Rules rules);
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher, Gnip.Client.Resource.Filter filter);
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher);
        Gnip.Client.Resource.Result Update(Gnip.Client.Resource.Publisher publisher, string filterName, Gnip.Client.Resource.Rules rules);
    }
}
