using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Mappers;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Presentation.ViewModels;

public partial class MemberListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemberService _memberService;
    private readonly IMemberMapper _memberMapper;

    public MemberListViewModel(IServiceProvider serviceProvider, IMemberService memberService, IMemberMapper memberMapper)
    {
        _serviceProvider = serviceProvider;
        _memberService = memberService;
        _memberMapper = memberMapper;

        Initialize();
    }

    [ObservableProperty]
    private string _title = "Member list";

    [ObservableProperty]
    private ObservableCollection<Member> _members = [];

    [ObservableProperty]
    private string _errorMessage = null!;

    [ObservableProperty]
    private string _successMessage = null!;

    private void Initialize()
    {
        _ = PopulateMemberListAsync();
    }

    [RelayCommand]
    public async Task PopulateMemberListAsync()
    {
        ResponseResult<IEnumerable<Member>> result = await _memberService.GetAllMembersAsync();
        if (!result.Success)
        {
            Members = new ObservableCollection<Member>();
            return;
        }

        IEnumerable<Member> members = result.Data ?? [];
        Members = new ObservableCollection<Member>(members);
    }

    [RelayCommand]
    private void NavigateToMemberAddView()
    {
        MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>(); // Hämta MainViewModel från DI
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MemberAddViewModel>(); // När man trycker på knappen byts innehållet i mainViewModel.CurrentViewModel till denna vy
    }

    [RelayCommand]
    private void Edit(Member selectedMember)
    {
        if (selectedMember is null)
        {
            ErrorMessage = "Choose a member to edit.";
            return;
        }
        MemberUpdateRequest dto = new MemberUpdateRequest 
        {
            SocialSecurityNumber = selectedMember.SocialSecurityNumber,
            FirstName = selectedMember.FirstName,
            LastName = selectedMember.LastName,
            Email = selectedMember.Email,
            Phonenumber = selectedMember.Phonenumber,
            Membership = selectedMember.Membership
        };
        // Hämta en instans av editviewmodel och skicka med medlemmen som ska redigeras
        MemberEditViewModel editviewmodel = _serviceProvider.GetRequiredService<MemberEditViewModel>();
        editviewmodel.SetMember(dto); // SetMember-metoden skrivs i edit-metoden i membereditviewmodel

        // byt instans av currentviewmodel till editviewmodel - edt-vyn visas
        MainViewModel mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MemberEditViewModel>();
    }

    //Fixa updateasync-metoden så att den tar request


    [RelayCommand]
    private async Task Delete(string SSN)
    {
        var response = await _memberService.DeleteMemberAsync(SSN);
        MemberListViewModel listViewModel = _serviceProvider.GetRequiredService<MemberListViewModel>();
        await listViewModel.PopulateMemberListAsync();
    }
}