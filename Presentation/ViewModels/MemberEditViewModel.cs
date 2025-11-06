using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.DTOs;
using Infrastructure.Interfaces;

namespace Presentation.ViewModels;

public partial class MemberEditViewModel: ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMemberService _memberService;

    public MemberEditViewModel(IServiceProvider serviceProvider, IMemberService memberService)
    {
        _serviceProvider = serviceProvider;
        _memberService = memberService;
    }


    [ObservableProperty]
    private string _title = "Update member";

    public void SetMember(MemberUpdateRequest member)
    {
   
    }

    [RelayCommand]
    private async Task Save()
    {
        
    }

    [RelayCommand]
    private void Cancel()
    {
       
    }
}
 