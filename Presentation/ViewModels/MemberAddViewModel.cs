using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;


namespace Presentation.ViewModels;

public partial class MemberAddViewModel(IServiceProvider serviceProvider, IMemberService memberService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IMemberService _memberService = memberService;

    [ObservableProperty]
    private string _title = "Register Member";

    [ObservableProperty]
    private Member _member = new()
    {

    };

    [ObservableProperty]
    private string _errorMessage = null!;

    [ObservableProperty]
    private string _successMessage = null!;


    [RelayCommand]
    public async Task Save()
    {

        ErrorMessage = null!;
        SuccessMessage = null!;

        if (string.IsNullOrWhiteSpace(Member.PostalCode) ||
         !Regex.IsMatch(Member.PostalCode, @"^\d{5}$"))
        {
            ErrorMessage = "Ogiltigt postnummer (måste vara exakt 5 siffror)";
            return;
        }
    

        var test = new Member()
        {
            SocialSecurityNumber = Member.SocialSecurityNumber,
        };
        var response = await _memberService.SaveMemberAsync(Member);

        if (!response.Success)
        {
            ErrorMessage = response.Message ?? "Unknown error";
            return;
        }

        else
        {
            Member = new Member();
            SuccessMessage = "New member created";
            return;
        }
    }
}

