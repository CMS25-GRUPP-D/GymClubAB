using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;


namespace Presentation.ViewModels;

public partial class MemberAddViewModel(IServiceProvider serviceProvider, IMemberService memberService, IDialogService dialogService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IMemberService _memberService = memberService;
    private readonly IDialogService _dialogService = dialogService;

    [ObservableProperty]
    private string _title = "Register Member";

    [ObservableProperty]
    private Member _member = new();

    [ObservableProperty]
    private string _errorMessage = null!;

    [ObservableProperty]
    private string _successMessage = null!;

    [ObservableProperty]
    private bool _termsAccepted;

    public ObservableCollection<MembershipLevel> MembershipLevels { get; } =
        new ObservableCollection<MembershipLevel>
            (Enum.GetValues(typeof(MembershipLevel)).Cast<MembershipLevel>());

    [RelayCommand]
    public void ShowTerms()
    {
        bool accepted = _dialogService.ShowTermsOfServiceDialog();
        TermsAccepted = accepted;
        Member.TermsAccepted = accepted;
    }

    [RelayCommand]
    public async Task Save()
    {

        ErrorMessage = null!;
        SuccessMessage = null!;

        if (!Member.TermsAccepted)
        {
            ErrorMessage = "Du måste acceptera användarvillkoren.";
            return;
        }

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
            TermsAccepted = false; // Reset checkbox after successful save
            SuccessMessage = "New member created";

            MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            MemberListViewModel listViewModel = _serviceProvider.GetRequiredService<MemberListViewModel>();
            await listViewModel.PopulateMemberListAsync();
            mainViewModel.CurrentViewModel = listViewModel;
        }
    }

}

