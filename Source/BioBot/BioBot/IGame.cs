using BotCore.GameClient;
using BotCore.GameServer;
using System;
using System.Drawing;
using System.Net;

namespace BioBot
{
	public interface IGame
	{
		public delegate void OnConnectedEventHandler();

		public delegate void OnDisconnectedEventHandler();

		public delegate void UpdateEventHandler();

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

		public delegate void OnReceiveMessageEventHandler(GameMessage Packet);

		public delegate void OnGameOverEventHandler(GameOver Packet);

		public delegate void OnGoldTradeEventHandler(GoldTrade Packet);

		public delegate void OnInformationMessageEventHandler(InformationMessage Packet);

		public delegate void OnOwnedItemActionEventHandler(OwnedItemAction Packet);

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

		event IGame.OnConnectedEventHandler OnConnected;

		event IGame.OnDisconnectedEventHandler OnDisconnected;

		event IGame.UpdateEventHandler Update;

		event IGame.OnAboutPlayerEventHandler OnAboutPlayer;

		event IGame.OnAcceptTradeEventHandler OnAcceptTrade;

		event IGame.OnAddUnitEventHandler OnAddUnit;

		event IGame.OnAssignGameObjectEventHandler OnAssignGameObject;

		event IGame.OnAssignMercEventHandler OnAssignMerc;

		event IGame.OnAssignNPCEventHandler OnAssignNPC;

		event IGame.OnAssignPlayerEventHandler OnAssignPlayer;

		event IGame.OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpse;

		event IGame.OnAssignPlayerToPartyEventHandler OnAssignPlayerToParty;

		event IGame.OnAssignSkillEventHandler OnAssignSkill;

		event IGame.OnAssignSkillHotkeyEventHandler OnAssignSkillHotkey;

		event IGame.OnAssignWarpEventHandler OnAssignWarp;

		event IGame.OnAttributeNotificationEventHandler OnAttributeNotification;

		event IGame.OnDelayedStateEventHandler OnDelayedState;

		event IGame.OnEndStateEventHandler OnEndState;

		event IGame.OnGainExperienceEventHandler OnGainExperience;

		event IGame.OnGameHandshakeEventHandler OnGameHandshake;

		event IGame.OnGameLoadingEventHandler OnGameLoading;

		event IGame.OnGameLogonReceiptEventHandler OnGameLogonReceipt;

		event IGame.OnGameLogonSuccessEventHandler OnGameLogonSuccess;

		event IGame.OnGameLogoutSuccessEventHandler OnGameLogoutSuccess;

		event IGame.OnReceiveMessageEventHandler OnReceiveMessage;

		event IGame.OnGameOverEventHandler OnGameOver;

		event IGame.OnGoldTradeEventHandler OnGoldTrade;

		event IGame.OnInformationMessageEventHandler OnInformationMessage;

		event IGame.OnOwnedItemActionEventHandler OnOwnedItemAction;

		event IGame.OnItemTriggerSkillEventHandler OnItemTriggerSkill;

		event IGame.OnLoadActEventHandler OnLoadAct;

		event IGame.OnLoadDoneEventHandler OnLoadDone;

		event IGame.OnMapAddEventHandler OnMapAdd;

		event IGame.OnMapRemoveEventHandler OnMapRemove;

		event IGame.OnMercAttributeNotificationEventHandler OnMercAttributeNotification;

		event IGame.OnMercForHireEventHandler OnMercForHire;

		event IGame.OnMercForHireListStartEventHandler OnMercForHireListStart;

		event IGame.OnMercGainExperienceEventHandler OnMercGainExperience;

		event IGame.OnMonsterAttackEventHandler OnMonsterAttack;

		event IGame.OnNPCActionEventHandler OnNPCAction;

		event IGame.OnNPCGetHitEventHandler OnNPCGetHit;

		event IGame.OnNPCHealEventHandler OnNPCHeal;

		event IGame.OnNPCInfoEventHandler OnNPCInfo;

		event IGame.OnNPCMoveEventHandler OnNPCMove;

		event IGame.OnNPCMoveToTargetEventHandler OnNPCMoveToTarget;

		event IGame.OnNPCStopEventHandler OnNPCStop;

