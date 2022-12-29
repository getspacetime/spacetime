using Fluxor;
using Spacetime.Settings;

namespace Spacetime.Store.Settings
{
    public class SettingEffects
    {
        private readonly ISettingsService _service;
        public SettingEffects(ISettingsService service)
        {
            _service = service;
        }

        [EffectMethod(typeof(FetchSettingsAction))]
        public async Task HandleFetchSettingsAction(Fluxor.IDispatcher dispatcher)
        {
            var settings = await _service.GetSettings();
            dispatcher.Dispatch(new FetchSettingsSuccessAction(settings));
        }

        [EffectMethod]
        public async Task HandleSaveSettingsAction(SaveSettingsAction action, Fluxor.IDispatcher dispatcher)
        {
            await _service.SaveSettings(action.Settings);

            // todo: dispatch failures too
            dispatcher.Dispatch(new SaveSettingsSuccessAction(action.Settings));
        }
    }
}
