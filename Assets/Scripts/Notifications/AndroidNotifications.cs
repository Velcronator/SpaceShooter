using System;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class AndroidNotifications : MonoBehaviour
{
    [SerializeField] private int m_notificationId = 1;
    [SerializeField] private string m_channelId = "default_channel";
    [SerializeField] private string m_channelName = "Notifications";
    [SerializeField] private string m_channelDescription = "General notifications";
    [SerializeField] private Importance m_channelImportance = Importance.Default;

    [SerializeField] private string m_notificationTitle = "Coming Back?";
    [SerializeField] private string m_notificationText = "We miss you";
    [SerializeField] private string m_notificationSmallIcon = "default";
    [SerializeField] private string m_notificationLargeIcon = "default";
    [SerializeField] private float m_notificationDelaySeconds = 10f;

    void Start()
    {
#if UNITY_ANDROID
        CreateNotificationChannel();
#endif
    }

#if UNITY_ANDROID
    private void CreateNotificationChannel()
    {
            AndroidNotificationChannel channel = new AndroidNotificationChannel()
            {
                Id = m_channelId,
                Name = m_channelName,
                Importance = m_channelImportance,
                Description = m_channelDescription,
            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    void ScheduleNotification(DateTime timeToFire)
    {
        AndroidNotification notification = new AndroidNotification()
        {
            Title = m_notificationTitle,
            Text = m_notificationText,
            SmallIcon = m_notificationSmallIcon,
            LargeIcon = m_notificationLargeIcon,
            FireTime = timeToFire,
        };

        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, m_channelId, m_notificationId);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            DateTime whenToFire = DateTime.Now.AddSeconds(m_notificationDelaySeconds);
            ScheduleNotification(whenToFire);
        }
        else
        {
            AndroidNotificationCenter.CancelScheduledNotification(m_notificationId);
        }
    }
#endif
}
