using System;
using System.Collections.Generic;

public class EventChannel
{
    private static Dictionary<CustomEvent.EventType, List<Subscriber>> subscribersByEventType = InitializeList();

    private static Dictionary<CustomEvent.EventType, List<Subscriber>> InitializeList()
    {
        var dict = new Dictionary<CustomEvent.EventType, List<Subscriber>>();
        Array eventTypes = Enum.GetValues(typeof(CustomEvent.EventType));

        foreach (CustomEvent.EventType eventType in eventTypes)
            dict.Add(eventType, new List<Subscriber>());

        return dict;
    }

    public static void SubscribeTo(CustomEvent.EventType eventType, Subscriber subscriber)
    {
        List<Subscriber> typeSubscribers = subscribersByEventType[eventType];

        typeSubscribers.Add(subscriber);
    }

    public static void Unsubscribe(Subscriber subscriber)
    {
        var keys = subscribersByEventType.Keys;

        foreach (var key in keys)
            subscribersByEventType[key].Remove(subscriber);
    }

    public static void Send(CustomEvent customEvent)
    {
        foreach (Subscriber subscriber in subscribersByEventType[customEvent.Type])
            subscriber.OnEvent(customEvent);
    }
}
