﻿using TechTalk.SpecFlow;
using Xamarin.Forms;

namespace XFTextpadApp.SpecFlowTests
{
    [Binding]
    public class TestHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public static TestApp App { get; private set; }
        
        [BeforeScenario]
        public void BeforeScenario()
        {
            Xamarin.Forms.Mocks.MockForms.Init();

            App = new TestApp();
        }

        [BeforeScenario("cleardata")]
        public async void BeforeScenarioClearData()
        {
            Xamarin.Forms.Mocks.MockForms.Init();
            
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();

            App = new TestApp();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}
