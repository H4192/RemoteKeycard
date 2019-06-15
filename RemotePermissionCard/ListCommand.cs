using Smod2.Commands;
using System.Collections.Generic;
using System.Linq;

namespace RemotePermissionCard
{
    internal class ListCommand : ICommandHandler
    {
        private RemotePermissionCard plugin;
        public ListCommand(RemotePermissionCard plugin) => this.plugin = plugin;

        public string GetCommandDescription()
        {
            return "List for debug";
        }

        public string GetUsage()
        {
            return "rpc_list";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (ConfigManagers.Keycardperm.Count > 0)
            {
                string result = "";
                foreach (KeyValuePair<int, string> key in ConfigManagers.Keycardperm.ToList())
                {
                    result += $"\nInt: '{key.Key}' value: '{key.Value}'";
                }
                return new string[] { result };
            }
            else
            {
                return new string[] { "Not found ¯\\_(ツ)_/¯" };
            }
        }
    }
}