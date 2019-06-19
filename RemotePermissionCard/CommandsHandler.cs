using Smod2.Commands;
using System.Collections.Generic;

namespace RemotePermissionCard
{
    class CardsListCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "List cards";
        }

        public string GetUsage()
        {
            return "rpc_list_card";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            List<int> list = ConfigManagers.CardsList;

            if (list.Count > 0)
            {
                return new string[] { $"Card List: {string.Join(",", list.ToArray())}" };
            }
            else return new string[] { "Not found." };
        }
    }

    class DoorListCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "List doors";
        }

        public string GetUsage()
        {
            return "rpc_list_door";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            Dictionary<string, CList> list = ConfigManagers.CustomDoorList;

            if (list.Count > 0)
            {
                string result = "";
                foreach (KeyValuePair<string, CList> node in list)
                {
                    result += $"\nDoor: {node.Key} = {string.Join(",", node.Value.ints.ToArray())}";
                }
                return new string[] { result };
            }
            else return new string[] { "Not found." };
        }
    }

    class DoorAccessCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "List doors access";
        }

        public string GetUsage()
        {
            return "rpc_access_door";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            Dictionary<string, string> list = ConfigManagers.CustomDoorAccess;

            if (list.Count > 0)
            {
                string result = "";
                foreach (KeyValuePair<string, string> node in list)
                {
                    result += $"\nDoor: {node.Key} = {node.Value}";
                }
                return new string[] { result };
            }
            else return new string[] { "Not found." };
        }
    }

    class CardAccessCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "List cards access";
        }

        public string GetUsage()
        {
            return "";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            Dictionary<int, PList> list = ConfigManagers.CustomCardAccess;

            if (list.Count > 0)
            {
                string result = "";
                foreach (KeyValuePair<int, PList> node in list)
                {
                    result += $"\nCardID: {node.Key} = {string.Join(",", node.Value.perms.ToArray())}";
                }
                return new string[] { result };
            }
            else return new string[] { "Not found." };
        }
    }
}
