using Smod2;
using Smod2.Commands;

namespace RemotePermissionCard
{
    internal class DisableCommand : ICommandHandler
    {
        public string GetCommandDescription()
        {
            return "Disabling this plugin";
        }

        public string GetUsage()
        {
            return "rpc_disable";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (ConfigManagers.Manager.RPCPermissionMode)
            {
                if (sender is Smod2.API.Player p)
                {
                    if (!p.HasPermission("remotepermissioncard.disable"))
                    {
                        return new string[] { "You don't have permission to use that command." };
                    }
                }
            }

            PluginManager.Manager.DisablePlugin(RemotePermissionCard.plugin);
            return new string[] { "RemotePermissionCard disable." };
        }
    }
}