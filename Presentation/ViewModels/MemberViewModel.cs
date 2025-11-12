using System;
using System.Collections.Generic;
using System.ComponentModel;
using Infrastructure.Models;

namespace Presentation.ViewModels
{
    public class MemberViewModel : INotifyPropertyChanged
    {
        private string _currentMembershipLevel = string.Empty;
        private MembershipLevel? _selectedMembershipLevel;
        private string _statusMessage = string.Empty;

        public string CurrentMembershipLevel
        {
            get => _currentMembershipLevel;
            set
            {
                _currentMembershipLevel = value;
                OnPropertyChanged(nameof(CurrentMembershipLevel));
            }
        }

        public MembershipLevel? SelectedMembershipLevel
        {
            get => _selectedMembershipLevel;
            set
            {
                _selectedMembershipLevel = value;
                OnPropertyChanged(nameof(SelectedMembershipLevel));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public List<MembershipLevel> MembershipLevels { get; set; } = new List<MembershipLevel>
        {
            MembershipLevel.Bronze,
            MembershipLevel.Silver,
            MembershipLevel.Gold
        };

        public Member CurrentMember { get; set; } = new Member();

        public MemberViewModel()
        {
            LoadMemberLevel();
        }

        private void LoadMemberLevel()
        {
            var repo = new MemberRepository();
            CurrentMember = repo.GetMemberByName("Alice") ?? new Member();
            CurrentMembershipLevel = CurrentMember.MembershipLevelString;
        }

        public void UpdateMembership()
        {
            if (SelectedMembershipLevel == null)
            {
                StatusMessage = "Välj en medlemsnivå innan du sparar.";
                return;
            }

            CurrentMember.Membership = SelectedMembershipLevel.Value;
            CurrentMembershipLevel = CurrentMember.MembershipLevelString;

            try
            {
                var repo = new MemberRepository();
                repo.UpdateMember(CurrentMember);
                StatusMessage = $"Medlemsnivån har uppdaterats till {CurrentMembershipLevel}.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Fel vid uppdatering: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
