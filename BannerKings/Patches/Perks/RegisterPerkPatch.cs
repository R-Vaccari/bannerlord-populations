﻿using HarmonyLib;
using Helpers;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using BannerKings.Utils;
using BannerKings.Settings;
using TaleWorlds.Library;
using BannerKings.Managers.Skills;
using static BannerKings.Utils.PerksHelpers;
using BannerKings.Patches.Perks;
using BannerKings.Managers.Court.Members;

namespace BannerKings.Patches
{
    internal partial class PerksAndSkillsPatches
    {
        public static Dictionary<string, PerkData> AllPerksData { get; private set; } = new Dictionary<string, PerkData>();

        #region DefaultPerks.Steward
        public static Dictionary<string, PerkData> StewardPerksData { get; set; } = new Dictionary<string, PerkData>()
            {
                //Steward.Frugal               
                {"StewardFrugal",
                   //this._stewardFrugal.Initialize("{=eJIbMa8P}Frugal", DefaultSkills.Steward, this.GetTierCost(1), this._stewardWarriorsDiet, "{=CJB5HCsI}{VALUE} wages in your party", SkillEffect.PerkRole.Quartermaster, -0.05f, SkillEffect.EffectIncrementType.AddFactor, "{=OTyYJ2Bt}{VALUE} recruitment costs", SkillEffect.PerkRole.PartyLeader, -0.15f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                   new PerkData (){
                        PrimaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} party wages for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",

                                      DescriptionOthers = "{VALUE} party wages for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartyLeader,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} recruitment costs for each {EVERYSKILLMAIN} steward point if the hero is the party leader",
                                      DescriptionOthers = "{VALUE} recruitment costs for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}
                   }
                },
                //Steward.WarriorsDiet                
                {"StewardWarriorsDiet",
                   //this._stewardWarriorsDiet.Initialize("{=mIDsxe1O}Warrior's Diet", DefaultSkills.Steward, this.GetTierCost(1), this._stewardFrugal, "{=6NHvsrrx}{VALUE} food consumption in your party", SkillEffect.PerkRole.Quartermaster, -0.1f, SkillEffect.EffectIncrementType.AddFactor, "{=mSvfxXVW}No morale penalty from having single type of food", SkillEffect.PerkRole.PartyLeader, 0f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                   new PerkData(){
                        PrimaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =15 ,EverySkillSecondary = 15 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.Quartermaster },
                                      DescriptionMain = "{VALUE} party food consumption for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                      DescriptionOthers = "{VALUE} party food consumption for each {EVERYSKILLOTHERS} steward point if the hero is a party member" }
                   }
                },
                //Steward.DrillSergant               
                {"StewardDrillSergant",
                  //this._stewardSevenVeterans.Initialize("{=2ryLuN2i}Seven Veterans", DefaultSkills.Steward, this.GetTierCost(2), this._stewardDrillSergant, "{=gX0edfpK}{VALUE} daily experience for tier 4+ troops in your party", SkillEffect.PerkRole.Quartermaster, 4f, SkillEffect.EffectIncrementType.Add, "{=g9gTYB8u}{VALUE} militia recruitment in the governed settlement", SkillEffect.PerkRole.Governor, 1f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 30 ,EverySkillMain =25 ,EverySkillSecondary = 25 ,EverySkillOthers = 100 ,SkillScale = SkillScale.Both,EverySkillCourtMember = 60, CourtPosition =DefaultCouncilPositions.MARSHL,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyLeader, (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} daily experience to troops in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster or the party leader",
                                      DescriptionCourt="{VALUE} daily experience to troops in your party for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                      DescriptionOthers = "{VALUE} daily experience to troops in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member",},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 40 ,EverySkillOthers = 100 ,SkillScale = SkillScale.Other,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} garrison wages in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                      DescriptionSecondary = "{VALUE} garrison wages in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                      DescriptionOthers = "{VALUE} garrison wages in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.SevenVeterans
                {"StewardSevenVeterans",
                   //this._stewardSevenVeterans.Initialize("{=2ryLuN2i}Seven Veterans", DefaultSkills.Steward, this.GetTierCost(2), this._stewardDrillSergant, "{=gX0edfpK}{VALUE} daily experience for tier 4+ troops in your party", SkillEffect.PerkRole.Quartermaster, 4f, SkillEffect.EffectIncrementType.Add, "{=g9gTYB8u}{VALUE} militia recruitment in the governed settlement", SkillEffect.PerkRole.Governor, 1f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                   new PerkData(){
                        PrimaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 2f ,MinBonus=0 ,MaxBonus = 60 ,EverySkillMain =25 ,EverySkillSecondary = 25 ,EverySkillOthers = 100 ,SkillScale = SkillScale.Both,EverySkillCourtMember = 60, CourtPosition =DefaultCouncilPositions.MARSHL,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyLeader, (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} daily experience to tier 4+ troops in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster or the party leader",
                                       DescriptionCourt="{VALUE} daily experience to tier 4+ troops in your party for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                      DescriptionOthers = "{VALUE} daily experience to tier 4+ troops in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member",
                                                                      },
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 10 ,EverySkillMain =50 ,EverySkillSecondary = 100 ,EverySkillOthers = 150 ,SkillScale = SkillScale.Other,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} militia recruitment in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                      DescriptionSecondary = "{VALUE} militia recruitment in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                      DescriptionOthers = "{VALUE} militia recruitment in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.StiffUpperLip
                {"StewardStiffUpperLip",
                  //this._stewardStiffUpperLip.Initialize("{=QUeJ4gc3}Stiff Upper Lip", DefaultSkills.Steward, this.GetTierCost(3), this._stewardSweatshops, "{=y9AsEMnV}{VALUE} food consumption in your party while it is part of an army", SkillEffect.PerkRole.Quartermaster, -0.1f, SkillEffect.EffectIncrementType.AddFactor, "{=1FPpHasQ}{VALUE} garrison wages in the governed castle", SkillEffect.PerkRole.Governor, -0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);          
                  new PerkData(){
                        PrimaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =15 ,EverySkillSecondary = 15 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} party food consumption while it is part of an army for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                      DescriptionOthers = "{VALUE} party food consumption while it is part of an army for each {EVERYSKILLOTHERS} steward point if the hero is a party member" },
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 40 ,EverySkillOthers = 100 ,SkillScale = SkillScale.Other,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} garrison wages in the castle for each {EVERYSKILLMAIN} steward point if the hero is the castle governer",
                                      DescriptionSecondary = "{VALUE} garrison wages in the castle for each {EVERYSKILLSECONDARY} steward point if the hero is the castle owner",
                                      DescriptionOthers = "{VALUE} garrison wages in the castle for each {EVERYSKILLOTHERS} steward point if the hero is staying in castle that belongs to his clan"}}
                },              
                //Steward.Sweatshops                
                {"StewardSweatshops",
                  //this._stewardSweatshops.Initialize("{=jbAtOsIy}Sweatshops", DefaultSkills.Steward, this.GetTierCost(3), this._stewardStiffUpperLip, "{=6wqJA77K}{VALUE} production rate to owned workshops", SkillEffect.PerkRole.Personal, 0.2f, SkillEffect.EffectIncrementType.AddFactor, "{=rA9nzrAr}{VALUE} siege engine build rate in your party", SkillEffect.PerkRole.Quartermaster, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 1f ,EverySkillMain =10 ,EverySkillSecondary = 40 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "{VALUE} production rate to clan owned workshops for each {EVERYSKILLMAIN} steward point",
                                      DescriptionSecondary = "{VALUE} production rate to clan owned workshops for each {EVERYSKILLSECONDARY} steward point if the hero is a family member",
                                      DescriptionOthers = "{VALUE} production rate to clan owned workshops for each {EVERYSKILLOTHERS} steward point if the hero is clan member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.5f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                      DescriptionMain = "Increase siege engine build rate in your party by {VALUE} for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                      DescriptionOthers = "Increase siege engine build rate in your party by {VALUE} for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}}
                },
                //Steward.PaidInPromise               
                {"StewardPaidInPromise",
                  //this._stewardPaidInPromise.Initialize("{=CPxbG7Zp}Paid in Promise", DefaultSkills.Steward, this.GetTierCost(4), this._stewardEfficientCampaigner, "{=H9tQfeBr}{VALUE} companion wages and recruitment fees", SkillEffect.PerkRole.PartyLeader, -0.25f, SkillEffect.EffectIncrementType.AddFactor, "{=1eKRHLur}Discarded armors are donated to troops for increased experience", SkillEffect.PerkRole.Quartermaster, 0f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.4f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 100 ,EverySkillOthers = 30 ,SkillScale = SkillScale.Other,Role =SkillEffect.PerkRole.ClanLeader,
                                                             AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.Captain, SkillEffect.PerkRole.PartyMember },
                                                             DescriptionMain = "Reduce all clan companions wages and recruitment fees by {VALUE} for each {EVERYSKILLMAIN} steward point if the hero is a clan leader",
                                                             DescriptionSecondary="Reduce all clan companions wages and recruitment fees by {VALUE} for each {EVERYSKILLSECONDARY} steward point if the hero is a family member" ,
                                                             DescriptionOthers = "Reduce the companion wages by {VALUE} for each {EVERYSKILLOTHERS} steward point if the hero is the companion"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.02f ,MinBonus=0 ,MaxBonus = 1.5f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 50 ,SkillScale = SkillScale.Both,
                                                             AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyLeader,SkillEffect.PerkRole.PartyMember },
                                                             DescriptionMain = "Discarded armors are donated to troops for increased experience.\n{VALUE} bonus experience from donated armors for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster or the party leader",
                                                             DescriptionOthers = "{VALUE} bonus experience from donated armors for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}
                                                         }
                }, 
                //Steward.ForeseeableFuture               
                {"StewardForeseeableFuture",
                  //this._stewardEfficientCampaigner.Initialize("{=sC53NYcA}Efficient Campaigner", DefaultSkills.Steward, this.GetTierCost(4), this._stewardPaidInPromise, "{=5t6cveXT}{VALUE} extra food for each food taken during village raids for your party", SkillEffect.PerkRole.PartyLeader, 1f, SkillEffect.EffectIncrementType.Add, "{=JhFCoWbE}{VALUE} troop wages in your party while it is part of an army", SkillEffect.PerkRole.Quartermaster, -0.25f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.02f ,MinBonus=0 ,MaxBonus = 1.5f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 50 ,SkillScale = SkillScale.Both,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "Discarded weapons are donated to troops for increased experience.\n{VALUE} bonus experience from donated weapons for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster or the party leader",
                                       DescriptionOthers = "{VALUE} bonus experience from donated weapons for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.3f ,EverySkillMain =30 ,EverySkillSecondary = 90 ,EverySkillOthers = 120 ,SkillScale = SkillScale.Other,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} tariff income in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                       DescriptionSecondary = "{VALUE} tariff income in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                       DescriptionOthers = "{VALUE} tariff income in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan" }}
                },
                //Steward.EfficientCampaigner               
                {"StewardEfficientCampaigner",
                  //this._stewardGivingHands.Initialize("{=VsqyzWYY}Giving Hands", DefaultSkills.Steward, this.GetTierCost(5), this._stewardLogistician, "{=WaGKvsfc}Discarded weapons are donated to troops for increased experience", SkillEffect.PerkRole.Quartermaster, 0f, SkillEffect.EffectIncrementType.AddFactor, "{=Eo958e7R}{VALUE} tariff income in the governed settlement", SkillEffect.PerkRole.Governor, 0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        SecondaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.02f ,MinBonus=-0.4f ,MaxBonus = 0 ,EverySkillMain =30 ,EverySkillSecondary = 30 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "While the party is part of an army reduce its wages by {VALUE} for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "While the party is part of an army reduce its wages by {VALUE} for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}}
                },
                //Steward.Logistician                
                {"StewardLogistician",
                  //this._stewardLogistician.Initialize("{=U2buPiec}Logistician", DefaultSkills.Steward, this.GetTierCost(5), this._stewardGivingHands, "{=sG9WGOeN}{VALUE} party morale when number of mounts is greater than number of foot troops in your party", SkillEffect.PerkRole.Quartermaster, 4f, SkillEffect.EffectIncrementType.Add, "{=Z1n0w5Kc}{VALUE} tax income", SkillEffect.PerkRole.Governor, 0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 1 ,MinBonus=0 ,MaxBonus = 20f ,EverySkillMain =60 ,EverySkillSecondary = 60 ,EverySkillOthers = 150 ,SkillScale = SkillScale.Both,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyLeader, SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} party morale when number of mounts is greater than number of foot troops in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster or the party leader",
                                       DescriptionOthers = "{VALUE} party morale when number of mounts is greater than number of foot troops in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.3f ,EverySkillMain =30 ,EverySkillSecondary = 90 ,EverySkillOthers = 120 ,SkillScale = SkillScale.Other,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} tax income in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                       DescriptionSecondary = "{VALUE} tax income in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                       DescriptionOthers = "{VALUE} tax income in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}
                                     }
                },
                //Steward.Relocation
                {"StewardRelocation",
                  //this._stewardRelocation.Initialize("{=R6dnhblo}Relocation", DefaultSkills.Steward, this.GetTierCost(6), this._stewardAidCorps, "{=urSSNtUD}{VALUE} influence gain from donating troops", SkillEffect.PerkRole.Quartermaster, 0.25f, SkillEffect.EffectIncrementType.AddFactor, "{=XmqJb7RN}{VALUE} effect from boosting projects in the governed settlement", SkillEffect.PerkRole.Governor, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.5f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 50 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} influence gain from donating troops for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} influence gain from donating troops for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.4f ,EverySkillMain =15 ,EverySkillSecondary = 50 ,EverySkillOthers = 80 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} effect from boosting projects for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary = "{VALUE} effect from boosting projects for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} effect from boosting projects for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.AidCorps                
                {"StewardAidCorps",
                  //this._stewardAidCorps.Initialize("{=4FdtVyj1}Aid Corps", DefaultSkills.Steward, this.GetTierCost(6), this._stewardRelocation, "{=ZLbCqt23}Wounded troops in your party are no longer paid wages", SkillEffect.PerkRole.Quartermaster, 0f, SkillEffect.EffectIncrementType.AddFactor, "{=ULY7byYc}{VALUE} hearth growth in villages bound to the governed settlement", SkillEffect.PerkRole.Governor, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        SecondaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.4f ,EverySkillMain =15 ,EverySkillSecondary = 50 ,EverySkillOthers = 80 ,SkillScale = SkillScale.Other,
                                         AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                         DescriptionMain = "{VALUE} hearth growth in villages bound to the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governor",
                                         DescriptionSecondary = "{VALUE} hearth growth for each {EVERYSKILLSECONDARY} steward point in villages bound to the settlement owned by the hero",
                                         DescriptionOthers = "{VALUE} hearth growth for each {EVERYSKILLOTHERS} steward point in villages bound to the settlement where the hero is staying if settlement belong to his clan"}}
                 },
                //Steward.Gourmet             
                {"StewardGourmet",
                  //this._stewardGourmet.Initialize("{=63lHFDSG}Gourmet", DefaultSkills.Steward, this.GetTierCost(7), this._stewardSoundReserves, "{=KDtcsKUs}Double the morale bonus from having diverse food in your party", SkillEffect.PerkRole.Quartermaster, 2f, SkillEffect.EffectIncrementType.AddFactor, "{=q2ZDAm2v}{VALUE} garrison food consumption during sieges in the governed settlement", SkillEffect.PerkRole.Governor, -0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.05f ,MinBonus=0 ,MaxBonus = 1f ,EverySkillMain =0 ,EverySkillSecondary = 0 ,EverySkillOthers = 20 ,SkillScale = SkillScale.OnlyPartySpecializedRole,EverySkillCourtMember = 20, CourtPosition =DefaultCouncilPositions.SPOUSE,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "Double the morale bonus from having diverse food in your party if the hero is the party quartermaster",
                                       DescriptionCourt="{VALUE} morale bonus from having diverse food in your party for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the court {COURTPOSITION}",
                                       DescriptionOthers = "{VALUE} morale bonus from having diverse food in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =15 ,EverySkillSecondary = 60 ,EverySkillOthers = 90 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} garrison food consumption during sieges in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary ="{VALUE} garrison food consumption during sieges in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} garrison food consumption during sieges in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.SoundReserves                
                {"StewardSoundReserves",
                  //this._stewardSoundReserves.Initialize("{=O5dgeoss}Sound Reserves", DefaultSkills.Steward, this.GetTierCost(7), this._stewardGourmet, "{=RkYL5eaP}{VALUE} troop upgrade costs", SkillEffect.PerkRole.Quartermaster, -0.1f, SkillEffect.EffectIncrementType.AddFactor, "{=P10E5o9l}{VALUE} food consumption during sieges in your party", SkillEffect.PerkRole.Quartermaster, -0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} troop upgrade costs for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} troop upgrade costs for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,EverySkillCourtMember = 20, CourtPosition =DefaultCouncilPositions.SPOUSE,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} food consumption during sieges in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                         DescriptionCourt="{VALUE} food consumption during sieges in your party for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the court {COURTPOSITION}",
                                        DescriptionOthers = "{VALUE} food consumption during sieges in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}}
                },
                //Steward.ForcedLabor 
                {"StewardForcedLabor",
                  //this._stewardForcedLabor.Initialize("{=cWyqiNrf}Forced Labor", DefaultSkills.Steward, this.GetTierCost(8), this._stewardContractors, "{=HrOTTjgo}Prisoners in your party provide carry capacity as if they are standard troops", SkillEffect.PerkRole.Quartermaster, 0f, SkillEffect.EffectIncrementType.AddFactor, "{=T9Viygs8}{VALUE} construction speed per every 3 prisoners", SkillEffect.PerkRole.Governor, 0.01f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.3f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,EverySkillCourtMember = 100, CourtPosition =DefaultCouncilPositions.SPYMASTER,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ (SkillEffect.PerkRole)15,SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "Prisoners in your party provide carry capacity as if they are standard troops.\n{VALUE} extra prisoners carry capacity for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionCourt="{VALUE} extra prisoners carry capacity for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                       DescriptionOthers = "{VALUE} extra prisoners carry capacity for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 2f ,EverySkillMain =30 ,EverySkillSecondary = 90 ,EverySkillOthers = 120 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} construction speed per 5 prisoners for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary = "{VALUE} construction speed per 5 prisoners for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} construction speed per 5 prisoners for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.Contractors
                {"StewardContractors",
                  //this._stewardContractors.Initialize("{=Pg5enC8c}Contractors", DefaultSkills.Steward, this.GetTierCost(8), this._stewardForcedLabor, "{=4220dQ4j}{VALUE} wages and upgrade costs of the mercenary troops in your party", SkillEffect.PerkRole.Quartermaster, -0.25f, SkillEffect.EffectIncrementType.AddFactor, "{=xiTD2qUv}{VALUE} town project effects in the governed settlement", SkillEffect.PerkRole.Governor, 0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);     
                  new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,EverySkillCourtMember = 100, CourtPosition =DefaultCouncilPositions.CHANCELLOR,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} wages and upgrade costs of the mercenary troops in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                        DescriptionCourt="{VALUE} wages and upgrade costs of the mercenary troops in your party for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                        DescriptionOthers = "{VALUE} wages and upgrade costs of the mercenary troops in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.3f ,EverySkillMain =30 ,EverySkillSecondary = 90 ,EverySkillOthers = 120 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} town project effects in the settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary = "{VALUE} town project effects in the settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} town project effects in the settlement for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.ArenicosMules
                {"StewardArenicosMules",
                //this._stewardArenicosMules.Initialize("{=qBx8UbUt}Arenicos' Mules", DefaultSkills.Steward, this.GetTierCost(9), this._stewardArenicosHorses, "{=Yp4zv2ib}{VALUE} carrying capacity for pack animals in your party", SkillEffect.PerkRole.Quartermaster, 0.2f, SkillEffect.EffectIncrementType.AddFactor, "{=fswrp38u}{VALUE} trade penalty for trading pack animals", SkillEffect.PerkRole.Quartermaster, -0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);      
                new PerkData{
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.8f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 50 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} carrying capacity for pack animals in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} carrying capacity for pack animals in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} trade penalty for trading pack animals for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                        DescriptionOthers = "{VALUE} trade penalty for trading pack animals for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}}
                },
                //Steward.ArenicosHorses
                {"StewardArenicosHorses",
                //this._stewardArenicosHorses.Initialize("{=tbQ5bUzD}Arenicos' Horses", DefaultSkills.Steward, this.GetTierCost(9), this._stewardArenicosMules, "{=G9OTNRs4}{VALUE} carrying capacity for troops in your party", SkillEffect.PerkRole.Quartermaster, 0.1f, SkillEffect.EffectIncrementType.AddFactor, "{=xm4eEbQY}{VALUE} trade penalty for trading mounts", SkillEffect.PerkRole.Personal, -0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);         
                new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.4f ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} carrying capacity for troops in your party for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} carrying capacity for troops in your party for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 100 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                        Role = SkillEffect.PerkRole.Quartermaster,
                                        DescriptionMain = "{VALUE} trade penalty for trading mounts for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                        DescriptionOthers = "{VALUE} trade penalty for trading mounts for each {EVERYSKILLOTHERS} steward point if the hero is a party member"}}
                },
                //Steward.MasterOfPlanning
                {"StewardMasterOfPlanning",
                //this._stewardMasterOfPlanning.Initialize("{=n5aT1Y7s}Master of Planning", DefaultSkills.Steward, this.GetTierCost(10), this._stewardMasterOfWarcraft, "{=KMmAG5bk}{VALUE} food consumption while your party is in a siege camp", SkillEffect.PerkRole.Quartermaster, -0.4f, SkillEffect.EffectIncrementType.AddFactor, "{=P5OjioRl}{VALUE} effectiveness to continuous projects in the governed settlement. ", SkillEffect.PerkRole.Governor, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);          
                new PerkData{
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.5f ,MaxBonus = 0 ,EverySkillMain =15 ,EverySkillSecondary = 15 ,EverySkillOthers = 90 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} food consumption while your party is in a siege camp for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} food consumption while your party is in a siege camp for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.4f ,EverySkillMain =15 ,EverySkillSecondary = 15 ,EverySkillOthers = 90 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner,SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} effectiveness to continuous projects in the governed settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary = "{VALUE} effectiveness to continuous projects for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} effectiveness to continuous projects for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },                              
                //Steward.MasterOfWarcraft
                {"StewardMasterOfWarcraft",
                //this._stewardMasterOfWarcraft.Initialize("{=MM0ARhGh}Master of Warcraft", DefaultSkills.Steward, this.GetTierCost(10), this._stewardMasterOfPlanning, "{=StzVsQ2P}{VALUE} troop wages while your party is in a siege camp", SkillEffect.PerkRole.Quartermaster, -0.25f, SkillEffect.EffectIncrementType.AddFactor, "{=ya7alenH}{VALUE} food consumption of town population in the governed settlement", SkillEffect.PerkRole.Governor, -0.05f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);          
                new PerkData(){
                        PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.3f ,MaxBonus = 0 ,EverySkillMain =30 ,EverySkillSecondary = 30 ,EverySkillOthers = 120 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                       DescriptionMain = "{VALUE} troop wages while your party is in a siege camp for each {EVERYSKILLMAIN} steward point if the hero is the party quartermaster",
                                       DescriptionOthers = "{VALUE} troop wages while your party is in a siege camp for each {EVERYSKILLOTHERS} steward point if the hero is a party member"},
                        SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.01f ,MinBonus=-0.15f ,MaxBonus = 0 ,EverySkillMain =50 ,EverySkillSecondary = 100 ,EverySkillOthers = 150 ,SkillScale = SkillScale.Other,
                                        AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, SkillEffect.PerkRole.PartyMember },
                                        DescriptionMain = "{VALUE} food consumption of town population in the governed settlement for each {EVERYSKILLMAIN} steward point if the hero is the settlement governer",
                                        DescriptionSecondary = "{VALUE} food consumption of town population in the governed settlement for each {EVERYSKILLSECONDARY} steward point if the hero is the settlement owner",
                                        DescriptionOthers = "{VALUE} food consumption of town population for each {EVERYSKILLOTHERS} steward point if the hero is staying in a settlement that belongs to his clan"}}
                },
                //Steward.PriceOfLoyalty
                {"StewardPriceOfLoyalty",
                //this._stewardPriceOfLoyalty.Initialize("{=eVTnUmSB}Price of Loyalty", DefaultSkills.Steward, this.GetTierCost(11), null, "{=sYrG8rNy}{VALUE} to food consumption, wages and combat related morale loss for each steward point above 250 in your party", SkillEffect.PerkRole.Quartermaster, -0.005f, SkillEffect.EffectIncrementType.AddFactor, "{=lwp50FuF}{VALUE} tax income for each skill point above 200 in the governed settlement", SkillEffect.PerkRole.Governor, 0.005f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
                new PerkData{
                    PrimaryPerk  = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = -0.005f ,MinBonus=-20f ,MaxBonus = 0 ,EverySkillMain =5 ,EverySkillSecondary = 0 ,EverySkillOthers = 25 ,StartSkillLevel=200,SkillScale = SkillScale.OnlyPartySpecializedRole,CourtPosition = DefaultCouncilPositions.STEWARD,RoyalCourtPosition = DefaultCouncilPositions.STEWARD,EverySkillCourtMember = 20,EverySkillRoyalCourtMember = 20,
                                                      AdditionalRoles = new List<SkillEffect.PerkRole>(){ (SkillEffect.PerkRole)15 , (SkillEffect.PerkRole)16, SkillEffect.PerkRole.PartyMember },
                                                      DescriptionMain = "{VALUE} to food consumption, wages and combat related morale loss for each steward point above {STARTSKILLLEVEL} in your party if the hero is the party quartermaster",
                                                      DescriptionCourt="{VALUE} to food consumption and wages for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                                      DescriptionRoylCourt="{VALUE} to food consumption and wages for each {EVERYSKILLROYALCOURTMEMBER} steward point if the hero is the Kingdom {ROYALCOURTPOSITION}",
                                                      DescriptionOthers = "{VALUE} to food consumption and wages for each {EVERYSKILLOTHERS} steward point above {STARTSKILLLEVEL} in your party if the hero is a party member",
                                                      DescriptionMax = ""},

                    SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Steward ,BonusEverySkill = 0.005f ,MinBonus=0 ,MaxBonus = 20 ,EverySkillMain =5 ,EverySkillSecondary = 20 ,EverySkillOthers = 25 ,StartSkillLevel=200,SkillScale = SkillScale.Other,CourtPosition = DefaultCouncilPositions.STEWARD,RoyalCourtPosition = DefaultCouncilPositions.STEWARD,EverySkillCourtMember = 25,EverySkillRoyalCourtMember = 25,
                                                       AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner, (SkillEffect.PerkRole)15, (SkillEffect.PerkRole)16, SkillEffect.PerkRole.PartyMember },
                                                       DescriptionMain = "{VALUE} tax income for each {EVERYSKILLMAIN} steward point above {STARTSKILLLEVEL} in the governed settlement by the hero",
                                                       DescriptionCourt="{VALUE} tax income for each {EVERYSKILLCOURTMEMBER} steward point if the hero is the clan {COURTPOSITION}",
                                                       DescriptionRoylCourt="{VALUE} tax income for each {EVERYSKILLROYALCOURTMEMBER} steward point if the hero is the Kingdom {ROYALCOURTPOSITION}",
                                                       DescriptionSecondary ="{VALUE} tax income for each {EVERYSKILLSECONDARY} steward point above {STARTSKILLLEVEL} in the settlement if the hero is the settlement owner",
                                                       DescriptionOthers = "{VALUE} tax income for each {EVERYSKILLOTHERS} steward point above {STARTSKILLLEVEL} if the hero is staying in a settlement that belongs to his clan",
                                                       DescriptionMax = ""}}
                },
            };
        #endregion
        #region DefaultPerks.Medicine
        public static Dictionary<string, PerkData> MedicinePerksData { get; set; } = new Dictionary<string, PerkData>()
        {   //Medicine.SelfMedication  (need testing)          
            {"MedicineSelfMedication",
            //this._medicineSelfMedication.Initialize("{=TLGvIdJB}Self Medication", DefaultSkills.Medicine, this.GetTierCost(1), this._medicinePreventiveMedicine, "{=bLAw2di4}{VALUE}% healing rate", SkillEffect.PerkRole.Personal, 0.3f, SkillEffect.EffectIncrementType.AddFactor, "{=V53EYEXx}{VALUE}% combat movement speed", SkillEffect.PerkRole.Personal, 0.02f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {
                PrimaryPerk  =  new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 0.02f ,MinBonus=0 ,MaxBonus = 0.6f ,EverySkillMain =10 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){ },
                                DescriptionMain = "{VALUE} healing rate for each {EVERYSKILLMAIN} medicine point for the hero"},
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 0.005f ,MinBonus=0 ,MaxBonus = 0.075f ,EverySkillMain =20 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){  },
                                DescriptionMain = "{VALUE} combat movement speed for each {EVERYSKILLMAIN} medicine point for the hero"}}
            },
            //Medicine.PreventiveMedicine (need testing)            
            {"MedicinePreventiveMedicine",
            //this._medicinePreventiveMedicine.Initialize("{=wI393cla}Preventive Medicine", DefaultSkills.Medicine, this.GetTierCost(1), this._medicineSelfMedication, "{=Ti9auMiO}{VALUE} hit points", SkillEffect.PerkRole.Personal, 5f, SkillEffect.EffectIncrementType.Add, "{=10cVZTTm}{VALUE}% recovery of lost hit points after each battle", SkillEffect.PerkRole.Personal, 0.3f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {
                PrimaryPerk  =  new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 20f ,EverySkillMain =15 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){ },
                                DescriptionMain = "{VALUE} hit points for the hero for each {EVERYSKILLMAIN} medicine point"},
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 0.02f ,MinBonus=0 ,MaxBonus = 0.6f ,EverySkillMain =10 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){  },
                                DescriptionMain = "{VALUE} recovery of lost hit points after each battle for each {EVERYSKILLMAIN} medicine point for the hero"} }

            },
            //Medicine.TriageTent (need testing)            
            {"MedicineTriageTent",
            //this._medicineTriageTent.Initialize("{=EU4JjLqV}Triage Tent", DefaultSkills.Medicine, this.GetTierCost(2), this._medicineWalkItOff, "{=ZMPhsLdx}{VALUE}% healing rate when stationary on the campaign map", SkillEffect.PerkRole.Surgeon, 0.3f, SkillEffect.EffectIncrementType.AddFactor, "{=Mn714dPH}{VALUE}% food consumption for besieged governed settlement", SkillEffect.PerkRole.Governor, -0.05f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {
                PrimaryPerk  =  new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.5f ,EverySkillMain =10 ,EverySkillSecondary = 10 ,EverySkillOthers = 60 ,SkillScale = SkillScale.OnlyPartySpecializedRole,EverySkillCourtMember = 100, CourtPosition =DefaultCouncilPositions.COURT_PHYSICIAN,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){  (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                DescriptionMain = "{VALUE} healing rate when stationary on the campaign map for each {EVERYSKILLMAIN} medicine point if the hero is the party surgeon",
                                DescriptionCourt="{VALUE} healing rate when stationary on the campaign map for each {EVERYSKILLCOURTMEMBER} medicine point if the hero is the clan {COURTPOSITION}",
                                DescriptionOthers = "{VALUE} healing rate when stationary on the campaign map for each {EVERYSKILLOTHERS} medicine point if the hero is a party member"},
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = -0.005f ,MinBonus=-0.1f ,MaxBonus = 0 ,EverySkillMain =30 ,EverySkillSecondary = 60 ,EverySkillOthers = 120 ,SkillScale = SkillScale.Other,
                                AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyOwner,SkillEffect.PerkRole.PartyMember },
                                DescriptionMain = "{VALUE} food consumption for besieged settlement for each {EVERYSKILLMAIN} medicine point if the hero is the settlement governer",
                                DescriptionSecondary = "{VALUE} food consumption for besieged settlement for each {EVERYSKILLSECONDARY} medicine point if the hero is the settlement owner",
                                DescriptionOthers = "{VALUE} food consumption for besieged governed settlement for each {EVERYSKILLOTHERS} medicine point if the hero is staying in a settlement that belongs to his clan"} }
            },
            //Medicine.WalkItOff (need testing)            
            {"MedicineWalkItOff",
            //this._medicineWalkItOff.Initialize("{=0pyLfrGZ}Walk It Off", DefaultSkills.Medicine, this.GetTierCost(2), this._medicineTriageTent, "{=NtCBRiLH}{VALUE}% healing rate when moving on the campaign map", SkillEffect.PerkRole.Surgeon, 0.15f, SkillEffect.EffectIncrementType.AddFactor, "{=4YNqWPEu}{VALUE} hit points recovery after each offensive battle", SkillEffect.PerkRole.Personal, 10f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {
                PrimaryPerk  =  new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 0.01f ,MinBonus=0 ,MaxBonus = 0.25f ,EverySkillMain =20 ,EverySkillSecondary = 20 ,EverySkillOthers = 120 ,SkillScale = SkillScale.OnlyPartySpecializedRole , EverySkillCourtMember = 100, CourtPosition =DefaultCouncilPositions.COURT_PHYSICIAN,
                                               AdditionalRoles = new List<SkillEffect.PerkRole>(){  (SkillEffect.PerkRole)15, SkillEffect.PerkRole.PartyMember },
                                               DescriptionMain = "{VALUE} healing rate when moving on the campaign map for each {EVERYSKILLMAIN} medicine point if the hero is the party surgeon",
                                               DescriptionCourt= "{VALUE} healing rate when moving on the campaign map for each {EVERYSKILLCOURTMEMBER} medicine point if the hero is the clan {COURTPOSITION}",
                                               DescriptionOthers = "{VALUE} healing rate when moving on the campaign map for each {EVERYSKILLOTHERS} medicine point if the hero is a party member"},
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 30f ,EverySkillMain =10 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                               AdditionalRoles = new List<SkillEffect.PerkRole>(){  },
                                               DescriptionMain = "{VALUE} hit points recovery after each offensive battle for each {EVERYSKILLMAIN} medicine point for the hero"}}
            },
            //Medicine.Sledges             
            {"MedicineSledges",
            //this._medicineSledges.Initialize("{=TyB6y5bh}Sledges", DefaultSkills.Medicine, this.GetTierCost(3), this._medicineDoctorsOath, "{=bFOfZmwC}{VALUE}% party speed penalty from the wounded", SkillEffect.PerkRole.Surgeon, -0.5f, SkillEffect.EffectIncrementType.AddFactor, "{=dfULyKsz}{VALUE} hit points to mounts in your party", SkillEffect.PerkRole.PartyLeader, 15f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {
                PrimaryPerk  =  new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = -0.05f ,MinBonus=-0.75f ,MaxBonus = 0 ,EverySkillMain =30 ,EverySkillSecondary = 30 ,EverySkillOthers = 120 ,SkillScale = SkillScale.OnlyPartySpecializedRole,
                                               AdditionalRoles = new List<SkillEffect.PerkRole>(){   SkillEffect.PerkRole.PartyMember },
                                               DescriptionMain   = "{VALUE} party speed penalty from the wounded for each {EVERYSKILLMAIN} medicine point if the hero is the party surgeon",
                                               DescriptionOthers = "{VALUE} party speed penalty from the wounded for each {EVERYSKILLOTHERS} medicine point if the hero is a party member"},
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 30f ,EverySkillMain =10 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.OnlyPartyLeader,
                                               AdditionalRoles = new List<SkillEffect.PerkRole>(){ SkillEffect.PerkRole.PartyMember },
                                               DescriptionMain = "{VALUE} hit points to mounts in your party for each {EVERYSKILLMAIN} medicine point if the hero is the party leader",
                                               DescriptionOthers = "{VALUE} hit points to mounts in your party for each {EVERYSKILLOTHERS} medicine point if the hero is a party member"}}
            },
             //Medicine.DoctorsOath             
            {"MedicineDoctorsOath",          
            //this._medicineDoctorsOath.Initialize("{=PAwDV08b}Doctor's Oath", DefaultSkills.Medicine, this.GetTierCost(3), this._medicineSledges, "{=XPB1iBkh}Your medicine skill also applies to enemy casualties, increasing potential prisoners", SkillEffect.PerkRole.Surgeon, 0f, SkillEffect.EffectIncrementType.AddFactor, "{=Ti9auMiO}{VALUE} hit points", SkillEffect.PerkRole.Personal, 5f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            new PerkData()
            {            
                SecondaryPerk = new PerkSubData(){ ScaleOnSkill =DefaultSkills.Medicine ,BonusEverySkill = 1f ,MinBonus=0 ,MaxBonus = 30f ,EverySkillMain =10 ,EverySkillSecondary = 0 ,EverySkillOthers = 0 ,SkillScale = SkillScale.Personal,
                                                              AdditionalRoles = new List<SkillEffect.PerkRole>(){  },
                                                              DescriptionMain = "{VALUE} hit points for the hero for each {EVERYSKILLMAIN} medicine point"}
            }}
            //this._medicineBestMedicine.Initialize("{=ei1JSeco}Best Medicine", DefaultSkills.Medicine, this.GetTierCost(4), this._medicineGoodLodging, "{=L3kTYA2p}{VALUE}% healing rate while party morale is above 70", SkillEffect.PerkRole.Surgeon, 0.15f, SkillEffect.EffectIncrementType.AddFactor, "{=At6b9vHF}{VALUE} relationship per day with a random notable over age 40 when party is in a town", SkillEffect.PerkRole.Personal, 1f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineGoodLodging.Initialize("{=RXo3edjn}Good Lodging", DefaultSkills.Medicine, this.GetTierCost(4), this._medicineBestMedicine, "{=NjMR2ypH}{VALUE}% healing rate while resting in settlements", SkillEffect.PerkRole.Surgeon, 0.2f, SkillEffect.EffectIncrementType.AddFactor, "{=ZH3U43xW}{VALUE} relationship per day with a random noble over age 40 when party is in a town", SkillEffect.PerkRole.Personal, 1f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineSiegeMedic.Initialize("{=ObwbbEqE}Siege Medic", DefaultSkills.Medicine, this.GetTierCost(5), this._medicineVeterinarian, "{=Gyy4rwnD}{VALUE}% chance of troops getting wounded instead of getting killed during siege bombardment", SkillEffect.PerkRole.Surgeon, 0.5f, SkillEffect.EffectIncrementType.AddFactor, "{=Nxh6aX2E}{VALUE}% chance to recover from lethal wounds during siege bombardment", SkillEffect.PerkRole.Surgeon, 0.3f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineVeterinarian.Initialize("{=DNPbZZPQ}Veterinarian", DefaultSkills.Medicine, this.GetTierCost(5), this._medicineSiegeMedic, "{=PZb8JrMH}{VALUE}% daily chance to recover a lame horse", SkillEffect.PerkRole.Surgeon, 0.3f, SkillEffect.EffectIncrementType.AddFactor, "{=GJRcFc0V}{VALUE}% chance to recover mounts of dead cavalry troops in battles", SkillEffect.PerkRole.Surgeon, 0.5f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicinePristineStreets.Initialize("{=72tbUfrz}Pristine Streets", DefaultSkills.Medicine, this.GetTierCost(6), this._medicineBushDoctor, "{=JMMVcpA0}{VALUE} settlement prosperity every day in governed settlements", SkillEffect.PerkRole.Governor, 1f, SkillEffect.EffectIncrementType.Add, "{=R9O0Y64L}{VALUE}% party healing rate while waiting in towns", SkillEffect.PerkRole.Surgeon, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineBushDoctor.Initialize("{=HGrsb7k2}Bush Doctor", DefaultSkills.Medicine, this.GetTierCost(6), this._medicinePristineStreets, "{=ULY7byYc}{VALUE}% hearth growth in villages bound to the governed settlement", SkillEffect.PerkRole.Governor, 0.2f, SkillEffect.EffectIncrementType.AddFactor, "{=UaKTuz1l}{VALUE}% party healing rate while waiting in villages", SkillEffect.PerkRole.Surgeon, 0.2f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicinePerfectHealth.Initialize("{=cGuPMx4p}Perfect Health", DefaultSkills.Medicine, this.GetTierCost(7), this._medicineHealthAdvise, "{=1yqMERf2}{VALUE}% recovery rate for each type of food in party inventory", SkillEffect.PerkRole.Surgeon, 0.05f, SkillEffect.EffectIncrementType.AddFactor, "{=QsMEML5E}{VALUE}% animal production rate in villages bound to the governed settlement", SkillEffect.PerkRole.Governor, 0.1f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineHealthAdvise.Initialize("{=NxcvQlAk}Health Advice", DefaultSkills.Medicine, this.GetTierCost(7), this._medicinePerfectHealth, "{=uRvym4tq}Chance of recovery from death due to old age for each clan member", SkillEffect.PerkRole.ClanLeader, 0f, SkillEffect.EffectIncrementType.AddFactor, "{=ioYR1Grc}Wounded troops do not decrease morale in battles", SkillEffect.PerkRole.Surgeon, 0f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicinePhysicianOfPeople.Initialize("{=5o6pSbCx}Physician of People", DefaultSkills.Medicine, this.GetTierCost(8), this._medicineCleanInfrastructure, "{=F7bbkYx4}{VALUE} loyalty per day in the governed settlement", SkillEffect.PerkRole.Governor, 1f, SkillEffect.EffectIncrementType.Add, "{=bNsaUb42}{VALUE}% chance to recover from lethal wounds for tier 1 and 2 troops", SkillEffect.PerkRole.Surgeon, 0.3f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineCleanInfrastructure.Initialize("{=CZ4y5NAf}Clean Infrastructure", DefaultSkills.Medicine, this.GetTierCost(8), this._medicinePhysicianOfPeople, "{=S9XsuYap}{VALUE} prosperity bonus from civilian projects in the governed settlement", SkillEffect.PerkRole.Governor, 1f, SkillEffect.EffectIncrementType.Add, "{=dYyFWmGB}{VALUE}% recovery rate from raids in villages bound to the governed settlement", SkillEffect.PerkRole.Governor, 0.3f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineCheatDeath.Initialize("{=cpg0oHZJ}Cheat Death", DefaultSkills.Medicine, this.GetTierCost(9), this._medicineFortitudeTonic, "{=n2xL3okw}Cheat death due to old age once", SkillEffect.PerkRole.Personal, 0f, SkillEffect.EffectIncrementType.Add, "{=b1IKTI8t}{VALUE}% chance to die when you fall unconscious in battle", SkillEffect.PerkRole.Surgeon, -0.5f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineFortitudeTonic.Initialize("{=ib2SMG9b}Fortitude Tonic", DefaultSkills.Medicine, this.GetTierCost(9), this._medicineCheatDeath, "{=v9NohO6l}{VALUE} hit points to other heroes in your party", SkillEffect.PerkRole.PartyLeader, 10f, SkillEffect.EffectIncrementType.Add, "{=Ti9auMiO}{VALUE} hit points", SkillEffect.PerkRole.Personal, 5f, SkillEffect.EffectIncrementType.Add, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineHelpingHands.Initialize("{=KavZKNaa}Helping Hands", DefaultSkills.Medicine, this.GetTierCost(10), this._medicineBattleHardened, "{=6NOzUcGN}{VALUE}% troop recovery rate for each 10 troop in your party", SkillEffect.PerkRole.Surgeon, 0.02f, SkillEffect.EffectIncrementType.AddFactor, "{=iHuzmdm2}{VALUE}% prosperity loss from starvation", SkillEffect.PerkRole.Governor, -0.5f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineBattleHardened.Initialize("{=oSbRD72H}Battle Hardened", DefaultSkills.Medicine, this.GetTierCost(10), this._medicineHelpingHands, "{=qWpabhp6}{VALUE} experience to wounded units at the end of the battle", SkillEffect.PerkRole.Surgeon, 25f, SkillEffect.EffectIncrementType.Add, "{=3tLU4AG7}{VALUE}% siege attrition loss in the governed settlement", SkillEffect.PerkRole.Governor, -0.25f, SkillEffect.EffectIncrementType.AddFactor, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
            //this._medicineMinisterOfHealth.Initialize("{=rtTjuJTc}Minister of Health", DefaultSkills.Medicine, this.GetTierCost(11), null, "{=cwFyqrfv}{VALUE} hit point to troops for each skill point above 250", SkillEffect.PerkRole.Personal, 1f, SkillEffect.EffectIncrementType.Add, "", SkillEffect.PerkRole.None, 0f, SkillEffect.EffectIncrementType.Invalid, TroopUsageFlags.Undefined, TroopUsageFlags.Undefined);
        };
        #endregion
        [HarmonyPatch(typeof(DefaultPerks), "RegisterAll")]
        class RegisterPerkPatch
        {
            public static MBReadOnlyList<PerkObject> AllPerks => Game.Current.ObjectManager.GetObjectTypeList<PerkObject>();

            static void Postfix()
            {

                if (BannerKingsSettings.Instance.EnableUsefulPerks)
                {
                    if (BannerKingsSettings.Instance.EnableUsefulStewardPerks)
                    {
                        foreach (var perkData in StewardPerksData)
                        {
                            var perk = AllPerks.FirstOrDefault(p => p.StringId == perkData.Key);
                            if (perk != null)
                            {
                                perkData.Value.ChangePerk(perk);
                                AllPerksData.Add(perkData.Key, perkData.Value);
                            }
                        }
                    }
                    if (BannerKingsSettings.Instance.EnableUsefulMedicinePerks)
                    {
                        foreach (var perkData in MedicinePerksData)
                        {
                            var perk = AllPerks.FirstOrDefault(p => p.StringId == perkData.Key);
                            if (perk != null)
                            {
                                perkData.Value.ChangePerk(perk);
                                AllPerksData.Add(perkData.Key, perkData.Value);
                            }
                        }
                    }
                }
            }

            private static void ChangePerkRequirement(string perkId, int tierIndex)
            {
                var perk = AllPerks.FirstOrDefault(d => d.StringId == perkId);
                if (perk != null)
                {
                    ChangePerkRequirement(perk, tierIndex);
                }
            }
            private static void ChangePerkRequirement(PerkObject perk, int tierIndex)
            {
                if (perk != null)
                {
                    perk.SetPrivatePropertyValue("RequiredSkillValue", (float)BKPerks.GetTierCost(tierIndex));
                    perk.AlternativePerk?.SetPrivatePropertyValue("RequiredSkillValue", (float)BKPerks.GetTierCost(tierIndex));
                }
            }

            private static void ChangePerk(string perkId, bool isSecondary, float bonus, string description1, string description2, params SkillEffect.PerkRole[] additionalSecondaryRoles)
            {
                var perk = AllPerks.FirstOrDefault(d => d.StringId == perkId);
                if (perk != null)
                {
                    ChangePerk(perk, isSecondary, bonus, description1, description2, additionalSecondaryRoles);
                }
            }
            private static void ChangePerk(PerkObject perk, bool isSecondary, float bonus, string description1, string description2, params SkillEffect.PerkRole[] additionalSecondaryRoles)
            {
                if (perk != null)
                {
                    if (BannerKingsSettings.Instance.EnableUsefulPerksFromAllPartyMembers)
                    {
                        if (isSecondary)
                        {
                            perk.SetPrivatePropertyValue("SecondaryBonus", bonus);
                            perk.SetPrivatePropertyValue("SecondaryDescription", new TextObject(description1, null));
                            PerkHelper.SetDescriptionTextVariable(perk.SecondaryDescription, perk.SecondaryBonus, perk.SecondaryIncrementType);
                            // PerksAdditionalSecondaryRoles.Add(perk.StringId, additionalSecondaryRoles.ToList());
                        }
                        else
                        {
                            perk.SetPrivatePropertyValue("PrimaryBonus", bonus);
                            perk.SetPrivatePropertyValue("PrimaryDescription", new TextObject(description1, null));
                            PerkHelper.SetDescriptionTextVariable(perk.PrimaryDescription, perk.PrimaryBonus, perk.PrimaryIncrementType);
                            // PerksAdditionalPrimaryRoles.Add(perk.StringId, additionalSecondaryRoles.ToList());
                        }
                    }
                    else
                    {
                        if (isSecondary)
                        {
                            perk.SetPrivatePropertyValue("SecondaryBonus", bonus);
                            perk.SetPrivatePropertyValue("SecondaryDescription", new TextObject(description2, null));
                            PerkHelper.SetDescriptionTextVariable(perk.SecondaryDescription, perk.SecondaryBonus, perk.SecondaryIncrementType);
                            //PerksAdditionalSecondaryRoles.Add(perk.StringId, additionalSecondaryRoles.ToList());
                        }
                        else
                        {
                            perk.SetPrivatePropertyValue("PrimaryBonus", bonus);
                            perk.SetPrivatePropertyValue("PrimaryDescription", new TextObject(description2, null));
                            PerkHelper.SetDescriptionTextVariable(perk.PrimaryDescription, perk.PrimaryBonus, perk.PrimaryIncrementType);
                            // PerksAdditionalPrimaryRoles.Add(perk.StringId, additionalSecondaryRoles.ToList());
                        }
                    }
                }
            }
        }
    }
}
