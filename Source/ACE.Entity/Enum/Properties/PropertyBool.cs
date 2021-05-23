using System.ComponentModel;

namespace ACE.Entity.Enum.Properties
{
    public enum PropertyBool : ushort
    {
        // properties marked as ServerOnly are properties we never saw in PCAPs, from here:
        // http://ac.yotesfan.com/ace_object/not_used_enums.php
        // source: @OptimShi
        // description attributes are used by the weenie editor for a cleaner display name

        Undef                            = 0,
        [Ephemeral][ServerOnly]
        Stuck                            = 1,
        [Ephemeral]
        Open                             = 2,
        Locked                           = 3,
        RotProof                         = 4,
        AllegianceUpdateRequest          = 5,
        AiUsesMana                       = 6,
        AiUseHumanMagicAnimations        = 7,
        AllowGive                        = 8,
        CurrentlyAttacking               = 9,
        AttackerAi                       = 10,
        [ServerOnly]
        IgnoreCollisions                 = 11,
        [ServerOnly]
        ReportCollisions                 = 12,
        [ServerOnly]
        Ethereal                         = 13,
        [ServerOnly]
        GravityStatus                    = 14,
        [ServerOnly]
        LightsStatus                     = 15,
        [ServerOnly]
        ScriptedCollision                = 16,
        [ServerOnly]
        Inelastic                        = 17,
        [ServerOnly][Ephemeral]
        Visibility                       = 18,
        [ServerOnly]
        Attackable                       = 19,
        SafeSpellComponents              = 20,
        AdvocateState                    = 21,
        Inscribable                      = 22,
        DestroyOnSell                    = 23,
        UiHidden                         = 24,
        IgnoreHouseBarriers              = 25,
        HiddenAdmin                      = 26,
        PkWounder                        = 27,
        PkKiller                         = 28,
        NoCorpse                         = 29,
        UnderLifestoneProtection         = 30,
        ItemManaUpdatePending            = 31,
        [Ephemeral]
        GeneratorStatus                  = 32,
        [Ephemeral]
        ResetMessagePending              = 33,
        DefaultOpen                      = 34,
        DefaultLocked                    = 35,
        DefaultOn                        = 36,
        OpenForBusiness                  = 37,
        IsFrozen                         = 38,
        DealMagicalItems                 = 39,
        LogoffImDead                     = 40,
        ReportCollisionsAsEnvironment    = 41,
        AllowEdgeSlide                   = 42,
        AdvocateQuest                    = 43,
        [Ephemeral][SendOnLogin]
        IsAdmin                          = 44,
        [Ephemeral][SendOnLogin]
        IsArch                           = 45,
        [Ephemeral][SendOnLogin]
        IsSentinel                       = 46,
        [SendOnLogin]
        IsAdvocate                       = 47,
        CurrentlyPoweringUp              = 48,
        [Ephemeral]
        GeneratorEnteredWorld            = 49,
        NeverFailCasting                 = 50,
        VendorService                    = 51,
        AiImmobile                       = 52,
        DamagedByCollisions              = 53,
        IsDynamic                        = 54,
        IsHot                            = 55,
        IsAffecting                      = 56,
        AffectsAis                       = 57,
        SpellQueueActive                 = 58,
        [Ephemeral]
        GeneratorDisabled                = 59,
        IsAcceptingTells                 = 60,
        LoggingChannel                   = 61,
        OpensAnyLock                     = 62,
        UnlimitedUse                     = 63,
        GeneratedTreasureItem            = 64,
        IgnoreMagicResist                = 65,
        IgnoreMagicArmor                 = 66,
        AiAllowTrade                     = 67,
        [SendOnLogin]
        SpellComponentsRequired          = 68,
        IsSellable                       = 69,
        IgnoreShieldsBySkill             = 70,
        NoDraw                           = 71,
        ActivationUntargeted             = 72,
        HouseHasGottenPriorityBootPos    = 73,
        [Ephemeral]
        GeneratorAutomaticDestruction    = 74,
        HouseHooksVisible                = 75,
        HouseRequiresMonarch             = 76,
        HouseHooksEnabled                = 77,
        HouseNotifiedHudOfHookCount      = 78,
        AiAcceptEverything               = 79,
        IgnorePortalRestrictions         = 80,
        RequiresBackpackSlot             = 81,
        DontTurnOrMoveWhenGiving         = 82,
        [ServerOnly]
        NpcLooksLikeObject               = 83,
        IgnoreCloIcons                   = 84,
        AppraisalHasAllowedWielder       = 85,
        ChestRegenOnClose                = 86,
        LogoffInMinigame                 = 87,
        PortalShowDestination            = 88,
        PortalIgnoresPkAttackTimer       = 89,
        NpcInteractsSilently             = 90,
        Retained                         = 91,
        IgnoreAuthor                     = 92,
        Limbo                            = 93,
        AppraisalHasAllowedActivator     = 94,
        ExistedBeforeAllegianceXpChanges = 95,
        IsDeaf                           = 96,
        [Ephemeral][SendOnLogin]
        IsPsr                            = 97,
        Invincible                       = 98,
        Ivoryable                        = 99,
        Dyable                           = 100,
        CanGenerateRare                  = 101,
        CorpseGeneratedRare              = 102,
        NonProjectileMagicImmune         = 103,
        [SendOnLogin]
        ActdReceivedItems                = 104,
        Unknown105                       = 105,
        [Ephemeral]
        FirstEnterWorldDone              = 106,
        RecallsDisabled                  = 107,
        RareUsesTimer                    = 108,
        ActdPreorderReceivedItems        = 109,
        [Ephemeral]
        Afk                              = 110,
        IsGagged                         = 111,
        ProcSpellSelfTargeted            = 112,
        IsAllegianceGagged               = 113,
        EquipmentSetTriggerPiece         = 114,
        Uninscribe                       = 115,
        WieldOnUse                       = 116,
        ChestClearedWhenClosed           = 117,
        NeverAttack                      = 118,
        SuppressGenerateEffect           = 119,
        TreasureCorpse                   = 120,
        EquipmentSetAddLevel             = 121,
        BarberActive                     = 122,
        TopLayerPriority                 = 123,
        NoHeldItemShown                  = 124,
        LoginAtLifestone                 = 125,
        OlthoiPk                         = 126,
        [SendOnLogin]
        Account15Days                    = 127,
        HadNoVitae                       = 128,
        NoOlthoiTalk                     = 129,
        AutowieldLeft                    = 130,

