using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;

namespace RemotePermissionCard
{
    internal class EventHandlers : IEventHandlerDoorAccess, IEventHandlerWaitingForPlayers
    {
        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (ev.Player.TeamRole.Team != Smod2.API.Team.SCP && !ev.Player.GetBypassMode())
            {
                if (ConfigManagers.Manager.RPCMode == 1) LogicManager.LogicOneRemote(ev);
                else if (ConfigManagers.Manager.RPCMode == 2) LogicManager.LogicTwo(ev);
                else if (ConfigManagers.Manager.RPCMode == 3) LogicManager.LogicTree(ev);
                else if (ConfigManagers.Manager.RPCMode == 4) LogicManager.LogicFour(ev);
                else if (ConfigManagers.Manager.RPCMode == 5) LogicManager.LogicFive(ev);
                else if (ConfigManagers.Manager.RPCMode == 6) LogicManager.LogicSix(ev);
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (ConfigFile.GetBool("rpc_disable", false)) PluginManager.Manager.DisablePlugin(RemoteKeycard.plugin);
        }
    }
}