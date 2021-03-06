﻿using System;
using System.Linq;
using System.Reflection;
using Prism.Commands;
using Shouldly;
using TechTalk.SpecFlow;
using Xamarin.Forms;

namespace XFTextpadApp.SpecFlowTests
{
    [Binding]
    public class SharedSteps
    {
        public TestApp App => TestHooks.App;

        [Given(@"I have launched the app")]
        public void GivenIHaveLaunchedApp()
        {
            App.ShouldNotBeNull();
        }
        
        [Then(@"I am in the ""(.*)"" Page")]
        public void ThenIAmOnThePage(string pageName)
        {
            var navigationStack = ((NavigationPage)App.MainPage).Navigation.NavigationStack;
            // Am I in the page
            navigationStack.Last().BindingContext.GetType().Name.ShouldBe(pageName + "PageViewModel");
        }

        [Then(@"I click on ""(.*)"" Button")]
        [When(@"I click on ""(.*)"" Button")]
        public void WhenIClickOnButton(string p0)
        {
            var navigationStack = ((NavigationPage)App.MainPage).Navigation.NavigationStack;
            // retrieve the current ViewModel
            var currentViewModel = navigationStack.Last().BindingContext;

            // Get type object of the ViewModel
            Type type = currentViewModel.GetType();

            // Loop over properties of the object
            foreach (PropertyInfo propertyInfo in type.GetRuntimeProperties())
            {
                // Find the matching name for the requested Command name
                if (propertyInfo.Name.Equals(p0 + "Command"))
                {
                    // retrieve Command object
                    var commandToExecute = ((DelegateCommand)propertyInfo.GetValue(currentViewModel));

                    commandToExecute.ShouldNotBeNull();

                    // Execute
                    commandToExecute.Execute();

                    break;
                }
            }
        }
    }
}
