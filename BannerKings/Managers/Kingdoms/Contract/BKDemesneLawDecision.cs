using System.Collections.Generic;
using System.Linq;
using BannerKings.Managers.Titles;
using BannerKings.Managers.Titles.Laws;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace BannerKings.Managers.Kingdoms.Contract
{
    public abstract class BKDemesneLawDecision : KingdomDecision
    {
        public BKDemesneLawDecision(Clan proposerClan, FeudalTitle title, DemesneLaw currentLaw, DemesneLaw proposedLaw) : base(proposerClan)
        {
            Title = title;
            CurrentLaw = currentLaw;
            ProposedLaw = proposedLaw;
        }

        [SaveableProperty(99)] public FeudalTitle Title { get; set; }

        [SaveableProperty(100)] public DemesneLaw CurrentLaw { get; set; }

        [SaveableProperty(101)] public DemesneLaw ProposedLaw { get; set; }

        public override void ApplyChosenOutcome(DecisionOutcome chosenOutcome)
        {
            var outcome = chosenOutcome as DemesneLawDecisionOutcome;
            Title.EnactLaw(outcome.Law);
        }

        public override void ApplySecondaryEffects(List<DecisionOutcome> possibleOutcomes, DecisionOutcome chosenOutcome)
        {
        }

        public override Clan DetermineChooser()
        {
            return Kingdom.RulingClan;
        }

        public override IEnumerable<DecisionOutcome> DetermineInitialCandidates()
        {
            yield return new DemesneLawDecisionOutcome(ProposedLaw);
            yield return new DemesneLawDecisionOutcome(CurrentLaw, true);
        }

        public override void DetermineSponsors(List<DecisionOutcome> possibleOutcomes)
        {
            foreach (var decisionOutcome in possibleOutcomes)
            {
                if (((DemesneLawDecisionOutcome)decisionOutcome).Law == ProposedLaw)
                {
                    decisionOutcome.SetSponsor(ProposerClan);
                }

                else
                {
                    AssignDefaultSponsor(decisionOutcome);
                }
            }
        }

        public override float DetermineSupport(Clan clan, DecisionOutcome possibleOutcome)
        {
            var outcome = possibleOutcome as DemesneLawDecisionOutcome;

            float egalitatian = 0.6f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Egalitarian) - 0.9f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Oligarchic);
            float oligarchic = 0.6f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Oligarchic) - 0.9f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Egalitarian) - 0.5f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Authoritarian);
            float authoritarian = 0.8f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Authoritarian) - 1.3f * (float)clan.Leader.GetTraitLevel(DefaultTraits.Oligarchic);
            float support = outcome.Law.EgalitarianWeight * egalitatian + 
                outcome.Law.OligarchicWeight * oligarchic + 
                outcome.Law.AuthoritarianWeight * authoritarian;

            return support;
        }

        public override TextObject GetChooseDescription()
        {
            var textObject = new TextObject("{=atiwRMmv}As the sovereign of {KINGDOM}, you must decide whether to approve this contract change or not.");
            textObject.SetTextVariable("KINGDOM", Kingdom.Name);
            return textObject;
        }


        public override TextObject GetChosenOutcomeText(DecisionOutcome chosenOutcome, SupportStatus supportStatus,
            bool isShortVersion = false)
        {
            var law = (chosenOutcome as DemesneLawDecisionOutcome).Law;
            return new TextObject("{=!}The peers of {TITLE} have decided on the {LAW} law.")
                .SetTextVariable("LAW", law.Name);
        }

        public override TextObject GetGeneralTitle() => new TextObject("{=!}Demesne Law");

        public override int GetProposalInfluenceCost() => ProposedLaw.InfluenceCost;

        public override DecisionOutcome GetQueriedDecisionOutcome(List<DecisionOutcome> possibleOutcomes) => 
            (from k in possibleOutcomes orderby k.Merit descending select k).ToList().FirstOrDefault();

        public override TextObject GetSecondaryEffects() => new TextObject("{=!}All supporters gains some relation with each other.", null);

        public override TextObject GetSupportDescription() => new TextObject("{=!}The peers of {TITLE} will decide the next {LAW} demesne law. You may pick your stance.")
            .SetTextVariable("TITLE", Title.FullName)
            .SetTextVariable("LAW", GameTexts.FindText("str_bk_demesne_law", CurrentLaw.LawType.ToString()));

        public override TextObject GetChooseTitle() => new TextObject("{=!}Vote for the next {LAW} demesne law")
            .SetTextVariable("LAW", GameTexts.FindText("str_bk_demesne_law", CurrentLaw.LawType.ToString()));

        public override TextObject GetSupportTitle() => new TextObject("{=!}Vote for the next {LAW} demesne law")
            .SetTextVariable("LAW", GameTexts.FindText("str_bk_demesne_law", CurrentLaw.LawType.ToString()));

        public override bool IsAllowed() => Title.contract != null && !ProposedLaw.Equals(CurrentLaw) && 
            ProposerClan.Influence >= ProposedLaw.InfluenceCost;
        

        public class DemesneLawDecisionOutcome : DecisionOutcome
        {
            public DemesneLawDecisionOutcome(DemesneLaw law, bool current = false)
            {
                Law = law;
            }

            [SaveableProperty(200)] public DemesneLaw Law { get; set; }
            [SaveableProperty(201)] public bool Current { get; set; }


            public override TextObject GetDecisionTitle() => Law.Name;

            public override TextObject GetDecisionDescription()
            {
                if (Current)
                {
                    return new TextObject("{=!}We support the continuation of the demesne law {LAW}")
                        .SetTextVariable("LAW", Law.Name);
                }

                return new TextObject("{=!}We support the enactment of the demesne law {LAW}")
                    .SetTextVariable("LAW", Law.Name);
            }
            

            public override string GetDecisionLink()
            {
                return null;
            }

            public override ImageIdentifier GetDecisionImageIdentifier()
            {
                return null;
            }
        }
    }
}