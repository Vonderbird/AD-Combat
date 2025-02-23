using Moq;
using NUnit.Framework;
using ADC.API;
using ADC.Currencies;
using UnityEngine;

namespace ADC._Tests.Editor.Components.EconomySystem
{
    public class EconomySystemTests
    {
        private ADC.Currencies.EconomySystem economySystem;
        private Mock<IIncomeSourceFactory> mockIncomeSourceFactory;
        private FactionEconomiesCollection factionEconomiesCollection;

        [SetUp]
        public void Setup()
        {
            // Arrange
            mockIncomeSourceFactory = new Mock<IIncomeSourceFactory>();
            var (fe1, fe2) = (new FactionEconomy(), new FactionEconomy());
            fe1.Init(Mock.Of<IIncomeSourceFactory>(), 1);
            fe1.Init(Mock.Of<IIncomeSourceFactory>(), 2);
            var factionEconomies = new IFactionEconomy[] {fe1, fe2 };
            
            factionEconomiesCollection = new FactionEconomiesCollection(factionEconomies);

            economySystem = new GameObject().AddComponent<ADC.Currencies.EconomySystem>();
        }

        [Test]
        public void Init_InitializesFactionsAndPopulatesDictionary()
        {
            // Act
            economySystem.Init(factionEconomiesCollection, mockIncomeSourceFactory.Object);

            // Assert
            Assert.AreEqual(2, economySystem.FactionsEconomiesDictionary.Count);
            Assert.IsTrue(economySystem.FactionsEconomiesDictionary.ContainsKey(0));
            Assert.IsTrue(economySystem.FactionsEconomiesDictionary.ContainsKey(1));
            Assert.IsInstanceOf<FactionEconomy>(economySystem.FactionsEconomiesDictionary[0]);
            Assert.IsInstanceOf<FactionEconomy>(economySystem.FactionsEconomiesDictionary[1]);
        }

        [Test]
        public void Indexer_RetrievesCorrectFactionEconomy()
        {
            // Arrange
            economySystem.Init(factionEconomiesCollection, mockIncomeSourceFactory.Object);

            // Act & Assert
            var faction0 = economySystem[0];
            var faction1 = economySystem[1];

            Assert.IsNotNull(faction0);
            Assert.IsNotNull(faction1);
            Assert.AreEqual(0, faction0.FactionId);
            Assert.AreEqual(1, faction1.FactionId);

            // Test non-existent faction
            var invalidFaction = economySystem[3];
            Assert.IsNull(invalidFaction);
        }

        // [Test]
        // public void Start_InitializesAllFactions()
        // {
        //     // Arrange
        //     economySystem.Init(factionEconomiesCollection.Object, mockIncomeSourceFactory.Object);
        //
        //     // Act
        //     economySystem.Start();
        //
        //     // Assert
        //     foreach (var faction in economySystem.FactionsEconomiesDictionary.Values)
        //     {
        //         Assert.IsTrue(((FactionEconomy)faction)
        //             .IsStarted); // Assuming FactionEconomy has a property IsStarted for testing
        //     }
        // }

        // [Test]
        // public void OnEnable_CallsOnEnableForAllFactions()
        // {
        //     // Arrange
        //     economySystem.Init(factionEconomiesCollection.Object, mockIncomeSourceFactory.Object);
        //
        //     // Act
        //     economySystem.enabled=false;
        //     economySystem.enabled=true;
        //
        //     // Assert
        //     foreach (var faction in economySystem.FactionsEconomiesDictionary.Values)
        //     {
        //         Assert.IsTrue(((FactionEconomy)faction)
        //             .IsEnabled); // Assuming FactionEconomy has a property IsEnabled for testing
        //     }
        // }
        //
        // [Test]
        // public void OnDisable_CallsOnDisableForAllFactions()
        // {
        //     // Arrange
        //     economySystem.Init(factionEconomiesCollection, mockIncomeSourceFactory.Object);
        //
        //     // Act
        //     economySystem.OnDisable();
        //
        //     // Assert
        //     foreach (var faction in economySystem.FactionsEconomiesDictionary.Values)
        //     {
        //         Assert.IsFalse(((FactionEconomy)faction)
        //             .IsEnabled); // Assuming FactionEconomy has a property IsEnabled for testing
        //     }
        // }
    }
}