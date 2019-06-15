# RemotePermissionCard
RemotePermissionCard created to open doors without holding a card, but having in your inventory.

## Installation
**For the plugin to work, you must have a working [Smod](https://github.com/Grover-c13/Smod2)**
1. Take the [latest version](https://github.com/iRebbok/RemotePermissionCard/releases/latest) of the plugin.
2. Put the **RemotePermissionCard.dll** file to the folder `sm_plugins`.

## Configuration
Config Option | Value Type | Default Value | Description
:---: | :---: | :---: | ---
rpc_cards | [CList](https://github.com/iRebbok/RemotePermissionCard/blob/master/README.md#clist-card-list) | 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 | List of cards that will open the door (Please see the below list to understand)
rpc_disable | Bool | false | Disabling the plugin at server startup
rpc_info | Bool | true | Information about the player use of the card in the inventory
rpc_debug | Bool | false | Detailed information about using the plugin
rpc_permission | Bool | false | Enable use with SCPermission
rpc_card_access | [CADict](https://github.com/iRebbok/RemotePermissionCard/blob/master/README.md#cadict-card-access-dictionary) | String.Empty | Gives the possibility to have own settings card permissions

## Commands
Command | Description
:---: | ---
rpc_disable | The command to disable this plugin
rpc_reload | Reboot plugin configuration (To avoid in order issues a invalid reboot, reboot the basic server configuration with `config r` command)
rpc_list | List of permissions for each card



**Permissions work correctly only if SCPDiscord is present and bool is enabled `rpc_permission`**
## Permission
Permission Name | Permission Description
:---: | -----
remotepermissioncard.reload | The possibility of using `rpc_reload`
remotepermissioncard.disable | The possibility of using `rpc_disable`



*:OMEGALUL:*
## CList (Card List)
Value | Name | Description
:----: | :----: | ----
0 | O5_LEVEL_KEYCARD | 05 Card
1 | CHAOS_INSURGENCY_DEVICE | Chaos Card
2 | FACILITY_MANAGER_KEYCARD | Facility Manager Keycard
3 | MTF_COMMANDER_KEYCARD | MTF Commander Keycard
4 | MTF_LIEUTENANT_KEYCARD | MTF Lieutenant Keycard
5 | CONTAINMENT_ENGINEER_KEYCARD | Containment Engineer Keycard
6 | SENIOR_GUARD_KEYCARD | Senior Guard Keycard
7 | GUARD_KEYCARD | Guard Keycard
8 | ZONE_MANAGER_KEYCARD | Zone Manager Keycard
9 | MAJOR_SCIENTIST_KEYCARD | Major Scientist Keycard
10 | SCIENTIST_KEYCARD | Scientist Keycard
11 | JANITOR_KEYCARD | Janitor Keycard



## CADict (Card Access Dictionary)
Access | Default [CList](https://github.com/iRebbok/RemotePermissionCard/blob/master/README.md#clist-card-list)
:----: | -------
CONT_LVL_1 | 0,1,2,3,4,5,6,7,8,9,10,11
CONT_LVL_2 | 0,1,2,3,4,5,6,7,8,9,10
CONT_LVL_3 | 0,2,5
ARMORY_LVL_1 | 0,1,3,4,6
ARMORY_LVL_2 | 0,1,3,6
ARMORY_LVL_3 | 0,1,3
CHCKPOINT_ACC | 0,1,2,3,4,5,6,7,8,9
EXIT_ACC | 0,1,2,3,4
INCOM_ACC | 0,1,2,3,5

# Example:
`rpc_card_access: PermissionName:Num&Num&Num, PermissionName:Num&Num&Num`

`rpc_card_access: PermissionName:Num&Num&Num&Num&Num&Num, PermissionName:Num`

# Illustrative example:
`rpc_card_access: ARMORY_LVL_1:1&7&8, INCOM_ACC:4&6&9` - CardID 1,7,8 will be to open doors with permission 'ARMORY_LVL_1' and CardID 4,6,9 will be to open Intercom.

`rpc_card_access: CONT_LVL_3:4&8&1&2&7&9, CHCKPOINT_ACC:11` - CardID 4,8,1,2,7,9 will be to open doors with permission 'CONT_LVL_3' and CardID 11 will be to open Checkpoint.

**In case of any problems please contact the Discord `iRebbok#2429` or in the Issues**
