using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NUnit.Framework;

namespace DotNet.Gexf.UnitTests
{
    [TestFixture]
    public class GexfIdTests
    {
        [Test]
        public void ToStringShouldCorrectlyRepresentIntegerId()
        {
            GexfId id = new GexfId.Int(1);
            id.ToString().Should().Be("1");
        }

        [Test]
        public void ToStringShouldCorrectlyRepresentStringId()
        {
            GexfId id = new GexfId.Str("A");
            id.ToString().Should().Be("A");
        }

        [Test]
        public void ImplicitConversionOperatorShouldReturnIntIdWhenInteger()
        {
            GexfId id = 1;
            id.Should().BeOfType<GexfId.Int>();
        }

        [Test]
        public void ImplicitConversionOperatorShouldReturnStrIdWhenString()
        {
            GexfId id = "A";
            id.Should().BeOfType<GexfId.Str>();
        }

        [Test]
        public void MatchShouldInvokeIntFunctionWhenIntId()
        {
            GexfId id = 1;

            Func<int, object> intFn = Substitute.For<Func<int, object>>();
            Func<string, object> strFn = Substitute.For<Func<string, object>>();

            id.Match(intFn, strFn);

            using (new AssertionScope())
            {
                intFn.Received().Invoke(1);
                strFn.DidNotReceiveWithAnyArgs();
            }
        }

        [Test]
        public void MatchShouldInvokeStrFunctionWhenStrId()
        {
            GexfId id = "A";

            Func<int, object> intFn = Substitute.For<Func<int, object>>();
            Func<string, object> strFn = Substitute.For<Func<string, object>>();

            id.Match(intFn, strFn);

            using (new AssertionScope())
            {
                intFn.DidNotReceiveWithAnyArgs();
                strFn.Received().Invoke("A");
            }
        }

        [Test]
        public void EqualsShouldReturnTrueForEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "23";

            id1.Equals(id2).Should().BeTrue();
        }

        [Test]
        public void EqualsShouldReturnFalseForNonEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "32";

            id1.Equals(id2).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnTrueForEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "A";

            id1.Equals(id2).Should().BeTrue();
        }

        [Test]
        public void EqualsShouldReturnFalseForNonEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "B";

            id1.Equals(id2).Should().BeFalse();
        }

        [Test]
        public void EqualsShouldReturnFalseForIdsWithDifferentTypes()
        {
            GexfId id1 = 1;
            GexfId id2 = "1";

            id1.Equals(id2).Should().BeFalse();
        }
        /// <summary>
        /// 
        /// </summary>

        [Test]
        public void BoxedEqualsShouldReturnTrueForEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "23";

            id1.Equals((object)id2).Should().BeTrue();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseForNonEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "32";

            id1.Equals((object)id2).Should().BeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnTrueForEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "A";

            id1.Equals((object)id2).Should().BeTrue();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseForNonEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "B";

            id1.Equals((object)id2).Should().BeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseForIdsWithDifferentTypes()
        {
            GexfId id1 = 1;
            GexfId id2 = "1";

            id1.Equals((object)id2).Should().BeFalse();
        }
    }
}
