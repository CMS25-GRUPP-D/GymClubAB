using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;


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


    [RelayCommand]
    public async Task Save()
    {

        ErrorMessage = null!;

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
            return;
        }
    }
}

