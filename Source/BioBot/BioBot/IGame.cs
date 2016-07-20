using BotCore.GameClient;
using BotCore.GameServer;
using System;
using System.Drawing;
using System.Net;

namespace BioBot
{
    namespace Game
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
    }

    public interface IGame
	{
		event Game.OnConnectedEventHandler OnConnected;

		event Game.OnDisconnectedEventHandler OnDisconnected;

		event Game.UpdateEventHandler Update;

		event Game.OnAboutPlayerEventHandler OnAboutPlayer;

		event Game.OnAcceptTradeEventHandler OnAcceptTrade;

		event Game.OnAddUnitEventHandler OnAddUnit;

		event Game.OnAssignGameObjectEventHandler OnAssignGameObject;

		event Game.OnAssignMercEventHandler OnAssignMerc;

		event Game.OnAssignNPCEventHandler OnAssignNPC;

		event Game.OnAssignPlayerEventHandler OnAssignPlayer;

		event Game.OnAssignPlayerCorpseEventHandler OnAssignPlayerCorpse;

		event Game.OnAssignPlayerToPartyEventHandler OnAssignPlayerToParty;

		event Game.OnAssignSkillEventHandler OnAssignSkill;

		event Game.OnAssignSkillHotkeyEventHandler OnAssignSkillHotkey;

		event Game.OnAssignWarpEventHandler OnAssignWarp;

		event Game.OnAttributeNotificationEventHandler OnAttributeNotification;

		event Game.OnDelayedStateEventHandler OnDelayedState;

		event Game.OnEndStateEventHandler OnEndState;

		event Game.OnGainExperienceEventHandler OnGainExperience;

		event Game.OnGameHandshakeEventHandler OnGameHandshake;

		event Game.OnGameLoadingEventHandler OnGameLoading;

		event Game.OnGameLogonReceiptEventHandler OnGameLogonReceipt;

		event Game.OnGameLogonSuccessEventHandler OnGameLogonSuccess;

		event Game.OnGameLogoutSuccessEventHandler OnGameLogoutSuccess;

		event Game.OnReceiveMessageEventHandler OnReceiveMessage;

		event Game.OnGameOverEventHandler OnGameOver;

		event Game.OnGoldTradeEventHandler OnGoldTrade;

		event Game.OnInformationMessageEventHandler OnInformationMessage;

		event Game.OnOwnedItemActionEventHandler OnOwnedItemAction;

		event Game.OnItemTriggerSkillEventHandler OnItemTriggerSkill;

		event Game.OnLoadActEventHandler OnLoadAct;

		event Game.OnLoadDoneEventHandler OnLoadDone;

		event Game.OnMapAddEventHandler OnMapAdd;

		event Game.OnMapRemoveEventHandler OnMapRemove;

		event Game.OnMercAttributeNotificationEventHandler OnMercAttributeNotification;

		event Game.OnMercForHireEventHandler OnMercForHire;

		event Game.OnMercForHireListStartEventHandler OnMercForHireListStart;

		event Game.OnMercGainExperienceEventHandler OnMercGainExperience;

		event Game.OnMonsterAttackEventHandler OnMonsterAttack;

		event Game.OnNPCActionEventHandler OnNPCAction;

		event Game.OnNPCGetHitEventHandler OnNPCGetHit;

		event Game.OnNPCHealEventHandler OnNPCHeal;

		event Game.OnNPCInfoEventHandler OnNPCInfo;

		event Game.OnNPCMoveEventHandler OnNPCMove;

		event Game.OnNPCMoveToTargetEventHandler OnNPCMoveToTarget;

		event Game.OnNPCStopEventHandler OnNPCStop;

		event Game.OnNPCWantsInteractEventHandler OnNPCWantsInteract;

		event Game.OnOpenWaypointEventHandler OnOpenWaypoint;

		event Game.OnPartyMemberPulseEventHandler OnPartyMemberPulse;

		event Game.OnPartyMemberUpdateEventHandler OnPartyMemberUpdate;

		event Game.OnPartyRefreshEventHandler OnPartyRefresh;

		event Game.OnPlayerAttributeNotificationEventHandler OnPlayerAttributeNotification;

		event Game.OnPlayerClearCursorEventHandler OnPlayerClearCursor;

		event Game.OnPlayerCorpseVisibleEventHandler OnPlayerCorpseVisible;

		event Game.OnPlayerInGameEventHandler OnPlayerInGame;

		event Game.OnPlayerInSightEventHandler OnPlayerInSight;

		event Game.OnPlayerKillCountEventHandler OnPlayerKillCount;

		event Game.OnPlayerLeaveGameEventHandler OnPlayerLeaveGame;

		event Game.OnPlayerLifeManaChangeEventHandler OnPlayerLifeManaChange;

		event Game.OnPlayerMoveEventHandler OnPlayerMove;

		event Game.OnPlayerMoveToTargetEventHandler OnPlayerMoveToTarget;

		event Game.OnPlayerPartyRelationshipEventHandler OnPlayerPartyRelationship;

		event Game.OnPlayerReassignEventHandler OnPlayerReassign;

		event Game.OnPlayerRelationshipEventHandler OnPlayerRelationship;

		event Game.OnPlayerStopEventHandler OnPlayerStop;

		event Game.OnPlaySoundEventHandler OnPlaySound;

		event Game.OnPongEventHandler OnPong;

		event Game.OnPortalInfoEventHandler OnPortalInfo;

		event Game.OnPortalOwnershipEventHandler OnPortalOwnership;

		event Game.OnQuestItemStateEventHandler OnQuestItemState;

		event Game.OnRelator1EventHandler OnRelator1;

		event Game.OnRelator2EventHandler OnRelator2;

		event Game.OnRemoveGroundUnitEventHandler OnRemoveGroundUnit;

		event Game.OnReportKillEventHandler OnReportKill;

		event Game.OnRequestLogonInfoEventHandler OnRequestLogonInfo;

		event Game.OnSetGameObjectModeEventHandler OnSetGameObjectMode;

		event Game.OnSetItemStateEventHandler OnSetItemState;

		event Game.OnSetNPCModeEventHandler OnSetNPCMode;

		event Game.OnSetStateEventHandler OnSetState;

		event Game.OnSkillsLogEventHandler OnSkillsLog;

		event Game.OnSmallGoldAddEventHandler OnSmallGoldAdd;

		event Game.OnSummonActionEventHandler OnSummonAction;

		event Game.OnSwitchWeaponSetEventHandler OnSwitchWeaponSet;

		event Game.OnTransactionCompleteEventHandler OnTransactionComplete;

		event Game.OnUnitUseSkillEventHandler OnUnitUseSkill;

		event Game.OnUnitUseSkillOnTargetEventHandler OnUnitUseSkillOnTarget;

		event Game.OnUnloadDoneEventHandler OnUnloadDone;

		event Game.OnUpdateGameQuestLogEventHandler OnUpdateGameQuestLog;

		event Game.OnUpdateItemStatsEventHandler OnUpdateItemStats;

		event Game.OnUpdateItemUIEventHandler OnUpdateItemUI;

		event Game.OnUpdatePlayerItemSkillEventHandler OnUpdatePlayerItemSkill;

		event Game.OnUpdateQuestInfoEventHandler OnUpdateQuestInfo;

		event Game.OnUpdateQuestLogEventHandler OnUpdateQuestLog;

		event Game.OnUpdateSkillEventHandler OnUpdateSkill;

		event Game.OnUseSpecialItemEventHandler OnUseSpecialItem;

		event Game.OnUseStackableItemEventHandler OnUseStackableItem;

		event Game.OnWalkVerifyEventHandler OnWalkVerify;

		event Game.OnWardenCheckEventHandler OnWardenCheck;

		event Game.OnWorldItemActionEventHandler OnWorldItemAction;

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