		event IGame.OnNPCWantsInteractEventHandler OnNPCWantsInteract;

		event IGame.OnOpenWaypointEventHandler OnOpenWaypoint;

		event IGame.OnPartyMemberPulseEventHandler OnPartyMemberPulse;

		event IGame.OnPartyMemberUpdateEventHandler OnPartyMemberUpdate;

		event IGame.OnPartyRefreshEventHandler OnPartyRefresh;

		event IGame.OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotification;

		event IGame.OnPlayerClearCursorEventHandler OnPlayerClearCursor;

		event IGame.OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisible;

		event IGame.OnPlayerInGameEventHandler OnPlayerInGame;

		event IGame.OnPlayerInSightEventHandler OnPlayerInSight;

		event IGame.OnPlayerKillCountEventHandler OnPlayerKillCount;

		event IGame.OnPlayerLeaveGameEventHandler OnPlayerLeaveGame;

		event IGame.OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChange;

		event IGame.OnPlayerMoveEventHandler OnPlayerMove;

		event IGame.OnPlayerMoveToTargetEventHandler OnPlayerMoveToTarget;

		event IGame.OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationship;

		event IGame.OnPlayerReassignEventHandler OnPlayerReassign;

		event IGame.OnPlayerRelationshipEventHandler OnPlayerRelationship;

		event IGame.OnPlayerStopEventHandler OnPlayerStop;

		event IGame.OnPlaySoundEventHandler OnPlaySound;

		event IGame.OnPongEventHandler OnPong;

		event IGame.OnPortalInfoEventHandler OnPortalInfo;

		event IGame.OnPortalOwnershipEventHandler OnPortalOwnership;

		event IGame.OnQuestItemStateEventHandler OnQuestItemState;

		event IGame.OnRelator1EventHandler OnRelator1;

		event IGame.OnRelator2EventHandler OnRelator2;

		event IGame.OnRemoveGroundUnitEventHandler OnRemoveGroundUnit;

		event IGame.OnReportKillEventHandler OnReportKill;

		event IGame.OnRequestLogonInfoEventHandler OnRequestLogonInfo;

		event IGame.OnSetGameObjectModeEventHandler OnSetGameObjectMode;

		event IGame.OnSetItemStateEventHandler OnSetItemState;

		event IGame.OnSetNPCModeEventHandler OnSetNPCMode;

		event IGame.OnSetStateEventHandler OnSetState;

		event IGame.OnSkillsLogEventHandler OnSkillsLog;

		event IGame.OnSmallGoldAddEventHandler OnSmallGoldAdd;

		event IGame.OnSummonActionEventHandler OnSummonAction;

		event IGame.OnSwitchWeaponSetEventHandler OnSwitchWeaponSet;

		event IGame.OnTransactionCompleteEventHandler OnTransactionComplete;

		event IGame.OnUnitUseSkillEventHandler OnUnitUseSkill;

		event IGame.OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTarget;

		event IGame.OnUnloadDoneEventHandler OnUnloadDone;

		event IGame.OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLog;

		event IGame.OnUpdateItemStatsEventHandler OnUpdateItemStats;

		event IGame.OnUpdateItemUIEventHandler OnUpdateItemUI;

		event IGame.OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkill;

		event IGame.OnUpdateQuestInfoEventHandler OnUpdateQuestInfo;

		event IGame.OnUpdateQuestLogEventHandler OnUpdateQuestLog;

		event IGame.OnUpdateSkillEventHandler OnUpdateSkill;

		event IGame.OnUseSpecialItemEventHandler OnUseSpecialItem;

		event IGame.OnUseStackableItemEventHandler OnUseStackableItem;

		event IGame.OnWalkVerifyEventHandler OnWalkVerify;

		event IGame.OnWardenCheckEventHandler OnWardenCheck;

		event IGame.OnWorldItemActionEventHandler OnWorldItemAction;

		bool Connected
		{
			get;
		}

		void WriteToLog(string Text, Color Color);

		void Connect(IPAddress GameServerIp);

		void Disconnect();

		void SendPacket(byte[] bytes);

		void SendPacket(GCPacket Packet);

		void SendMessage(string Message);

		void ExitGame();
	}
}
