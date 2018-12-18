using System;
using Framework.Core.Helpers;
using Shouldly;
using Xunit;

namespace Framework.Core.Tests.Helpers
{
    public class CommonHelpersTest
    {
        [Fact]
        public void WhenEnvNotExists_ShouldThrowException()
        {
            Should.Throw<ArgumentNullException>(() => CommonHelpers.GetEnvironmentVariable("TESTE"));
        }

        [Fact]
        public void WhenEnvNotExists_ShouldReturnNull()
        {
            //Arrange
            var env = CommonHelpers.GetEnvironmentVariable("TESTE", false);

            //Act

            //Assert
            env.ShouldBe(null);
        }

        [Fact]
        public void WhenEnvNameNull_ShouldThrowException()
        {
            Should.Throw<ArgumentNullException>(() => CommonHelpers.GetEnvironmentVariable(""));
        }

        [Fact]
        public void WhenEnvNotNullAndExists_ShouldReturnValue()
        {
            //Arange
            Environment.SetEnvironmentVariable(nameof(WhenEnvNotNullAndExists_ShouldReturnValue), "value");

            //Act
            var envValue = CommonHelpers.GetEnvironmentVariable(nameof(WhenEnvNotNullAndExists_ShouldReturnValue), false);

            //Assert
            envValue.ShouldBe("value");
        }

        [Fact]
        public void WhenEnvNotNullAndExists_ShouldReturnIntValue()
        {
            //Arange
            Environment.SetEnvironmentVariable(nameof(WhenEnvNotNullAndExists_ShouldReturnIntValue), "10");

            //Act
            var envValue = CommonHelpers.GetValueFromEnv<int>(nameof(WhenEnvNotNullAndExists_ShouldReturnIntValue), false);

            //Assert
            envValue.ShouldBe(10);
        }

        [Fact]
        public void WhenEnvNotNullAndExists_ShouldThrowFormatException()
        {
            //Arange
            Environment.SetEnvironmentVariable(nameof(WhenEnvNotNullAndExists_ShouldThrowFormatException), "value");

            //Act
            Should.Throw<FormatException>(() => CommonHelpers.GetValueFromEnv<int>(nameof(WhenEnvNotNullAndExists_ShouldThrowFormatException), false));
        }

        [Fact]
        public void WhenEnvNotNullAndExists_ShouldThrowInvalidCastException()
        {
            //Act
            Should.Throw<InvalidCastException>(() => CommonHelpers.GetValueFromEnv<int>(nameof(WhenEnvNotNullAndExists_ShouldThrowInvalidCastException), false));
        }
    }
}