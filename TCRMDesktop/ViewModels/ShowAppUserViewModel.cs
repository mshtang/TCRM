using AutoMapper;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var appUserList = new List<AppUser>();
            var allRoleDict = new Dictionary<string, string>();
            try
            {
                appUserList = await _userEndpoint.GetAll();
                allRoleDict = await _userEndpoint.GetAllRoles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while fetching all users/roles. {ex.Message}");
            }

            var appUsers = new List<AppUserDisplayModel>();
            foreach (var item in appUserList)
            {
                var appUser = new AppUserDisplayModel
                {
                    Id = item.Id,
                    Email = item.Email
                };

                var appUserActualRoles = item.Roles.Values;

                foreach (var kvp in allRoleDict)
                {
                    var roleVM = new RoleViewModel(_userEndpoint, item.Id)
                    {
                        RoleId = kvp.Key,
                        RoleName = kvp.Value,
                        IsSelected = appUserActualRoles.Contains(kvp.Value)
                    };
                    appUser.Roles.Add(roleVM);
                }

                appUsers.Add(appUser);
            }

            AppUsers = new ObservableCollection<AppUserDisplayModel>(appUsers);
        }
    }
}