        /* custom */
        [ServerOnly]
        LinkedPortalOneSummon            = 9001,
        [ServerOnly]
        LinkedPortalTwoSummon            = 9002,
        [ServerOnly]
        HouseEvicted                     = 9003,
        [ServerOnly]
        UntrainedSkills                  = 9004,
        [Ephemeral][ServerOnly]
        IsEnvoy                          = 9005,
        [ServerOnly]
        UnspecializedSkills              = 9006,
        [ServerOnly]
        FreeSkillResetRenewed            = 9007,
        [ServerOnly]
        FreeAttributeResetRenewed        = 9008,
        [ServerOnly]
        SkillTemplesTimerReset           = 9009,
        [ServerOnly]
        CombatPetUpgraded                = 9010,
        [ServerOnly]
        UpgradedUber1                    = 9011,

        // achievements
        [ServerOnly]
        HP1                              = 9012,
        [ServerOnly]
        HP2                              = 9013,
        [ServerOnly]
        HP3                              = 9014,
        [ServerOnly]
        HP4                              = 9015,
        [ServerOnly]
        HP5                              = 9016,
        [ServerOnly]
        HP6                              = 9017,

        [ServerOnly]
        ST1                              = 9018,
        [ServerOnly]
        ST2                              = 9019,
        [ServerOnly]
        ST3                              = 9020,
        [ServerOnly]
        ST4                              = 9021,
        [ServerOnly]
        ST5                              = 9022,
        [ServerOnly]
        ST6                              = 9023,

        [ServerOnly]
        MA1                              = 9024,
        [ServerOnly]
        MA2                              = 9025,
        [ServerOnly]
        MA3                              = 9026,
        [ServerOnly]
        MA4                              = 9027,
        [ServerOnly]
        MA5                              = 9028,
        [ServerOnly]
        MA6                              = 9029,

        [ServerOnly]
        ENL1                             = 9030,
        [ServerOnly]
        ENL2                             = 9031,
        [ServerOnly]
        ENL3                             = 9032,
        [ServerOnly]
        ENL4                             = 9033,
        [ServerOnly]
        ENL5                             = 9034,
        [ServerOnly]
        ENL6                             = 9035,

        [ServerOnly]
        AT1                              = 9036,
        [ServerOnly]
        AT2                              = 9037,
        [ServerOnly]
        AT3                              = 9038,
        [ServerOnly]
        AT4                              = 9039,
        [ServerOnly]
        AT5                              = 9040,
        [ServerOnly]
        AT6                              = 9041,

