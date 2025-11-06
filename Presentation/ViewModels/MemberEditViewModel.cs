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

    [ObservableProperty]
    private string _lastName; // nytt fält för efternamn

    public void SetMember(MemberUpdateRequest member)
    {
        LastName = member.LastName; // visar befintligt efternamn om det finns
    }

    [RelayCommand]
    private async Task Save()
    {
        var updateRequest = new MemberUpdateRequest
        {
            LastName = LastName // skickar med efternamnet till service-lagret
        };

        await _memberService.UpdateMemberAsync(updateRequest);
    }

    [RelayCommand]
    private void Cancel()
    {
        // eventuellt logik för att avbryta redigering
    }
}

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
 