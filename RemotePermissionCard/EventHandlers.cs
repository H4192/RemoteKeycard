using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;

namespace RemotePermissionCard
{
    internal class EventHandlers : IEventHandler, IEventHandlerDoorAccess, IEventHandlerWaitingForPlayers
    {
        private RemotePermissionCard plugin;
        public EventHandlers(RemotePermissionCard plugin) => this.plugin = plugin;


        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP && !ev.Player.GetBypassMode())
            {
                if (ConfigManagers.RPCMode == 1) LogicManager.LogicOneRemote(plugin, ev);
                else if (ConfigManagers.RPCMode == 2) LogicManager.LogicTwo(plugin, ev);
                else if (ConfigManagers.RPCMode == 3) LogicManager.LogicTree(plugin, ev);
                else if (ConfigManagers.RPCMode == 4) LogicManager.LogicFour(plugin, ev);
                else if (ConfigManagers.RPCMode == 5) LogicManager.LogicFive(plugin, ev);
                else if (ConfigManagers.RPCMode == 6) LogicManager.LogicSix(plugin, ev);
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (plugin.GetConfigBool("rpc_disable")) PluginManager.Manager.DisablePlugin(plugin);
        }
    }
}