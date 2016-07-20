using BotCore.GameClient;
using BotCore.GameServer;
using D2Data;
using MBNCSUtil;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BioBot
{
	public class GameSocket : D2Socket
	{
		public delegate void OnAboutPlayerEventHandler(AboutPlayer Packet);

		public delegate void OnAcceptTradeEventHandler(AcceptTrade Packet);

		public delegate void OnAddUnitEventHandler(AddUnit Packet);

		public delegate void OnAssignGameObjectEventHandler(AssignGameObject Packet);

		public delegate void OnAssignMercEventHandler(AssignMerc Packet);

		public delegate void OnAssignNPCEventHandler(AssignNPC Packet);

		public delegate void OnAssignPlayerEventHandler(AssignPlayer Packet);

		public delegate void OnAssignPlayerCorpseEventHandler(AssignPlayerCorpse Packet);

		public delegate void OnAssignPlayerToPartyEventHandler(AssignPlayerToParty Packet);

		public delegate void OnAssignSkillEventHandler(AssignSkill Packet);

		public delegate void OnAssignSkillHotkeyEventHandler(AssignSkillHotkey Packet);

		public delegate void OnAssignWarpEventHandler(AssignWarp Packet);

		public delegate void OnAttributeNotificationEventHandler(AttributeNotification Packet);

		public delegate void OnDelayedStateEventHandler(DelayedState Packet);

		public delegate void OnEndStateEventHandler(EndState Packet);

		public delegate void OnGainExperienceEventHandler(GainExperience Packet);

		public delegate void OnGameHandshakeEventHandler(GameHandshake Packet);

		public delegate void OnGameLoadingEventHandler(GameLoading Packet);

		public delegate void OnGameLogonReceiptEventHandler(GameLogonReceipt Packet);

		public delegate void OnGameLogonSuccessEventHandler(GameLogonSuccess Packet);

		public delegate void OnGameLogoutSuccessEventHandler(GameLogoutSuccess Packet);

		public delegate void OnGameOverEventHandler(GameOver Packet);

		public delegate void OnGoldTradeEventHandler(GoldTrade Packet);

		public delegate void OnInformationMessageEventHandler(InformationMessage Packet);

		public delegate void OnItemTriggerSkillEventHandler(ItemTriggerSkill Packet);

		public delegate void OnLoadActEventHandler(LoadAct Packet);

		public delegate void OnLoadDoneEventHandler(LoadDone Packet);

		public delegate void OnMapAddEventHandler(MapAdd Packet);

		public delegate void OnMapRemoveEventHandler(MapRemove Packet);

		public delegate void OnMercAttributeNotificationEventHandler(MercAttributeNotification Packet);

		public delegate void OnMercForHireEventHandler(MercForHire Packet);

		public delegate void OnMercForHireListStartEventHandler(MercForHireListStart Packet);

		public delegate void OnMercGainExperienceEventHandler(MercGainExperience Packet);

		public delegate void OnMonsterAttackEventHandler(MonsterAttack Packet);

		public delegate void OnNPCActionEventHandler(NPCAction Packet);

		public delegate void OnNPCGetHitEventHandler(NPCGetHit Packet);

		public delegate void OnNPCHealEventHandler(NPCHeal Packet);

		public delegate void OnNPCInfoEventHandler(NPCInfo Packet);

		public delegate void OnNPCMoveEventHandler(NPCMove Packet);

		public delegate void OnNPCMoveToTargetEventHandler(NPCMoveToTarget Packet);

		public delegate void OnNPCStopEventHandler(NPCStop Packet);

		public delegate void OnNPCWantsInteractEventHandler(NPCWantsInteract Packet);

		public delegate void OnOpenWaypointEventHandler(OpenWaypoint Packet);

		public delegate void OnOwnedItemActionEventHandler(OwnedItemAction Packet);

		public delegate void OnPartyMemberPulseEventHandler(PartyMemberPulse Packet);

		public delegate void OnPartyMemberUpdateEventHandler(PartyMemberUpdate Packet);

		public delegate void OnPartyRefreshEventHandler(PartyRefresh Packet);

		public delegate void OnPlayerAttributeNotificationEventHandler(PlayerAttributeNotification Packet);

		public delegate void OnPlayerClearCursorEventHandler(PlayerClearCursor Packet);

		public delegate void OnPlayerCorpseVisibleEventHandler(PlayerCorpseVisible Packet);

		public delegate void OnPlayerInGameEventHandler(PlayerInGame Packet);

		public delegate void OnPlayerInSightEventHandler(PlayerInSight Packet);

		public delegate void OnPlayerKillCountEventHandler(PlayerKillCount Packet);

		public delegate void OnPlayerLeaveGameEventHandler(PlayerLeaveGame Packet);

		public delegate void OnPlayerLifeManaChangeEventHandler(PlayerLifeManaChange Packet);

		public delegate void OnPlayerMoveEventHandler(PlayerMove Packet);

		public delegate void OnPlayerMoveToTargetEventHandler(PlayerMoveToTarget Packet);

		public delegate void OnPlayerPartyRelationshipEventHandler(PlayerPartyRelationship Packet);

		public delegate void OnPlayerReassignEventHandler(PlayerReassign Packet);

		public delegate void OnPlayerRelationshipEventHandler(PlayerRelationship Packet);

		public delegate void OnPlayerStopEventHandler(PlayerStop Packet);

		public delegate void OnPlaySoundEventHandler(PlaySound Packet);

		public delegate void OnPongEventHandler(Pong Packet);

		public delegate void OnPortalInfoEventHandler(PortalInfo Packet);

		public delegate void OnPortalOwnershipEventHandler(PortalOwnership Packet);

		public delegate void OnQuestItemStateEventHandler(QuestItemState Packet);

		public delegate void OnReceiveMessageEventHandler(GameMessage Packet);

		public delegate void OnRelator1EventHandler(Relator1 Packet);

		public delegate void OnRelator2EventHandler(Relator2 Packet);

		public delegate void OnRemoveGroundUnitEventHandler(RemoveGroundUnit Packet);

		public delegate void OnReportKillEventHandler(ReportKill Packet);

		public delegate void OnRequestLogonInfoEventHandler(RequestLogonInfo Packet);

		public delegate void OnSetGameObjectModeEventHandler(SetGameObjectMode Packet);

		public delegate void OnSetItemStateEventHandler(SetItemState Packet);

		public delegate void OnSetNPCModeEventHandler(SetNPCMode Packet);

		public delegate void OnSetStateEventHandler(SetState Packet);

		public delegate void OnSkillsLogEventHandler(SkillsLog Packet);

		public delegate void OnSmallGoldAddEventHandler(SmallGoldAdd Packet);

		public delegate void OnSummonActionEventHandler(SummonAction Packet);

		public delegate void OnSwitchWeaponSetEventHandler(SwitchWeaponSet Packet);

		public delegate void OnTransactionCompleteEventHandler(TransactionComplete Packet);

		public delegate void OnUnitUseSkillEventHandler(UnitUseSkill Packet);

		public delegate void OnUnitUseSkillOnTargetEventHandler(UnitUseSkillOnTarget Packet);

		public delegate void OnUnloadDoneEventHandler(UnloadDone Packet);

		public delegate void OnUpdateGameQuestLogEventHandler(UpdateGameQuestLog Packet);

		public delegate void OnUpdateItemStatsEventHandler(UpdateItemStats Packet);

		public delegate void OnUpdateItemUIEventHandler(UpdateItemUI Packet);

		public delegate void OnUpdatePlayerItemSkillEventHandler(UpdatePlayerItemSkill Packet);

		public delegate void OnUpdateQuestInfoEventHandler(UpdateQuestInfo Packet);

		public delegate void OnUpdateQuestLogEventHandler(UpdateQuestLog Packet);

		public delegate void OnUpdateSkillEventHandler(UpdateSkill Packet);

		public delegate void OnUseSpecialItemEventHandler(UseSpecialItem Packet);

		public delegate void OnUseStackableItemEventHandler(UseStackableItem Packet);

		public delegate void OnWalkVerifyEventHandler(WalkVerify Packet);

		public delegate void OnWardenCheckEventHandler(WardenCheck Packet);

		public delegate void OnWorldItemActionEventHandler(WorldItemAction Packet);

		private GameSocket.OnAboutPlayerEventHandler OnAboutPlayerEvent;

		private GameSocket.OnAcceptTradeEventHandler OnAcceptTradeEvent;

		private GameSocket.OnAddUnitEventHandler OnAddUnitEvent;

		private GameSocket.OnAssignGameObjectEventHandler OnAssignGameObjectEvent;

		private GameSocket.OnAssignMercEventHandler OnAssignMercEvent;

		private GameSocket.OnAssignNPCEventHandler OnAssignNPCEvent;

		private GameSocket.OnAssignPlayerEventHandler OnAssignPlayerEvent;

		private GameSocket.OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpseEvent;

		private GameSocket.OnAssignPlayerToPartyEventHandler OnAssignPlayerToPartyEvent;

		private GameSocket.OnAssignSkillEventHandler OnAssignSkillEvent;

		private GameSocket.OnAssignSkillHotkeyEventHandler OnAssignSkillHotkeyEvent;

		private GameSocket.OnAssignWarpEventHandler OnAssignWarpEvent;

		private GameSocket.OnAttributeNotificationEventHandler OnAttributeNotificationEvent;

		private GameSocket.OnDelayedStateEventHandler OnDelayedStateEvent;

		private GameSocket.OnEndStateEventHandler OnEndStateEvent;

		private GameSocket.OnGainExperienceEventHandler OnGainExperienceEvent;

		private GameSocket.OnGameHandshakeEventHandler OnGameHandshakeEvent;

		private GameSocket.OnGameLoadingEventHandler OnGameLoadingEvent;

		private GameSocket.OnGameLogonReceiptEventHandler OnGameLogonReceiptEvent;

		private GameSocket.OnGameLogonSuccessEventHandler OnGameLogonSuccessEvent;

		private GameSocket.OnGameLogoutSuccessEventHandler OnGameLogoutSuccessEvent;

		private GameSocket.OnGameOverEventHandler OnGameOverEvent;

		private GameSocket.OnGoldTradeEventHandler OnGoldTradeEvent;

		private GameSocket.OnInformationMessageEventHandler OnInformationMessageEvent;

		private GameSocket.OnItemTriggerSkillEventHandler OnItemTriggerSkillEvent;

		private GameSocket.OnLoadActEventHandler OnLoadActEvent;

		private GameSocket.OnLoadDoneEventHandler OnLoadDoneEvent;

		private GameSocket.OnMapAddEventHandler OnMapAddEvent;

		private GameSocket.OnMapRemoveEventHandler OnMapRemoveEvent;

		private GameSocket.OnMercAttributeNotificationEventHandler OnMercAttributeNotificationEvent;

		private GameSocket.OnMercForHireEventHandler OnMercForHireEvent;

		private GameSocket.OnMercForHireListStartEventHandler OnMercForHireListStartEvent;

		private GameSocket.OnMercGainExperienceEventHandler OnMercGainExperienceEvent;

		private GameSocket.OnMonsterAttackEventHandler OnMonsterAttackEvent;

		private GameSocket.OnNPCActionEventHandler OnNPCActionEvent;

		private GameSocket.OnNPCGetHitEventHandler OnNPCGetHitEvent;

		private GameSocket.OnNPCHealEventHandler OnNPCHealEvent;

		private GameSocket.OnNPCInfoEventHandler OnNPCInfoEvent;

		private GameSocket.OnNPCMoveEventHandler OnNPCMoveEvent;

		private GameSocket.OnNPCMoveToTargetEventHandler OnNPCMoveToTargetEvent;

		private GameSocket.OnNPCStopEventHandler OnNPCStopEvent;

		private GameSocket.OnNPCWantsInteractEventHandler OnNPCWantsInteractEvent;

		private GameSocket.OnOpenWaypointEventHandler OnOpenWaypointEvent;

		private GameSocket.OnOwnedItemActionEventHandler OnOwnedItemActionEvent;

		private GameSocket.OnPartyMemberPulseEventHandler OnPartyMemberPulseEvent;

		private GameSocket.OnPartyMemberUpdateEventHandler OnPartyMemberUpdateEvent;

		private GameSocket.OnPartyRefreshEventHandler OnPartyRefreshEvent;

		private GameSocket.OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotificationEvent;

		private GameSocket.OnPlayerClearCursorEventHandler OnPlayerClearCursorEvent;

		private GameSocket.OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisibleEvent;

		private GameSocket.OnPlayerInGameEventHandler OnPlayerInGameEvent;

		private GameSocket.OnPlayerInSightEventHandler OnPlayerInSightEvent;

		private GameSocket.OnPlayerKillCountEventHandler OnPlayerKillCountEvent;

		private GameSocket.OnPlayerLeaveGameEventHandler OnPlayerLeaveGameEvent;

		private GameSocket.OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChangeEvent;

		private GameSocket.OnPlayerMoveEventHandler OnPlayerMoveEvent;

		private GameSocket.OnPlayerMoveToTargetEventHandler OnPlayerMoveToTargetEvent;

		private GameSocket.OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationshipEvent;

		private GameSocket.OnPlayerReassignEventHandler OnPlayerReassignEvent;

		private GameSocket.OnPlayerRelationshipEventHandler OnPlayerRelationshipEvent;

		private GameSocket.OnPlayerStopEventHandler OnPlayerStopEvent;

		private GameSocket.OnPlaySoundEventHandler OnPlaySoundEvent;

		private GameSocket.OnPongEventHandler OnPongEvent;

		private GameSocket.OnPortalInfoEventHandler OnPortalInfoEvent;

		private GameSocket.OnPortalOwnershipEventHandler OnPortalOwnershipEvent;

		private GameSocket.OnQuestItemStateEventHandler OnQuestItemStateEvent;

		private GameSocket.OnReceiveMessageEventHandler OnReceiveMessageEvent;

		private GameSocket.OnRelator1EventHandler OnRelator1Event;

		private GameSocket.OnRelator2EventHandler OnRelator2Event;

		private GameSocket.OnRemoveGroundUnitEventHandler OnRemoveGroundUnitEvent;

		private GameSocket.OnReportKillEventHandler OnReportKillEvent;

		private GameSocket.OnRequestLogonInfoEventHandler OnRequestLogonInfoEvent;

		private GameSocket.OnSetGameObjectModeEventHandler OnSetGameObjectModeEvent;

		private GameSocket.OnSetItemStateEventHandler OnSetItemStateEvent;

		private GameSocket.OnSetNPCModeEventHandler OnSetNPCModeEvent;

		private GameSocket.OnSetStateEventHandler OnSetStateEvent;

		private GameSocket.OnSkillsLogEventHandler OnSkillsLogEvent;

		private GameSocket.OnSmallGoldAddEventHandler OnSmallGoldAddEvent;

		private GameSocket.OnSummonActionEventHandler OnSummonActionEvent;

		private GameSocket.OnSwitchWeaponSetEventHandler OnSwitchWeaponSetEvent;

		private GameSocket.OnTransactionCompleteEventHandler OnTransactionCompleteEvent;

		private GameSocket.OnUnitUseSkillEventHandler OnUnitUseSkillEvent;

		private GameSocket.OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTargetEvent;

		private GameSocket.OnUnloadDoneEventHandler OnUnloadDoneEvent;

		private GameSocket.OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLogEvent;

		private GameSocket.OnUpdateItemStatsEventHandler OnUpdateItemStatsEvent;

		private GameSocket.OnUpdateItemUIEventHandler OnUpdateItemUIEvent;

		private GameSocket.OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkillEvent;

		private GameSocket.OnUpdateQuestInfoEventHandler OnUpdateQuestInfoEvent;

		private GameSocket.OnUpdateQuestLogEventHandler OnUpdateQuestLogEvent;

		private GameSocket.OnUpdateSkillEventHandler OnUpdateSkillEvent;

		private GameSocket.OnUseSpecialItemEventHandler OnUseSpecialItemEvent;

		private GameSocket.OnUseStackableItemEventHandler OnUseStackableItemEvent;

		private GameSocket.OnWalkVerifyEventHandler OnWalkVerifyEvent;

		private GameSocket.OnWardenCheckEventHandler OnWardenCheckEvent;

		private GameSocket.OnWorldItemActionEventHandler OnWorldItemActionEvent;

		private LogBox Log;

		private IPAddress G_ServerIp;

		public event GameSocket.OnAboutPlayerEventHandler OnAboutPlayer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAboutPlayerEvent = (GameSocket.OnAboutPlayerEventHandler)Delegate.Combine(this.OnAboutPlayerEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAboutPlayerEvent = (GameSocket.OnAboutPlayerEventHandler)Delegate.Remove(this.OnAboutPlayerEvent, value);
			}
		}

		public event GameSocket.OnAcceptTradeEventHandler OnAcceptTrade
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAcceptTradeEvent = (GameSocket.OnAcceptTradeEventHandler)Delegate.Combine(this.OnAcceptTradeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAcceptTradeEvent = (GameSocket.OnAcceptTradeEventHandler)Delegate.Remove(this.OnAcceptTradeEvent, value);
			}
		}

		public event GameSocket.OnAddUnitEventHandler OnAddUnit
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAddUnitEvent = (GameSocket.OnAddUnitEventHandler)Delegate.Combine(this.OnAddUnitEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAddUnitEvent = (GameSocket.OnAddUnitEventHandler)Delegate.Remove(this.OnAddUnitEvent, value);
			}
		}

		public event GameSocket.OnAssignGameObjectEventHandler OnAssignGameObject
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignGameObjectEvent = (GameSocket.OnAssignGameObjectEventHandler)Delegate.Combine(this.OnAssignGameObjectEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignGameObjectEvent = (GameSocket.OnAssignGameObjectEventHandler)Delegate.Remove(this.OnAssignGameObjectEvent, value);
			}
		}

		public event GameSocket.OnAssignMercEventHandler OnAssignMerc
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignMercEvent = (GameSocket.OnAssignMercEventHandler)Delegate.Combine(this.OnAssignMercEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignMercEvent = (GameSocket.OnAssignMercEventHandler)Delegate.Remove(this.OnAssignMercEvent, value);
			}
		}

		public event GameSocket.OnAssignNPCEventHandler OnAssignNPC
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignNPCEvent = (GameSocket.OnAssignNPCEventHandler)Delegate.Combine(this.OnAssignNPCEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignNPCEvent = (GameSocket.OnAssignNPCEventHandler)Delegate.Remove(this.OnAssignNPCEvent, value);
			}
		}

		public event GameSocket.OnAssignPlayerEventHandler OnAssignPlayer
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignPlayerEvent = (GameSocket.OnAssignPlayerEventHandler)Delegate.Combine(this.OnAssignPlayerEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignPlayerEvent = (GameSocket.OnAssignPlayerEventHandler)Delegate.Remove(this.OnAssignPlayerEvent, value);
			}
		}

		public event GameSocket.OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignPlayerCorpseEvent = (GameSocket.OnAssignPlayerCorpseEventHandler)Delegate.Combine(this.OnAssignPlayerCorpseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignPlayerCorpseEvent = (GameSocket.OnAssignPlayerCorpseEventHandler)Delegate.Remove(this.OnAssignPlayerCorpseEvent, value);
			}
		}

		public event GameSocket.OnAssignPlayerToPartyEventHandler OnAssignPlayerToParty
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignPlayerToPartyEvent = (GameSocket.OnAssignPlayerToPartyEventHandler)Delegate.Combine(this.OnAssignPlayerToPartyEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignPlayerToPartyEvent = (GameSocket.OnAssignPlayerToPartyEventHandler)Delegate.Remove(this.OnAssignPlayerToPartyEvent, value);
			}
		}

		public event GameSocket.OnAssignSkillEventHandler OnAssignSkill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignSkillEvent = (GameSocket.OnAssignSkillEventHandler)Delegate.Combine(this.OnAssignSkillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignSkillEvent = (GameSocket.OnAssignSkillEventHandler)Delegate.Remove(this.OnAssignSkillEvent, value);
			}
		}

		public event GameSocket.OnAssignSkillHotkeyEventHandler OnAssignSkillHotkey
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignSkillHotkeyEvent = (GameSocket.OnAssignSkillHotkeyEventHandler)Delegate.Combine(this.OnAssignSkillHotkeyEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignSkillHotkeyEvent = (GameSocket.OnAssignSkillHotkeyEventHandler)Delegate.Remove(this.OnAssignSkillHotkeyEvent, value);
			}
		}

		public event GameSocket.OnAssignWarpEventHandler OnAssignWarp
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAssignWarpEvent = (GameSocket.OnAssignWarpEventHandler)Delegate.Combine(this.OnAssignWarpEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAssignWarpEvent = (GameSocket.OnAssignWarpEventHandler)Delegate.Remove(this.OnAssignWarpEvent, value);
			}
		}

		public event GameSocket.OnAttributeNotificationEventHandler OnAttributeNotification
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnAttributeNotificationEvent = (GameSocket.OnAttributeNotificationEventHandler)Delegate.Combine(this.OnAttributeNotificationEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnAttributeNotificationEvent = (GameSocket.OnAttributeNotificationEventHandler)Delegate.Remove(this.OnAttributeNotificationEvent, value);
			}
		}

		public event GameSocket.OnDelayedStateEventHandler OnDelayedState
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnDelayedStateEvent = (GameSocket.OnDelayedStateEventHandler)Delegate.Combine(this.OnDelayedStateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnDelayedStateEvent = (GameSocket.OnDelayedStateEventHandler)Delegate.Remove(this.OnDelayedStateEvent, value);
			}
		}

		public event GameSocket.OnEndStateEventHandler OnEndState
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnEndStateEvent = (GameSocket.OnEndStateEventHandler)Delegate.Combine(this.OnEndStateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnEndStateEvent = (GameSocket.OnEndStateEventHandler)Delegate.Remove(this.OnEndStateEvent, value);
			}
		}

		public event GameSocket.OnGainExperienceEventHandler OnGainExperience
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGainExperienceEvent = (GameSocket.OnGainExperienceEventHandler)Delegate.Combine(this.OnGainExperienceEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGainExperienceEvent = (GameSocket.OnGainExperienceEventHandler)Delegate.Remove(this.OnGainExperienceEvent, value);
			}
		}

		public event GameSocket.OnGameHandshakeEventHandler OnGameHandshake
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameHandshakeEvent = (GameSocket.OnGameHandshakeEventHandler)Delegate.Combine(this.OnGameHandshakeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameHandshakeEvent = (GameSocket.OnGameHandshakeEventHandler)Delegate.Remove(this.OnGameHandshakeEvent, value);
			}
		}

		public event GameSocket.OnGameLoadingEventHandler OnGameLoading
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameLoadingEvent = (GameSocket.OnGameLoadingEventHandler)Delegate.Combine(this.OnGameLoadingEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameLoadingEvent = (GameSocket.OnGameLoadingEventHandler)Delegate.Remove(this.OnGameLoadingEvent, value);
			}
		}

		public event GameSocket.OnGameLogonReceiptEventHandler OnGameLogonReceipt
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameLogonReceiptEvent = (GameSocket.OnGameLogonReceiptEventHandler)Delegate.Combine(this.OnGameLogonReceiptEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameLogonReceiptEvent = (GameSocket.OnGameLogonReceiptEventHandler)Delegate.Remove(this.OnGameLogonReceiptEvent, value);
			}
		}

		public event GameSocket.OnGameLogonSuccessEventHandler OnGameLogonSuccess
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameLogonSuccessEvent = (GameSocket.OnGameLogonSuccessEventHandler)Delegate.Combine(this.OnGameLogonSuccessEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameLogonSuccessEvent = (GameSocket.OnGameLogonSuccessEventHandler)Delegate.Remove(this.OnGameLogonSuccessEvent, value);
			}
		}

		public event GameSocket.OnGameLogoutSuccessEventHandler OnGameLogoutSuccess
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameLogoutSuccessEvent = (GameSocket.OnGameLogoutSuccessEventHandler)Delegate.Combine(this.OnGameLogoutSuccessEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameLogoutSuccessEvent = (GameSocket.OnGameLogoutSuccessEventHandler)Delegate.Remove(this.OnGameLogoutSuccessEvent, value);
			}
		}

		public event GameSocket.OnGameOverEventHandler OnGameOver
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameOverEvent = (GameSocket.OnGameOverEventHandler)Delegate.Combine(this.OnGameOverEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameOverEvent = (GameSocket.OnGameOverEventHandler)Delegate.Remove(this.OnGameOverEvent, value);
			}
		}

		public event GameSocket.OnGoldTradeEventHandler OnGoldTrade
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGoldTradeEvent = (GameSocket.OnGoldTradeEventHandler)Delegate.Combine(this.OnGoldTradeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGoldTradeEvent = (GameSocket.OnGoldTradeEventHandler)Delegate.Remove(this.OnGoldTradeEvent, value);
			}
		}

		public event GameSocket.OnInformationMessageEventHandler OnInformationMessage
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnInformationMessageEvent = (GameSocket.OnInformationMessageEventHandler)Delegate.Combine(this.OnInformationMessageEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnInformationMessageEvent = (GameSocket.OnInformationMessageEventHandler)Delegate.Remove(this.OnInformationMessageEvent, value);
			}
		}

		public event GameSocket.OnItemTriggerSkillEventHandler OnItemTriggerSkill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnItemTriggerSkillEvent = (GameSocket.OnItemTriggerSkillEventHandler)Delegate.Combine(this.OnItemTriggerSkillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnItemTriggerSkillEvent = (GameSocket.OnItemTriggerSkillEventHandler)Delegate.Remove(this.OnItemTriggerSkillEvent, value);
			}
		}

		public event GameSocket.OnLoadActEventHandler OnLoadAct
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnLoadActEvent = (GameSocket.OnLoadActEventHandler)Delegate.Combine(this.OnLoadActEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnLoadActEvent = (GameSocket.OnLoadActEventHandler)Delegate.Remove(this.OnLoadActEvent, value);
			}
		}

		public event GameSocket.OnLoadDoneEventHandler OnLoadDone
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnLoadDoneEvent = (GameSocket.OnLoadDoneEventHandler)Delegate.Combine(this.OnLoadDoneEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnLoadDoneEvent = (GameSocket.OnLoadDoneEventHandler)Delegate.Remove(this.OnLoadDoneEvent, value);
			}
		}

		public event GameSocket.OnMapAddEventHandler OnMapAdd
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMapAddEvent = (GameSocket.OnMapAddEventHandler)Delegate.Combine(this.OnMapAddEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMapAddEvent = (GameSocket.OnMapAddEventHandler)Delegate.Remove(this.OnMapAddEvent, value);
			}
		}

		public event GameSocket.OnMapRemoveEventHandler OnMapRemove
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMapRemoveEvent = (GameSocket.OnMapRemoveEventHandler)Delegate.Combine(this.OnMapRemoveEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMapRemoveEvent = (GameSocket.OnMapRemoveEventHandler)Delegate.Remove(this.OnMapRemoveEvent, value);
			}
		}

		public event GameSocket.OnMercAttributeNotificationEventHandler OnMercAttributeNotification
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMercAttributeNotificationEvent = (GameSocket.OnMercAttributeNotificationEventHandler)Delegate.Combine(this.OnMercAttributeNotificationEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMercAttributeNotificationEvent = (GameSocket.OnMercAttributeNotificationEventHandler)Delegate.Remove(this.OnMercAttributeNotificationEvent, value);
			}
		}

		public event GameSocket.OnMercForHireEventHandler OnMercForHire
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMercForHireEvent = (GameSocket.OnMercForHireEventHandler)Delegate.Combine(this.OnMercForHireEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMercForHireEvent = (GameSocket.OnMercForHireEventHandler)Delegate.Remove(this.OnMercForHireEvent, value);
			}
		}

		public event GameSocket.OnMercForHireListStartEventHandler OnMercForHireListStart
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMercForHireListStartEvent = (GameSocket.OnMercForHireListStartEventHandler)Delegate.Combine(this.OnMercForHireListStartEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMercForHireListStartEvent = (GameSocket.OnMercForHireListStartEventHandler)Delegate.Remove(this.OnMercForHireListStartEvent, value);
			}
		}

		public event GameSocket.OnMercGainExperienceEventHandler OnMercGainExperience
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMercGainExperienceEvent = (GameSocket.OnMercGainExperienceEventHandler)Delegate.Combine(this.OnMercGainExperienceEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMercGainExperienceEvent = (GameSocket.OnMercGainExperienceEventHandler)Delegate.Remove(this.OnMercGainExperienceEvent, value);
			}
		}

		public event GameSocket.OnMonsterAttackEventHandler OnMonsterAttack
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMonsterAttackEvent = (GameSocket.OnMonsterAttackEventHandler)Delegate.Combine(this.OnMonsterAttackEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMonsterAttackEvent = (GameSocket.OnMonsterAttackEventHandler)Delegate.Remove(this.OnMonsterAttackEvent, value);
			}
		}

		public event GameSocket.OnNPCActionEventHandler OnNPCAction
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCActionEvent = (GameSocket.OnNPCActionEventHandler)Delegate.Combine(this.OnNPCActionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCActionEvent = (GameSocket.OnNPCActionEventHandler)Delegate.Remove(this.OnNPCActionEvent, value);
			}
		}

		public event GameSocket.OnNPCGetHitEventHandler OnNPCGetHit
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCGetHitEvent = (GameSocket.OnNPCGetHitEventHandler)Delegate.Combine(this.OnNPCGetHitEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCGetHitEvent = (GameSocket.OnNPCGetHitEventHandler)Delegate.Remove(this.OnNPCGetHitEvent, value);
			}
		}

		public event GameSocket.OnNPCHealEventHandler OnNPCHeal
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCHealEvent = (GameSocket.OnNPCHealEventHandler)Delegate.Combine(this.OnNPCHealEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCHealEvent = (GameSocket.OnNPCHealEventHandler)Delegate.Remove(this.OnNPCHealEvent, value);
			}
		}

		public event GameSocket.OnNPCInfoEventHandler OnNPCInfo
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCInfoEvent = (GameSocket.OnNPCInfoEventHandler)Delegate.Combine(this.OnNPCInfoEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCInfoEvent = (GameSocket.OnNPCInfoEventHandler)Delegate.Remove(this.OnNPCInfoEvent, value);
			}
		}

		public event GameSocket.OnNPCMoveEventHandler OnNPCMove
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCMoveEvent = (GameSocket.OnNPCMoveEventHandler)Delegate.Combine(this.OnNPCMoveEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCMoveEvent = (GameSocket.OnNPCMoveEventHandler)Delegate.Remove(this.OnNPCMoveEvent, value);
			}
		}

		public event GameSocket.OnNPCMoveToTargetEventHandler OnNPCMoveToTarget
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCMoveToTargetEvent = (GameSocket.OnNPCMoveToTargetEventHandler)Delegate.Combine(this.OnNPCMoveToTargetEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCMoveToTargetEvent = (GameSocket.OnNPCMoveToTargetEventHandler)Delegate.Remove(this.OnNPCMoveToTargetEvent, value);
			}
		}

		public event GameSocket.OnNPCStopEventHandler OnNPCStop
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCStopEvent = (GameSocket.OnNPCStopEventHandler)Delegate.Combine(this.OnNPCStopEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCStopEvent = (GameSocket.OnNPCStopEventHandler)Delegate.Remove(this.OnNPCStopEvent, value);
			}
		}

		public event GameSocket.OnNPCWantsInteractEventHandler OnNPCWantsInteract
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnNPCWantsInteractEvent = (GameSocket.OnNPCWantsInteractEventHandler)Delegate.Combine(this.OnNPCWantsInteractEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnNPCWantsInteractEvent = (GameSocket.OnNPCWantsInteractEventHandler)Delegate.Remove(this.OnNPCWantsInteractEvent, value);
			}
		}

		public event GameSocket.OnOpenWaypointEventHandler OnOpenWaypoint
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnOpenWaypointEvent = (GameSocket.OnOpenWaypointEventHandler)Delegate.Combine(this.OnOpenWaypointEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnOpenWaypointEvent = (GameSocket.OnOpenWaypointEventHandler)Delegate.Remove(this.OnOpenWaypointEvent, value);
			}
		}

		public event GameSocket.OnOwnedItemActionEventHandler OnOwnedItemAction
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnOwnedItemActionEvent = (GameSocket.OnOwnedItemActionEventHandler)Delegate.Combine(this.OnOwnedItemActionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnOwnedItemActionEvent = (GameSocket.OnOwnedItemActionEventHandler)Delegate.Remove(this.OnOwnedItemActionEvent, value);
			}
		}

		public event GameSocket.OnPartyMemberPulseEventHandler OnPartyMemberPulse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPartyMemberPulseEvent = (GameSocket.OnPartyMemberPulseEventHandler)Delegate.Combine(this.OnPartyMemberPulseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPartyMemberPulseEvent = (GameSocket.OnPartyMemberPulseEventHandler)Delegate.Remove(this.OnPartyMemberPulseEvent, value);
			}
		}

		public event GameSocket.OnPartyMemberUpdateEventHandler OnPartyMemberUpdate
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPartyMemberUpdateEvent = (GameSocket.OnPartyMemberUpdateEventHandler)Delegate.Combine(this.OnPartyMemberUpdateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPartyMemberUpdateEvent = (GameSocket.OnPartyMemberUpdateEventHandler)Delegate.Remove(this.OnPartyMemberUpdateEvent, value);
			}
		}

		public event GameSocket.OnPartyRefreshEventHandler OnPartyRefresh
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPartyRefreshEvent = (GameSocket.OnPartyRefreshEventHandler)Delegate.Combine(this.OnPartyRefreshEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPartyRefreshEvent = (GameSocket.OnPartyRefreshEventHandler)Delegate.Remove(this.OnPartyRefreshEvent, value);
			}
		}

		public event GameSocket.OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotification
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerAttributeNotificationEvent = (GameSocket.OnPlayerAttributeNotificationEventHandler)Delegate.Combine(this.OnPlayerAttributeNotificationEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerAttributeNotificationEvent = (GameSocket.OnPlayerAttributeNotificationEventHandler)Delegate.Remove(this.OnPlayerAttributeNotificationEvent, value);
			}
		}

		public event GameSocket.OnPlayerClearCursorEventHandler OnPlayerClearCursor
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerClearCursorEvent = (GameSocket.OnPlayerClearCursorEventHandler)Delegate.Combine(this.OnPlayerClearCursorEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerClearCursorEvent = (GameSocket.OnPlayerClearCursorEventHandler)Delegate.Remove(this.OnPlayerClearCursorEvent, value);
			}
		}

		public event GameSocket.OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisible
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerCorpseVisibleEvent = (GameSocket.OnPlayerCorpseVisibleEventHandler)Delegate.Combine(this.OnPlayerCorpseVisibleEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerCorpseVisibleEvent = (GameSocket.OnPlayerCorpseVisibleEventHandler)Delegate.Remove(this.OnPlayerCorpseVisibleEvent, value);
			}
		}

		public event GameSocket.OnPlayerInGameEventHandler OnPlayerInGame
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerInGameEvent = (GameSocket.OnPlayerInGameEventHandler)Delegate.Combine(this.OnPlayerInGameEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerInGameEvent = (GameSocket.OnPlayerInGameEventHandler)Delegate.Remove(this.OnPlayerInGameEvent, value);
			}
		}

		public event GameSocket.OnPlayerInSightEventHandler OnPlayerInSight
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerInSightEvent = (GameSocket.OnPlayerInSightEventHandler)Delegate.Combine(this.OnPlayerInSightEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerInSightEvent = (GameSocket.OnPlayerInSightEventHandler)Delegate.Remove(this.OnPlayerInSightEvent, value);
			}
		}

		public event GameSocket.OnPlayerKillCountEventHandler OnPlayerKillCount
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerKillCountEvent = (GameSocket.OnPlayerKillCountEventHandler)Delegate.Combine(this.OnPlayerKillCountEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerKillCountEvent = (GameSocket.OnPlayerKillCountEventHandler)Delegate.Remove(this.OnPlayerKillCountEvent, value);
			}
		}

		public event GameSocket.OnPlayerLeaveGameEventHandler OnPlayerLeaveGame
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerLeaveGameEvent = (GameSocket.OnPlayerLeaveGameEventHandler)Delegate.Combine(this.OnPlayerLeaveGameEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerLeaveGameEvent = (GameSocket.OnPlayerLeaveGameEventHandler)Delegate.Remove(this.OnPlayerLeaveGameEvent, value);
			}
		}

		public event GameSocket.OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChange
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerLifeManaChangeEvent = (GameSocket.OnPlayerLifeManaChangeEventHandler)Delegate.Combine(this.OnPlayerLifeManaChangeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerLifeManaChangeEvent = (GameSocket.OnPlayerLifeManaChangeEventHandler)Delegate.Remove(this.OnPlayerLifeManaChangeEvent, value);
			}
		}

		public event GameSocket.OnPlayerMoveEventHandler OnPlayerMove
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerMoveEvent = (GameSocket.OnPlayerMoveEventHandler)Delegate.Combine(this.OnPlayerMoveEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerMoveEvent = (GameSocket.OnPlayerMoveEventHandler)Delegate.Remove(this.OnPlayerMoveEvent, value);
			}
		}

		public event GameSocket.OnPlayerMoveToTargetEventHandler OnPlayerMoveToTarget
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerMoveToTargetEvent = (GameSocket.OnPlayerMoveToTargetEventHandler)Delegate.Combine(this.OnPlayerMoveToTargetEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerMoveToTargetEvent = (GameSocket.OnPlayerMoveToTargetEventHandler)Delegate.Remove(this.OnPlayerMoveToTargetEvent, value);
			}
		}

		public event GameSocket.OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationship
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerPartyRelationshipEvent = (GameSocket.OnPlayerPartyRelationshipEventHandler)Delegate.Combine(this.OnPlayerPartyRelationshipEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerPartyRelationshipEvent = (GameSocket.OnPlayerPartyRelationshipEventHandler)Delegate.Remove(this.OnPlayerPartyRelationshipEvent, value);
			}
		}

		public event GameSocket.OnPlayerReassignEventHandler OnPlayerReassign
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerReassignEvent = (GameSocket.OnPlayerReassignEventHandler)Delegate.Combine(this.OnPlayerReassignEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerReassignEvent = (GameSocket.OnPlayerReassignEventHandler)Delegate.Remove(this.OnPlayerReassignEvent, value);
			}
		}

		public event GameSocket.OnPlayerRelationshipEventHandler OnPlayerRelationship
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerRelationshipEvent = (GameSocket.OnPlayerRelationshipEventHandler)Delegate.Combine(this.OnPlayerRelationshipEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerRelationshipEvent = (GameSocket.OnPlayerRelationshipEventHandler)Delegate.Remove(this.OnPlayerRelationshipEvent, value);
			}
		}

		public event GameSocket.OnPlayerStopEventHandler OnPlayerStop
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlayerStopEvent = (GameSocket.OnPlayerStopEventHandler)Delegate.Combine(this.OnPlayerStopEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlayerStopEvent = (GameSocket.OnPlayerStopEventHandler)Delegate.Remove(this.OnPlayerStopEvent, value);
			}
		}

		public event GameSocket.OnPlaySoundEventHandler OnPlaySound
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPlaySoundEvent = (GameSocket.OnPlaySoundEventHandler)Delegate.Combine(this.OnPlaySoundEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPlaySoundEvent = (GameSocket.OnPlaySoundEventHandler)Delegate.Remove(this.OnPlaySoundEvent, value);
			}
		}

		public event GameSocket.OnPongEventHandler OnPong
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPongEvent = (GameSocket.OnPongEventHandler)Delegate.Combine(this.OnPongEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPongEvent = (GameSocket.OnPongEventHandler)Delegate.Remove(this.OnPongEvent, value);
			}
		}

		public event GameSocket.OnPortalInfoEventHandler OnPortalInfo
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPortalInfoEvent = (GameSocket.OnPortalInfoEventHandler)Delegate.Combine(this.OnPortalInfoEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPortalInfoEvent = (GameSocket.OnPortalInfoEventHandler)Delegate.Remove(this.OnPortalInfoEvent, value);
			}
		}

		public event GameSocket.OnPortalOwnershipEventHandler OnPortalOwnership
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnPortalOwnershipEvent = (GameSocket.OnPortalOwnershipEventHandler)Delegate.Combine(this.OnPortalOwnershipEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnPortalOwnershipEvent = (GameSocket.OnPortalOwnershipEventHandler)Delegate.Remove(this.OnPortalOwnershipEvent, value);
			}
		}

		public event GameSocket.OnQuestItemStateEventHandler OnQuestItemState
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnQuestItemStateEvent = (GameSocket.OnQuestItemStateEventHandler)Delegate.Combine(this.OnQuestItemStateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnQuestItemStateEvent = (GameSocket.OnQuestItemStateEventHandler)Delegate.Remove(this.OnQuestItemStateEvent, value);
			}
		}

		public event GameSocket.OnReceiveMessageEventHandler OnReceiveMessage
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnReceiveMessageEvent = (GameSocket.OnReceiveMessageEventHandler)Delegate.Combine(this.OnReceiveMessageEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnReceiveMessageEvent = (GameSocket.OnReceiveMessageEventHandler)Delegate.Remove(this.OnReceiveMessageEvent, value);
			}
		}

		public event GameSocket.OnRelator1EventHandler OnRelator1
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnRelator1Event = (GameSocket.OnRelator1EventHandler)Delegate.Combine(this.OnRelator1Event, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnRelator1Event = (GameSocket.OnRelator1EventHandler)Delegate.Remove(this.OnRelator1Event, value);
			}
		}

		public event GameSocket.OnRelator2EventHandler OnRelator2
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnRelator2Event = (GameSocket.OnRelator2EventHandler)Delegate.Combine(this.OnRelator2Event, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnRelator2Event = (GameSocket.OnRelator2EventHandler)Delegate.Remove(this.OnRelator2Event, value);
			}
		}

		public event GameSocket.OnRemoveGroundUnitEventHandler OnRemoveGroundUnit
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnRemoveGroundUnitEvent = (GameSocket.OnRemoveGroundUnitEventHandler)Delegate.Combine(this.OnRemoveGroundUnitEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnRemoveGroundUnitEvent = (GameSocket.OnRemoveGroundUnitEventHandler)Delegate.Remove(this.OnRemoveGroundUnitEvent, value);
			}
		}

		public event GameSocket.OnReportKillEventHandler OnReportKill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnReportKillEvent = (GameSocket.OnReportKillEventHandler)Delegate.Combine(this.OnReportKillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnReportKillEvent = (GameSocket.OnReportKillEventHandler)Delegate.Remove(this.OnReportKillEvent, value);
			}
		}

		public event GameSocket.OnRequestLogonInfoEventHandler OnRequestLogonInfo
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnRequestLogonInfoEvent = (GameSocket.OnRequestLogonInfoEventHandler)Delegate.Combine(this.OnRequestLogonInfoEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnRequestLogonInfoEvent = (GameSocket.OnRequestLogonInfoEventHandler)Delegate.Remove(this.OnRequestLogonInfoEvent, value);
			}
		}

		public event GameSocket.OnSetGameObjectModeEventHandler OnSetGameObjectMode
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSetGameObjectModeEvent = (GameSocket.OnSetGameObjectModeEventHandler)Delegate.Combine(this.OnSetGameObjectModeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSetGameObjectModeEvent = (GameSocket.OnSetGameObjectModeEventHandler)Delegate.Remove(this.OnSetGameObjectModeEvent, value);
			}
		}

		public event GameSocket.OnSetItemStateEventHandler OnSetItemState
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSetItemStateEvent = (GameSocket.OnSetItemStateEventHandler)Delegate.Combine(this.OnSetItemStateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSetItemStateEvent = (GameSocket.OnSetItemStateEventHandler)Delegate.Remove(this.OnSetItemStateEvent, value);
			}
		}

		public event GameSocket.OnSetNPCModeEventHandler OnSetNPCMode
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSetNPCModeEvent = (GameSocket.OnSetNPCModeEventHandler)Delegate.Combine(this.OnSetNPCModeEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSetNPCModeEvent = (GameSocket.OnSetNPCModeEventHandler)Delegate.Remove(this.OnSetNPCModeEvent, value);
			}
		}

		public event GameSocket.OnSetStateEventHandler OnSetState
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSetStateEvent = (GameSocket.OnSetStateEventHandler)Delegate.Combine(this.OnSetStateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSetStateEvent = (GameSocket.OnSetStateEventHandler)Delegate.Remove(this.OnSetStateEvent, value);
			}
		}

		public event GameSocket.OnSkillsLogEventHandler OnSkillsLog
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSkillsLogEvent = (GameSocket.OnSkillsLogEventHandler)Delegate.Combine(this.OnSkillsLogEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSkillsLogEvent = (GameSocket.OnSkillsLogEventHandler)Delegate.Remove(this.OnSkillsLogEvent, value);
			}
		}

		public event GameSocket.OnSmallGoldAddEventHandler OnSmallGoldAdd
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSmallGoldAddEvent = (GameSocket.OnSmallGoldAddEventHandler)Delegate.Combine(this.OnSmallGoldAddEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSmallGoldAddEvent = (GameSocket.OnSmallGoldAddEventHandler)Delegate.Remove(this.OnSmallGoldAddEvent, value);
			}
		}

		public event GameSocket.OnSummonActionEventHandler OnSummonAction
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSummonActionEvent = (GameSocket.OnSummonActionEventHandler)Delegate.Combine(this.OnSummonActionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSummonActionEvent = (GameSocket.OnSummonActionEventHandler)Delegate.Remove(this.OnSummonActionEvent, value);
			}
		}

		public event GameSocket.OnSwitchWeaponSetEventHandler OnSwitchWeaponSet
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnSwitchWeaponSetEvent = (GameSocket.OnSwitchWeaponSetEventHandler)Delegate.Combine(this.OnSwitchWeaponSetEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnSwitchWeaponSetEvent = (GameSocket.OnSwitchWeaponSetEventHandler)Delegate.Remove(this.OnSwitchWeaponSetEvent, value);
			}
		}

		public event GameSocket.OnTransactionCompleteEventHandler OnTransactionComplete
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnTransactionCompleteEvent = (GameSocket.OnTransactionCompleteEventHandler)Delegate.Combine(this.OnTransactionCompleteEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnTransactionCompleteEvent = (GameSocket.OnTransactionCompleteEventHandler)Delegate.Remove(this.OnTransactionCompleteEvent, value);
			}
		}

		public event GameSocket.OnUnitUseSkillEventHandler OnUnitUseSkill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUnitUseSkillEvent = (GameSocket.OnUnitUseSkillEventHandler)Delegate.Combine(this.OnUnitUseSkillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUnitUseSkillEvent = (GameSocket.OnUnitUseSkillEventHandler)Delegate.Remove(this.OnUnitUseSkillEvent, value);
			}
		}

		public event GameSocket.OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTarget
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUnitUseSkillOnTargetEvent = (GameSocket.OnUnitUseSkillOnTargetEventHandler)Delegate.Combine(this.OnUnitUseSkillOnTargetEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUnitUseSkillOnTargetEvent = (GameSocket.OnUnitUseSkillOnTargetEventHandler)Delegate.Remove(this.OnUnitUseSkillOnTargetEvent, value);
			}
		}

		public event GameSocket.OnUnloadDoneEventHandler OnUnloadDone
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUnloadDoneEvent = (GameSocket.OnUnloadDoneEventHandler)Delegate.Combine(this.OnUnloadDoneEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUnloadDoneEvent = (GameSocket.OnUnloadDoneEventHandler)Delegate.Remove(this.OnUnloadDoneEvent, value);
			}
		}

		public event GameSocket.OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLog
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateGameQuestLogEvent = (GameSocket.OnUpdateGameQuestLogEventHandler)Delegate.Combine(this.OnUpdateGameQuestLogEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateGameQuestLogEvent = (GameSocket.OnUpdateGameQuestLogEventHandler)Delegate.Remove(this.OnUpdateGameQuestLogEvent, value);
			}
		}

		public event GameSocket.OnUpdateItemStatsEventHandler OnUpdateItemStats
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateItemStatsEvent = (GameSocket.OnUpdateItemStatsEventHandler)Delegate.Combine(this.OnUpdateItemStatsEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateItemStatsEvent = (GameSocket.OnUpdateItemStatsEventHandler)Delegate.Remove(this.OnUpdateItemStatsEvent, value);
			}
		}

		public event GameSocket.OnUpdateItemUIEventHandler OnUpdateItemUI
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateItemUIEvent = (GameSocket.OnUpdateItemUIEventHandler)Delegate.Combine(this.OnUpdateItemUIEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateItemUIEvent = (GameSocket.OnUpdateItemUIEventHandler)Delegate.Remove(this.OnUpdateItemUIEvent, value);
			}
		}

		public event GameSocket.OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdatePlayerItemSkillEvent = (GameSocket.OnUpdatePlayerItemSkillEventHandler)Delegate.Combine(this.OnUpdatePlayerItemSkillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdatePlayerItemSkillEvent = (GameSocket.OnUpdatePlayerItemSkillEventHandler)Delegate.Remove(this.OnUpdatePlayerItemSkillEvent, value);
			}
		}

		public event GameSocket.OnUpdateQuestInfoEventHandler OnUpdateQuestInfo
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateQuestInfoEvent = (GameSocket.OnUpdateQuestInfoEventHandler)Delegate.Combine(this.OnUpdateQuestInfoEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateQuestInfoEvent = (GameSocket.OnUpdateQuestInfoEventHandler)Delegate.Remove(this.OnUpdateQuestInfoEvent, value);
			}
		}

		public event GameSocket.OnUpdateQuestLogEventHandler OnUpdateQuestLog
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateQuestLogEvent = (GameSocket.OnUpdateQuestLogEventHandler)Delegate.Combine(this.OnUpdateQuestLogEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateQuestLogEvent = (GameSocket.OnUpdateQuestLogEventHandler)Delegate.Remove(this.OnUpdateQuestLogEvent, value);
			}
		}

		public event GameSocket.OnUpdateSkillEventHandler OnUpdateSkill
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUpdateSkillEvent = (GameSocket.OnUpdateSkillEventHandler)Delegate.Combine(this.OnUpdateSkillEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUpdateSkillEvent = (GameSocket.OnUpdateSkillEventHandler)Delegate.Remove(this.OnUpdateSkillEvent, value);
			}
		}

		public event GameSocket.OnUseSpecialItemEventHandler OnUseSpecialItem
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUseSpecialItemEvent = (GameSocket.OnUseSpecialItemEventHandler)Delegate.Combine(this.OnUseSpecialItemEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUseSpecialItemEvent = (GameSocket.OnUseSpecialItemEventHandler)Delegate.Remove(this.OnUseSpecialItemEvent, value);
			}
		}

		public event GameSocket.OnUseStackableItemEventHandler OnUseStackableItem
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnUseStackableItemEvent = (GameSocket.OnUseStackableItemEventHandler)Delegate.Combine(this.OnUseStackableItemEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnUseStackableItemEvent = (GameSocket.OnUseStackableItemEventHandler)Delegate.Remove(this.OnUseStackableItemEvent, value);
			}
		}

		public event GameSocket.OnWalkVerifyEventHandler OnWalkVerify
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnWalkVerifyEvent = (GameSocket.OnWalkVerifyEventHandler)Delegate.Combine(this.OnWalkVerifyEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnWalkVerifyEvent = (GameSocket.OnWalkVerifyEventHandler)Delegate.Remove(this.OnWalkVerifyEvent, value);
			}
		}

		public event GameSocket.OnWardenCheckEventHandler OnWardenCheck
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnWardenCheckEvent = (GameSocket.OnWardenCheckEventHandler)Delegate.Combine(this.OnWardenCheckEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnWardenCheckEvent = (GameSocket.OnWardenCheckEventHandler)Delegate.Remove(this.OnWardenCheckEvent, value);
			}
		}

		public event GameSocket.OnWorldItemActionEventHandler OnWorldItemAction
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnWorldItemActionEvent = (GameSocket.OnWorldItemActionEventHandler)Delegate.Combine(this.OnWorldItemActionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnWorldItemActionEvent = (GameSocket.OnWorldItemActionEventHandler)Delegate.Remove(this.OnWorldItemActionEvent, value);
			}
		}

		public GameSocket(ref LogBox LogWriter) : base(SockType.Game)
		{
			base.Disconnection += new Sockets.DisconnectionEventHandler(this.OnDisconnect);
			this.Log = LogWriter;
			new Thread(new ThreadStart(this.PingThread))
			{
				IsBackground = true
			}.Start();
		}

		public void Connect(IPAddress GameServerIp, ProxyInfo Proxy = null)
		{
			this.G_ServerIp = GameServerIp;
			int num = 4000;
			checked
			{
				if (Proxy == null)
				{
					this.WriteToLog("Connecting: " + this.G_ServerIp.ToString(), Color.Gray);
					base.Connect(GameServerIp.ToString(), (ushort)num);
				}
				else
				{
					this.WriteToLog("Connecting W/ Proxy: " + this.G_ServerIp.ToString(), Color.Gray);
					base.Connect(GameServerIp.ToString(), (ushort)num, Proxy.Address, Conversions.ToString((uint)Proxy.Port), Proxy.Username, Proxy.Password);
				}
			}
		}

		private void OnDisconnect()
		{
			this.WriteToLog("Connection Lost..", Color.Red);
		}

		private void PingThread()
		{
			while (true)
			{
				Thread.Sleep(5000);
				Ping packet = new Ping(checked((uint)Environment.TickCount), 0L);
				this.SendPacket(packet);
			}
		}

		public override void ParsePacket(byte[] Data)
		{
			DataReader dataReader = new DataReader(Data);
			GameServerPacket packetID = (GameServerPacket)dataReader.ReadByte();
			this.ReportPacket((int)packetID);
			try
			{
				switch (packetID)
				{
				case GameServerPacket.GameLoading:
				{
					GameSocket.OnGameLoadingEventHandler onGameLoadingEvent = this.OnGameLoadingEvent;
					if (onGameLoadingEvent != null)
					{
						onGameLoadingEvent(new GameLoading(Data));
					}
					break;
				}
				case GameServerPacket.GameLogonReceipt:
				{
					GameSocket.OnGameLogonReceiptEventHandler onGameLogonReceiptEvent = this.OnGameLogonReceiptEvent;
					if (onGameLogonReceiptEvent != null)
					{
						onGameLogonReceiptEvent(new GameLogonReceipt(Data));
					}
					break;
				}
				case GameServerPacket.GameLogonSuccess:
				{
					GameSocket.OnGameLogonSuccessEventHandler onGameLogonSuccessEvent = this.OnGameLogonSuccessEvent;
					if (onGameLogonSuccessEvent != null)
					{
						onGameLogonSuccessEvent(new GameLogonSuccess(Data));
					}
					break;
				}
				case GameServerPacket.LoadAct:
				{
					GameSocket.OnLoadActEventHandler onLoadActEvent = this.OnLoadActEvent;
					if (onLoadActEvent != null)
					{
						onLoadActEvent(new LoadAct(Data));
					}
					break;
				}
				case GameServerPacket.LoadDone:
				{
					GameSocket.OnLoadDoneEventHandler onLoadDoneEvent = this.OnLoadDoneEvent;
					if (onLoadDoneEvent != null)
					{
						onLoadDoneEvent(new LoadDone(Data));
					}
					break;
				}
				case GameServerPacket.UnloadDone:
				{
					GameSocket.OnUnloadDoneEventHandler onUnloadDoneEvent = this.OnUnloadDoneEvent;
					if (onUnloadDoneEvent != null)
					{
						onUnloadDoneEvent(new UnloadDone(Data));
					}
					break;
				}
				case GameServerPacket.GameLogoutSuccess:
				{
					GameSocket.OnGameLogoutSuccessEventHandler onGameLogoutSuccessEvent = this.OnGameLogoutSuccessEvent;
					if (onGameLogoutSuccessEvent != null)
					{
						onGameLogoutSuccessEvent(new GameLogoutSuccess(Data));
					}
					break;
				}
				case GameServerPacket.MapAdd:
				{
					GameSocket.OnMapAddEventHandler onMapAddEvent = this.OnMapAddEvent;
					if (onMapAddEvent != null)
					{
						onMapAddEvent(new MapAdd(Data));
					}
					break;
				}
				case GameServerPacket.MapRemove:
				{
					GameSocket.OnMapRemoveEventHandler onMapRemoveEvent = this.OnMapRemoveEvent;
					if (onMapRemoveEvent != null)
					{
						onMapRemoveEvent(new MapRemove(Data));
					}
					break;
				}
				case GameServerPacket.AssignWarp:
				{
					GameSocket.OnAssignWarpEventHandler onAssignWarpEvent = this.OnAssignWarpEvent;
					if (onAssignWarpEvent != null)
					{
						onAssignWarpEvent(new AssignWarp(Data));
					}
					break;
				}
				case GameServerPacket.RemoveGroundUnit:
				{
					GameSocket.OnRemoveGroundUnitEventHandler onRemoveGroundUnitEvent = this.OnRemoveGroundUnitEvent;
					if (onRemoveGroundUnitEvent != null)
					{
						onRemoveGroundUnitEvent(new RemoveGroundUnit(Data));
					}
					break;
				}
				case GameServerPacket.GameHandshake:
				{
					GameSocket.OnGameHandshakeEventHandler onGameHandshakeEvent = this.OnGameHandshakeEvent;
					if (onGameHandshakeEvent != null)
					{
						onGameHandshakeEvent(new GameHandshake(Data));
					}
					break;
				}
				case GameServerPacket.NPCGetHit:
				{
					GameSocket.OnNPCGetHitEventHandler onNPCGetHitEvent = this.OnNPCGetHitEvent;
					if (onNPCGetHitEvent != null)
					{
						onNPCGetHitEvent(new NPCGetHit(Data));
					}
					break;
				}
				case GameServerPacket.PlayerStop:
				{
					GameSocket.OnPlayerStopEventHandler onPlayerStopEvent = this.OnPlayerStopEvent;
					if (onPlayerStopEvent != null)
					{
						onPlayerStopEvent(new PlayerStop(Data));
					}
					break;
				}
				case GameServerPacket.SetGameObjectMode:
				{
					GameSocket.OnSetGameObjectModeEventHandler onSetGameObjectModeEvent = this.OnSetGameObjectModeEvent;
					if (onSetGameObjectModeEvent != null)
					{
						onSetGameObjectModeEvent(new SetGameObjectMode(Data));
					}
					break;
				}
				case GameServerPacket.PlayerMove:
				{
					GameSocket.OnPlayerMoveEventHandler onPlayerMoveEvent = this.OnPlayerMoveEvent;
					if (onPlayerMoveEvent != null)
					{
						onPlayerMoveEvent(new PlayerMove(Data));
					}
					break;
				}
				case GameServerPacket.PlayerMoveToTarget:
				{
					GameSocket.OnPlayerMoveToTargetEventHandler onPlayerMoveToTargetEvent = this.OnPlayerMoveToTargetEvent;
					if (onPlayerMoveToTargetEvent != null)
					{
						onPlayerMoveToTargetEvent(new PlayerMoveToTarget(Data));
					}
					break;
				}
				case GameServerPacket.ReportKill:
				{
					GameSocket.OnReportKillEventHandler onReportKillEvent = this.OnReportKillEvent;
					if (onReportKillEvent != null)
					{
						onReportKillEvent(new ReportKill(Data));
					}
					break;
				}
				case GameServerPacket.PlayerReassign:
				{
					GameSocket.OnPlayerReassignEventHandler onPlayerReassignEvent = this.OnPlayerReassignEvent;
					if (onPlayerReassignEvent != null)
					{
						onPlayerReassignEvent(new PlayerReassign(Data));
					}
					break;
				}
				case GameServerPacket.SmallGoldAdd:
				{
					GameSocket.OnSmallGoldAddEventHandler onSmallGoldAddEvent = this.OnSmallGoldAddEvent;
					if (onSmallGoldAddEvent != null)
					{
						onSmallGoldAddEvent(new SmallGoldAdd(Data));
					}
					break;
				}
				case GameServerPacket.ByteToExperience:
				{
					GameSocket.OnGainExperienceEventHandler onGainExperienceEvent = this.OnGainExperienceEvent;
					if (onGainExperienceEvent != null)
					{
						onGainExperienceEvent(new ByteToExperience(Data));
					}
					break;
				}
				case GameServerPacket.WordToExperience:
				{
					GameSocket.OnGainExperienceEventHandler onGainExperienceEvent = this.OnGainExperienceEvent;
					if (onGainExperienceEvent != null)
					{
						onGainExperienceEvent(new WordToExperience(Data));
					}
					break;
				}
				case GameServerPacket.DWordToExperience:
				{
					GameSocket.OnGainExperienceEventHandler onGainExperienceEvent = this.OnGainExperienceEvent;
					if (onGainExperienceEvent != null)
					{
						onGainExperienceEvent(new DWordToExperience(Data));
					}
					break;
				}
				case GameServerPacket.AttributeByte:
				{
					GameSocket.OnAttributeNotificationEventHandler onAttributeNotificationEvent = this.OnAttributeNotificationEvent;
					if (onAttributeNotificationEvent != null)
					{
						onAttributeNotificationEvent(new AttributeByte(Data));
					}
					break;
				}
				case GameServerPacket.AttributeWord:
				{
					GameSocket.OnAttributeNotificationEventHandler onAttributeNotificationEvent = this.OnAttributeNotificationEvent;
					if (onAttributeNotificationEvent != null)
					{
						onAttributeNotificationEvent(new AttributeWord(Data));
					}
					break;
				}
				case GameServerPacket.AttributeDWord:
				{
					GameSocket.OnAttributeNotificationEventHandler onAttributeNotificationEvent = this.OnAttributeNotificationEvent;
					if (onAttributeNotificationEvent != null)
					{
						onAttributeNotificationEvent(new AttributeDWord(Data));
					}
					break;
				}
				case GameServerPacket.PlayerAttributeNotification:
				{
					GameSocket.OnPlayerAttributeNotificationEventHandler onPlayerAttributeNotificationEvent = this.OnPlayerAttributeNotificationEvent;
					if (onPlayerAttributeNotificationEvent != null)
					{
						onPlayerAttributeNotificationEvent(new PlayerAttributeNotification(Data));
					}
					break;
				}
				case GameServerPacket.UpdateSkill:
				{
					GameSocket.OnUpdateSkillEventHandler onUpdateSkillEvent = this.OnUpdateSkillEvent;
					if (onUpdateSkillEvent != null)
					{
						onUpdateSkillEvent(new UpdateSkill(Data));
					}
					break;
				}
				case GameServerPacket.UpdatePlayerItemSkill:
				{
					GameSocket.OnUpdatePlayerItemSkillEventHandler onUpdatePlayerItemSkillEvent = this.OnUpdatePlayerItemSkillEvent;
					if (onUpdatePlayerItemSkillEvent != null)
					{
						onUpdatePlayerItemSkillEvent(new UpdatePlayerItemSkill(Data));
					}
					break;
				}
				case GameServerPacket.AssignSkill:
				{
					GameSocket.OnAssignSkillEventHandler onAssignSkillEvent = this.OnAssignSkillEvent;
					if (onAssignSkillEvent != null)
					{
						onAssignSkillEvent(new AssignSkill(Data));
					}
					break;
				}
				case GameServerPacket.GameMessage:
				{
					GameSocket.OnReceiveMessageEventHandler onReceiveMessageEvent = this.OnReceiveMessageEvent;
					if (onReceiveMessageEvent != null)
					{
						onReceiveMessageEvent(new GameMessage(Data));
					}
					break;
				}
				case GameServerPacket.NPCInfo:
				{
					GameSocket.OnNPCInfoEventHandler onNPCInfoEvent = this.OnNPCInfoEvent;
					if (onNPCInfoEvent != null)
					{
						onNPCInfoEvent(new NPCInfo(Data));
					}
					break;
				}
				case GameServerPacket.UpdateQuestInfo:
				{
					GameSocket.OnUpdateQuestInfoEventHandler onUpdateQuestInfoEvent = this.OnUpdateQuestInfoEvent;
					if (onUpdateQuestInfoEvent != null)
					{
						onUpdateQuestInfoEvent(new UpdateQuestInfo(Data));
					}
					break;
				}
				case GameServerPacket.UpdateGameQuestLog:
				{
					GameSocket.OnUpdateGameQuestLogEventHandler onUpdateGameQuestLogEvent = this.OnUpdateGameQuestLogEvent;
					if (onUpdateGameQuestLogEvent != null)
					{
						onUpdateGameQuestLogEvent(new UpdateGameQuestLog(Data));
					}
					break;
				}
				case GameServerPacket.TransactionComplete:
				{
					GameSocket.OnTransactionCompleteEventHandler onTransactionCompleteEvent = this.OnTransactionCompleteEvent;
					if (onTransactionCompleteEvent != null)
					{
						onTransactionCompleteEvent(new TransactionComplete(Data));
					}
					break;
				}
				case GameServerPacket.PlaySound:
				{
					GameSocket.OnPlaySoundEventHandler onPlaySoundEvent = this.OnPlaySoundEvent;
					if (onPlaySoundEvent != null)
					{
						onPlaySoundEvent(new PlaySound(Data));
					}
					break;
				}
				case GameServerPacket.UpdateItemStats:
				{
					GameSocket.OnUpdateItemStatsEventHandler onUpdateItemStatsEvent = this.OnUpdateItemStatsEvent;
					if (onUpdateItemStatsEvent != null)
					{
						onUpdateItemStatsEvent(new UpdateItemStats(Data));
					}
					break;
				}
				case GameServerPacket.UseStackableItem:
				{
					GameSocket.OnUseStackableItemEventHandler onUseStackableItemEvent = this.OnUseStackableItemEvent;
					if (onUseStackableItemEvent != null)
					{
						onUseStackableItemEvent(new UseStackableItem(Data));
					}
					break;
				}
				case GameServerPacket.PlayerClearCursor:
				{
					GameSocket.OnPlayerClearCursorEventHandler onPlayerClearCursorEvent = this.OnPlayerClearCursorEvent;
					if (onPlayerClearCursorEvent != null)
					{
						onPlayerClearCursorEvent(new PlayerClearCursor(Data));
					}
					break;
				}
				case GameServerPacket.Relator1:
				{
					GameSocket.OnRelator1EventHandler onRelator1Event = this.OnRelator1Event;
					if (onRelator1Event != null)
					{
						onRelator1Event(new Relator1(Data));
					}
					break;
				}
				case GameServerPacket.Relator2:
				{
					GameSocket.OnRelator2EventHandler onRelator2Event = this.OnRelator2Event;
					if (onRelator2Event != null)
					{
						onRelator2Event(new Relator2(Data));
					}
					break;
				}
				case GameServerPacket.UnitUseSkillOnTarget:
				{
					GameSocket.OnUnitUseSkillOnTargetEventHandler onUnitUseSkillOnTargetEvent = this.OnUnitUseSkillOnTargetEvent;
					if (onUnitUseSkillOnTargetEvent != null)
					{
						onUnitUseSkillOnTargetEvent(new UnitUseSkillOnTarget(Data));
					}
					break;
				}
				case GameServerPacket.UnitUseSkill:
				{
					GameSocket.OnUnitUseSkillEventHandler onUnitUseSkillEvent = this.OnUnitUseSkillEvent;
					if (onUnitUseSkillEvent != null)
					{
						onUnitUseSkillEvent(new UnitUseSkill(Data));
					}
					break;
				}
				case GameServerPacket.MercForHire:
				{
					GameSocket.OnMercForHireEventHandler onMercForHireEvent = this.OnMercForHireEvent;
					if (onMercForHireEvent != null)
					{
						onMercForHireEvent(new MercForHire(Data));
					}
					break;
				}
				case GameServerPacket.MercForHireListStart:
				{
					GameSocket.OnMercForHireListStartEventHandler onMercForHireListStartEvent = this.OnMercForHireListStartEvent;
					if (onMercForHireListStartEvent != null)
					{
						onMercForHireListStartEvent(new MercForHireListStart(Data));
					}
					break;
				}
				case GameServerPacket.AssignGameObject:
				{
					GameSocket.OnAssignGameObjectEventHandler onAssignGameObjectEvent = this.OnAssignGameObjectEvent;
					if (onAssignGameObjectEvent != null)
					{
						onAssignGameObjectEvent(new AssignGameObject(Data));
					}
					break;
				}
				case GameServerPacket.UpdateQuestLog:
				{
					GameSocket.OnUpdateQuestLogEventHandler onUpdateQuestLogEvent = this.OnUpdateQuestLogEvent;
					if (onUpdateQuestLogEvent != null)
					{
						onUpdateQuestLogEvent(new UpdateQuestLog(Data));
					}
					break;
				}
				case GameServerPacket.PartyRefresh:
				{
					GameSocket.OnPartyRefreshEventHandler onPartyRefreshEvent = this.OnPartyRefreshEvent;
					if (onPartyRefreshEvent != null)
					{
						onPartyRefreshEvent(new PartyRefresh(Data));
					}
					break;
				}
				case GameServerPacket.AssignPlayer:
				{
					GameSocket.OnAssignPlayerEventHandler onAssignPlayerEvent = this.OnAssignPlayerEvent;
					if (onAssignPlayerEvent != null)
					{
						onAssignPlayerEvent(new AssignPlayer(Data));
					}
					break;
				}
				case GameServerPacket.InformationMessage:
				{
					GameSocket.OnInformationMessageEventHandler onInformationMessageEvent = this.OnInformationMessageEvent;
					if (onInformationMessageEvent != null)
					{
						onInformationMessageEvent(new InformationMessage(Data));
					}
					break;
				}
				case GameServerPacket.PlayerInGame:
				{
					GameSocket.OnPlayerInGameEventHandler onPlayerInGameEvent = this.OnPlayerInGameEvent;
					if (onPlayerInGameEvent != null)
					{
						onPlayerInGameEvent(new PlayerInGame(Data));
					}
					break;
				}
				case GameServerPacket.PlayerLeaveGame:
				{
					GameSocket.OnPlayerLeaveGameEventHandler onPlayerLeaveGameEvent = this.OnPlayerLeaveGameEvent;
					if (onPlayerLeaveGameEvent != null)
					{
						onPlayerLeaveGameEvent(new PlayerLeaveGame(Data));
					}
					break;
				}
				case GameServerPacket.QuestItemState:
				{
					GameSocket.OnQuestItemStateEventHandler onQuestItemStateEvent = this.OnQuestItemStateEvent;
					if (onQuestItemStateEvent != null)
					{
						onQuestItemStateEvent(new QuestItemState(Data));
					}
					break;
				}
				case GameServerPacket.PortalInfo:
				{
					GameSocket.OnPortalInfoEventHandler onPortalInfoEvent = this.OnPortalInfoEvent;
					if (onPortalInfoEvent != null)
					{
						onPortalInfoEvent(new PortalInfo(Data));
					}
					break;
				}
				case GameServerPacket.OpenWaypoint:
				{
					GameSocket.OnOpenWaypointEventHandler onOpenWaypointEvent = this.OnOpenWaypointEvent;
					if (onOpenWaypointEvent != null)
					{
						onOpenWaypointEvent(new OpenWaypoint(Data));
					}
					break;
				}
				case GameServerPacket.PlayerKillCount:
				{
					GameSocket.OnPlayerKillCountEventHandler onPlayerKillCountEvent = this.OnPlayerKillCountEvent;
					if (onPlayerKillCountEvent != null)
					{
						onPlayerKillCountEvent(new PlayerKillCount(Data));
					}
					break;
				}
				case GameServerPacket.NPCMove:
				{
					GameSocket.OnNPCMoveEventHandler onNPCMoveEvent = this.OnNPCMoveEvent;
					if (onNPCMoveEvent != null)
					{
						onNPCMoveEvent(new NPCMove(Data));
					}
					break;
				}
				case GameServerPacket.NPCMoveToTarget:
				{
					GameSocket.OnNPCMoveToTargetEventHandler onNPCMoveToTargetEvent = this.OnNPCMoveToTargetEvent;
					if (onNPCMoveToTargetEvent != null)
					{
						onNPCMoveToTargetEvent(new NPCMoveToTarget(Data));
					}
					break;
				}
				case GameServerPacket.SetNPCMode:
				{
					GameSocket.OnSetNPCModeEventHandler onSetNPCModeEvent = this.OnSetNPCModeEvent;
					if (onSetNPCModeEvent != null)
					{
						onSetNPCModeEvent(new SetNPCMode(Data));
					}
					break;
				}
				case GameServerPacket.NPCAction:
				{
					GameSocket.OnNPCActionEventHandler onNPCActionEvent = this.OnNPCActionEvent;
					if (onNPCActionEvent != null)
					{
						onNPCActionEvent(new NPCAction(Data));
					}
					break;
				}
				case GameServerPacket.MonsterAttack:
				{
					GameSocket.OnMonsterAttackEventHandler onMonsterAttackEvent = this.OnMonsterAttackEvent;
					if (onMonsterAttackEvent != null)
					{
						onMonsterAttackEvent(new MonsterAttack(Data));
					}
					break;
				}
				case GameServerPacket.NPCStop:
				{
					GameSocket.OnNPCStopEventHandler onNPCStopEvent = this.OnNPCStopEvent;
					if (onNPCStopEvent != null)
					{
						onNPCStopEvent(new NPCStop(Data));
					}
					break;
				}
				case GameServerPacket.PlayerCorpseVisible:
				{
					GameSocket.OnPlayerCorpseVisibleEventHandler onPlayerCorpseVisibleEvent = this.OnPlayerCorpseVisibleEvent;
					if (onPlayerCorpseVisibleEvent != null)
					{
						onPlayerCorpseVisibleEvent(new PlayerCorpseVisible(Data));
					}
					break;
				}
				case GameServerPacket.AboutPlayer:
				{
					GameSocket.OnAboutPlayerEventHandler onAboutPlayerEvent = this.OnAboutPlayerEvent;
					if (onAboutPlayerEvent != null)
					{
						onAboutPlayerEvent(new AboutPlayer(Data));
					}
					break;
				}
				case GameServerPacket.PlayerInSight:
				{
					GameSocket.OnPlayerInSightEventHandler onPlayerInSightEvent = this.OnPlayerInSightEvent;
					if (onPlayerInSightEvent != null)
					{
						onPlayerInSightEvent(new PlayerInSight(Data));
					}
					break;
				}
				case GameServerPacket.UpdateItemUI:
				{
					GameSocket.OnUpdateItemUIEventHandler onUpdateItemUIEvent = this.OnUpdateItemUIEvent;
					if (onUpdateItemUIEvent != null)
					{
						onUpdateItemUIEvent(new UpdateItemUI(Data));
					}
					break;
				}
				case GameServerPacket.AcceptTrade:
				{
					GameSocket.OnAcceptTradeEventHandler onAcceptTradeEvent = this.OnAcceptTradeEvent;
					if (onAcceptTradeEvent != null)
					{
						onAcceptTradeEvent(new AcceptTrade(Data));
					}
					break;
				}
				case GameServerPacket.GoldTrade:
				{
					GameSocket.OnGoldTradeEventHandler onGoldTradeEvent = this.OnGoldTradeEvent;
					if (onGoldTradeEvent != null)
					{
						onGoldTradeEvent(new GoldTrade(Data));
					}
					break;
				}
				case GameServerPacket.SummonAction:
				{
					GameSocket.OnSummonActionEventHandler onSummonActionEvent = this.OnSummonActionEvent;
					if (onSummonActionEvent != null)
					{
						onSummonActionEvent(new SummonAction(Data));
					}
					break;
				}
				case GameServerPacket.AssignSkillHotkey:
				{
					GameSocket.OnAssignSkillHotkeyEventHandler onAssignSkillHotkeyEvent = this.OnAssignSkillHotkeyEvent;
					if (onAssignSkillHotkeyEvent != null)
					{
						onAssignSkillHotkeyEvent(new AssignSkillHotkey(Data));
					}
					break;
				}
				case GameServerPacket.UseSpecialItem:
				{
					GameSocket.OnUseSpecialItemEventHandler onUseSpecialItemEvent = this.OnUseSpecialItemEvent;
					if (onUseSpecialItemEvent != null)
					{
						onUseSpecialItemEvent(new UseSpecialItem(Data));
					}
					break;
				}
				case GameServerPacket.SetItemState:
				{
					GameSocket.OnSetItemStateEventHandler onSetItemStateEvent = this.OnSetItemStateEvent;
					if (onSetItemStateEvent != null)
					{
						onSetItemStateEvent(new SetItemState(Data));
					}
					break;
				}
				case GameServerPacket.PartyMemberUpdate:
				{
					GameSocket.OnPartyMemberUpdateEventHandler onPartyMemberUpdateEvent = this.OnPartyMemberUpdateEvent;
					if (onPartyMemberUpdateEvent != null)
					{
						onPartyMemberUpdateEvent(new PartyMemberUpdate(Data));
					}
					break;
				}
				case GameServerPacket.AssignMerc:
				{
					GameSocket.OnAssignMercEventHandler onAssignMercEvent = this.OnAssignMercEvent;
					if (onAssignMercEvent != null)
					{
						onAssignMercEvent(new AssignMerc(Data));
					}
					break;
				}
				case GameServerPacket.PortalOwnership:
				{
					GameSocket.OnPortalOwnershipEventHandler onPortalOwnershipEvent = this.OnPortalOwnershipEvent;
					if (onPortalOwnershipEvent != null)
					{
						onPortalOwnershipEvent(new PortalOwnership(Data));
					}
					break;
				}
				case GameServerPacket.NPCWantsInteract:
				{
					GameSocket.OnNPCWantsInteractEventHandler onNPCWantsInteractEvent = this.OnNPCWantsInteractEvent;
					if (onNPCWantsInteractEvent != null)
					{
						onNPCWantsInteractEvent(new NPCWantsInteract(Data));
					}
					break;
				}
				case GameServerPacket.PlayerPartyRelationship:
				{
					GameSocket.OnPlayerPartyRelationshipEventHandler onPlayerPartyRelationshipEvent = this.OnPlayerPartyRelationshipEvent;
					if (onPlayerPartyRelationshipEvent != null)
					{
						onPlayerPartyRelationshipEvent(new PlayerPartyRelationship(Data));
					}
					break;
				}
				case GameServerPacket.PlayerRelationship:
				{
					GameSocket.OnPlayerRelationshipEventHandler onPlayerRelationshipEvent = this.OnPlayerRelationshipEvent;
					if (onPlayerRelationshipEvent != null)
					{
						onPlayerRelationshipEvent(new PlayerRelationship(Data));
					}
					break;
				}
				case GameServerPacket.AssignPlayerToParty:
				{
					GameSocket.OnAssignPlayerToPartyEventHandler onAssignPlayerToPartyEvent = this.OnAssignPlayerToPartyEvent;
					if (onAssignPlayerToPartyEvent != null)
					{
						onAssignPlayerToPartyEvent(new AssignPlayerToParty(Data));
					}
					break;
				}
				case GameServerPacket.AssignPlayerCorpse:
				{
					GameSocket.OnAssignPlayerCorpseEventHandler onAssignPlayerCorpseEvent = this.OnAssignPlayerCorpseEvent;
					if (onAssignPlayerCorpseEvent != null)
					{
						onAssignPlayerCorpseEvent(new AssignPlayerCorpse(Data));
					}
					break;
				}
				case GameServerPacket.Pong:
				{
					GameSocket.OnPongEventHandler onPongEvent = this.OnPongEvent;
					if (onPongEvent != null)
					{
						onPongEvent(new Pong(Data));
					}
					break;
				}
				case GameServerPacket.PartyMemberPulse:
				{
					GameSocket.OnPartyMemberPulseEventHandler onPartyMemberPulseEvent = this.OnPartyMemberPulseEvent;
					if (onPartyMemberPulseEvent != null)
					{
						onPartyMemberPulseEvent(new PartyMemberPulse(Data));
					}
					break;
				}
				case GameServerPacket.SkillsLog:
				{
					GameSocket.OnSkillsLogEventHandler onSkillsLogEvent = this.OnSkillsLogEvent;
					if (onSkillsLogEvent != null)
					{
						onSkillsLogEvent(new SkillsLog(Data));
					}
					break;
				}
				case GameServerPacket.PlayerLifeManaChange:
				{
					GameSocket.OnPlayerLifeManaChangeEventHandler onPlayerLifeManaChangeEvent = this.OnPlayerLifeManaChangeEvent;
					if (onPlayerLifeManaChangeEvent != null)
					{
						onPlayerLifeManaChangeEvent(new PlayerLifeManaChange(Data));
					}
					break;
				}
				case GameServerPacket.WalkVerify:
				{
					GameSocket.OnWalkVerifyEventHandler onWalkVerifyEvent = this.OnWalkVerifyEvent;
					if (onWalkVerifyEvent != null)
					{
						onWalkVerifyEvent(new WalkVerify(Data));
					}
					break;
				}
				case GameServerPacket.SwitchWeaponSet:
				{
					GameSocket.OnSwitchWeaponSetEventHandler onSwitchWeaponSetEvent = this.OnSwitchWeaponSetEvent;
					if (onSwitchWeaponSetEvent != null)
					{
						onSwitchWeaponSetEvent(new SwitchWeaponSet(Data));
					}
					break;
				}
				case GameServerPacket.ItemTriggerSkill:
				{
					GameSocket.OnItemTriggerSkillEventHandler onItemTriggerSkillEvent = this.OnItemTriggerSkillEvent;
					if (onItemTriggerSkillEvent != null)
					{
						onItemTriggerSkillEvent(new ItemTriggerSkill(Data));
					}
					break;
				}
				case GameServerPacket.WorldItemAction:
				{
					GameSocket.OnWorldItemActionEventHandler onWorldItemActionEvent = this.OnWorldItemActionEvent;
					if (onWorldItemActionEvent != null)
					{
						onWorldItemActionEvent(new WorldItemAction(Data));
					}
					break;
				}
				case GameServerPacket.OwnedItemAction:
				{
					GameSocket.OnOwnedItemActionEventHandler onOwnedItemActionEvent = this.OnOwnedItemActionEvent;
					if (onOwnedItemActionEvent != null)
					{
						onOwnedItemActionEvent(new OwnedItemAction(Data));
					}
					break;
				}
				case GameServerPacket.MercAttributeByte:
				{
					GameSocket.OnMercAttributeNotificationEventHandler onMercAttributeNotificationEvent = this.OnMercAttributeNotificationEvent;
					if (onMercAttributeNotificationEvent != null)
					{
						onMercAttributeNotificationEvent(new MercAttributeByte(Data));
					}
					break;
				}
				case GameServerPacket.MercAttributeWord:
				{
					GameSocket.OnMercAttributeNotificationEventHandler onMercAttributeNotificationEvent = this.OnMercAttributeNotificationEvent;
					if (onMercAttributeNotificationEvent != null)
					{
						onMercAttributeNotificationEvent(new MercAttributeWord(Data));
					}
					break;
				}
				case GameServerPacket.MercAttributeDWord:
				{
					GameSocket.OnMercAttributeNotificationEventHandler onMercAttributeNotificationEvent = this.OnMercAttributeNotificationEvent;
					if (onMercAttributeNotificationEvent != null)
					{
						onMercAttributeNotificationEvent(new MercAttributeDWord(Data));
					}
					break;
				}
				case GameServerPacket.MercByteToExperience:
				{
					GameSocket.OnMercGainExperienceEventHandler onMercGainExperienceEvent = this.OnMercGainExperienceEvent;
					if (onMercGainExperienceEvent != null)
					{
						onMercGainExperienceEvent(new MercByteToExperience(Data));
					}
					break;
				}
				case GameServerPacket.MercWordToExperience:
				{
					GameSocket.OnMercGainExperienceEventHandler onMercGainExperienceEvent = this.OnMercGainExperienceEvent;
					if (onMercGainExperienceEvent != null)
					{
						onMercGainExperienceEvent(new MercWordToExperience(Data));
					}
					break;
				}
				case GameServerPacket.DelayedState:
				{
					GameSocket.OnDelayedStateEventHandler onDelayedStateEvent = this.OnDelayedStateEvent;
					if (onDelayedStateEvent != null)
					{
						onDelayedStateEvent(new DelayedState(Data));
					}
					break;
				}
				case GameServerPacket.SetState:
				{
					GameSocket.OnSetStateEventHandler onSetStateEvent = this.OnSetStateEvent;
					if (onSetStateEvent != null)
					{
						onSetStateEvent(new SetState(Data));
					}
					break;
				}
				case GameServerPacket.EndState:
				{
					GameSocket.OnEndStateEventHandler onEndStateEvent = this.OnEndStateEvent;
					if (onEndStateEvent != null)
					{
						onEndStateEvent(new EndState(Data));
					}
					break;
				}
				case GameServerPacket.AddUnit:
				{
					GameSocket.OnAddUnitEventHandler onAddUnitEvent = this.OnAddUnitEvent;
					if (onAddUnitEvent != null)
					{
						onAddUnitEvent(new AddUnit(Data));
					}
					break;
				}
				case GameServerPacket.NPCHeal:
				{
					GameSocket.OnNPCHealEventHandler onNPCHealEvent = this.OnNPCHealEvent;
					if (onNPCHealEvent != null)
					{
						onNPCHealEvent(new NPCHeal(Data));
					}
					break;
				}
				case GameServerPacket.AssignNPC:
				{
					GameSocket.OnAssignNPCEventHandler onAssignNPCEvent = this.OnAssignNPCEvent;
					if (onAssignNPCEvent != null)
					{
						onAssignNPCEvent(new AssignNPC(Data));
					}
					break;
				}
				case GameServerPacket.WardenCheck:
				{
					GameSocket.OnWardenCheckEventHandler onWardenCheckEvent = this.OnWardenCheckEvent;
					if (onWardenCheckEvent != null)
					{
						onWardenCheckEvent(new WardenCheck(Data));
					}
					break;
				}
				case GameServerPacket.RequestLogonInfo:
				{
					GameSocket.OnRequestLogonInfoEventHandler onRequestLogonInfoEvent = this.OnRequestLogonInfoEvent;
					if (onRequestLogonInfoEvent != null)
					{
						onRequestLogonInfoEvent(new RequestLogonInfo(Data));
					}
					break;
				}
				case GameServerPacket.GameOver:
				{
					GameSocket.OnGameOverEventHandler onGameOverEvent = this.OnGameOverEvent;
					if (onGameOverEvent != null)
					{
						onGameOverEvent(new GameOver(Data));
					}
					break;
				}
				}
			}
			catch (Exception expr_100D)
			{
				ProjectData.SetProjectError(expr_100D);
				ProjectData.ClearProjectError();
			}
		}

		public void SendPacket(GCPacket Packet)
		{
			this.SendPacket(Packet.Data);
		}

		public void SendMessage(string Message)
		{
			SendMessage packet = new SendMessage(GameMessageType.GameMessage, Message, null);
			this.SendPacket(packet);
		}

		public void ExitGame()
		{
			ExitGame packet = new ExitGame();
			this.SendPacket(packet);
			this.Disconnect();
		}

		public void WriteToLog(string Text, Color Color)
		{
			this.Log.AddLine("[GAME] " + Text, Color, HorizontalAlignment.Left);
		}
	}
}
