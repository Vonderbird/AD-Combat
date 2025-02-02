using NUnit.Framework;
using ADC.API;
using ADC.Currencies;
using UnityEngine;

namespace ADC.Editor.Tests
{
    [TestFixture]
    public class BiofuelTests
    {
        [Test]
        public void Constructor_SetsValueCorrectly()
        {
            var biofuel = new Biofuel(50.5m);
            Assert.AreEqual(50.5m, biofuel.Value);
        }

        [Test]
        public void ImplicitConversion_ToDecimal_ReturnsValue()
        {
            var biofuel = new Biofuel(30m);
            decimal value = biofuel;
            Assert.AreEqual(30m, value);
        }

        [TestCase(10, 10, true)]
        [TestCase(10, 20, false)]
        [TestCase(10, 10.0, true)]
        public void EqualityOperators_WorkCorrectly(decimal a, decimal b, bool expected)
        {
            var b1 = new Biofuel(a);
            var b2 = new Biofuel(b);

            Assert.AreEqual(expected, b1 == b2);
            Assert.AreEqual(!expected, b1 != b2);
            Assert.AreEqual(expected, b1 == b);
            Assert.AreEqual(expected, b == b1);
        }

        [TestCase(20, 10, true)]
        [TestCase(10, 20, false)]
        [TestCase(10, 10, false)]
        public void ComparisonOperators_WorkCorrectly(decimal a, decimal b, bool expected)
        {
            var b1 = new Biofuel(a);
            var b2 = new Biofuel(b);
            
            Assert.AreEqual(expected, b1 > b2);
            Assert.AreEqual(expected, b1 > b);
            Assert.AreEqual(expected, a > b2);
        }

        [Test]
        public void ArithmeticOperators_WorkCorrectly()
        {
            var b1 = new Biofuel(100m);
            var b2 = new Biofuel(30m);

            Assert.AreEqual(130m, (b1 + b2).Value);
            Assert.AreEqual(70m, (b1 - b2).Value);
            Assert.AreEqual(70m, (b1 - 30m).Value);
            Assert.AreEqual(200m, (b1 * 2m).Value);
            Assert.AreEqual(50m, (b1 / 2m).Value);
        }

        [Test]
        public void Subtraction_ClampsToZero()
        {
            var b1 = new Biofuel(10m);
            Assert.AreEqual(0m, (b1 - 15m).Value);
        }

        [Test]
        public void Equals_WorksWithDifferentTypes()
        {
            var b = new Biofuel(10m);
            Assert.IsTrue(b.Equals(10));
            Assert.IsTrue(b.Equals(10.0f));
            Assert.IsTrue(b.Equals(new Biofuel(10.0m)));
            Assert.IsTrue(b.Equals((object)new Biofuel(10.0m)));
            Assert.IsFalse(b.Equals("string"));
            Assert.IsFalse(b.Equals(null));
        }

        [Test]
        public void GetHashCode_MatchesValueHash()
        {
            var b = new Biofuel(25m);
            Assert.AreEqual(25m.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ToString_ReturnsValueString()
        {
            var b = new Biofuel(12.34m);
            Assert.AreEqual("12.34", b.ToString());
        }

        [Test]
        public void IsEmpty_WhenDefaultValue()
        {
            var b = new Biofuel();
            Assert.IsTrue(b.IsEmpty);
        }
    }
}