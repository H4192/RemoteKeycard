using EXILED;

namespace RemoteKeycard
{
	internal class EventHandlers
	{
		public void OnDoorAccess(ref DoorInteractionEvent ev)
		{
			if (ev.Allow == true) return;
			foreach (var item in ev.Player.inventory.items)
			{
				if (ev.Player.inventory.GetItemByID(item.id).permissions.Contains(ev.Door.permissionLevel))
				{
					ev.Allow = true;
					return;
				}
			}
		}
	}
}