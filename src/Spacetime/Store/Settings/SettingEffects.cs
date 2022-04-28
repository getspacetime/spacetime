using Fluxor;
using Spacetime.Core.Services;

namespace Spacetime.Store.Settings
{
    public class SettingEffects
    {
        private readonly SettingsService _service;
        public SettingEffects(SettingsService service)
        {
            _service = service;
        }

        [EffectMethod(typeof(FetchSettingsAction))]
        public async Task HandleFetchSettingsAction(IDispatcher dispatcher)
        {
            var settings = await _service.GetSettings();
            dispatcher.Dispatch(new FetchSettingsSuccessAction(settings));
        }

        [EffectMethod]
        public async Task HandleSaveSettingsAction(SaveSettingsAction action, IDispatcher dispatcher)
        {
            await _service.SaveSettings(action.Settings);

            // todo: dispatch failures too
            dispatcher.Dispatch(new SaveSettingsSuccessAction(action.Settings));
        }
    }
}