        [ServerOnly]
        LV1                              = 9042,
        [ServerOnly]
        LV2                              = 9043,
        [ServerOnly]
        LV3                              = 9044,
        [ServerOnly]
        LV4                              = 9045,
        [ServerOnly]
        LV5                              = 9046,
        [ServerOnly]
        LV6                              = 9047,

        [ServerOnly]
        MC1                              = 9048,
        [ServerOnly]
        MC2                              = 9049,
        [ServerOnly]
        MC3                              = 9050,
        [ServerOnly]
        MC4                              = 9051,
        [ServerOnly]
        MC5                              = 9052,
        [ServerOnly]
        MC6                              = 9053,

        [ServerOnly]
        HMC1                             = 9054,
        [ServerOnly]
        HMC2                             = 9055,
        [ServerOnly]
        HMC3                             = 9056,
        [ServerOnly]
        HMC4                             = 9057,
        [ServerOnly]
        HMC5                             = 9058,
        [ServerOnly]
        HMC6                             = 9059,

        [ServerOnly]
        TS1                              = 9060,
        [ServerOnly]
        TS2                              = 9061,
        [ServerOnly]
        TS3                              = 9062,
        [ServerOnly]
        TS4                              = 9063,
        [ServerOnly]
        TS5                              = 9064,
        [ServerOnly]
        TS6                              = 9065,

        [ServerOnly]
        CS1                              = 9066,
        [ServerOnly]
        CS2                              = 9067,
        [ServerOnly]
        CS3                              = 9068,
        [ServerOnly]
        CS4                              = 9069,
        [ServerOnly]
        CS5                              = 9070,
        [ServerOnly]
        CS6                              = 9071,

        [ServerOnly]
        CSD1                             = 9072,
        [ServerOnly]
        CSD2                             = 9073,
        [ServerOnly]
        CSD3                             = 9074,
        [ServerOnly]
        CSD4                             = 9075,
        [ServerOnly]
        CSD5                             = 9076,
        [ServerOnly]
        CSD6                             = 9077,

        [ServerOnly]
        SCB1                             = 9078,
        [ServerOnly]
        SCB2                             = 9079,
        [ServerOnly]
        SCB3                             = 9080,
        [ServerOnly]
        SCB4                             = 9081,
        [ServerOnly]
        SCB5                             = 9082,
        [ServerOnly]
        SCB6                             = 9083,

        [ServerOnly]
        MAC1                             = 9084,
        [ServerOnly]
        MAC2                             = 9085,
        [ServerOnly]
        MAC3                             = 9086,
        [ServerOnly]
        MAC4                             = 9087,
        [ServerOnly]
        MAC5                             = 9088,
        [ServerOnly]
        MAC6                             = 9089,

        [ServerOnly]
        SD1                              = 9090,
        [ServerOnly]
        SD2                              = 9091,
        [ServerOnly]
        SD3                              = 9092,
        [ServerOnly]
        SD4                              = 9093,
        [ServerOnly]
        SD5                              = 9094,
        [ServerOnly]
        SD6                              = 9095,

        [ServerOnly]
        V1                               = 9096,
        [ServerOnly]
        V2                               = 9097,
        [ServerOnly]
        V3                               = 9098,
        [ServerOnly]
        V4                               = 9099,
        [ServerOnly]
        V5                               = 9100,
        [ServerOnly]
        V6                               = 9101,

        [ServerOnly]
        AM1                              = 9102,
        [ServerOnly]
        AM2                              = 9103,
        [ServerOnly]
        AM3                              = 9104,
        [ServerOnly]
        AM4                              = 9105,
        [ServerOnly]
        AM5                              = 9106,
        [ServerOnly]
        AM6                              = 9107,

        [ServerOnly]
        TAC1                             = 9108,
        [ServerOnly]
        TAC2                             = 9109,
        [ServerOnly]
        TAC3                             = 9110,
        [ServerOnly]
        TAC4                             = 9111,
        [ServerOnly]
        TAC5                             = 9112,
        [ServerOnly]
        TAC6                             = 9113,
    }

    public static class PropertyBoolExtensions
    {
        public static string GetDescription(this PropertyBool prop)
        {
            var description = prop.GetAttributeOfType<DescriptionAttribute>();
            return description?.Description ?? prop.ToString();
        }
    }
}
