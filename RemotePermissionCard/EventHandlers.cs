using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;

namespace RemotePermissionCard
{
    internal class EventHandlers : IEventHandler, IEventHandlerDoorAccess, IEventHandlerWaitingForPlayers
    {
        private RemotePermissionCard plugin;
        public EventHandlers(RemotePermissionCard plugin) => this.plugin = plugin;


        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Destroyed == false && ev.Door.BlockAfterWarheadDetonation == false && ev.Player.GetInventory().Count > 0 && ev.Door.Locked == false && ev.Door.Permission != String.Empty && ev.Player.TeamRole.Team != Team.SCP && ev.Allow == false)
            {
                if (ConfigManagers.RPCDebug) plugin.Info($"Door perm: {ev.Door.Permission}");
                for(int z = 0; z < 12; z++)
                {
                    if (ConfigManagers.Keycardperm.ContainsKey(z))
                    {
                        ItemType item = ConfigManagers.keycards[z];
                        if (ConfigManagers.RPCDebug) plugin.Info($"Check has item: '{item}'");
                        if (ev.Player.HasItem(item))
                        {
                            if (ConfigManagers.RPCDebug) plugin.Info($"Successfully found item: '{item}' on number: '{z}'");
                            if (ConfigManagers.Keycardperm.ContainsKey(z))
                            {
                                string[] perms = ConfigManagers.Keycardperm[z].Split(',');
                                foreach (string perm in perms)
                                {
                                    if (ConfigManagers.RPCDebug) plugin.Info($"Check {perm} permission...");
                                    if (ev.Door.Permission == perm)
                                    {
                                        ev.Allow = true;
                                        if (ConfigManagers.RPCDebug) plugin.Info($"Successfully on {perm} permission!");
                                        if (ConfigManagers.RPCInfo) plugin.Info($"Player {ev.Player.Name} open the door '{ev.Door.Name}' with the help '{item}' thanks to permission '{perm}'.");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (plugin.GetConfigBool("rpc_disable")) PluginManager.Manager.DisablePlugin(plugin);
        }
    }
}