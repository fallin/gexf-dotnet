using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Gexf.UnitTests
{
    [TestFixture]
    public class GexfIdTests
    {
        [Test]
        public void ToStringShouldCorrectlyRepresentIntegerId()
        {
            GexfId id = new GexfId.Int(1);
            id.ToString().ShouldBe("1");
        }

        [Test]
        public void ToStringShouldCorrectlyRepresentStringId()
        {
            GexfId id = new GexfId.Str("A");
            id.ToString().ShouldBe("A");
        }

        [Test]
        public void ImplicitConversionOperatorShouldReturnIntIdWhenInteger()
        {
            GexfId id = 1;
            id.ShouldBeOfType<GexfId.Int>();
        }

        [Test]
        public void ImplicitConversionOperatorShouldReturnStrIdWhenString()
        {
            GexfId id = "A";
            id.ShouldBeOfType<GexfId.Str>();
        }

        [Test]
        public void MatchShouldInvokeIntFunctionWhenIntId()
        {
            GexfId id = 1;

            Func<int, object> intFn = Substitute.For<Func<int, object>>();
            Func<string, object> strFn = Substitute.For<Func<string, object>>();

            id.Match(intFn, strFn);

            intFn.Received().Invoke(1);
            strFn.DidNotReceiveWithAnyArgs();
        }

        [Test]
        public void MatchShouldInvokeStrFunctionWhenStrId()
        {
            GexfId id = "A";

            Func<int, object> intFn = Substitute.For<Func<int, object>>();
            Func<string, object> strFn = Substitute.For<Func<string, object>>();

            id.Match(intFn, strFn);

            intFn.DidNotReceiveWithAnyArgs();
            strFn.Received().Invoke("A");
        }

        [Test]
        public void EqualsShouldReturnTrueForEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "23";

            id1.Equals(id2).ShouldBeTrue();
        }

        [Test]
        public void EqualsShouldReturnFalseForNonEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "32";

            id1.Equals(id2).ShouldBeFalse();
        }

        [Test]
        public void EqualsShouldReturnTrueForEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "A";

            id1.Equals(id2).ShouldBeTrue();
        }

        [Test]
        public void EqualsShouldReturnFalseForNonEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "B";

            id1.Equals(id2).ShouldBeFalse();
        }

        [Test]
        public void EqualsShouldReturnTrueWhenIntIdAndStrIdHaveEquivalentValue()
        {
            // Treating an int and str id value as equivalent when they have equivalent
            // values (e.g., treating "2" == 2) is not ideal by any means. However, the
            // spec says ids are represented as xml string types and must be unique in the
            // nodes/edge sets, so equality of the types must be defined this way too!

            GexfId id1 = 1;
            GexfId id2 = "1";

            id1.Equals(id2).ShouldBeTrue();
            id2.Equals(id1).ShouldBeTrue();
        }
        /// <summary>
        /// 
        /// </summary>

        [Test]
        public void BoxedEqualsShouldReturnTrueForEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "23";

            id1.Equals((object)id2).ShouldBeTrue();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseForNonEqualIntIdValues()
        {
            GexfId id1 = "23";
            GexfId id2 = "32";

            id1.Equals((object)id2).ShouldBeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnTrueForEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "A";

            id1.Equals((object)id2).ShouldBeTrue();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseForNonEqualStrIdValues()
        {
            GexfId id1 = "A";
            GexfId id2 = "B";

            id1.Equals((object)id2).ShouldBeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseWhenComparingGexfIdIntTypeWithInteger()
        {
            GexfId id1 = 1;
            object id2 = 1;

            id1.Equals(id2).ShouldBeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseWhenComparingGexfIdIntTypeWithString()
        {
            GexfId id1 = 1;
            object id2 = "1";

            id1.Equals(id2).ShouldBeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseWhenComparingGexfIdStrTypeWithInteger()
        {
            GexfId id1 = "1";
            object id2 = "1";

            id1.Equals(id2).ShouldBeFalse();
        }

        [Test]
        public void BoxedEqualsShouldReturnFalseWhenComparingGexfIdStrTypeWithString()
        {
            GexfId id1 = "A";
            object id2 = "A";

            id1.Equals(id2).ShouldBeFalse();
        }
    }
}
