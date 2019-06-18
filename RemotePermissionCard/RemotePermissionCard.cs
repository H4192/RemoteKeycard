﻿using Smod2;
using Smod2.Attributes;

namespace RemotePermissionCard
{
    [PluginDetails(
        author = "iRebbok",
        name = "RemotePermissionCard",
        id = "irebbok.remote.permission.card",
        description = "Door Manager",
        version = "1.2.0-Pre",
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
            this.AddEventHandlers(new EventHandlers(this), Smod2.Events.Priority.High);
            this.AddCommands(new string[] { "rpc_disable" }, new DisableCommand(this));
            this.AddCommands(new string[] { "rpc_reload" }, new ReloadCommand(this));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_card_list", "0,1,2,3,4,5,6,7,8,9,10,11", true, "CList settings")); // CL
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_door_list", string.Empty, true, "DList setings")); // DL
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_card_access", string.Empty, true, "Customized permissions to card")); // CA
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_door_access", string.Empty, true, "Customized permissions to door")); // DA
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_mode", 1, true, "Work mode"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_remote", true, true, "Remote door opening"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_default_if_none", false, true, "Use default settings if they are not specified, for example you want to change only one door"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_permission", false, true, "Permission mode"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_disable", false, true, "Disable this pluign"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_info", true, true, "Usage information"));
            this.AddConfig(new Smod2.Config.ConfigSetting("rpc_debug", false, true, "Debug this plugin"));
        }
    }
}
