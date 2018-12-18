using System;
using Demo.Core.Contracts.SignUp;
using Demo.Core.Shared.Enum;
using Demo.Core.Validations.SignUp;
using FluentValidation.TestHelper;
using Xunit;

namespace Demo.Core.Tests.Validations.SignUp
{
    public class SignUpPostRequestValidatorTest
    {
        private readonly SignUpPostRequestValidator validator;
        public SignUpPostRequestValidatorTest()
        {
            validator = new SignUpPostRequestValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void WhenNameNull_ShouldHaveError(string value)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, value);
        }

        [Fact]
        public void WhenHaveGender_ShouldHaveNoError()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Gender, EGender.Male);
        }

        [Theory]
        [InlineData("")]
        [InlineData("alef@.com")]
        [InlineData("alef")]
        [InlineData("@.com")]
        public void WhenEmailInvalid_ShouldHaveError(string value)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, value);
        }

        [Theory]
        [InlineData("alef.carlos@gmai.com")]
        [InlineData("alef@alef.com.br")]
        public void WhenEmailValid_ShouldHaveNoError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, value);
        }

        [Fact]
        public void WhenPasswordInvalid_ShouldHaveError()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Password, string.Empty);
        }

        [Fact]
        public void WhenConfirmPasswordInvalid_ShouldHaveError()
        {
            //Arrange
            var request = new SignUpPostRequest
            {
                Password = "alef",
                ConfirmPassword = "alef2"
            };


            validator.ShouldHaveValidationErrorFor(x => x.Password, request);
        }

        [Fact]
        public void WhenPasswordValid_ShouldHaveNoError()
        {
            //Arrange
            var request = new SignUpPostRequest
            {
                Password = "alef",
                ConfirmPassword = "alef"
            };

            //Assert
            validator.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, request);
        }

        [Fact]
        public void WhenBotnDateInvalid_ShouldHaveError()
        {
            validator.ShouldHaveValidationErrorFor(x => x.BornDate, DateTime.MinValue);
        }

        [Fact]
        public void WhenBotnDateInvalid_ShouldHaveNoError()
        {
            //Arrange
            var date = new DateTime(1993, 11, 18);
            //Assert
            validator.ShouldNotHaveValidationErrorFor(x => x.BornDate, date);
        }
    }
}