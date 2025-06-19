﻿using System;
using System.Threading.Tasks;

namespace GorillaInfoWatch.Models
{
    [Serializable]
    public class Notification(string content, float duration, InfoWatchSound sound, Notification.ExternalScreen screen = null)
    {
        public string Content { get; } = content;
        public float Duration { get; } = duration;
        public InfoWatchSound Sound { get; } = sound;
        public ExternalScreen Screen { get; } = screen;

        public bool Opened = false, Processing = false;

        public string DisplayText = content;

        public DateTime Created = DateTime.Now;
        public string RoomName = NetworkSystem.Instance.InRoom ? NetworkSystem.Instance.RoomName : null;
        public bool SessionIsPrivate = NetworkSystem.Instance.InRoom && NetworkSystem.Instance.SessionIsPrivate;

        public Notification(string head, string body, float duration, InfoWatchSound sound, ExternalScreen screen = null) : this(string.Format("<size=6>{0}:</size><br><b>{1}</b>", head, body), duration, sound, screen)
        {
            DisplayText = $"{head.TrimEnd(':')}: {body}";
        }

        public class ExternalScreen(Type screen, string displayText, Task task)
        {
            public Type ScreenType { get; } = screen;
            public string DisplayText { get; } = displayText;
            public Task Task { get; } = task;

            public ExternalScreen(Type screen, string displayText, Action action) : this(screen, displayText, Task.Run(action))
            {
                // Must require a body
            }
        }
    }
}
