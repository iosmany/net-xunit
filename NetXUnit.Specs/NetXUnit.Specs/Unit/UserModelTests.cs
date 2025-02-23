using FluentAssertions;
using NetXUnit.Webapi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXUnit.Specs.Unit;

public class UserModelTests
{
    [Fact]
    public void UserModelFieldsValidationOk()
    {
        //arrange
        var userModel = new UserModel
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@test.com"
        };

        //act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), validationResults, true);
        isValid.Should().BeTrue();
    }

    [Fact]
    public void UserModelFieldsValidationFail()
    {
        //arrange
        var userModel = new UserModel
        {
# pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            FirstName = null,
            LastName = null,
            Email = string.Empty
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        };

        //act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), validationResults, true);
        isValid.Should().BeFalse();

        const string errorMessageNotNullableOrEmpty = "The field is required. Cannot be null or empty";
        //assert
        var firstNameValidationResult = validationResults.FirstOrDefault(v => v.MemberNames.Contains(nameof(userModel.FirstName)));
        firstNameValidationResult.Should().NotBeNull(); 
        firstNameValidationResult.ErrorMessage.Should().NotBeNullOrEmpty();
        firstNameValidationResult.ErrorMessage.Should().Be(errorMessageNotNullableOrEmpty);   

        var emailValidationResult = validationResults.FirstOrDefault(v => v.MemberNames.Contains(nameof(userModel.Email)));
        emailValidationResult.Should().NotBeNull();
        emailValidationResult.ErrorMessage.Should().NotBeNullOrEmpty();
        emailValidationResult.ErrorMessage.Should().Be(errorMessageNotNullableOrEmpty);
    }

    [Fact]
    public void UserModelFieldsLenghtCheck()
    {
        //arrange
        var userModel = new UserModel
        {
            FirstName = "John abcdefghiklnmopqrsdty abcdefghiklnmopqrsdtyabcdefghiklnmopqrsdty",
            LastName = "Doe",
            Email = "johnabcdefghiklnmopqrsdtyabcdefghiklnmopqrsdty@testabcdefghiklnmopqrsdtyabcdefghiklnmopqrsdty.com"
        };

        //act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), validationResults, true);
        isValid.Should().BeFalse();
            
        //asset
        var firstNameValidationResult = validationResults.FirstOrDefault(v => v.MemberNames.Contains(nameof(userModel.FirstName)));
        firstNameValidationResult.Should().NotBeNull();
        firstNameValidationResult.ErrorMessage.Should().NotBeNullOrEmpty();
        firstNameValidationResult.ErrorMessage.Should().Be("The field FirstName must be a string or array type with a maximum length of '50'.");

        var emailValidationResult = validationResults.FirstOrDefault(v => v.MemberNames.Contains(nameof(userModel.Email)));
        emailValidationResult.Should().NotBeNull();
        emailValidationResult.ErrorMessage.Should().NotBeNullOrEmpty();
        emailValidationResult.ErrorMessage.Should().Be("The field Email must be a string or array type with a maximum length of '60'.");
    }

    [Fact]
    public void UserModelFieldsEmailValidation()
    {
        //arrange
        var userModel = new UserModel
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@test"
        };
        //act
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), validationResults, true);
        isValid.Should().BeFalse();
        //asset
        var emailValidationResult = validationResults.FirstOrDefault(v => v.MemberNames.Contains(nameof(userModel.Email)));
        emailValidationResult.Should().NotBeNull();
        emailValidationResult.ErrorMessage.Should().NotBeNullOrEmpty();
        emailValidationResult.ErrorMessage.Should().Be("Invalid email address");
    }
}

