using System;
using System.Linq;
using System.Collections.Generic;

namespace DataWorx.Core.Persistence
{
    public interface IServer
    {
        string Name { get; set; }
        string Database { get; set; }

        bool TestConnection();
        void Drop();
        int Run(string query);
        void CreateSchema(params Type[] model);
    }
}
