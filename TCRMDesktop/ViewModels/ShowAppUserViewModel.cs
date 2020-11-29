using AutoMapper;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TCRMDesktopUI.Library.Api;
using TCRMDesktopUI.Library.Models;
using TCRMDesktopUI.Models;

namespace TCRMDesktopUI.ViewModels
{
    public class ShowAppUserViewModel : Screen
    {
        private IUserEndpoint _userEndpoint;
        private IMapper _mapper;
        public ISnackbarMessageQueue SbMessQ { get; set; }

        public ShowAppUserViewModel(IUserEndpoint userEndpoint, IMapper mapper, ISnackbarMessageQueue sbMessQ)
        {
            _userEndpoint = userEndpoint;
            _mapper = mapper;
            SbMessQ = sbMessQ;
        }

        private ObservableCollection<AppUserDisplayModel> _appUsers;

        public ObservableCollection<AppUserDisplayModel> AppUsers
        {
            get => _appUsers;
            set
            {
                _appUsers = value;
                NotifyOfPropertyChange();
            }
        }

        private ObservableCollection<string> _allRoles;
        public ObservableCollection<string> AllRoles
        {
            get => _allRoles;
            set
            {
                _allRoles = value;
                NotifyOfPropertyChange();
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            List<AppUser> appUserList = new List<AppUser>();
            List<string> allRoleList = new List<string>();
            try
            {
                appUserList = await _userEndpoint.GetAll();
                allRoleList = await _userEndpoint.GetAllRoles();
            }
            catch (Exception ex)
            {
                SbMessQ.Enqueue($"Error occured while fetching all users/roles. {ex.Message}", "Close", () => TryClose());
            }

            var appUsers = _mapper.Map<List<AppUserDisplayModel>>(appUserList);
            AppUsers = new ObservableCollection<AppUserDisplayModel>(appUsers);

            AllRoles = new ObservableCollection<string>(allRoleList);
        }
    }
}
