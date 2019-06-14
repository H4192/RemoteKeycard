﻿using Smod2;
using Smod2.Attributes;

namespace RemotePermissionCard
{
    [PluginDetails(
        author = "iRebbok",
        name = "RemotePermissionCard",
        id = "irebbok.remote.permission.card",
        description = "Using the card remotely",
        version = "1.0.0",
        SmodMajor = 3,
        SmodMinor = 4,
        SmodRevision = 1)]

    public class RemotePermissionCard : Plugin
    {
        public override void OnDisable()
        {

        }

        public override void OnEnable()
        {
            ConfigManagers.ReloadConfig(this);
        }

        public override void Register()
        {
            this.AddEventHandlers(new EventHandlers(this), Smod2.Events.Priority.Normal);
            this.AddCommands(new string[] { "rpc_disable" }, new DisableCommand(this));
            this.AddCommands(new string[] { "rpc_reload" }, new ReloadComman(this));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_cards", "0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11", true, "List of cards that will work"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_disable", false, true, "Disable this pluign"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_info", true, true, "Usage information"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_debug", false, true, "Debug this plugin"));
        }
    }
}