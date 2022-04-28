using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Store.Settings
{
    public static class Reducers
    {
        [ReducerMethod(typeof(FetchSettingsAction))]
        public static SettingState FetchSettings(SettingState state) => new SettingState(true, state.Settings);

        [ReducerMethod]
        public static SettingState FetchSettingsSuccess(SettingState state, FetchSettingsSuccessAction action) => new SettingState(false, action.Settings);

        [ReducerMethod(typeof(SaveSettingsAction))]
        public static SettingState SaveSettings(SettingState state) => new SettingState(true, state.Settings);

        [ReducerMethod]
        public static SettingState SaveSettingsSuccess(SettingState state, SaveSettingsSuccessAction action) => new SettingState(false, action.Settings);
    }
}
