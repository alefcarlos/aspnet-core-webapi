using Demo.Core.Shared.Enum;
using Demo.Core.Validations.DragonBall;
using FluentValidation.TestHelper;
using Xunit;

namespace Demo.Tests.Validations.Dragonbal
{
    public class DragonBallPostRequestValidatorTest
    {
        private DragonBallPostRequestValidator validator;

        public DragonBallPostRequestValidatorTest()
        {
            validator = new DragonBallPostRequestValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenNameNull_ShouldHaveError(string value)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, value);
        }

        [Fact]
        public void WhenHaveName_ShouldHaveNoError()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, "Alef");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenBirthNull_ShouldHaveError(string value)
        {
            validator.ShouldHaveValidationErrorFor(x => x.BirthDate, value);
        }

        [Fact]
        public void WhenHaveBirth_ShouldHaveNoError()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.BirthDate, "18/11/1993");
        }

        [Fact]
        public void WhenInvalidKind_ShouldHaveError()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Kind, ECharecterKind.Sayajin);
        }

        [Fact]
        public void WhenHaveKind_ShouldHaveNoError()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Kind, ECharecterKind.Sayajin);
        }
    }
}
