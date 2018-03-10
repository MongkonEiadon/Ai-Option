using System;
using System.Linq;
using iqoption.core.Extensions;
using Shouldly;
using Xunit;

namespace iqopion.core.unit
{
    public class StringMixinsTest
    {
        [Fact]
        public void JoinWithSemicolon_WithNull_StringEmptyMustReturn()
        {
            //arrange

            //act
            var result = StringMixins.JoinWithSemicolon(null);

            //assert
            result.ShouldBeEmpty();
        }
        [Fact]
        public void JoinWithSemicolon_WithEmptyStringList_StringEmptyMustReturn()
        {
            //arrange

            //act
            var result = StringMixins.JoinWithSemicolon(Enumerable.Empty<string>());

            //assert
            result.ShouldBeEmpty();
        }

        [Fact]
        public void JoinWithSemicolon_With2StringElements_StringEmptyMustReturn()
        {
            //arrange

            //act
            var result = StringMixins.JoinWithSemicolon(new []{"1", "2"});

            //assert
            result.ShouldBe("1, 2");
        }
    }
}
