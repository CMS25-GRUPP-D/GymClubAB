using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels;

public partial class MemberListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemberService _memberService;

    public MemberListViewModel(IServiceProvider serviceProvider, IMemberService memberService)
    {
        _serviceProvider = serviceProvider;
        _memberService = memberService;

        //Addera PopulateMemberList
    }

    [ObservableProperty]
    private string _title = "Member list";

    [ObservableProperty]
    private ObservableCollection<Member> _members = [];

    [ObservableProperty]
    private string _successMessage = null!;

    [RelayCommand]
    private void PopulateMemberList()
    {

    }

    [RelayCommand]
    private void GoToMemberAddView()
    {

    }

    [RelayCommand]
    private void Edit(Member member)
    {

    }

    [RelayCommand]
    private void Delete(Member member)
    {

    }

}