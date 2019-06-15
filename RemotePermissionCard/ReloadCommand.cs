using Smod2.Commands;

namespace RemotePermissionCard
{
    internal class ReloadCommand : ICommandHandler
    {
        private RemotePermissionCard plugin;
        public ReloadCommand(RemotePermissionCard plugin) => this.plugin = plugin;

        public string GetCommandDescription()
        {
            return "Reload configuration";
        }

        public string GetUsage()
        {
            return "rpc_reload";
        }

        public string[] OnCall(ICommandSender sender, string[] args)
        {
            if (ConfigManagers.RPCPermissionMode)
            {
                if (sender is Smod2.API.Player p)
                {
                    if (!p.HasPermission("remotepermissioncard.reload"))
                    {
                        return new string[] { "You don't have permission to use that command." };
                    }
                }
            }

            ConfigManagers.cardlist.Clear();
            ConfigManagers.ReloadConfig(plugin);
            return new string[] { "The configuration was successfully reloaded." };
        }
    }
}