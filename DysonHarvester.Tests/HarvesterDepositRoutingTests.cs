using System.Collections.Generic;
using DysonHarvester;
using Xunit;

namespace DysonHarvester.Tests
{
    public class HarvesterDepositRoutingTests
    {
        [Fact]
        public void ExecuteDepositRoute_prefers_collector_path()
        {
            var order = new List<string>();
            int deposited = HarvesterDepositLogic.ExecuteDepositRoute(
                () => { order.Add("collector"); return 5; },
                () => { order.Add("logistics"); return 0; },
                () => { order.Add("player"); return 0; });

            Assert.Equal(5, deposited);
            Assert.Equal(new[] { "collector" }, order);
        }

        [Fact]
        public void ExecuteDepositRoute_uses_logistics_when_collector_empty()
        {
            var order = new List<string>();
            int deposited = HarvesterDepositLogic.ExecuteDepositRoute(
                () => { order.Add("collector"); return 0; },
                () => { order.Add("logistics"); return 3; },
                () => { order.Add("player"); return 0; });

            Assert.Equal(3, deposited);
            Assert.Equal(new[] { "collector", "logistics" }, order);
        }

        [Fact]
        public void ExecuteDepositRoute_falls_back_to_player()
        {
            var order = new List<string>();
            int deposited = HarvesterDepositLogic.ExecuteDepositRoute(
                () => { order.Add("collector"); return 0; },
                () => { order.Add("logistics"); return 0; },
                () => { order.Add("player"); return 1; });

            Assert.Equal(1, deposited);
            Assert.Equal(new[] { "collector", "logistics", "player" }, order);
        }

        [Fact]
        public void Station_candidates_match_AddItem_paths_used_by_service()
        {
            Assert.True(HarvesterDepositLogic.IsCollectorStationCandidate(true, false, 1));
            Assert.True(HarvesterDepositLogic.IsLogisticsStationCandidate(2, 10));
            Assert.Equal(
                HarvesterDepositLogic.DepositTarget.CollectorStation,
                HarvesterDepositLogic.SelectTarget(true, true, true));
        }
    }
}