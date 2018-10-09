using DataWorx.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWorx.Core
{
    public interface IGenerate
    {
        string Name { get; }
        string Description { get; }
        IServer Server { get; }
        void Data();

        event EventHandler<TaskEventArgs> Notify;
    }

    public enum NotificationType
    {
        Primary,
        Secondary
    }

    public class TaskEventArgs : EventArgs
    {
        public NotificationType Type { get; private set; }
        public string Description { get; private set; }
        public double Progress { get; private set; }

        public TaskEventArgs(NotificationType type, string description, double progress)
        {
            Type = type;
            Description = description;
            Progress = progress;
        }
    }

    public class TaskParameters
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool Drop {get; set;}
        public Action<string, double> MainProgress { get; set; }
        public Action<string, double> SubProgress { get; set; }

        public TaskParameters()
        {
            Drop = true;
            Server = "";
            Database = "";
            UserId = "";
            Password = "";
            MainProgress = null;
            SubProgress = null;
        }
    }
}
